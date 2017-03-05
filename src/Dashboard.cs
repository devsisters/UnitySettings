using UnityEngine;
using BehaviourListeners = System.Collections.Generic.List<Dashboard.IBehaviourListener>;

namespace Dashboard
{
    public class Dashboard : MonoBehaviour
    {
        private Config _config;
        private string _viewToDraw;
        private IGesture _gesture;
        private GUI.Drawer _guiDrawer;
        private bool _isShowingGUI;
        private readonly BehaviourListeners _behaviourListeners = new BehaviourListeners(8);

        private void Awake()
        {
            _config = Config.LoadFromPrefs();
            _viewToDraw = _config.StartView;
            var gestureLeastLength = (Screen.width + Screen.height) / 4;
            _gesture = new CircleGesture(new TouchProvider(), gestureLeastLength);
            _guiDrawer = new GUI.Drawer();
            InjectDependency();
            // var icons = GUI.Icons.Load();
            // var styles = new GUI.Styles();
        }

        private void InjectDependency()
        {
            _behaviourListeners.Add(new Log.Watch(new Log.Provider()));
        }

        private void OnEnable()
        {
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
            foreach (var l in _behaviourListeners)
                l.Update();
            DetectGestureOrUpdateGUI();
        }

        private void DetectGestureOrUpdateGUI()
        {
            if (_isShowingGUI)
            {
                _guiDrawer.OnGUI(_viewToDraw);
            }
            else
            {
                _gesture.SampleOrCancel();
                if (_gesture.CheckAndClear())
                    _isShowingGUI = true;
            }
        }
    }
}

// Rect toolBarRect;
// Rect stackRect;
// Rect graphRect;
// Rect graphMinRect;
// Rect graphMaxRect;
// Rect buttomRect;
// Vector2 stackRectTopLeft;

// Rect countRect;
// Rect timeRect;
// Rect timeLabelRect;
// Rect sceneRect;
// Rect memoryRect;
// Rect memoryLabelRect;
// Rect fpsRect;
// Rect fpsLabelRect;
//calculate the start index of visible log
// void calculateStartIndex()
// {
//     startIndex = (int)(scrollPosition.y / size.y);
//     startIndex = Mathf.Clamp(startIndex, 0, currentLog.Count);
// }

// Vector2 scrollPosition;
// Vector2 scrollPosition2;
// Vector2 toolbarScrollPosition;

// Log selectedLog;

// int startIndex;


// Vector2 infoScrollPosition;
// Vector2 oldInfoDrag;

// float graphSize = 4f;
// int startFrame = 0;
// int currentFrame = 0;
// Vector3 tempVector1;
// Vector3 tempVector2;
// Vector2 graphScrollerPos;
// float maxFpsValue;
// float minFpsValue;
// float maxMemoryValue;
// float minMemoryValue;

// //calculate  pos of first click on screen
// Vector2 startPos;
// //calculate drag amount , this is used for scrolling

// Vector2 mousePosition;

// //total number of logs
// int numOfLogs = 0;
// //total number of warnings
// int numOfLogsWarning = 0;
// //total number of errors
// int numOfLogsError = 0;
// //total number of collapsed logs
// int numOfCollapsedLogs = 0;
// //total number of collapsed warnings
// int numOfCollapsedLogsWarning = 0;
// //total number of collapsed errors
// int numOfCollapsedLogsError = 0;

// float logsMemUsage;
// float graphMemUsage;
// string buildDate;
// public float TotalMemUsage { get { return logsMemUsage + graphMemUsage; } }
// public Vector2 size = new Vector2(32, 32);
// public float maxSize = 20;
// string filterText = "";
