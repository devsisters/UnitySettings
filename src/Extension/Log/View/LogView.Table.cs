using UnityEngine;

namespace Settings.Extension.Log
{
    internal partial class View
    {
        private static readonly GUIContent _tempContent = new GUIContent();

        private static Rect OnGUILogRowRightToLeft(string text, GUIStyle fontStyle, ref float x)
        {
            _tempContent.text = text;
            var w = fontStyle.CalcSize(_tempContent).x;
            x -= w;
            var r = new Rect(x, 0, w, _rowHeight);
            UnityEngine.GUI.Label(r, _tempContent, fontStyle);
            return r;
        }

        private static Rect OnGUILogRowRightToLeft(GUIContent icon, GUIStyle iconStyle, ref float x)
        {
            x -= _iconWidth;
            var r = new Rect(x, 0, _iconWidth, _rowHeight);
            UnityEngine.GUI.Box(r, icon, iconStyle);
            return r;
        }

        private void OnGUILogRow(float width, AbstractLog log, int index, bool isSelected)
        {
            const int rightPadding = 10;

            GUIStyle backStyle = null;
            GUIStyle fontStyle = null;
            if (isSelected)
            {
                backStyle = Styles.SelectedLog;
                fontStyle = Styles.SelectedLogFont;
            }
            else
            {
                backStyle = (index % 2 == 0) ? Styles.EvenLog : Styles.OddLog;
                fontStyle = Styles.Font;
            }

            var rect = new Rect(0, 0, width, _rowHeight);
            if (UnityEngine.GUI.Button(rect, "", backStyle))
                _selectedLog = index;

            var rightX = width - rightPadding;
            // draw sample datas
            if (log.Sample.HasValue)
            {
                System.Action<GUIContent, string> drawIconAndLabel = (icon, text) =>
                {
                    OnGUILogRowRightToLeft(text, fontStyle, ref rightX);
                    OnGUILogRowRightToLeft(icon, Styles.Icon, ref rightX);
                };
                var sample = log.Sample.Value;
                if (_config.ShowScene) drawIconAndLabel(Icons.ShowScene, sample.Scene);
                if (_config.ShowTime) drawIconAndLabel(Icons.ShowTime, sample.TimeToDisplay);
            }

            // draw count
            if (log.Count.HasValue)
            {
                var count = log.Count.Value;
                if (count != 0)
                    OnGUILogRowRightToLeft(count.ToString(), Styles.CollapsedCountFont, ref rightX);
            }

            // draw message
            {
                var icon = Icons.ForLogType(log.Type);
                var iconRect = new Rect(0, 0, _iconWidth, _rowHeight);
                UnityEngine.GUI.Box(iconRect, icon, Styles.Icon);
                var labelRect = new Rect(_iconWidth, 0, rightX - _iconWidth, _rowHeight);
                UnityEngine.GUI.Label(labelRect, log.Message, fontStyle);
            }
        }

        private void OnGUITable(Rect area, AbstractLogs logs)
        {
            if (_keepInSelectedLog)
            {
                _table.SetScrollToKeepIn(area, _selectedLog);
                _keepInSelectedLog = false;
            }

            _table.OnGUI(area, logs.Count,
                i => OnGUILogRow(area.width, logs[i], i, i == _selectedLog));
        }
    }
}