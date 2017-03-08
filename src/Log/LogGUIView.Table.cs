using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private static readonly GUIContent _tempContent = new GUIContent();
        private static void DrawIconAndLabelFromRightToLeft(
            GUIContent icon, string text,
            GUIStyle iconStyle, GUIStyle fontStyle,
            ref float x)
        {
            {
                _tempContent.text = text;
                var w = fontStyle.CalcSize(_tempContent).x;
                x -= w;
                var r = new Rect(x, 0, w, rowHeight);
                UnityEngine.GUI.Label(r, _tempContent, fontStyle);
            }

            {
                x -= iconWidth;
                var r = new Rect(x, 0, iconWidth, rowHeight);
                UnityEngine.GUI.Box(r, icon, iconStyle);
            }
        }

        private void DrawRow(Rect area, Log log, int index, bool isSelected)
        {
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

            var rightX = area.width;
            // draw sample datas
            {
                System.Action<GUIContent, string> drawIconAndLabel = (icon, text) =>
                    DrawIconAndLabelFromRightToLeft(icon, text, _styles.Icon, fontStyle, ref rightX);
                if (showScene) drawIconAndLabel(_icons.ShowScene, log.Sample.Scene);
                if (showTime) drawIconAndLabel(_icons.ShowTime, log.Sample.Time.ToString("0.000"));
            }

            // draw message
            {
                var content = IconForLogType(log.Type);
                var iconRect = new Rect(0, 0, iconWidth, rowHeight);
                UnityEngine.GUI.Box(iconRect, content, _styles.Icon);
                var labelRect = new Rect(iconWidth, 0, rightX - iconWidth, rowHeight);
                UnityEngine.GUI.Label(labelRect, log.Message, fontStyle);
            }
        }

        private void DrawTable(Rect area)
        {
            // TODO:
            var logMask = new Mask();
            logMask.AllTrue();
            var logs = _stash.All();

            // TODO
            // var logCount = logs.Count;
            // if (i >= logs.Count) break;
            // if (!logMask.Check(log.Type)) continue;

            _selectedLog = Mathf.Clamp(_selectedLog, 0, logs.Count - 1);
            if (_keepInSelectedLog)
            {
                _table.SetScrollToKeepIn(area, _selectedLog);
                _keepInSelectedLog = false;
            }
            _table.Draw(area, logs.Count, (rect, i) =>
                DrawRow(area, logs[i], i, i == _selectedLog));
        }
    }
}