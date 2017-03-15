using UnityEngine;
using Views = System.Collections.Generic.List<Settings.GUI.IView>;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        const int _toolbarH = 60;

        private readonly Views _views = new Views(8);
        private IView _curView;
        private readonly Table _toolbarTable;

        public System.Action OnClose;

        public Drawer()
        {
            _toolbarTable = new GUI.Table(
                GUI.Table.Direction.Horizontal,
                _toolbarH, 0,
                Styles.ToolbarBG);
        }

        public void Add(IView view)
        {
            _views.Add(view);
            if (_curView == null)
                _curView = view;
        }

        public void Update()
        {
            UpdateKeyboard();
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
            y += _toolbarH; h -= _toolbarH;
            var viewArea = new Rect(0, y, w, h);

            // draw
            OnGUIToolbar(toolbarArea);
            if (_curView != null)
                _curView.OnGUI(viewArea);
        }
    }
}