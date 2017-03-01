using UnityEngine;

namespace Dashboard
{
    internal class CircleGesture : IGesture
    {
        private const int _minSqrDistToSample = 100;

        private readonly ITouchProvider _touchProvider;

        private int _touchCnt;
        private Vector2 _touchSum;
        private float _touchLength;

        private Vector2 _lastTouch;
        private Vector2 _lastDelta;

        public CircleGesture(ITouchProvider touchProvider)
        {
            _touchProvider = touchProvider;
        }

        public void Clear()
        {
            _touchCnt = 0;
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

            if (_touchCnt == 0)
            {
                ++_touchCnt;
                _lastTouch = touchPos;
                return;
            }

            var delta = touchPos - _lastTouch;
            if (delta.sqrMagnitude > _minSqrDistToSample)
                return;

            if (_touchCnt >= 2)
            {
                var dot = Vector2.Dot(delta, _lastDelta);
                if (dot < 0)
                {
                    Clear();
                    return;
                }
            }

            ++_touchCnt;
            _touchSum += delta;
            _touchLength += delta.magnitude;
            _lastTouch = touchPos;
            _lastDelta = delta;
        }

        public bool CheckAndClear()
        {
            if (_touchCnt < 10)
                return false;

            var gestureBase = (Screen.width + Screen.height) / 4;
            if (_touchLength > gestureBase && _touchSum.magnitude < gestureBase / 2)
            {
                Clear();
                return true;
            }

            return false;
        }
    }
}
