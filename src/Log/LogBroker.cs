using Queue = System.Collections.Generic.List<Dashboard.Log.RawLog>;
using UnityEngine;

namespace Dashboard.Log
{
    internal class Broker
    {
        private bool _isConnected = false;
        private bool _isQueueEmpty = true;
        private readonly Queue _queue = new Queue(16);

        public void Connect()
        {
            if (_isConnected)
            {
                // something went wrong.
                return;
            }

            _isConnected = true;
            Application.logMessageReceivedThreaded += OnLogMessageReceivedThreaded;
        }

        public void Disconnect()
        {
            if (!_isConnected)
            {
                // something went wrong.
                return;
            }

            _isConnected = false;
            Application.logMessageReceivedThreaded -= OnLogMessageReceivedThreaded;
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
