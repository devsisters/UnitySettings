using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private GUIContent IconForLogType(LogType logType)
        {
            switch (logType)
            {
                case LogType.Log: return _icons.Log;
                case LogType.Warning: return _icons.Warning;
                default: return _icons.Error;
            }
        }

        private static readonly GUIContent _tempContent = new GUIContent();
        private static void OnGUIIconAndLabelFromRightToLeft(
            GUIContent icon, string text,
            GUIStyle iconStyle, GUIStyle fontStyle,
            ref float x)
        {
            {
                _tempContent.text = text;
                var w = fontStyle.CalcSize(_tempContent).x;
                x -= w;
                var r = new Rect(x, 0, w, _rowHeight);
                UnityEngine.GUI.Label(r, _tempContent, fontStyle);
            }

            {
                x -= _iconWidth;
                var r = new Rect(x, 0, _iconWidth, _rowHeight);
                UnityEngine.GUI.Box(r, icon, iconStyle);
            }
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
                    OnGUIIconAndLabelFromRightToLeft(icon, text, Styles.Icon, fontStyle, ref rightX);
                var sample = log.Sample.Value;
                if (_config.ShowScene) drawIconAndLabel(_icons.ShowScene, sample.Scene);
                if (_config.ShowTime) drawIconAndLabel(_icons.ShowTime, sample.TimeToDisplay);
            }

            // draw count
            if (log.Count.HasValue)
            {
                // TODO
            }

            // draw message
            {
                var icon = IconForLogType(log.Type);
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