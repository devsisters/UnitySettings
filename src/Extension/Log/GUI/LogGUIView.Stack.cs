using UnityEngine;

namespace Settings.Extension.Log
{
    internal partial class GUIView
    {
        private void DrawStackEmpty(Rect area)
        {
            UnityEngine.GUI.Box(area, "", Styles.StackBG);
        }

        private void DrawStack(Rect area, AbstractLog log)
        {
            DrawStackEmpty(area);

            // layout
            const int stackPadding = 12;
            const int stackSpace = 12;
            var x = area.x + stackPadding; var y = area.y;
            var w = area.width - 2 * stackPadding; var h = area.height;

            const int sampleH = 40;
            var stackH = h;
            var drawSample = log.Sample.HasValue;
            if (drawSample) stackH -= sampleH;

            // draw stack
            {
                if (_isSelectedLogDirty)
                    _stackScroll.Scroll.Set(0, 0);
                _stackScroll.BeginLayout(new Rect(x, y, w, stackH));
                GUILayout.Space(stackSpace);
                GUILayout.Label(log.Message, Styles.Font);
                GUILayout.Space(stackSpace);
                GUILayout.Label(log.Stacktrace, Styles.StackFont);
                GUILayout.Space(stackSpace);
                _stackScroll.EndLayout();
                y += stackH;
            }

            // draw sample
            if (drawSample)
            {
                System.Action<GUIContent, string> drawIconAndLabel = (icon, label) =>
                {
                    GUILayout.Box(icon, Styles.Icon, GUILayout.Width(_iconWidth), GUILayout.Height(sampleH));
                    GUILayout.Label(label, Styles.Font);
                    GUILayout.Space(stackSpace);
                };

                GUILayout.BeginArea(new Rect(x, y, w, sampleH));
                GUILayout.BeginHorizontal();
                var sample = log.Sample.Value;
                drawIconAndLabel(Icons.ShowTime, sample.TimeToDisplay);
                drawIconAndLabel(Icons.ShowScene, sample.Scene);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
                y += sampleH;
            }
        }
    }
}
