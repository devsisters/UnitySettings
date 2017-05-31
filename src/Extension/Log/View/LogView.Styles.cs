using UnityEngine;
using Helper = Settings.GUI.Helper;

namespace Settings.Extension.Log
{
    internal partial class View
    {
        private static class Styles
        {
            public static readonly GUIStyle Font;
            public static readonly GUIStyle Icon;

            public static readonly GUIStyle EvenLog;
            public static readonly GUIStyle OddLog;
            public static readonly GUIStyle SelectedLog;
            public static readonly GUIStyle SelectedLogFont;
            public static readonly GUIStyle CollapsedCountFont;

            public static readonly GUIStyle StackBG;
            public static readonly GUIStyle StackFont;

            public static readonly GUIStyle ToolbarBG;
            public static readonly GUIStyle ToolbarButton;
            public static readonly GUIStyle ToolbarButtonOn;

            private static GUIStyle MakeBaseLogStyle()
            {
                var ret = new GUIStyle();
                ret.clipping = TextClipping.Clip;
                ret.alignment = TextAnchor.UpperLeft;
                ret.imagePosition = ImagePosition.ImageLeft;
                return ret;
            }

            static Styles()
            {
                const int fontSize = 32;

                Font = new GUIStyle();
                Font.fontSize = fontSize;
                Font.alignment = TextAnchor.MiddleLeft;
                Font.clipping = TextClipping.Clip;
                Icon = new GUIStyle();
                Icon.alignment = TextAnchor.MiddleCenter;

                EvenLog = MakeBaseLogStyle();
                EvenLog.normal.background = Helper.Solid(0xeeeeeee0);
                OddLog = MakeBaseLogStyle();
                OddLog.normal.background = Helper.Solid(0xe0e0e0e0);

                SelectedLog = MakeBaseLogStyle();
                SelectedLog.normal.background = Helper.Solid(0x0d47a1e0);
                SelectedLogFont = new GUIStyle();
                SelectedLogFont.fontSize = fontSize;
                SelectedLogFont.alignment = TextAnchor.MiddleLeft;
                SelectedLogFont.clipping = TextClipping.Clip;
                SelectedLogFont.normal.textColor = Color.white;

                CollapsedCountFont = new GUIStyle();
                CollapsedCountFont.alignment = TextAnchor.MiddleCenter;
                CollapsedCountFont.fontSize = fontSize;
                CollapsedCountFont.normal.background = Helper.Solid(0x75757570);
                CollapsedCountFont.fixedWidth = fontSize * 2;

                StackBG = new GUIStyle();
                StackBG.normal.background = Helper.Solid(0x9e9e9ef0);
                StackFont = new GUIStyle();
                StackFont.fontSize = 28;
                StackFont.normal.textColor = Helper.UintToColor(0x424242ff);

                ToolbarBG = new GUIStyle();
                ToolbarBG.normal.background = Helper.Solid(0xbdbdbdf0);
                ToolbarButton = new GUIStyle();
                ToolbarButton.margin = new RectOffset(2, 2, 2, 2);
                ToolbarButton.alignment = TextAnchor.MiddleCenter;
                ToolbarButton.normal.background = Helper.Solid(0x9e9e9ef0);
                ToolbarButton.hover.background = Helper.Solid(0x757575f0);
                ToolbarButtonOn = new GUIStyle(ToolbarButton);
                ToolbarButtonOn.normal.background = Helper.Solid(0x616161f0);
                ToolbarButtonOn.hover.background = Helper.Solid(0x424242f0);
            }
        }
    }
}
