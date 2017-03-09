using UnityEngine;
using Helper = Settings.GUI.Helper;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        internal class Styles
        {
            public readonly GUIStyle Font;
            public readonly GUIStyle Icon;

            public readonly GUIStyle TableBG;
            public readonly GUIStyle EvenLog;
            public readonly GUIStyle OddLog;
            public readonly GUIStyle SelectedLog;
            public readonly GUIStyle SelectedLogFont;

            public readonly GUIStyle StackBG;
            public readonly GUIStyle StackFont;

            private static GUIStyle MakeBaseLogStyle()
            {
                var ret = new GUIStyle();
                ret.clipping = TextClipping.Clip;
                ret.alignment = TextAnchor.UpperLeft;
                ret.imagePosition = ImagePosition.ImageLeft;
                return ret;
            }

            public Styles()
            {
                const int fontSize = 32;

                Font = new GUIStyle();
                Font.fontSize = fontSize;
                Font.alignment = TextAnchor.MiddleLeft;
                Font.clipping = TextClipping.Clip;
                Icon = new GUIStyle();
                Icon.alignment = TextAnchor.MiddleCenter;

                TableBG = new GUIStyle();
                TableBG.normal.background = Helper.Solid(0xffffffa0);

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

                StackBG = new GUIStyle();
                StackBG.normal.background = Helper.Solid(0x9e9e9ef0);
                StackFont = new GUIStyle();
                StackFont.fontSize = 28;
                StackFont.normal.textColor = Helper.UintToColor(0x424242ff);
            }
        }
    }
}