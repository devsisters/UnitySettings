using S = UnityEngine.SystemInfo;
using A = UnityEngine.Application;

namespace Settings.Extension.SystemInfo
{
    public partial class View
    {
        private void AddAllPages()
        {
            {
                var b = AddPage("Device");
                b.Header(S.deviceUniqueIdentifier);
                b.Row("Model", S.deviceModel, S.deviceType);
                b.Row("Name", S.deviceName);
                b.Row("OS", S.operatingSystemFamily, S.operatingSystem);
                b.Row("Memory Size", S.systemMemorySize);
                b.Row("Processor Type", S.processorType);
                b.Row("Processor Count", S.processorCount);
                b.Row("Processor Hz", S.processorFrequency);
            }

            {
                var b = AddPage("Graphics");
                b.Header(RowBuilder.DescAndDetail(S.graphicsDeviceName, S.graphicsDeviceID));
                b.Row("Type", S.graphicsDeviceType);
                b.Row("Vendor", S.graphicsDeviceVendor, S.graphicsDeviceVendorID);
                b.Row("Version", S.graphicsDeviceVersion);
                b.Row("Support Multithread", S.graphicsMultiThreaded);
                b.Row("Memory Size", S.graphicsMemorySize);
                b.Row("Max Texture Size", S.maxTextureSize);
                b.Row("Shader Level", S.graphicsShaderLevel);
                b.Row("NPOT Support", S.npotSupport);
            }

            {
                var b = AddPage("Application");
                b.Header(RowBuilder.DescAndDetail(A.productName, A.identifier));
                b.Row("Ver", A.version + " / " + A.unityVersion);
                b.Row("Company Name", A.companyName);
                b.Row("Platform", A.platform);
                b.Row("System Lang", A.systemLanguage);

                b.Row("Data Path", A.dataPath);
                b.Row("Persistent Data Path", A.persistentDataPath);
                b.Row("Streaming Assets Path", A.streamingAssetsPath);
                b.Row("Temp Cache Path", A.temporaryCachePath);

                b.Row("Installer Name", A.installerName);
                b.Row("Install Mode", A.installMode);
                b.Row("Run In BG", A.runInBackground);
                b.Row("Sandbox Type", A.sandboxType);
                b.Row("BG Loading Priority", A.backgroundLoadingPriority);
                b.Row("Cloud Project Id", A.cloudProjectId);

                b.Row("Target Frame Rate", () => A.targetFrameRate);
                b.Row("Internet Reachability", () => A.internetReachability);
                b.Row("Genuine", () => A.genuine);
                b.Row("Genuine Check Available", () => A.genuineCheckAvailable);
                b.Row("Streamed Bytes", () => A.streamedBytes);
            }

            {
                var b = AddPage("G/Supported");
                b.Row("Render Target", S.supportedRenderTargetCount);
                b.Row("Copy Texture", S.copyTextureSupport);
                b.Row("Shadow", S.supportsShadows);
                b.Row("Raw Shadow Depth Sampling", S.supportsRawShadowDepthSampling);
                b.Row("Compute Shader", S.supportsComputeShaders);
                b.Row("2D Array Tex", S.supports2DArrayTextures);
                b.Row("3D Tex", S.supports3DTextures);
                b.Row("Sparse Tex", S.supportsSparseTextures);
                b.Row("Image Effects", S.supportsImageEffects);
                b.Row("Motion Vectors", S.supportsMotionVectors);
                b.Row("Cubemap Array Tex", S.supportsCubemapArrayTextures);
                b.Row("Render To Cubemap", S.supportsRenderToCubemap);
                b.Row("Reversed Z Buffer", S.usesReversedZBuffer);
            }

            // skip
            // web player
            // b.Row("", A.srcValue);
            // b.Row("", A.absoluteURL);
            // b.Row("", A.isWebPlayer);
            // etc
            // b.Row("", A.isMobilePlatform);
            // b.Row("", A.isConsolePlatform);
        }
    }
}
