using UnityEngine;

namespace Dashboard.Util
{
    internal static class Mouse
    {
        public static bool DownPos(out Vector2 result)
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
                if (Input.GetMouseButtonDown(0))
                {
                    result = Input.mousePosition;
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
                if (Input.GetMouseButton(0))
                {
                    result = Input.mouseScrollDelta;
                    return true;
                }
            }

            result = default(Vector2);
            return false;
        }
    }
}
