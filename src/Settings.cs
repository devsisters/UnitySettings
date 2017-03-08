using UnityEngine;
using BehaviourListeners = System.Collections.Generic.List<Settings.IBehaviourListener>;

namespace Settings
{
    public class Settings : MonoBehaviour
    {
        private bool _isInited { get { return _config != null; } }

        private Config _config;
        private string _viewToDraw;
        private GUI.Drawer _guiDrawer;
        // TODO
        // private bool _isShowingGUI;
        private bool _isShowingGUI = true;
        private IGesture _gesture;
        private readonly BehaviourListeners _behaviourListeners = new BehaviourListeners(8);

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (_isInited) return;
            _config = Config.LoadFromPrefs();
            _viewToDraw = _config.StartView;
            _guiDrawer = new GUI.Drawer();
            var gestureLeastLength = (Screen.width + Screen.height) / 4;
            _gesture = new CircleGesture(new TouchProvider(), gestureLeastLength);
        }

        private void OnEnable()
        {
            Init();
            foreach (var l in _behaviourListeners)
                l.OnEnable();
        }

        private void OnDestroy()
        {
            foreach (var l in _behaviourListeners)
                l.OnDisable();
            _config.SaveToPrefs();
        }

        private void Update()
        {
            Init();
            Util.Mouse.UpdatePos();
            if (_isShowingGUI)
                _guiDrawer.Update(_viewToDraw);
            foreach (var l in _behaviourListeners)
                l.Update();
            DetectGesture();
        }

        private void DetectGesture()
        {
            if (_isShowingGUI) return;
            _gesture.SampleOrCancel();
            if (_gesture.CheckAndClear())
                _isShowingGUI = true;
        }

        private void OnGUI()
        {
            Init();
            if (!_isShowingGUI) return;
            _guiDrawer.OnGUI(_viewToDraw);
        }

        public void AddBehaviourListener(IBehaviourListener behaviourListener)
        {
            Init();
            _behaviourListeners.Add(behaviourListener);
            if (enabled) behaviourListener.OnEnable();
        }

        public void AddGUIView(string key, GUI.IView view)
        {
            Init();
            _guiDrawer.Add(key, view);
        }
    }
}