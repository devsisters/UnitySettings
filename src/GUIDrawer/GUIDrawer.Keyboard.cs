using UnityEngine;
using K = UnityEngine.KeyCode;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        private static readonly KeyCode[] _alphaNumKeys = new[] {
            K.Alpha1, K.Alpha2, K.Alpha3,
            K.Alpha4, K.Alpha5, K.Alpha6,
            K.Alpha7, K.Alpha8, K.Alpha9,
            K.Alpha0,
        };

        private static int GetAlphaNumKeyDown()
        {
            for (var i = 0; i != _alphaNumKeys.Length; ++i)
                if (Input.GetKeyDown(_alphaNumKeys[i]))
                    return i;
            return -1;
        }

        private void UpdateKeyboard()
        {
            var i = GetAlphaNumKeyDown();
            if (i == -1 || i >= _views.Count) return;
            _curView = _views[i];
        }
    }
}
