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

        private bool DrawToolbarToggle(GUIContent icon, ref bool value)
        {
            var style = value ? _styles.ToolbarButtonOn : _styles.ToolbarButton;
            var clicked = GUILayout.Button(icon, style, _toolbarTempWidth, _toolbarTempHeight);
            if (clicked) value = !value;
            return clicked;
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
            if (DrawToolbarButton(_icons.Clear)) Debug.Log("Clear"); // TODO
            DrawToolbarToggle(_icons.Collapse, ref c.Collapse);

            // draw show sample
            DrawToolbarToggle(_icons.ShowTime, ref c.ShowTime);
            DrawToolbarToggle(_icons.ShowScene, ref c.ShowScene);

            GUILayout.FlexibleSpace();

            // draw log mask
            DrawToolbarToggle(_icons.Log, ref c.ShowLog);
            DrawToolbarToggle(_icons.Warning, ref c.ShowWarning);
            DrawToolbarToggle(_icons.Error, ref c.ShowError);

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}