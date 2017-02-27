using UnityEngine;

namespace GUI
{
    public class Icons
    {
        public readonly GUIContent Clear;
        public readonly GUIContent Collapse;
        public readonly GUIContent ClearOnNewScene;
        public readonly GUIContent ShowTime;
        public readonly GUIContent ShowScene;
        public readonly GUIContent User;
        public readonly GUIContent ShowMemory;
        public readonly GUIContent Software;
        public readonly GUIContent Date;
        public readonly GUIContent ShowFps;
        public readonly GUIContent Info;
        public readonly GUIContent Search;
        public readonly GUIContent Close;

        public readonly GUIContent BuildFrom;
        public readonly GUIContent SystemInfo;
        public readonly GUIContent GraphicsInfo;
        public readonly GUIContent Back;

        public readonly GUIContent Log;
        public readonly GUIContent Warning;
        public readonly GUIContent Error;

        public static Icons Load()
        {
            return new Icons();
        }

        private static GUIContent I(byte[] tex, string tooltip)
        {
            return new GUIContent("", tex.ToTex(), tooltip);
        }

        private Icons()
        {
            Clear = I(PNGs.Clear, "Clear logs");
            Collapse = I(PNGs.Collapse, "Collapse logs");
            ClearOnNewScene = I(PNGs.Clear, "Clear logs on new scene loaded");
            ShowTime = I(PNGs.Time, "Show Hide Time");
            ShowScene = I(PNGs.Unity, "Show Hide Scene");
            Software = I(PNGs.Software, "Software");
            Info = I(PNGs.Info, "Information about application");
            Search = I(PNGs.Search, "Search for logs");
            Close = I(PNGs.Close, "Hide logs");
            User = I(PNGs.User, "User");

            BuildFrom = I(PNGs.BuildFrom, "Build From");
            SystemInfo = I(PNGs.SystemInfo, "System Info");
            GraphicsInfo = I(PNGs.GraphicsInfo, "Graphics Info");
            Back = I(PNGs.Back, "Back");

            Log = I(PNGs.Log, "show or hide logs");
            Warning = I(PNGs.Warning, "show or hide warnings");
            Error = I(PNGs.Error, "show or hide errors");
        }
    }

    public class Styles
    {
        public GUIStyle Bar;
        public GUIStyle ButtonActive;

        public GUIStyle None;
        public GUIStyle LowerLeftFont;
        public GUIStyle Back;
        public GUIStyle EvenLog;
        public GUIStyle OddLog;
        public GUIStyle LogButton;
        public GUIStyle SelectedLog;
        public GUIStyle SelectedLogFont;
        public GUIStyle StackLabel;
        public GUIStyle Scroller;
        public GUIStyle Search;
        public GUIStyle SliderBack;
        public GUIStyle SliderThumb;
        public GUISkin ToolbarScroller;
        public GUISkin LogScroller;

        public void Init()
        {
            //initialize gui and styles for gui porpose
            // TODO
            initializeStyle(new Vector2(32, 32));
        }

        void initializeStyle(Vector2 size)
        {
            int paddingX = (int)(size.x * 0.2f);
            int paddingY = (int)(size.y * 0.2f);
            None = new GUIStyle();
            None.clipping = TextClipping.Clip;
            None.border = new RectOffset(0, 0, 0, 0);
            None.normal.background = null;
            None.fontSize = (int)(size.y / 2);
            None.alignment = TextAnchor.MiddleCenter;

            LowerLeftFont = new GUIStyle();
            LowerLeftFont.clipping = TextClipping.Clip;
            LowerLeftFont.border = new RectOffset(0, 0, 0, 0);
            LowerLeftFont.normal.background = null;
            LowerLeftFont.fontSize = (int)(size.y / 2);
            LowerLeftFont.fontStyle = FontStyle.Bold;
            LowerLeftFont.alignment = TextAnchor.LowerLeft;

            Bar = new GUIStyle();
            Bar.border = new RectOffset(1, 1, 1, 1);
            Bar.normal.background = PNGs.SkinBar.ToTex();
            Bar.active.background = PNGs.SkinButtonActive.ToTex();
            Bar.alignment = TextAnchor.MiddleCenter;
            Bar.margin = new RectOffset(1, 1, 1, 1);
            Bar.clipping = TextClipping.Clip;
            Bar.fontSize = (int)(size.y / 2);

            ButtonActive = new GUIStyle();
            ButtonActive.border = new RectOffset(1, 1, 1, 1);
            ButtonActive.normal.background = PNGs.SkinButtonActive.ToTex();
            ButtonActive.alignment = TextAnchor.MiddleCenter;
            ButtonActive.margin = new RectOffset(1, 1, 1, 1);
            ButtonActive.fontSize = (int)(size.y / 2);

            Back = new GUIStyle();
            Back.normal.background = PNGs.SkinEvenLog.ToTex();
            Back.clipping = TextClipping.Clip;
            Back.fontSize = (int)(size.y / 2);

            EvenLog = new GUIStyle();
            EvenLog.normal.background = PNGs.SkinEvenLog.ToTex();
            EvenLog.fixedHeight = size.y;
            EvenLog.clipping = TextClipping.Clip;
            EvenLog.alignment = TextAnchor.UpperLeft;
            EvenLog.imagePosition = ImagePosition.ImageLeft;
            EvenLog.fontSize = (int)(size.y / 2);

            OddLog = new GUIStyle();
            OddLog.normal.background = PNGs.SkinOddLog.ToTex();
            OddLog.fixedHeight = size.y;
            OddLog.clipping = TextClipping.Clip;
            OddLog.alignment = TextAnchor.UpperLeft;
            OddLog.imagePosition = ImagePosition.ImageLeft;
            OddLog.fontSize = (int)(size.y / 2);

            LogButton = new GUIStyle();
            LogButton.fixedHeight = size.y;
            LogButton.clipping = TextClipping.Clip;
            LogButton.alignment = TextAnchor.UpperLeft;
            LogButton.fontSize = (int)(size.y / 2);
            LogButton.padding = new RectOffset(paddingX, paddingX, paddingY, paddingY);

            SelectedLog = new GUIStyle();
            SelectedLog.normal.background = PNGs.SkinSelected.ToTex();
            SelectedLog.fixedHeight = size.y;
            SelectedLog.clipping = TextClipping.Clip;
            SelectedLog.alignment = TextAnchor.UpperLeft;
            SelectedLog.normal.textColor = Color.white;
            SelectedLog.fontSize = (int)(size.y / 2);

            SelectedLogFont = new GUIStyle();
            SelectedLogFont.normal.background = PNGs.SkinSelected.ToTex();
            SelectedLogFont.fixedHeight = size.y;
            SelectedLogFont.clipping = TextClipping.Clip;
            SelectedLogFont.alignment = TextAnchor.UpperLeft;
            SelectedLogFont.normal.textColor = Color.white;
            SelectedLogFont.fontSize = (int)(size.y / 2);
            SelectedLogFont.padding = new RectOffset(paddingX, paddingX, paddingY, paddingY);

            StackLabel = new GUIStyle();
            StackLabel.wordWrap = true;
            StackLabel.fontSize = (int)(size.y / 2);
            StackLabel.padding = new RectOffset(paddingX, paddingX, paddingY, paddingY);

            Scroller = new GUIStyle();
            Scroller.normal.background = PNGs.SkinBar.ToTex();

            Search = new GUIStyle();
            Search.clipping = TextClipping.Clip;
            Search.alignment = TextAnchor.LowerCenter;
            Search.fontSize = (int)(size.y / 2);
            Search.wordWrap = true;

            SliderBack = new GUIStyle();
            SliderBack.normal.background = PNGs.SkinBar.ToTex();
            SliderBack.fixedHeight = size.y;
            SliderBack.border = new RectOffset(1, 1, 1, 1);

            SliderThumb = new GUIStyle();
            SliderThumb.normal.background = PNGs.SkinSelected.ToTex();
            SliderThumb.fixedWidth = size.x;
        }
    }
}
