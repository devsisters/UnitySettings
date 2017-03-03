using Vector2 = UnityEngine.Vector2;

namespace Dashboard
{
    internal class CircleGesture : IGesture
    {
        private const int _minSqrDistToSample = 100;
        private const int _minTouchToCheck = 10;

        private readonly ITouchProvider _touchProvider;
        private readonly float _leastLength;

        public int TouchCount { get; private set; }
        private Vector2 _touchSum;
        private float _touchLength;

        private Vector2 _lastTouch;
        private Vector2 _lastDelta;

        public CircleGesture(ITouchProvider touchProvider, float leastLength)
        {
            _touchProvider = touchProvider;
            _leastLength = leastLength;
        }

        public void Clear()
        {
            TouchCount = 0;
            _touchSum = Vector2.zero;
            _touchLength = 0;
        }

        public void SampleOrCancel()
        {
            Vector2 touchPos;
            if (!_touchProvider.GetDown(out touchPos))
            {
                Clear();
                return;
            }

            if (TouchCount == 0)
            {
                ++TouchCount;
                _lastTouch = touchPos;
                return;
            }

            var delta = touchPos - _lastTouch;
            if (delta.sqrMagnitude < _minSqrDistToSample)
                return;

            if (TouchCount >= 2)
            {
                var dot = Vector2.Dot(delta, _lastDelta);
                if (dot < 0)
                {
                    Clear();
                    return;
                }
            }

            ++TouchCount;
            _touchSum += delta;
            _touchLength += delta.magnitude;
            _lastTouch = touchPos;
            _lastDelta = delta;
        }

        public bool CheckAndClear()
        {
            if (TouchCount < _minTouchToCheck)
                return false;

            if (_touchLength > _leastLength && _touchSum.magnitude < _leastLength / 2)
            {
                Clear();
                return true;
            }

            return false;
        }
    }
}
