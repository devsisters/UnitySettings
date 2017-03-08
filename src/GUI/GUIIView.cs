namespace Settings.GUI
{
    public abstract class IView
    {
        public virtual void Update() { }
        public abstract void Draw(UnityEngine.Rect area);
    }
}