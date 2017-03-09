using System.Collections.ObjectModel;
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

        private void OnGUILogRow(Rect area, Log log, int index, bool isSelected)
        {
            const int rightPadding = 10;

            GUIStyle backStyle = null;
            GUIStyle fontStyle = null;
            if (isSelected)
            {
                backStyle = _styles.SelectedLog;
                fontStyle = _styles.SelectedLogFont;
            }
            else
            {
                backStyle = (index % 2 == 0) ? _styles.EvenLog : _styles.OddLog;
                fontStyle = _styles.Font;
            }

            // TODO: wrong touch focus when scroll down.
            if (UnityEngine.GUI.Button(area, "", backStyle))
                _selectedLog = index;

            var rightX = area.width - rightPadding;
            // draw sample datas
            {
                System.Action<GUIContent, string> drawIconAndLabel = (icon, text) =>
                    OnGUIIconAndLabelFromRightToLeft(icon, text, _styles.Icon, fontStyle, ref rightX);
                if (_showScene) drawIconAndLabel(_icons.ShowScene, log.Sample.Scene);
                if (_showTime) drawIconAndLabel(_icons.ShowTime, log.Sample.TimeToDisplay);
            }

            // draw message
            {
                var icon = IconForLogType(log.Type);
                var iconRect = new Rect(0, 0, _iconWidth, _rowHeight);
                UnityEngine.GUI.Box(iconRect, icon, _styles.Icon);
                var labelRect = new Rect(_iconWidth, 0, rightX - _iconWidth, _rowHeight);
                UnityEngine.GUI.Label(labelRect, log.Message, fontStyle);
            }
        }

        private void OnGUITable(Rect area, ReadOnlyCollection<Log> logs)
        {
            if (_keepInSelectedLog)
            {
                _table.SetScrollToKeepIn(area, _selectedLog);
                _keepInSelectedLog = false;
            }

            _table.OnGUI(area, logs.Count, (rect, i) =>
                OnGUILogRow(area, logs[i], i, i == _selectedLog));
        }
    }
}