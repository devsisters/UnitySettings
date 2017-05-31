using UnityEngine;

namespace Settings.GUI
{
    public abstract class IView
    {
        public readonly string Title;
        public readonly GUIContent ToolbarIcon;

        public IView(string title, GUIContent toolbarIcon)
        {
            Title = title;
            ToolbarIcon = toolbarIcon;
        }

        public virtual void Update() { }
        public abstract void OnGUI(Rect area);
    }
}