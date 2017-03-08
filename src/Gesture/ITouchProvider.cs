using UnityEngine;

namespace Settings
{
    internal interface ITouchProvider
    {
        bool GetDown(out Vector2 result);
    }

    internal class TouchProvider : ITouchProvider
    {
        public bool GetDown(out Vector2 result)
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
    }
}