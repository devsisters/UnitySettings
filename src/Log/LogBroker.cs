using Queue = System.Collections.Generic.List<Dashboard.Log.RawLog>;
using LogType = UnityEngine.LogType;

namespace Dashboard.Log
{
    internal class Broker
    {
        private bool _isConnected { get { return _provider != null; } }
        private IProvider _provider;
        private bool _isQueueEmpty = true;
        private readonly Queue _queue = new Queue(16);

        public void Connect(IProvider provider)
        {
            if (_isConnected)
            {
                // something went wrong.
                return;
            }

            _provider = provider;
            _provider.RegisterThreaded(OnLogMessageReceivedThreaded);
        }

        public void Disconnect()
        {
            if (!_isConnected)
            {
                // something went wrong.
                return;
            }

            _provider.UnregisterThreaded(OnLogMessageReceivedThreaded);
            _provider = null;
        }

        private void OnLogMessageReceivedThreaded(string message, string stacktrace, LogType type)
        {
            var log = new RawLog(type, message, stacktrace);
            lock (_queue)
            {
                _isQueueEmpty = false;
                _queue.Add(log);
            }
        }

        public void Transfer(Stash stash)
        {
            if (_isQueueEmpty) return;
            var sample = Sample();
            lock (_queue)
            {
                foreach (var l in _queue)
                    stash.Add(l, sample);
                _isQueueEmpty = true;
                _queue.Clear();
            }
        }

        private Sample Sample()
        {
            var time = UnityEngine.Time.realtimeSinceStartup;
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            return new Sample(time, scene);
        }
    }
}
