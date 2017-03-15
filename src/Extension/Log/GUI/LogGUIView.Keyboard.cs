using UnityEngine;
using K = UnityEngine.KeyCode;

namespace Settings.Extension.Log
{
    internal partial class GUIView
    {
        private float _upDownTimeLeft;
        private K _curUpDownKey;
        private readonly K[] _upDownKeys = new[] {
            K.UpArrow, K.DownArrow,
            K.PageUp, K.PageDown,
            K.Home, K.End,
        };

        private static int UpDownAmountForKey(K key)
        {
            switch (key)
            {
                case K.UpArrow: return -1;
                case K.DownArrow: return 1;
                case K.PageUp: return -5;
                case K.PageDown: return 5;
                case K.Home: return -10000;
                case K.End: return 10000;
                default: return 0;
            }
        }

        private void UpDownSelectedLogWithCurKey(float cooltime)
        {
            _keepInSelectedLog = true;
            _selectedLog += UpDownAmountForKey(_curUpDownKey);
            _upDownTimeLeft = cooltime;
        }

        private void CheckAndUpdateSelectedLog(K key)
        {
            const float coolTimeFirst = 0.3f;
            if (_curUpDownKey != key && (Input.GetKeyDown(key) || Input.GetKey(key)))
            {
                _curUpDownKey = key;
                UpDownSelectedLogWithCurKey(coolTimeFirst);
            }
            else if (_curUpDownKey == key && (Input.GetKeyUp(key) || !Input.GetKey(key)))
            {
                _curUpDownKey = K.None;
                _upDownTimeLeft = coolTimeFirst;
            }
        }

        private void UpdateKeyboardAction()
        {
            foreach (var key in _upDownKeys)
                CheckAndUpdateSelectedLog(key);
        }

        private void UpdateKeyboardStay()
        {
            if (_curUpDownKey == K.None)
                return;

            if (_upDownTimeLeft > 0)
            {
                _upDownTimeLeft -= Time.unscaledDeltaTime;
                return;
            }

            const float coolTimeFast = 0.02f;
            UpDownSelectedLogWithCurKey(coolTimeFast);
        }

        private void UpdateKeyboardCollapse()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _config.Collapse = !_config.Collapse;
        }

        private void UpdateKeyboard()
        {
            UpdateKeyboardAction();
            UpdateKeyboardStay();
            UpdateKeyboardCollapse();
        }
    }
}
