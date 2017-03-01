using UnityEngine;

namespace Dashboard.Info
{
    internal struct FPS
    {
        private float _lastUpdate;

        public float Update()
        {
            var realTime = Time.realtimeSinceStartup;

            if (_lastUpdate == 0)
            {
                _lastUpdate = realTime;
                return 0;
            }

            var elapsed = realTime - _lastUpdate;
            _lastUpdate = realTime;
            return 1f / elapsed;
        }
    }
}