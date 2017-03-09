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
