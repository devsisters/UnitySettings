using UnityEngine;

namespace Settings.Util
{
    internal static class Mouse
    {
        private static bool _curMousePresent;
        private static Vector2 _curMousePos;
        private static bool _lastMousePresent;
        private static Vector2 _lastMousePos;

        public static void RefreshPos()
        {
            _lastMousePresent = _curMousePresent;
            _lastMousePos = _curMousePos;

            if (Input.touchSupported)
            {
                var touches = Input.touches;
                if (touches.Length == 1)
                {
                    _curMousePresent = true;
                    _curMousePos = touches[0].position;
                    return;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    _curMousePresent = true;
                    _curMousePos = Input.mousePosition;
                    return;
                }
            }

            _curMousePresent = false;
            _curMousePos = default(Vector2);
        }

        public static bool CurPos(out Vector2 result)
        {
            result = _curMousePos;
            return _curMousePresent;
        }

        public static bool Delta(out Vector2 result)
        {
            if (_curMousePresent && _lastMousePresent)
            {
                result = _curMousePos - _lastMousePos;
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
