using UnityEngine;

namespace Settings.Extension.FPS
{
    internal class ToolbarWidget : GUI.IToolbarWidget
    {
        private readonly Counter _counter;

        public ToolbarWidget(Counter counter)
        {
            _counter = counter;
        }

        private static int GetTargetFrameRate()
        {
            var target = Application.targetFrameRate;
            if (target != -1) return target;
            if (Application.isMobilePlatform) return 30;
            else if (Application.isEditor) return 60;
            else return 60;
        }

        private static Color ColorForFPS(int fps)
        {
            var target = GetTargetFrameRate();
            if (fps < target / 2) return Color.red;
            else if (fps < ((target * 5) / 6f)) return Color.yellow;
            else return Color.green;
        }

        public override void OnGUI(Rect area)
        {
            var fps = _counter.FPS;
            var style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = (int)area.height - 30;
            style.normal.textColor = ColorForFPS(fps);
            UnityEngine.GUI.Label(area, fps.ToString(), style);
        }
    }
}