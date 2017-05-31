using UnityEngine;

namespace Settings.Util
{
    internal static class Mouse
    {
        public static bool IsDown { get; private set; }

        private static bool _curPresent;
        private static Vector2 _curPos;
        private static bool _lastPresent;
        private static Vector2 _lastPos;

        public static void Refresh()
        {
            RefreshDown();
            RefreshPos();
        }

        private static void RefreshDown()
        {
            if (Input.touchSupported)
            {
                var touches = Input.touches;
                if (touches.Length != 1)
                {
                    IsDown = false;
                    return;
                }

                IsDown = touches[0].phase == TouchPhase.Began;
            }
            else
            {
                IsDown = Input.GetMouseButtonDown(0);
            }
        }

        private static void RefreshPos()
        {
            _lastPresent = _curPresent;
            _lastPos = _curPos;

            if (Input.touchSupported)
            {
                var touches = Input.touches;
                if (touches.Length == 1)
                {
                    _curPresent = true;
                    _curPos = touches[0].position;
                    return;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    _curPresent = true;
                    _curPos = Input.mousePosition;
                    return;
                }
            }

            _curPresent = false;
            _curPos = default(Vector2);
        }

        public static bool CurPos(out Vector2 result)
        {
            result = _curPos;
            return _curPresent;
        }

        public static bool Delta(out Vector2 result)
        {
            if (_curPresent && _lastPresent)
            {
                result = _curPos - _lastPos;
                return true;
            }
            else
            {
                result = Vector2.zero;
                return false;
            }
        }
    }
}
