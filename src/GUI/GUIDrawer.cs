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
            public static readonly GUIStyle ToolbarButton;
            public static readonly GUIStyle ToolbarButtonOn;

            static Styles()
            {
                ToolbarBG = new GUIStyle();
                ToolbarBG.normal.background = Helper.Solid(0x039be5f0);
                ToolbarButton = new GUIStyle();
                ToolbarButton.margin = new RectOffset(2, 2, 2, 2);
                ToolbarButton.alignment = TextAnchor.MiddleCenter;
                ToolbarButton.normal.background = Helper.Solid(0x0288d1f0);
                ToolbarButton.hover.background = Helper.Solid(0xfdd83580);
                ToolbarButtonOn = new GUIStyle(ToolbarButton);
                ToolbarButtonOn.normal.background = Helper.Solid(0x0277bdf0);
                ToolbarButtonOn.hover.background = Helper.Solid(0xfbc02d80);
            }
        }

        private readonly View _views = new View(4);
        private ViewList _viewList;

        private string _curViewKey;
        private IView _curView;

        private Icons _icons;
        private readonly Table _toolbarTable;

        public System.Action OnClose;

        public Drawer(Icons icons)
        {
            _icons = icons;
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
            if (_icons == null)
            {
                L.SomethingWentWrong();
                return;
            }

            // layout
            var y = 0;
            var w = Screen.width;
            var h = Screen.height;

            var toolbarArea = new Rect(0, y, w, _toolbarH);
            y += _toolbarH; h -= _toolbarH;
            var viewArea = new Rect(0, y, w, h);

            // draw
            OnGUIToolbar(toolbarArea);
            if (_curView != null) _curView.OnGUI(viewArea);
        }
    }
}