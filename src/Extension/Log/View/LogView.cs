using UnityEngine;

namespace Settings.Extension.Log
{
    internal partial class View : GUI.IView
    {
        private const int _iconWidth = 40;
        private const int _rowHeight = 80;
        private const int _toolbarHeight = 80;

        private readonly GUI.Table _table;
        private GUI.ScrollView _stackScroll;
        private readonly Organizer _organizer;

        private int _selectedLog = -1;
        private int _lastSelectedLog = -1;
        private bool _isSelectedLogDirty { get { return _selectedLog != _lastSelectedLog; } }
        private bool _keepInSelectedLog;

        private Toggle _isClickedClear;
        public System.Action OnClickClear;

        public class Config
        {
            public bool Collapse = false;
            public bool ShowTime = false;
            public bool ShowScene = false;
            public Mask Filter = Mask.AllTrue;
        }

        private readonly Config _config;

        public View(Config config, Organizer organizer)
            : base("Log", Icons.Log)
        {
            _config = config;
            _table = new GUI.Table(
                GUI.Table.Direction.Vertical,
                _rowHeight, 0,
                GUI.Styles.BG);
            _stackScroll = new GUI.ScrollView(Vector2.zero, true, true);
            _organizer = organizer;
        }

        public override void Update()
        {
            UpdateKeyboard();
            _table.Update();
            _stackScroll.Update();
            if (_isClickedClear.Off())
                OnClickClear();
        }

        private void ClampSelectedLog(int logCount)
        {
            if (logCount == 0) _selectedLog = -1;
            else _selectedLog = Mathf.Clamp(_selectedLog, 0, logCount - 1);
        }

        public override void OnGUI(Rect area)
        {
            // layout
            Rect toolbarArea, tableArea, stackArea;

            {
                var x = area.x; var y = area.y;
                var w = area.width; var h = area.height;
                var tableH = (h - _toolbarHeight) * 0.75f;
                var stackH = (h - _toolbarHeight) * 0.25f;
                toolbarArea = new Rect(x, y, w, _toolbarHeight);
                y += _toolbarHeight;
                tableArea = new Rect(x, y, w, tableH);
                y += tableH;
                stackArea = new Rect(x, y, w, stackH);
                y += stackH;
            }

            // filter
            AbstractLogs logs = null;
            if (_config.Collapse)
            {
                var rawLogs = _organizer.FilterCollapsed(_config.Filter);
                logs = new AbstractLogs(rawLogs);
            }
            else
            {
                var rawLogs = _organizer.Filter(_config.Filter);
                logs = new AbstractLogs(rawLogs);
            }

            // draw
            OnGUIToolbar(toolbarArea);
            ClampSelectedLog(logs.Count);
            OnGUITable(tableArea, logs);
            if (_selectedLog >= 0) DrawStack(stackArea, logs[_selectedLog]);
            else DrawStackEmpty(stackArea);
            _lastSelectedLog = _selectedLog;
        }
    }
}
