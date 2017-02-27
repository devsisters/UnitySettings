using UnityEngine;
using UnityEngine.SceneManagement;

public enum ReportView
{
    Logs,
    Info,
}

public class Reporter : MonoBehaviour
{
    private Config _config;
    private Log.Stash _logStash;
    private Log.Broker _logBroker;
    private GUI.Drawer _guiDrawer;
    private Util.CircleGestureDetector _gestureDetector;
    private bool _isShowingGUI;

    private void Awake()
    {
        _config = Config.LoadFromPrefs();
        _logStash = new Log.Stash();
        _logBroker = new Log.Broker(_logStash);
        var icons = GUI.Icons.Load();
        var styles = new GUI.Styles();
        _guiDrawer = new GUI.Drawer(icons, styles, _logStash);
    }

    private void OnDestroy()
    {
        _config.SaveToPrefs();
    }

    private void OnEnable()
    {
        _logBroker.Connect();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        _logBroker.Disconnect();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        _logBroker.Update();
        if (_isShowingGUI)
        {
            // TODO: close
            _guiDrawer.OnGUI(ReportView.Logs);
        }
        else
        {
            _gestureDetector.SampleOrCancel();
            if (_gestureDetector.CheckAndClear())
                _isShowingGUI = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (_config.ClearOnSceneLoad)
            Clear();
    }

    private void Clear()
    {
        _logStash.Clear();
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
