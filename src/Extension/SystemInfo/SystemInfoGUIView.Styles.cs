using UnityEngine;
using Helper = Settings.GUI.Helper;

namespace Settings.Extension.SystemInfo
{
    public partial class GUIView
    {
        private static class Styles
        {
            public static GUIStyle BG;
            public static GUIStyle HeaderFont;
            public static GUIStyle Header2Font;
            public static GUIStyle Font;

            static Styles()
            {
                BG = new GUIStyle();
                BG.normal.background = Helper.Solid(0xffffffa0);

                HeaderFont = new GUIStyle();
                HeaderFont.fontSize = 36;
                HeaderFont.padding = new RectOffset(5, 5, 5, 5);
                HeaderFont.normal.textColor = Color.black;
                HeaderFont.fontStyle = FontStyle.Bold;

                Header2Font = new GUIStyle(HeaderFont);
                Header2Font.fontSize = 32;
                Header2Font.fontStyle = FontStyle.Italic;

                Font = new GUIStyle();
                Font.fontSize = 32;
                Font.padding = new RectOffset(5, 5, 5, 5);
                Font.normal.textColor = Color.black;
            }
        }
    }
}