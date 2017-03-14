using System.Collections.Generic;
using UnityEngine;
using S = UnityEngine.SystemInfo;
using A = UnityEngine.Application;

namespace Settings.Extension.SystemInfo
{
    public partial class GUIView : GUI.IView
    {
        private static GUILayoutOption _minTitleWidth = GUILayout.MinWidth(240);

        private readonly GUI.Icons _icons;
        private readonly GUI.ScrollView _scroll;

        private string _curPage;
        private readonly Dictionary<string, List<RowDef>> _pages;

        public GUIView(GUI.Icons icons)
            : base("SystemInfo", icons.SystemInfo)
        {
            _icons = icons;
            _scroll = new GUI.ScrollView(Vector2.zero, true, true);
            _pages = new Dictionary<string, List<RowDef>>(8);

            {
                var b = AddPage("Device");
                b.Header2(S.deviceUniqueIdentifier);
                b.Row("Model", S.deviceModel, S.deviceType);
                b.Row("Name", S.deviceName);
                b.Row("OS", S.operatingSystemFamily, S.operatingSystem);
                b.Row("Memory Size", S.systemMemorySize);
                b.Row("Processor Type", S.processorType);
                b.Row("Processor Count", S.processorCount);
                b.Row("Processor Hz", S.processorFrequency);
            }

            {
                var b = AddPage("Graphics Device");
                b.Header2(RowBuilder.DescAndDetail(S.graphicsDeviceName, S.graphicsDeviceID));
                b.Row("Type", S.graphicsDeviceType);
                b.Row("Vendor", S.graphicsDeviceVendor, S.graphicsDeviceVendorID);
                b.Row("Version", S.graphicsDeviceVersion);
                b.Row("Support Multithread", S.graphicsMultiThreaded);
                b.Row("Memory Size", S.graphicsMemorySize);
                b.Row("Shader Level", S.graphicsShaderLevel);
                b.Row("NPOT Support", S.npotSupport);
                b.Row("Max Texture Size", S.maxTextureSize);
            }

            {
                var b = AddPage("Application");
                b.Row("", A.temporaryCachePath);
                b.Row("", A.srcValue);
                b.Row("", A.absoluteURL);
                b.Row("", A.unityVersion);
                b.Row("", A.version);
                b.Row("", A.installerName);
                b.Row("", A.bundleIdentifier);
                b.Row("", A.installMode);
                b.Row("", A.persistentDataPath);
                b.Row("", A.isWebPlayer);
                b.Row("", A.platform);
                b.Row("", A.isMobilePlatform);
                b.Row("", A.isConsolePlatform);
                b.Row("", A.runInBackground);
                b.Row("", A.dataPath);
                b.Row("", A.streamingAssetsPath);
                b.Row("", A.sandboxType);
                // b.Row("", A.internetReachability);
                // b.Row("", A.genuine);
                // b.Row("", A.genuineCheckAvailable);
                b.Row("", A.backgroundLoadingPriority);
                b.Row("", A.productName);
                b.Row("", A.companyName);
                b.Row("", A.cloudProjectId);
                // b.Row("", A.targetFrameRate);
                b.Row("", A.systemLanguage);
                b.Row("", A.isEditor);
                b.Row("", A.streamedBytes);
                b.Row("", A.isPlaying);
            }

            {
                // b.Header("Graphics Others");
                // b.Row("", S.copyTextureSupport);
                // b.Row("", S.supportedRenderTargetCount);
                // b.Row("", S.supports2DArrayTextures);
                // b.Row("", S.supports3DTextures);
                // b.Row("", S.supportsComputeShaders);
                // b.Row("", S.supportsCubemapArrayTextures);
                // b.Row("", S.supportsImageEffects);
                // b.Row("", S.supportsMotionVectors);
                // b.Row("", S.supportsRawShadowDepthSampling);
                // b.Row("", S.supportsRenderToCubemap);
                // b.Row("", S.supportsShadows);
                // b.Row("", S.supportsSparseTextures);
                // b.Row("", S.usesReversedZBuffer);
            }
        }

        public override void OnGUI(Rect area)
        {
            UnityEngine.GUI.Box(area, "", Styles.BG);
            _scroll.BeginLayout(area);

            List<RowDef> page;
            if (_pages.TryGetValue(_curPage, out page))
                page.ForEach(OnGUIRow);

            _scroll.EndLayout();
        }

        private RowBuilder AddPage(string key)
        {
            var rows = new List<RowDef>(32);
            if (_curPage == null) _curPage = key;
            _pages.Add(key, rows);
            var b = new RowBuilder(rows);
            b.Header("Device");
            return b;
        }
    }
}