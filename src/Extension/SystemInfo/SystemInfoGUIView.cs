using System.Collections.Generic;
using UnityEngine;

namespace Settings.Extension.SystemInfo
{
    public partial class GUIView : GUI.IView
    {
        private readonly GUI.Icons _icons;
        private readonly GUI.ScrollView _scroll;

        private string _curPage;
        private readonly Dictionary<string, List<RowDef>> _pages;

        public GUIView(GUI.Icons icons)
            : base("SystemInfo", icons.SystemInfo)
        {
            _icons = icons;
            _scroll = new GUI.ScrollView(Vector2.zero, true, true);
            _pages = new Dictionary<string, List<RowDef>>(8);
            AddAllPages();
        }

        public override void Update()
        {
            _scroll.Update();
            UpdateKeyboard();
        }

        private void UpdateKeyboard()
        {
            var dir = 0;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) dir = -1;
            if (Input.GetKeyDown(KeyCode.RightArrow)) dir = 1;
            if (dir == 0) return;
            var keys = new List<string>(_pages.Keys);
            var curIndex = keys.FindIndex(x => x == _curPage);
            if (curIndex != -1)
            {
                curIndex = (curIndex + dir + keys.Count) % keys.Count;
                _curPage = keys[curIndex];
            }
        }

        public override void OnGUI(Rect area)
        {
            UnityEngine.GUI.Box(area, "", Styles.BG);
            _scroll.BeginLayout(area);
            OnGUIPageSelect();
            OnGUICurPage();
            _scroll.EndLayout();
        }
    }
}