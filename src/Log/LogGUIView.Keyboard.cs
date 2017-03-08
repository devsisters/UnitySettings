using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private const float _upDownCoolTimeFirst = 0.3f;
        private const float _upDownCoolTimeFast = 0.02f;
        private float _upDownTimeLeft;

        private void UpSelectedLog(float cooltime)
        {
            _keepInSelectedLog = true;
            _upDownTimeLeft = cooltime;
            --_selectedLog;
        }

        private void DownSelectedLog(float cooltime)
        {
            _keepInSelectedLog = true;
            _upDownTimeLeft = cooltime;
            ++_selectedLog;
        }

        private void UpdateKeyboardAction()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                UpSelectedLog(_upDownCoolTimeFirst);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                DownSelectedLog(_upDownCoolTimeFirst);
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                _upDownTimeLeft = 0;
        }

        private void UpdateKeyboardStay()
        {
            if (_upDownTimeLeft > 0)
            {
                _upDownTimeLeft -= Time.unscaledDeltaTime;
                return;
            }

            else if (Input.GetKey(KeyCode.UpArrow))
                UpSelectedLog(_upDownCoolTimeFast);
            else if (Input.GetKey(KeyCode.DownArrow))
                DownSelectedLog(_upDownCoolTimeFast);
        }

        private void UpdateKeyboard()
        {
            UpdateKeyboardAction();
            UpdateKeyboardStay();
        }
    }
}
