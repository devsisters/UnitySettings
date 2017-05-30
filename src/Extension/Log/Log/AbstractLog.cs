using System.Collections.ObjectModel;
using LogType = UnityEngine.LogType;

namespace Settings.Extension.Log
{
    internal struct AbstractLog
    {
        public LogType Type;
        public string Message;
        public string Stacktrace;
        public Sample? Sample;
        public int? Count;
    }

    internal class AbstractLogs
    {
        private readonly ReadOnlyCollection<Log> _logs;
        private readonly ReadOnlyCollection<LogAndCount> _collapsedLogs;

        public int Count
        {
            get
            {
                if (_logs != null) return _logs.Count;
                else return _collapsedLogs.Count;
            }
        }

        public AbstractLog this[int index]
        {
            get
            {
                if (_logs != null)
                {
                    var log = _logs[index];
                    return new AbstractLog
                    {
                        Type = log.Type,
                        Message = log.Message,
                        Stacktrace = log.Stacktrace,
                        Sample = log.Sample,
                    };
                }
                else
                {
                    var clog = _collapsedLogs[index];
                    return new AbstractLog
                    {
                        Type = clog.Log.Type,
                        Message = clog.Log.Message,
                        Stacktrace = clog.Log.Stacktrace,
                        Count = clog.Count,
                    };
                }
            }
        }

        public AbstractLogs(ReadOnlyCollection<Log> logs) { _logs = logs; }
        public AbstractLogs(ReadOnlyCollection<LogAndCount> logs) { _collapsedLogs = logs; }
    }
}
