using UnityEngine;
using List = System.Collections.Generic.List<Log.Log>;

namespace GUI
{
    internal class LogDrawer
    {
        private readonly Icons _icons;
        private readonly Styles _styles;
        private float _scrollY;
        private const int startIndex = 0; // TODO

        public LogDrawer(Icons icons, Styles styles)
        {
            _icons = icons;
            _styles = styles;
        }

        private void UpdateScroll(Rect area)
        {
            Vector2 downPos;
            if (!Util.Mouse.DownPos(out downPos)) return;
            downPos = new Vector2(downPos.x, Screen.height - downPos.y);
            if (!area.Contains(downPos)) return;

            Vector2 delta;
            if (!Util.Mouse.Delta(out delta)) return;
            if (delta.y == 0) return;

            _scrollY += delta.y;
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

        private static void DrawRightLabel(string text, float rowHeight, GUIStyle style, ref float x)
        {
            _tempContent.text = text;
            var w = style.CalcSize(_tempContent).x;
            x -= w;
            var r = new Rect(x, 0, w, rowHeight);
            UnityEngine.GUI.Label(r, text, style);
        }

        private static void DrawRightIcon(GUIContent icon, float rowHeight, float cellX, GUIStyle style, ref float x)
        {
            x -= cellX;
            var r = new Rect(x, 0, cellX, rowHeight);
            UnityEngine.GUI.Box(r, icon, style);
        }

        private void DrawRow(
            Log.Log log,
            float rowHeight, float cellX, bool showTime, bool showScene,
            GUIStyle style, GUIStyle fontStyle,
            System.Action onClick)
        {
            // _styles.selectedLogFontStyle
            // _styles.logButtonStyle
            GUILayout.BeginHorizontal(style);
            var content = IconForLogType(log.Type);
            if (GUILayout.Button(content, _styles.None, GUILayout.Width(cellX), GUILayout.Height(rowHeight)))
                onClick();
            if (GUILayout.Button(log.Message, fontStyle))
                onClick();

            float rightX = Screen.width;
            if (showScene)
            {
                DrawRightLabel(log.Scene, rowHeight, style, ref rightX);
                DrawRightIcon(_icons.ShowScene, rowHeight, cellX, style, ref rightX);
            }
            if (showTime)
            {
                DrawRightLabel(log.Time.ToString("0.000"), rowHeight, style, ref rightX);
                DrawRightIcon(_icons.ShowTime, rowHeight, cellX, style, ref rightX);
            }
        }

        public void Draw(
            Rect area, float rowHeight, float cellX,
            List logs, int selectedLog, Log.Mask logMask,
            bool showTime, bool showScene)
        {
            UpdateScroll(area);

            GUILayout.BeginArea(area, _styles.Back);
            // GUI.skin = logScrollerSkin;
            _scrollY = GUILayout.BeginScrollView(new Vector2(0, _scrollY)).y;

            int totalVisibleCount = (int)(area.height / rowHeight);
            int totalCount = logs.Count;

            totalVisibleCount = Mathf.Min(totalVisibleCount, totalCount - startIndex);
            int beforeHeight = (int)(startIndex * rowHeight);
            if (beforeHeight > 0)
            {
                //fill invisible gap befor scroller to make proper scroller pos
                GUILayout.BeginHorizontal(GUILayout.Height(beforeHeight));
                GUILayout.Label("---");
                GUILayout.EndHorizontal();
            }

            int endIndex = startIndex + totalVisibleCount;
            endIndex = Mathf.Clamp(endIndex, 0, totalCount);
            // bool scrollerVisible = (totalVisibleCount < totalCount);
            for (int i = startIndex, order = 0; (startIndex + order) < endIndex; ++i)
            {
                if (i >= logs.Count) break;
                var log = logs[i];
                if (!logMask.Check(log.Type)) continue;
                if (order >= totalVisibleCount) break;

                // var currentLogStyle = ((startIndex + order) % 2 == 0) ? _styles.EvenLog : _styles.OddLog;
                // var isSelectedLog = i == selectedLog;
                // if (isSelectedLog) currentLogStyle = _styles.SelectedLog;

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
                */

                /*
                if (collapse)
                    GUI.Label(countRect, log.count.ToString(), barStyle);
                */

                GUILayout.EndHorizontal();
                order++;
            }

            int afterHeight = (int)((totalCount - (startIndex + totalVisibleCount)) * rowHeight);
            if (afterHeight > 0)
            {
                //fill invisible gap after scroller to make proper scroller pos
                GUILayout.BeginHorizontal(GUILayout.Height(afterHeight));
                GUILayout.Label(" ");
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();

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
}
