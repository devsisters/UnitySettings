using UnityEngine;

namespace Settings.GUI
{
    internal static class Styles
    {
        public static readonly GUIStyle BG;
        public static readonly GUIStyle ButtonGray;
        public static readonly GUIStyle ButtonGraySelected;

        static Styles()
        {
            BG = new GUIStyle();
            BG.normal.background = Helper.Solid(0xffffffa0);

            ButtonGray = new GUIStyle();
            ButtonGray.padding = new RectOffset(10, 10, 10, 10);
            ButtonGray.margin = new RectOffset(2, 2, 2, 2);
            ButtonGray.fontSize = 32;
            ButtonGray.normal.background = Helper.Solid(0x9e9e9ef0);
            ButtonGray.hover.background = Helper.Solid(0x757575f0);

            ButtonGraySelected = new GUIStyle(ButtonGray);
            ButtonGraySelected.fontStyle = FontStyle.Bold;
            ButtonGraySelected.normal.background = Helper.Solid(0x616161f0);
            ButtonGraySelected.normal.textColor = Color.white;
            ButtonGraySelected.hover.background = Helper.Solid(0x424242f0);
            ButtonGraySelected.hover.textColor = Color.white;
        }
    }
}