using System.Collections.Generic;
using UnityEngine;

namespace Settings.Extension.SystemInfo
{
    public partial class View
    {
        private enum RowType
        {
            Header, Row,
        }

        private struct RowDef
        {
            public readonly RowType Type;
            public readonly string Col1;
            public readonly string Col2;

            public RowDef(RowType type, string col1, string col2)
            {
                Type = type;
                Col1 = col1;
                Col2 = col2;
            }
        }

        private struct RowBuilder
        {
            private readonly List<RowDef> _rows;
            public RowBuilder(List<RowDef> rows) { _rows = rows; }
            public void Header(string title) { _rows.Add(new RowDef(RowType.Header, title, null)); }
            public void Row(string title, object desc) { _rows.Add(new RowDef(RowType.Row, title, desc.ToString())); }
            public void Row(string title, object desc, object descDetail) { _rows.Add(new RowDef(RowType.Row, title, DescAndDetail(desc, descDetail))); }
            public static string DescAndDetail(object desc, object descDetail) { return desc + " (" + descDetail + ")"; }
        }

        private static GUILayoutOption _minTitleWidth = GUILayout.MinWidth(240);

        private static void OnGUIRow(RowDef rowDef)
        {
            switch (rowDef.Type)
            {
                case RowType.Header:
                    GUILayout.Label(rowDef.Col1, Styles.HeaderFont);
                    break;
                case RowType.Row:
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(rowDef.Col1, Styles.RowTitleFont, _minTitleWidth);
                    GUILayout.Label(rowDef.Col2, Styles.RowFont);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    break;
            }
        }
    }
}