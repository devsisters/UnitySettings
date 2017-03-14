using UnityEngine;
using Settings.GUI;

namespace Settings.Extension.SystemInfo
{
    public partial class GUIView
    {
        private static class Styles
        {
            public static readonly GUIStyle BG;
            public static readonly GUIStyle PageBar;
            public static readonly GUIStyle PageFont;
            public static readonly GUIStyle SelectedPageFont;
            public static readonly GUIStyle HeaderFont;
            public static readonly GUIStyle RowTitleFont;
            public static readonly GUIStyle RowFont;

            static Styles()
            {
                BG = new GUIStyle();
                BG.normal.background = Helper.Solid(0xffffffa0);

                PageBar = new GUIStyle();
                PageBar.normal.background = Helper.Solid(0xbdbdbdf0);

                PageFont = new GUIStyle();
                PageFont.padding = new RectOffset(10, 10, 10, 10);
                PageFont.margin = new RectOffset(2, 2, 2, 2);
                PageFont.fontSize = 32;
                PageFont.normal.background = Helper.Solid(0x9e9e9ef0);
                PageFont.hover.background = Helper.Solid(0x757575f0);
                SelectedPageFont = new GUIStyle(PageFont);
                SelectedPageFont.fontStyle = FontStyle.Bold;
                SelectedPageFont.normal.background = Helper.Solid(0x616161f0);
                SelectedPageFont.normal.textColor = Color.white;
                SelectedPageFont.hover.background = Helper.Solid(0x424242f0);
                SelectedPageFont.hover.textColor = Color.white;

                HeaderFont = new GUIStyle();
                HeaderFont.padding = new RectOffset(5, 5, 5, 5);
                HeaderFont.fontSize = 32;
                HeaderFont.fontStyle = FontStyle.BoldAndItalic;
                HeaderFont.normal.textColor = Color.black;

                RowFont = new GUIStyle();
                RowFont.fontSize = 32;
                RowFont.padding = new RectOffset(5, 5, 5, 5);
                RowFont.normal.textColor = Color.black;

                RowTitleFont = new GUIStyle(RowFont);
                RowTitleFont.fontStyle = FontStyle.Bold;
            }
        }
    }
}