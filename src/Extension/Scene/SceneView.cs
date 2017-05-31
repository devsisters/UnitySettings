using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Settings.Extension.Scene
{
    internal partial class View : GUI.IView
    {
        private const int _buttonHeight = 80;
        private int? _focus;

        public View()
            : base("Scene", Icons.Icon)
        {
        }

        public override void Update()
        {
            UpdateFocus();
        }

        private void UpdateFocus()
        {
            // up down focus
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!_focus.HasValue)
                    _focus = 0;
                ++_focus;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!_focus.HasValue)
                    _focus = 0;
                --_focus;
            }

            if (!_focus.HasValue) return;

            // cull focus
            var max = SceneManager.sceneCount;
            _focus = Mathf.Clamp(_focus.Value, 0, max);

            // on hit space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var scene = SceneManager.GetSceneAt(_focus.Value);
                SceneManager.LoadScene(scene.name);
                _focus = null;
            }
        }

        public override void OnGUI(Rect area)
        {
            GUILayout.BeginArea(area, GUI.Styles.BG);

            var sceneCount = SceneManager.sceneCount;
            var drawnScenes = new List<string>(sceneCount);
            for (var i = 0; i != sceneCount; ++i)
            {
                var scene = SceneManager.GetSceneAt(i);

                // draw check
                var isDrawn = drawnScenes.Contains(scene.path);
                if (isDrawn) continue;
                drawnScenes.Add(scene.path);

                // select style
                var isCurrentScene = scene == SceneManager.GetActiveScene();
                var style = isCurrentScene ? Styles.ButtonSelected : Styles.Button;

                // draw button
                var order = drawnScenes.Count - 1;
                var rect = new Rect(0, order * _buttonHeight, area.width, _buttonHeight);
                if (UnityEngine.GUI.Button(rect, scene.name, style))
                    SceneManager.LoadScene(scene.name);

                // draw overlay
                if (order == _focus)
                    UnityEngine.GUI.Box(rect, "", Styles.ButtonFocusOverlay);
            }

            GUILayout.EndArea();
        }
    }
}