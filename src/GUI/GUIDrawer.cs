using UnityEngine;
using View = System.Collections.Generic.Dictionary<string, Settings.GUI.IView>;
using ViewList = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, Settings.GUI.IView>>;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        const int _toolbarH = 60;

        private class Styles
        {
            public static readonly GUIStyle ToolbarBG;

            static Styles()
            {
                ToolbarBG = new GUIStyle();
                ToolbarBG.normal.background = Helper.Solid(0xbdbdbdf0);
            }
        }

        private readonly View _views = new View(4);
        private ViewList _viewList;

        private string _curViewKey;
        private IView _curView;
        private readonly Table _toolbarTable;

        public Drawer()
        {
            _toolbarTable = new GUI.Table(
                GUI.Table.Direction.Horizontal,
                _toolbarH, 0,
                Styles.ToolbarBG);
        }

        public void Add(string key, IView view)
        {
            _views.Add(key, view);
            _viewList = null;

            if (_curViewKey == null)
            {
                _curViewKey = key;
                _curView = view;
            }
            else if (_curViewKey == key)
            {
                _curView = view;
            }
        }

        public void Update()
        {
            if (_curView != null)
                _curView.Update();
        }

        public void OnGUI()
        {
            // layout
            var y = 0;
            var w = Screen.width;
            var h = Screen.height;

            var toolbarArea = new Rect(0, y, w, _toolbarH);
            y += _toolbarH;
            var viewArea = new Rect(0, y, w, h);

            // draw
            OnGUIToolbar(toolbarArea);
            if (_curView != null) _curView.OnGUI(viewArea);
        }
    }
}