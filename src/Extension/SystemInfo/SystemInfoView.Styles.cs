using UnityEngine;
using Settings.GUI;

namespace Settings.Extension.SystemInfo
{
    public partial class View
    {
        private static class Styles
        {
            public static readonly GUIStyle PageBar;
            public static readonly GUIStyle PageFont = GUI.Styles.ButtonGray;
            public static readonly GUIStyle SelectedPageFont = GUI.Styles.ButtonGraySelected;
            public static readonly GUIStyle HeaderFont;
            public static readonly GUIStyle RowTitleFont;
            public static readonly GUIStyle RowFont;

            static Styles()
            {
                PageBar = new GUIStyle();
                PageBar.normal.background = Helper.Solid(0xbdbdbdf0);

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