using UnityEngine;

namespace Dashboard.GUI
{
    internal class InfoDrawer
    {
        private Vector2 _scrollPos;
        private Icons _icons;
        private Styles _styles;

        public InfoDrawer(Icons icons, Styles styles)
        {
            _icons = icons;
            _styles = styles;
        }

        private struct RowDrawer
        {
            private readonly GUIStyle _style;
            private readonly GUILayoutOption _height;
            private readonly float _marginX;

            public RowDrawer(GUIStyle style, float height, float marginX)
            {
                _style = style;
                _height = GUILayout.Height(height);
                _marginX = marginX;
            }

            public void Draw(GUIContent icon, string label1, string label2 = null, string label3 = null)
            {
                Draw(icon, _style, _height, _marginX,
                    label1, label2, label3);
            }

            private static void Draw(
                GUIContent icon, GUIStyle style,
                GUILayoutOption height, float marginX,
                string label1, string label2, string label3)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(marginX);
                if (icon != null) GUILayout.Box(icon, style, GUILayout.Width(marginX), height);
                GUILayout.Space(marginX);
                if (label1 != null) GUILayout.Label(label1, style, height);
                GUILayout.Space(marginX);
                if (label2 != null) GUILayout.Label(label2, style, height);
                GUILayout.Space(marginX);
                if (label3 != null) GUILayout.Label(label3, style, height);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }

        public void Draw(Rect area, float rowHeight, float marginX)
        {
            var buildDate = ""; // TODO
            var deviceModel = SystemInfo.deviceModel;
            var deviceType = SystemInfo.deviceType.ToString();
            var deviceName = SystemInfo.deviceName;
            var graphicsDeviceName = SystemInfo.graphicsDeviceName;
            var graphicsMemorySize = SystemInfo.graphicsMemorySize.ToString();
            var maxTextureSize = SystemInfo.maxTextureSize.ToString();
            var systemMemorySize = SystemInfo.systemMemorySize.ToString();

            GUILayout.BeginArea(area, _styles.Back);

            Vector2 drag;
            if (Util.Mouse.Delta(out drag))
                _scrollPos += drag;
            // GUI.skin = toolbarScrollerSkin;
            _scrollPos = GUILayout.BeginScrollView(_scrollPos);
            GUILayout.Space(rowHeight);

            var i = _icons;
            var d = new RowDrawer(_styles.None, rowHeight, marginX);
            d.Draw(i.BuildFrom, buildDate);
            d.Draw(i.SystemInfo, deviceModel, deviceType, deviceName);
            d.Draw(i.GraphicsInfo, graphicsDeviceName, graphicsMemorySize, maxTextureSize);
            d.Draw(null, "Screen Width " + Screen.width, "Screen Height " + Screen.height);
            d.Draw(i.ShowMemory, systemMemorySize + " mb");
            var gcTotalMemory = (((float)System.GC.GetTotalMemory(false)) / 1024 / 1024);
            d.Draw(null, "GC Memory " + gcTotalMemory.ToString("0.000") + " mb");
            d.Draw(i.Software, SystemInfo.operatingSystem);
            // TODO
            // var logDate;
            // DrawInfoRow(i.dateContent, System.DateTime.Now.ToString(), " - Application Started At " + logDate);
            d.Draw(i.ShowTime, Time.realtimeSinceStartup.ToString("000"));
            // TODO
            // DrawInfoRow(i.showFpsContent, FormatFpsToDisplay());
            // TODO
            // DrawInfoRow(i.userContent, GetUserData());
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            d.Draw(i.ShowScene, currentScene);
            d.Draw(i.ShowScene, "Unity Version = " + Application.unityVersion);

            /*
            GUILayout.BeginHorizontal();
            GUILayout.Space(size.x);
            GUILayout.Label("Size = " + size.x.ToString("0.0"), nonStyle, GUILayout.Height(size.y));
            GUILayout.Space(size.x);
            float _size = GUILayout.HorizontalSlider(size.x, 16, 64, sliderBackStyle, sliderThumbStyle, GUILayout.Width(Screen.width * 0.5f));
            if (size.x != _size)
            {
                size.x = size.y = _size;
                initializeStyle();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            */

            /*
            GUILayout.BeginHorizontal();
            GUILayout.Space(size.x);
            if (GUILayout.Button(backContent, barStyle, GUILayout.Width(size.x * 2), GUILayout.Height(size.y * 2)))
            {
                currentView = ReportView.Logs;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            */

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}

