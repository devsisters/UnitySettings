using UnityEngine;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        private static Rect _toolbarIconRect = new Rect(0, 0, _toolbarH, _toolbarH);

        private bool DrawToolbarToggle(GUIContent icon, bool value)
        {
            var style = value ? Styles.ToolbarButtonOn : Styles.ToolbarButton;
            var clicked = UnityEngine.GUI.Button(_toolbarIconRect, icon, style);
            if (clicked) value = !value;
            return value;
        }

        private void OnGUIToolbar(Rect area)
        {
            ResetViewList();

            // draw views
            var viewArea = new Rect(area); viewArea.width -= _toolbarH;
            _toolbarIconRect = new Rect(0, 0, _toolbarH, _toolbarH);
            _toolbarTable.OnGUI(viewArea, _viewList.Count, OnGUIToolbarIcon);

            // draw close
            var closeRect = new Rect(area.xMax - _toolbarH, area.y, _toolbarH, area.height);
            if (UnityEngine.GUI.Button(closeRect, Icons.Close, Styles.ToolbarButton))
                OnClose();
        }

        private void OnGUIToolbarIcon(int i)
        {
            var kv = _viewList[i];
            var key = kv.Key;
            var view = kv.Value;

            var icon = view.ToolbarIcon;
            if (DrawToolbarToggle(icon, view == _curView))
            {
                _curViewKey = key;
                _curView = view;
            }
        }
    }
}