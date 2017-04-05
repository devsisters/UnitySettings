using UnityEngine;
using BehaviourListeners = System.Collections.Generic.List<Settings.IBehaviourListener>;

namespace Settings
{
    public class Settings : MonoBehaviour
    {
        // private bool _isInited { get { return _config != null; } }
        private bool _isInited { get { return _guiDrawer != null; } }

        // private Config _config;
        private GUI.Drawer _guiDrawer;
        private bool _isShowingGUI;
        private IGesture _gesture;
        private readonly BehaviourListeners _behaviourListeners = new BehaviourListeners(8);

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (_isInited) return;
            // _config = Config.LoadFromPrefs();
            _guiDrawer = new GUI.Drawer();
            _guiDrawer.OnClose += () => _isShowingGUI = false;
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
            // _config.SaveToPrefs();
        }

        private void Update()
        {
            Init();
            if (Input.GetKeyDown(KeyCode.Escape))
                _isShowingGUI = false;
            if (_isShowingGUI)
            {
                Util.Mouse.RefreshPos();
                _guiDrawer.Update();
            }
            foreach (var l in _behaviourListeners)
                l.Update(_isShowingGUI);
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
            var orgDepth = UnityEngine.GUI.depth;
            UnityEngine.GUI.depth = -1000;
            _guiDrawer.OnGUI();
            UnityEngine.GUI.depth = orgDepth;
        }

        public void AddBehaviourListener(IBehaviourListener behaviourListener)
        {
            Init();
            _behaviourListeners.Add(behaviourListener);
            if (enabled) behaviourListener.OnEnable();
        }

        public void AddView(GUI.IView view)
        {
            Init();
            _guiDrawer.Add(view);
        }
    }
}
