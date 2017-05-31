using UnityEngine;

namespace Settings.GUI
{
    public abstract class IToolbarWidget
    {
        public abstract void OnGUI(Rect area);
    }
}