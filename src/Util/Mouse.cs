using UnityEngine;

namespace Settings.Util
{
    internal static class Mouse
    {
        private static bool _curMousePresent;
        private static Vector2 _curMousePos;
        private static Vector2 _lastMousePos;

        public static bool RefreshPos()
        {
            _lastMousePos = _curMousePos;

            if (Input.touchSupported)
            {
                var touches = Input.touches;
                if (touches.Length == 1)
                {
                    _curMousePresent = true;
                    _curMousePos = touches[0].position;
                    return true;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    _curMousePresent = true;
                    _curMousePos = Input.mousePosition;
                    return true;
                }
            }

            _curMousePresent = false;
            _curMousePos = default(Vector2);
            return false;
        }

        public static bool CurPos(out Vector2 result)
        {
            result = _curMousePos;
            return _curMousePresent;
        }

        // TODO: why scale 4?
        public static bool Delta(out Vector2 result)
        {
            if (!RefreshPos())
            {
                result = default(Vector2);
                return false;
            }

            if (_curMousePresent)
                result = (_curMousePos - _lastMousePos);
            else result = Vector2.zero;
            return true;
        }
    }
}
