using System.Collections.Generic;
using UnityEngine;

namespace Settings.Extension.SystemInfo
{
    public partial class GUIView : GUI.IView
    {
        private RowBuilder AddPage(string title)
        {
            var key = title;
            var rows = new List<RowDef>(32);
            if (_curPage == null) _curPage = key;
            _pages.Add(key, rows);
            return new RowBuilder(rows);
        }

        private void OnGUIPageSelect()
        {
            var height = GUILayout.Height(32);
            GUILayout.BeginHorizontal(Styles.PageBar, height);
            foreach (var kv in _pages)
            {
                var title = kv.Key;
                var isSelected = _curPage == title;
                var style = isSelected ? Styles.SelectedPageFont : Styles.PageFont;
                if (GUILayout.Button(title, style)) _curPage = title;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void OnGUICurPage()
        {
            List<RowDef> page;
            if (_pages.TryGetValue(_curPage, out page))
                page.ForEach(OnGUIRow);
        }
    }
}
