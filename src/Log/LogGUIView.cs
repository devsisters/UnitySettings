using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private const int _iconWidth = 40;
        private const int _rowHeight = 64;

        private readonly GUI.Icons _icons;
        private readonly Styles _styles;
        private readonly GUI.Table _table;
        private GUI.ScrollView _stackScroll;
        private readonly Stash _stash;

        private int _selectedLog = -1;
        private int _lastSelectedLog = -1;
        private bool _isSelectedLogDirty { get { return _selectedLog != _lastSelectedLog; } }
        private bool _keepInSelectedLog;

        private bool _collapse = false; // TODO
        private bool _showTime = false; // TODO
        private bool _showScene = false; // TODO
        private bool _showLog = true; // TODO
        private bool _showWarning = true; // TODO
        private bool _showError = true; // TODO

        public GUIView(GUI.Icons icons, Stash stash)
        {
            _icons = icons;
            _styles = new Styles();
            _table = new GUI.Table(_rowHeight, 0, _styles.TableBG);
            _stackScroll = new GUI.ScrollView(Vector2.zero, true, true);
            _stash = stash;
        }

        public override void Update()
        {
            UpdateKeyboard();
            _table.Update();
            _stackScroll.Update();
        }

        public override void OnGUI(Rect area)
        {
            // TODO: filter, collapse
            var logs = _stash.All();
            // var logMask = new Mask();
            // logMask.AllTrue();
            // if (!logMask.Check(log.Type)) continue;

            if (logs.Count == 0) _selectedLog = -1;
            else _selectedLog = Mathf.Clamp(_selectedLog, 0, logs.Count - 1);

            var x = area.x; var y = area.y;
            var w = area.width; var h = area.height;

            const int toolbarH = 40;
            var tableH = (h - toolbarH) * 0.75f;
            var stackH = (h - toolbarH) * 0.25f;

            {
                var toolbarArea = new Rect(x, y, w, toolbarH);
                OnGUIToolbar(toolbarArea);
                y += toolbarH;
            }

            {
                var tableArea = new Rect(x, y, w, tableH);
                OnGUITable(tableArea, logs);
                y += tableH;
            }

            {
                var stackArea = new Rect(x, y, w, stackH);
                if (_selectedLog >= 0) DrawStack(stackArea, logs[_selectedLog]);
                else DrawStackEmpty(stackArea);
                y += stackH;
            }

            _lastSelectedLog = _selectedLog;
        }
    }
}
