using UnityEngine;

namespace Settings.Extension.Scene
{
    internal partial class View
    {
        private static class Styles
        {
            public static readonly GUIStyle Button;
            public static readonly GUIStyle ButtonSelected;

            static Styles()
            {
                Button = new GUIStyle(GUI.Styles.ButtonGray);
                Button.alignment = TextAnchor.MiddleCenter;
                ButtonSelected = new GUIStyle(GUI.Styles.ButtonGraySelected);
                ButtonSelected.alignment = TextAnchor.MiddleCenter;
            }
        }
    }
}
