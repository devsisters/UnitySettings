namespace Log
{
    internal class Log
    {
        public readonly UnityEngine.LogType Type;
        public readonly string Message;
        public readonly string Stacktrace;
        public readonly float Time;
        public readonly string Scene;

        public Log(
            UnityEngine.LogType type, string message, string stacktrace,
            float time, string scene)
        {
            Type = type;
            Message = message;
            Stacktrace = stacktrace;
            Time = time;
            Scene = scene;
        }
    }
}
