using UnityEngine;

namespace Settings.Util
{
    internal static class Mouse
    {
        private static int _curMouseFrame = -1;
        private static Vector2 _curMousePos;
        private static int _lastMouseFrame = -1;
        private static Vector2 _lastMousePos;

        public static bool UpdatePos()
        {
            if (Input.touchSupported)
                return false;

            var curFrame = Time.frameCount;
            if (_curMouseFrame == curFrame)
                return true;

            _lastMouseFrame = _curMouseFrame;
            _lastMousePos = _curMousePos;

            if (Input.GetMouseButton(0))
            {
                _curMouseFrame = curFrame;
                _curMousePos = Input.mousePosition;
                return true;
            }
            else
            {
                _curMouseFrame = -1;
                _curMousePos = Input.mousePosition;
                return false;
            }
        }

        public static bool CurPos(out Vector2 result)
        {
            if (Input.touchSupported)
            {
                var touches = Input.touches;
                if (touches.Length == 1
                    && touches[0].phase == TouchPhase.Began)
                {
                    result = touches[0].position;
                    return true;
                }
            }
            else
            {
                if (UpdatePos())
                {
                    result = _curMousePos;
                    return true;
                }
            }

            result = default(Vector2);
            return false;
        }

        public static bool Delta(out Vector2 result)
        {
            if (Input.touchSupported)
            {
                if (Input.touches.Length == 1)
                {
                    result = Input.touches[0].deltaPosition;
                    return true;
                }
            }
            else
            {
                if (UpdatePos())
                {
                    if (_lastMouseFrame != -1)
                        result = _curMousePos - _lastMousePos;
                    else result = Vector2.zero;
                    return true;
                }
            }

            result = default(Vector2);
            return false;
        }
    }
}
