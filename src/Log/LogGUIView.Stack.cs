using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView
    {
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
                if (_isSelectedLogDirty)
                    _stackScroll.Scroll.Set(0, 0);
                var stackH = h - sampleH;
                _stackScroll.BeginLayout(new Rect(x, y, w, stackH));
                GUILayout.Space(stackSpace);
                GUILayout.Label(log.Message, _styles.Font);
                GUILayout.Space(stackSpace);
                GUILayout.Label(log.Stacktrace, _styles.StackFont);
                GUILayout.Space(stackSpace);
                _stackScroll.EndLayout();
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
