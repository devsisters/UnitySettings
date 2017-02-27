using UnityEngine;

namespace Util
{
    internal class CircleGestureDetector
    {
        private const int _minSqrDistToSample = 100;

        private int _touchCnt;
        private Vector2 _touchSum;
        private float _touchLength;

        private Vector2 _lastTouch;
        private Vector2 _lastDelta;

        public void Clear()
        {
            _touchCnt = 0;
            _touchSum = Vector2.zero;
            _touchLength = 0;
        }

        public bool GetTouchPosition(out Vector2 result)
        {
            result = Vector2.zero;
            if (Input.touchSupported)
            {
                if (Input.touches.Length != 1) return false;
                var touch = Input.touches[0];
                if (touch.phase != TouchPhase.Moved) return false;
                result = touch.position;
                return true;
            }
            else
            {
                if (Input.GetMouseButtonUp(0)) return false;
                if (!Input.GetMouseButton(0)) return false;
                result = Input.mousePosition;
                return true;
            }
        }

        public void SampleOrCancel()
        {
            Vector2 touchPos;
            if (!GetTouchPosition(out touchPos))
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
