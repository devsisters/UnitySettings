using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Settings.Extension.Scene
{
    internal partial class View : GUI.IView
    {
        public View()
            : base("Scene", Icons.Icon)
        {
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

                // draw
                if (GUILayout.Button(scene.name, style))
                    SceneManager.LoadScene(scene.name);
            }

            GUILayout.EndArea();
        }
    }
}