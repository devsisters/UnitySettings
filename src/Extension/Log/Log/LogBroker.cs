using Queue = System.Collections.Generic.List<Settings.Extension.Log.RawLog>;
using LogType = UnityEngine.LogType;

namespace Settings.Extension.Log
{
    internal class Broker
    {
        private bool _isConnected { get { return _provider != null; } }
        private IProvider _provider;
        private bool _isQueueEmpty = true;
        private readonly Queue _queue = new Queue(16);
        private readonly ISampler _sampler;

        public Broker(ISampler sampler)
        {
            _sampler = sampler;
        }

        public void Connect(IProvider provider)
        {
            if (_isConnected)
            {
                L.SomethingWentWrong();
                return;
            }

            _provider = provider;
            _provider.RegisterThreaded(OnSubscribe);
        }

        public void Disconnect()
        {
            if (!_isConnected)
            {
                L.SomethingWentWrong();
                return;
            }

            _provider.UnregisterThreaded(OnSubscribe);
            _provider = null;
        }

        private void OnSubscribe(string message, string stacktrace, LogType type)
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
            var sample = _sampler.Sample();
            lock (_queue)
            {
                foreach (var l in _queue)
                    stash.Add(l, sample);
                _isQueueEmpty = true;
                _queue.Clear();
            }
        }

        public void Clear()
        {
            lock (_queue)
            {
                _isQueueEmpty = true;
                _queue.Clear();
            }
        }
    }
}
