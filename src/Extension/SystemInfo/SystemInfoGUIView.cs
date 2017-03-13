using UnityEngine;
using S = UnityEngine.SystemInfo;
using A = UnityEngine.Application;
using Helper = Settings.GUI.Helper;

namespace Settings.Extension.SystemInfo
{
    public class GUIView : GUI.IView
    {
        private static class Styles
        {
            public static GUIStyle BG;
            public static GUIStyle HeaderFont;
            public static GUIStyle Header2Font;
            public static GUIStyle Font;

            static Styles()
            {
                BG = new GUIStyle();
                BG.normal.background = Helper.Solid(0xffffffa0);

                HeaderFont = new GUIStyle();
                HeaderFont.fontSize = 36;
                HeaderFont.padding = new RectOffset(5, 5, 5, 5);
                HeaderFont.normal.textColor = Color.black;
                HeaderFont.fontStyle = FontStyle.Bold;

                Header2Font = new GUIStyle(HeaderFont);
                Header2Font.fontSize = 32;
                Header2Font.fontStyle = FontStyle.Italic;

                Font = new GUIStyle();
                Font.fontSize = 32;
                Font.padding = new RectOffset(5, 5, 5, 5);
                Font.normal.textColor = Color.black;
            }
        }

        private static GUILayoutOption _minTitleWidth = GUILayout.MinWidth(240);

        private readonly GUI.Icons _icons;
        private readonly GUI.ScrollView _scroll;

        public GUIView(GUI.Icons icons)
            : base("SystemInfo", icons.SystemInfo)
        {
            _icons = icons;
            _scroll = new GUI.ScrollView(Vector2.zero, true, true);
        }

        private void OnGUIHeader(string title)
        {
            GUILayout.Space(12);
            GUILayout.Label(title, Styles.HeaderFont);
            GUILayout.Space(4);
        }

        private void OnGUIHeader2(string content)
        {
            GUILayout.TextArea(content.ToString(), Styles.Header2Font);
        }

        private void OnGUIRow(string title, object content)
        {
            GUILayout.BeginHorizontal();
            GUILayout.TextArea(title, Styles.Font, _minTitleWidth);
            GUILayout.TextArea(content.ToString(), Styles.Font);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private static string Content(object content, object detail)
        {
            return content + " (" + detail + ")";
        }

        public override void OnGUI(Rect area)
        {
            UnityEngine.GUI.Box(area, "", Styles.BG);
            _scroll.BeginLayout(area);

            OnGUIHeader("Device");
            OnGUIHeader2(S.deviceUniqueIdentifier);
            OnGUIRow("Model", Content(S.deviceModel, S.deviceType));
            OnGUIRow("Name", S.deviceName);
            OnGUIRow("OS", Content(S.operatingSystemFamily, S.operatingSystem));
            OnGUIRow("Memory Size", S.systemMemorySize);
            OnGUIRow("Processor Type", S.processorType);
            OnGUIRow("Processor Count", S.processorCount);
            OnGUIRow("Processor Hz", S.processorFrequency);

            OnGUIHeader("Graphics Device");
            OnGUIHeader2(Content(S.graphicsDeviceName, S.graphicsDeviceID));
            OnGUIRow("Type", S.graphicsDeviceType);
            OnGUIRow("Vendor", Content(S.graphicsDeviceVendor, S.graphicsDeviceVendorID));
            OnGUIRow("Version", S.graphicsDeviceVersion);
            OnGUIRow("Support Multithread", S.graphicsMultiThreaded);
            OnGUIRow("Memory Size", S.graphicsMemorySize);
            OnGUIRow("Shader Level", S.graphicsShaderLevel);
            OnGUIRow("NPOT Support", S.npotSupport);
            OnGUIRow("Max Texture Size", S.maxTextureSize);

            // OnGUIHeader("Others");
            // OnGUIRow("", S.copyTextureSupport);
            // OnGUIRow("", S.supportedRenderTargetCount);
            // OnGUIRow("", S.supports2DArrayTextures);
            // OnGUIRow("", S.supports3DTextures);
            // OnGUIRow("", S.supportsComputeShaders);
            // OnGUIRow("", S.supportsCubemapArrayTextures);
            // OnGUIRow("", S.supportsImageEffects);
            // OnGUIRow("", S.supportsMotionVectors);
            // OnGUIRow("", S.supportsRawShadowDepthSampling);
            // OnGUIRow("", S.supportsRenderToCubemap);
            // OnGUIRow("", S.supportsShadows);
            // OnGUIRow("", S.supportsSparseTextures);
            // OnGUIRow("", S.usesReversedZBuffer);

            /*
            OnGUIHeader("Application");
            OnGUIRow("", A.temporaryCachePath);
            OnGUIRow("", A.srcValue);
            OnGUIRow("", A.absoluteURL);
            OnGUIRow("", A.unityVersion);
            OnGUIRow("", A.version);
            OnGUIRow("", A.installerName);
            OnGUIRow("", A.bundleIdentifier);
            OnGUIRow("", A.installMode);
            OnGUIRow("", A.persistentDataPath);
            OnGUIRow("", A.isWebPlayer);
            OnGUIRow("", A.platform);
            OnGUIRow("", A.isMobilePlatform);
            OnGUIRow("", A.isConsolePlatform);
            OnGUIRow("", A.runInBackground);
            OnGUIRow("", A.dataPath);
            OnGUIRow("", A.streamingAssetsPath);
            OnGUIRow("", A.sandboxType);
            // OnGUIRow("", A.internetReachability);
            // OnGUIRow("", A.genuine);
            // OnGUIRow("", A.genuineCheckAvailable);
            OnGUIRow("", A.backgroundLoadingPriority);
            OnGUIRow("", A.productName);
            OnGUIRow("", A.companyName);
            OnGUIRow("", A.cloudProjectId);
            // OnGUIRow("", A.targetFrameRate);
            OnGUIRow("", A.systemLanguage);
            OnGUIRow("", A.isEditor);
            OnGUIRow("", A.streamedBytes);
            OnGUIRow("", A.isPlaying);
            */

            _scroll.EndLayout();
        }
    }
}