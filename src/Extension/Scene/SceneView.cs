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
                if (!_focus.HasValue) _focus = -1;
                else --_focus;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!_focus.HasValue) _focus = 0;
                else ++_focus;
            }

            if (!_focus.HasValue) return;

            // cull focus
            var max = SceneManager.sceneCountInBuildSettings;
            _focus = (_focus.Value + max) % max;

            // on hit space
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(_focus.Value);
                _focus = null;
            }
        }

        public override void OnGUI(Rect area)
        {
            GUILayout.BeginArea(area, GUI.Styles.BG);

            var sceneCount = SceneManager.sceneCountInBuildSettings;
            for (var i = 0; i != sceneCount; ++i)
            {
                var scene = SceneManager.GetSceneByBuildIndex(i);

                // select style
                var isCurrentScene = scene == SceneManager.GetActiveScene();
                var style = isCurrentScene ? Styles.ButtonSelected : Styles.Button;

                // draw button
                var rect = new Rect(0, i * _buttonHeight, area.width, _buttonHeight);
                var path = SceneUtility.GetScenePathByBuildIndex(i);
                var name = System.IO.Path.GetFileNameWithoutExtension(path);
                if (UnityEngine.GUI.Button(rect, name, style))
                    SceneManager.LoadScene(i);

                // draw overlay
                if (i == _focus)
                    UnityEngine.GUI.Box(rect, "", Styles.ButtonFocusOverlay);
            }

            GUILayout.EndArea();
        }
    }
}