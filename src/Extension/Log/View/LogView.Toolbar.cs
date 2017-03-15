using UnityEngine;

namespace Settings.Extension.Log
{
    internal partial class View
    {
        private static GUILayoutOption _toolbarTempWidth;
        private static GUILayoutOption _toolbarTempHeight;

        private bool DrawToolbarButton(GUIContent icon)
        {
            return GUILayout.Button(icon, Styles.ToolbarButton, _toolbarTempWidth, _toolbarTempHeight);
        }

        private bool DrawToolbarToggle(GUIContent icon, bool value)
        {
            var style = value ? Styles.ToolbarButtonOn : Styles.ToolbarButton;
            var clicked = GUILayout.Button(icon, style, _toolbarTempWidth, _toolbarTempHeight);
            if (clicked) value = !value;
            return value;
        }

        private void DrawToolbarToggle(GUIContent icon, ref bool value)
        {
            value = DrawToolbarToggle(icon, value);
        }

        private void OnGUIToolbar(Rect area)
        {
            const int padding = 2;
            _toolbarTempWidth = GUILayout.MinWidth(64);
            _toolbarTempHeight = GUILayout.Height(area.height - padding * 2);

            GUILayout.BeginArea(area, Styles.ToolbarBG);
            GUILayout.BeginHorizontal();

            var c = _config;

            // draw actions
            if (DrawToolbarButton(Icons.Clear)) _isClickedClear.On();
            DrawToolbarToggle(Icons.Collapse, ref c.Collapse);

            // draw show sample
            if (!c.Collapse)
            {
                DrawToolbarToggle(Icons.ShowTime, ref c.ShowTime);
                DrawToolbarToggle(Icons.ShowScene, ref c.ShowScene);
            }

            GUILayout.FlexibleSpace();

            // draw log mask
            c.Filter.Log = DrawToolbarToggle(Icons.Log, c.Filter.Log);
            c.Filter.Warning = DrawToolbarToggle(Icons.Warning, c.Filter.Warning);
            var showError = DrawToolbarToggle(Icons.Error, c.Filter.Error);
            c.Filter.Error = showError;
            c.Filter.Exception = showError;
            c.Filter.Assert = showError;

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}