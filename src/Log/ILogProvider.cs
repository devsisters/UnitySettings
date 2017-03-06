using LogCallback = UnityEngine.Application.LogCallback;

namespace Dashboard.Log
{
    public interface IProvider
    {
        void RegisterThreaded(LogCallback listener);
        void UnregisterThreaded(LogCallback listener);
    }

    internal class Provider : IProvider
    {
        public void RegisterThreaded(LogCallback listener)
        {
            UnityEngine.Application.logMessageReceivedThreaded += listener;
        }

        public void UnregisterThreaded(LogCallback listener)
        {
            UnityEngine.Application.logMessageReceivedThreaded -= listener;
        }
    }
}
