using System.Collections.Generic;
using UnityEngine;
using S = UnityEngine.SystemInfo;
using A = UnityEngine.Application;
using Helper = Settings.GUI.Helper;

namespace Settings.Extension.SystemInfo
{
    public class GUIView : GUI.IView
    {
        private enum RowType
        {
            Header, Header2, Row,
        }

        private struct RowDef
        {
            public readonly RowType Type;
            public readonly string Col1;
            public readonly string Col2;

            public RowDef(RowType type, string col1, string col2)
            {
                Type = type;
                Col1 = col1;
                Col2 = col2;
            }
        }

        private struct RowBuilder
        {
            private readonly List<RowDef> _rows;
            public RowBuilder(List<RowDef> rows) { _rows = rows; }
            public void Header(string title) { _rows.Add(new RowDef(RowType.Header, title, null)); }
            public void Header2(string title) { _rows.Add(new RowDef(RowType.Header2, title, null)); }
            public void Row(string title, object desc) { _rows.Add(new RowDef(RowType.Row, title, desc.ToString())); }
            public void Row(string title, object desc, object descDetail) { _rows.Add(new RowDef(RowType.Row, title, DescAndDetail(desc, descDetail))); }
            public static string DescAndDetail(object desc, object descDetail) { return desc + " (" + descDetail + ")"; }
        }

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

        private readonly List<RowDef> _rowsDevice = new List<RowDef>(32);
        private readonly List<RowDef> _rowsGraphicDevice = new List<RowDef>(32);
        private readonly List<RowDef> _rowsApplication = new List<RowDef>(32);

        public GUIView(GUI.Icons icons)
            : base("SystemInfo", icons.SystemInfo)
        {
            _icons = icons;
            _scroll = new GUI.ScrollView(Vector2.zero, true, true);

            {
                var b = new RowBuilder(_rowsDevice);
                b.Header("Device");
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
                var b = new RowBuilder(_rowsGraphicDevice);
                b.Header("Graphics Device");
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
                var b = new RowBuilder(_rowsApplication);
                b.Header("Application");
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
        }

        private void OnGUIRow(RowDef rowDef)
        {
            switch (rowDef.Type)
            {
                case RowType.Header:
                    GUILayout.Space(12);
                    GUILayout.Label(rowDef.Col1, Styles.HeaderFont);
                    GUILayout.Space(4);
                    break;
                case RowType.Header2:
                    GUILayout.TextArea(rowDef.Col1, Styles.Header2Font);
                    break;
                case RowType.Row:
                    GUILayout.BeginHorizontal();
                    GUILayout.TextArea(rowDef.Col1, Styles.Font, _minTitleWidth);
                    GUILayout.TextArea(rowDef.Col2, Styles.Font);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    break;
            }
        }

        public override void OnGUI(Rect area)
        {
            UnityEngine.GUI.Box(area, "", Styles.BG);
            _scroll.BeginLayout(area);

            _rowsDevice.ForEach(OnGUIRow);
            _rowsGraphicDevice.ForEach(OnGUIRow);
            _rowsApplication.ForEach(OnGUIRow);

            // b.Header("Others");
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

            _scroll.EndLayout();
        }
    }
}