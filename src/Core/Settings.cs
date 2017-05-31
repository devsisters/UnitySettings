using UnityEngine;
using BehaviourListeners = System.Collections.Generic.List<Settings.IBehaviourListener>;

namespace Settings
{
    [AddComponentMenu("Settings/Settings.Settings")]
    public class Settings : MonoBehaviour
    {
        public bool EnableGesture;
        public KeyCode KeyboardShortcutForShow;

        private bool _isInited { get { return _guiDrawer != null; } }
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
        }

        private void Update()
        {
            Init();

            UpdateKeyboard();

            if (_isShowingGUI)
            {
                Util.Mouse.Refresh();
                _guiDrawer.Update();
            }

            foreach (var l in _behaviourListeners)
                l.Update(_isShowingGUI);

            DetectGesture();
        }

        private void UpdateKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _isShowingGUI = false;
            if (Input.GetKeyDown(KeyboardShortcutForShow))
                _isShowingGUI = !_isShowingGUI;
        }

        private void DetectGesture()
        {
            if (!EnableGesture) return;
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
