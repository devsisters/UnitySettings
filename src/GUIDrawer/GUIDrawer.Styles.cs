using UnityEngine;

namespace Settings.GUI
{
    internal partial class Drawer
    {
        private class Styles
        {
            public static readonly GUIStyle ToolbarBG;
            public static readonly GUIStyle ToolbarButton;
            public static readonly GUIStyle ToolbarButtonOn;

            static Styles()
            {
                ToolbarBG = new GUIStyle();
                ToolbarBG.normal.background = Helper.Solid(0x039be5f0);
                ToolbarButton = new GUIStyle();
                ToolbarButton.margin = new RectOffset(2, 2, 2, 2);
                ToolbarButton.alignment = TextAnchor.MiddleCenter;
                ToolbarButton.normal.background = Helper.Solid(0x0288d1f0);
                ToolbarButton.hover.background = Helper.Solid(0xfdd83580);
                ToolbarButtonOn = new GUIStyle(ToolbarButton);
                ToolbarButtonOn.normal.background = Helper.Solid(0x0277bdf0);
                ToolbarButtonOn.hover.background = Helper.Solid(0xfbc02d80);
            }
        }
    }
}