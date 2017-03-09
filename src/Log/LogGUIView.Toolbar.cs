using UnityEngine;

namespace Settings.Log
{
    internal partial class GUIView : GUI.IView
    {
        private void OnGUIToolbar(Rect area)
        {
            // TODO
            const int padding = 2;
            var w = GUILayout.MinWidth(64);
            var h = GUILayout.Height(area.height - padding * 2);
            System.Func<GUIContent, bool> drawButton =
                icon => GUILayout.Button(icon, _styles.ToolbarButton, w, h);

            GUILayout.BeginArea(area, _styles.ToolbarBG);
            GUILayout.BeginHorizontal();
            if (drawButton(_icons.Clear)) Debug.Log("Clear");
            if (drawButton(_icons.Collapse)) Debug.Log("Collapse");
            if (drawButton(_icons.ShowTime)) Debug.Log("Time");
            if (drawButton(_icons.ShowScene)) Debug.Log("Scene");
            GUILayout.FlexibleSpace();
            if (drawButton(_icons.Log)) Debug.Log("Log");
            if (drawButton(_icons.Warning)) Debug.Log("Warning");
            if (drawButton(_icons.Error)) Debug.Log("Error");
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}