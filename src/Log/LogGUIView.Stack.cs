using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView
    {
        private Vector2 _stackScroll;

        private void DrawStackEmpty(Rect area)
        {
            UnityEngine.GUI.Box(area, "", _styles.StackBG);
        }

        private void DrawStack(Rect area, Log log)
        {
            DrawStackEmpty(area);
            //     Vector2 drag = getDrag();
            //     if (drag.y != 0 && stackRect.Contains(new Vector2(downPos.x, Screen.height - downPos.y)))
            //     {
            //         scrollPosition2.y += drag.y - oldDrag2;
            //     }
            //     oldDrag2 = drag.y;

            //     GUILayout.BeginArea(stackRect, backStyle);
            //     scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
            //     Sample selectedSample = null;
            //     try
            //     {
            //         selectedSample = samples[selectedLog.sampleId];
            //     }
            //     catch (System.Exception e)
            //     {
            //         Debug.LogException(e);
            //     }

            const int sampleH = 40;
            const int stackPadding = 12;
            const int stackSpace = 12;
            var x = area.x + stackPadding; var y = area.y;
            var w = area.width - 2 * stackPadding; var h = area.height;

            // draw stack
            {
                var stackH = h - sampleH;
                GUILayout.BeginArea(new Rect(x, y, w, stackH));
                _stackScroll = GUILayout.BeginScrollView(_stackScroll);
                GUILayout.Space(stackSpace);
                GUILayout.Label(log.Message, _styles.Font);
                GUILayout.Space(stackSpace);
                GUILayout.Label(log.Stacktrace, _styles.StackFont);
                GUILayout.Space(stackSpace);
                GUILayout.EndScrollView();
                GUILayout.EndArea();
                y += stackH;
            }

            // draw sample
            {
                System.Action<GUIContent, string> drawIconAndLabel = (icon, label) =>
                {
                    GUILayout.Box(icon, _styles.Icon, GUILayout.Width(_iconWidth), GUILayout.Height(sampleH));
                    GUILayout.Label(label, _styles.Font);
                    GUILayout.Space(stackSpace);
                };

                GUILayout.BeginArea(new Rect(x, y, w, sampleH));
                GUILayout.BeginHorizontal();
                drawIconAndLabel(_icons.ShowTime, log.Sample.TimeToDisplay);
                drawIconAndLabel(_icons.ShowScene, log.Sample.Scene);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
                y += sampleH;
            }
        }
    }
}
