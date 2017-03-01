using UnityEngine;

namespace Dashboard.GUI
{
    public class Icons
    {
        public readonly GUIContent Clear;
        public readonly GUIContent Collapse;
        public readonly GUIContent ClearOnNewScene;
        public readonly GUIContent ShowTime;
        public readonly GUIContent ShowScene;
        public readonly GUIContent User;
        public readonly GUIContent ShowMemory;
        public readonly GUIContent Software;
        public readonly GUIContent Date;
        public readonly GUIContent ShowFps;
        public readonly GUIContent Info;
        public readonly GUIContent Search;
        public readonly GUIContent Close;

        public readonly GUIContent BuildFrom;
        public readonly GUIContent SystemInfo;
        public readonly GUIContent GraphicsInfo;
        public readonly GUIContent Back;

        public readonly GUIContent Log;
        public readonly GUIContent Warning;
        public readonly GUIContent Error;

        public static Icons Load()
        {
            return new Icons();
        }

        private static GUIContent I(byte[] tex, string tooltip)
        {
            return new GUIContent("", tex.ToTex(), tooltip);
        }

        private Icons()
        {
            Clear = I(PNGs.Clear, "Clear logs");
            Collapse = I(PNGs.Collapse, "Collapse logs");
            ClearOnNewScene = I(PNGs.Clear, "Clear logs on new scene loaded");
            ShowTime = I(PNGs.Time, "Show Hide Time");
            ShowScene = I(PNGs.Unity, "Show Hide Scene");
            Software = I(PNGs.Software, "Software");
            Info = I(PNGs.Info, "Information about application");
            Search = I(PNGs.Search, "Search for logs");
            Close = I(PNGs.Close, "Hide logs");
            User = I(PNGs.User, "User");

            BuildFrom = I(PNGs.BuildFrom, "Build From");
            SystemInfo = I(PNGs.SystemInfo, "System Info");
            GraphicsInfo = I(PNGs.GraphicsInfo, "Graphics Info");
            Back = I(PNGs.Back, "Back");

            Log = I(PNGs.Log, "show or hide logs");
            Warning = I(PNGs.Warning, "show or hide warnings");
            Error = I(PNGs.Error, "show or hide errors");
        }
    }
}