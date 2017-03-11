using UnityEngine;
using ViewList = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, Settings.GUI.IView>>;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        private static Rect _tempToolbarRect;

        private void OnGUIToolbar(Rect area)
        {
            if (_viewList == null) _viewList = new ViewList(_views);
            _tempToolbarRect = new Rect(0, 0, _toolbarH, _toolbarH);
            _toolbarTable.OnGUI(area, _viewList.Count, OnGUIToolbarIcon);
        }

        private void OnGUIToolbarIcon(int i)
        {
            var kv = _viewList[i];
            var key = kv.Key;
            var view = kv.Value;

            var icon = view.ToolbarIcon;
            if (UnityEngine.GUI.Button(_tempToolbarRect, icon))
            {
                _curViewKey = key;
                _curView = view;
            }
        }
    }
}