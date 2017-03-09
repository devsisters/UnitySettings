using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private const int _iconWidth = 40;
        private const int _rowHeight = 64;
        private const bool _showTime = false; // TODO
        private const bool _showScene = false; // TODO

        private readonly GUI.Icons _icons;
        private readonly Styles _styles;
        private readonly GUI.Table _table;
        private readonly Stash _stash;

        private int _selectedLog = -1;
        private bool _keepInSelectedLog;

        public GUIView(GUI.Icons icons, Stash stash)
        {
            _icons = icons;
            _styles = new Styles();
            _table = new GUI.Table(_rowHeight, 0, _styles.TableBG);
            _stash = stash;
        }

        public override void Update()
        {
            UpdateKeyboard();
        }

        public override void Draw(Rect area)
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

            {
                var tableH = h * 0.75f;
                var tableArea = new Rect(x, y, w, tableH);
                DrawTable(tableArea, logs);
                y += tableH;
            }

            {
                var stackH = h * 0.25f;
                var stackArea = new Rect(x, y, w, stackH);
                if (_selectedLog >= 0) DrawStack(stackArea, logs[_selectedLog]);
                else DrawStackEmpty(stackArea);
                y += stackH;
            }
        }
    }
}
