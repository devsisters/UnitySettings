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
            // TODO
            const int padding = 2;
            _toolbarTempWidth = GUILayout.MinWidth(64);
            _toolbarTempHeight = GUILayout.Height(area.height - padding * 2);
            GUILayout.BeginArea(area, _styles.ToolbarBG);
            GUILayout.BeginHorizontal();
            if (DrawToolbarButton(_icons.Clear)) Debug.Log("Clear");
            if (DrawToolbarToggle(_icons.Collapse, ref _collapse)) Debug.Log("Collapse");
            if (DrawToolbarToggle(_icons.ShowTime, ref _showTime)) Debug.Log("Time");
            if (DrawToolbarToggle(_icons.ShowScene, ref _showScene)) Debug.Log("Scene");
            GUILayout.FlexibleSpace();
            if (DrawToolbarToggle(_icons.Log, ref _showLog)) Debug.Log("Log");
            if (DrawToolbarToggle(_icons.Warning, ref _showWarning)) Debug.Log("Warning");
            if (DrawToolbarToggle(_icons.Error, ref _showError)) Debug.Log("Error");
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}