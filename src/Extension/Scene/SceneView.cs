using System;
using System.Collections.Generic;
using Settings.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Settings.Extension.Scene
{
    internal partial class View : GUI.ListView
    {
        private class ListViewDelegate : GUI.IListViewDelegate
        {
            public int Count => SceneManager.sceneCountInBuildSettings;

            public ListViewItem GetItem(int index)
            {
                var scene = SceneManager.GetSceneByBuildIndex(index);
                var isCurrentScene = scene == SceneManager.GetActiveScene();
                var path = SceneUtility.GetScenePathByBuildIndex(index);
                var name = System.IO.Path.GetFileNameWithoutExtension(path);
                return new ListViewItem(name, isCurrentScene);
            }

            public void OnSelect(int index)
                => SceneManager.LoadScene(index);
        }

        public View()
            : base("Scene", Icons.Icon, 80, new ListViewDelegate())
        {
        }
    }
}