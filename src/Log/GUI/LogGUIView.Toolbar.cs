using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private GUILayoutOption _toolbarTempWidth;
        private GUILayoutOption _toolbarTempHeight;

        private bool DrawToolbarButton(GUIContent icon)
        {
            return GUILayout.Button(icon, _styles.ToolbarButton, _toolbarTempWidth, _toolbarTempHeight);
        }

        private bool DrawToolbarToggle(GUIContent icon, bool value)
        {
            var style = value ? _styles.ToolbarButtonOn : _styles.ToolbarButton;
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

            GUILayout.BeginArea(area, _styles.ToolbarBG);
            GUILayout.BeginHorizontal();

            var c = _config;

            // draw actions
            if (DrawToolbarButton(_icons.Clear)) _isClickedClear.On();
            DrawToolbarToggle(_icons.Collapse, ref c.Collapse);

            // draw show sample
            DrawToolbarToggle(_icons.ShowTime, ref c.ShowTime);
            DrawToolbarToggle(_icons.ShowScene, ref c.ShowScene);

            GUILayout.FlexibleSpace();

            // draw log mask
            c.Filter.Log = DrawToolbarToggle(_icons.Log, c.Filter.Log);
            c.Filter.Warning = DrawToolbarToggle(_icons.Warning, c.Filter.Warning);
            c.Filter.Error = DrawToolbarToggle(_icons.Error, c.Filter.Error);

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}