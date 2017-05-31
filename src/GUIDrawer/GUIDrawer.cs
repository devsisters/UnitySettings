using System.Collections.Generic;
using UnityEngine;
using Views = System.Collections.Generic.List<Settings.GUI.IView>;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        const int _toolbarH = 80;

        private readonly Views _views = new Views(8);
        private IView _curView;
        private readonly Table _toolbarTable;
        private readonly List<IToolbarWidget> _toolbarWidgets = new List<IToolbarWidget>();

        public System.Action OnClose;

        public Drawer()
        {
            _toolbarTable = new GUI.Table(
                GUI.Table.Direction.Horizontal,
                _toolbarH, 0,
                Styles.ToolbarBG);
        }

        public void AddView(IView view)
        {
            _views.Add(view);
            if (_curView == null)
                _curView = view;
        }

        public void AddToolbarWidget(IToolbarWidget toolbarWidget)
        {
            _toolbarWidgets.Add(toolbarWidget);
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

            // touch block
            UnityEngine.GUI.Button(new Rect(0, 0, w, h), "", new GUIStyle());
        }
    }
}
