using UnityEngine;

namespace Settings.Extension.Scene
{
    internal partial class View
    {
        private static class Styles
        {
            public static readonly GUIStyle Button;
            public static readonly GUIStyle ButtonSelected;
            public static readonly GUIStyle ButtonFocusOverlay;

            static Styles()
            {
                Button = new GUIStyle(GUI.Styles.ButtonGray);
                Button.alignment = TextAnchor.MiddleCenter;
                ButtonSelected = new GUIStyle(GUI.Styles.ButtonGraySelected);
                ButtonSelected.alignment = TextAnchor.MiddleCenter;
                ButtonFocusOverlay = new GUIStyle();
                ButtonFocusOverlay.normal.background = GUI.Helper.Solid(0xffff0060);
            }
        }
    }
}
