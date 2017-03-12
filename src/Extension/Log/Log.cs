using LogType = UnityEngine.LogType;

namespace Settings.Extension.Log
{
    internal struct RawLog
    {
        public readonly LogType Type;
        public readonly string Message;
        public readonly string Stacktrace;
        private int _cachedHash;

        public RawLog(LogType type, string message, string stacktrace)
        {
            Type = type;
            Message = message;
            Stacktrace = stacktrace;
            _cachedHash = 0;
        }

        public override int GetHashCode()
        {
            if (_cachedHash != 0)
                return _cachedHash;

            unchecked
            {
                _cachedHash = (int)Type * 311
                    + Message.GetHashCode() * 659
                    + Stacktrace.GetHashCode() * 823;
            }

            return _cachedHash;
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

    internal struct LogAndCount
    {
        public readonly RawLog Log;
        public int Count { get; private set; }

        public LogAndCount(RawLog log)
        {
            Log = log;
            Count = 1;
        }

        public void Inc() { ++Count; }
    }
}
