using LogType = UnityEngine.LogType;

namespace Dashboard.Log
{
    internal struct Sample
    {
        public readonly float Time;
        public readonly string Scene;

        public Sample(float time, string scene)
        {
            Time = time;
            Scene = scene;
        }
    }

    internal struct RawLog
    {
        public readonly LogType Type;
        public readonly string Message;
        public readonly string Stacktrace;

        public RawLog(LogType type, string message, string stacktrace)
        {
            Type = type;
            Message = message;
            Stacktrace = stacktrace;
        }
    }

    internal struct Log
    {
        public readonly LogType Type;
        public readonly string Message;
        public readonly string Stacktrace;
        public readonly Sample Sample;

        public Log(LogType type, string message, string stacktrace, Sample sample)
        {
            Type = type;
            Message = message;
            Stacktrace = stacktrace;
            Sample = sample;
        }
    }
}
