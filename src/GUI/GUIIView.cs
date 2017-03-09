namespace Settings.GUI
{
    public abstract class IView
    {
        public virtual void Update() { }
        public abstract void OnGUI(UnityEngine.Rect area);
    }
}