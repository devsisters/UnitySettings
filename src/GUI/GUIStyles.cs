using UnityEngine;

namespace Settings.GUI
{
    public class Styles
    {
        public GUIStyle None;
        public GUIStyle Back;

        public GUIStyle Bar;
        public GUIStyle ButtonActive;

        public GUIStyle LowerLeftFont;
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
            Back = new GUIStyle();
            Back.normal.background = PNGs.SkinEvenLog.ToTex();
            Back.clipping = TextClipping.Clip;
            Back.fontSize = (int)(size.y / 2);

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
