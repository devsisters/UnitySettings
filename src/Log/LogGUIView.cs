using UnityEngine;
using Helper = Settings.GUI.Helper;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        const int fontSize = 32;
        const int rowHeight = 64;
        const int iconWidth = 40;
        const bool showTime = true;
        const bool showScene = true;

        internal class Styles
        {
            public readonly GUIStyle BG;
            public readonly GUIStyle Font;
            public readonly GUIStyle Icon;

            public readonly GUIStyle EvenLog;
            public readonly GUIStyle OddLog;
            public readonly GUIStyle SelectedLog;
            public readonly GUIStyle SelectedLogFont;

            private static GUIStyle MakeBaseLogStyle()
            {
                var ret = new GUIStyle();
                ret.clipping = TextClipping.Clip;
                ret.alignment = TextAnchor.UpperLeft;
                ret.imagePosition = ImagePosition.ImageLeft;
                return ret;
            }

            public Styles()
            {
                BG = new GUIStyle();
                BG.normal.background = Helper.Solid(0xffffff04);
                Font = new GUIStyle();
                Font.fontSize = fontSize;
                Font.alignment = TextAnchor.MiddleLeft;
                Font.clipping = TextClipping.Clip;
                Icon = new GUIStyle();
                Icon.alignment = TextAnchor.MiddleCenter;

                EvenLog = MakeBaseLogStyle();
                EvenLog.normal.background = Helper.Solid(0xeeeeeeff);
                OddLog = MakeBaseLogStyle();
                OddLog.normal.background = Helper.Solid(0xe0e0e0ff);

                SelectedLog = MakeBaseLogStyle();
                SelectedLog.normal.background = Helper.Solid(0x0d47a1ff);
                SelectedLogFont = new GUIStyle();
                SelectedLogFont.fontSize = fontSize;
                SelectedLogFont.alignment = TextAnchor.MiddleLeft;
                SelectedLogFont.clipping = TextClipping.Clip;
                SelectedLogFont.normal.textColor = Color.white;
            }
        }

        private readonly GUI.Icons _icons;
        private readonly Styles _styles;
        private readonly GUI.Table _table;
        private readonly Stash _stash;

        private int _selectedLog;
        private bool _keepInSelectedLog;

        public GUIView(GUI.Icons icons, Stash stash)
        {
            _icons = icons;
            _styles = new Styles();
            _stash = stash;
            _table = new GUI.Table(rowHeight, 0, _styles.BG);
        }

        public override void Update()
        {
            UpdateKeyboard();
        }

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

        public override void Draw(UnityEngine.Rect area)
        {
            // TODO:
            var logMask = new Mask();
            logMask.AllTrue();
            var logs = _stash.All();

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

            /*
            tempContent.text = log.count.ToString();
            float w = 0f;
            if (collapse)
                w = barStyle.CalcSize(tempContent).x + 3;
            countRect.x = Screen.width - w;
            countRect.y = size.y * i;
            if (beforeHeight > 0)
                countRect.y += 8;//i will check later why
            countRect.width = w;
            countRect.height = size.y;

            if (scrollerVisible)
                countRect.x -= size.x * 2;

            if (collapse)
                GUI.Label(countRect, log.count.ToString(), barStyle);
            */
        }
    }

    // buttomRect.x = 0f;
    // buttomRect.y = Screen.height - size.y;
    // buttomRect.width = Screen.width;
    // buttomRect.height = size.y;
    /*
    void drawStack()
    {
        stackRectTopLeft.x = 0f;
        stackRect.x = 0f;
        stackRectTopLeft.y = Screen.height * 0.75f;
        stackRect.y = Screen.height * 0.75f;
        stackRect.width = Screen.width;
        stackRect.height = Screen.height * 0.25f - size.y;


        if (selectedLog != null)
        {
            Vector2 drag = getDrag();
            if (drag.y != 0 && stackRect.Contains(new Vector2(downPos.x, Screen.height - downPos.y)))
            {
                scrollPosition2.y += drag.y - oldDrag2;
            }
            oldDrag2 = drag.y;



            GUILayout.BeginArea(stackRect, backStyle);
            scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
            Sample selectedSample = null;
            try
            {
                selectedSample = samples[selectedLog.sampleId];
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label(selectedLog.condition, stackLabelStyle);
            GUILayout.EndHorizontal();
            GUILayout.Space(size.y * 0.25f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(selectedLog.stacktrace, stackLabelStyle);
            GUILayout.EndHorizontal();
            GUILayout.Space(size.y);
            GUILayout.EndScrollView();
            GUILayout.EndArea();


            GUILayout.BeginArea(buttomRect, backStyle);
            GUILayout.BeginHorizontal();

            GUILayout.Box(showTimeContent, nonStyle, GUILayout.Width(size.x), GUILayout.Height(size.y));
            GUILayout.Label(selectedSample.time.ToString("0.000"), nonStyle);
            GUILayout.Space(size.x);

            GUILayout.Box(showSceneContent, nonStyle, GUILayout.Width(size.x), GUILayout.Height(size.y));
            GUILayout.Label(scenes[selectedSample.loadedScene], nonStyle);
            GUILayout.Space(size.x);

            GUILayout.Box(showMemoryContent, nonStyle, GUILayout.Width(size.x), GUILayout.Height(size.y));
            GUILayout.Label(selectedSample.memory.ToString("0.000"), nonStyle);
            GUILayout.Space(size.x);

            GUILayout.Box(showFpsContent, nonStyle, GUILayout.Width(size.x), GUILayout.Height(size.y));
            GUILayout.Label(selectedSample.FormatFpsToDisplay(), nonStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        else
        {
            GUILayout.BeginArea(stackRect, backStyle);
            GUILayout.EndArea();
            GUILayout.BeginArea(buttomRect, backStyle);
            GUILayout.EndArea();
        }
    }
     */
}

//         if (selectedLog != null)
//         {
//             int newSelectedIndex = currentLog.IndexOf(selectedLog);
//             if (newSelectedIndex == -1)
//             {
//                 Log collapsedSelected = logsDic[selectedLog.condition][selectedLog.stacktrace];
//     newSelectedIndex = currentLog.IndexOf(collapsedSelected);
//                 if (newSelectedIndex != -1)
//                     scrollPosition.y = newSelectedIndex* size.y;
// }
//             else
//             {
//                 scrollPosition.y = newSelectedIndex* size.y;
//             }
//         }
//     }
// }
