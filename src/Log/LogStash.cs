using Logs = System.Collections.Generic.List<Settings.Log.Log>;
using ReadOnlyLogs = System.Collections.ObjectModel.ReadOnlyCollection<Settings.Log.Log>;

namespace Settings.Log
{
    internal class Stash
    {
        private readonly Logs _logs;
        private readonly ReadOnlyLogs _logsReadOnly;
        private readonly Util.StringCache _strCache;

        public readonly Organizer Organizer;

        public Stash()
        {
            _logs = new Logs(256);
            _logsReadOnly = _logs.AsReadOnly();
            _strCache = new Util.StringCache();
            Organizer = new Organizer(this);
        }

        public void Clear()
        {
            _logs.Clear();
            _strCache.Clear();
            Organizer.Clear();
        }

        public void Add(RawLog raw, Sample sample)
        {
            var msg = _strCache.Cache(raw.Message);
            var stacktrace = _strCache.Cache(raw.Stacktrace);
            var newLog = new Log(raw.Type, msg, stacktrace, sample);
            _logs.Add(newLog);
            Organizer.Internal_AddToCache(newLog);
        }

        public ReadOnlyLogs All()
        {
            return _logsReadOnly;
        }
    }
}