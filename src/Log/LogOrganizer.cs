using Logs = System.Collections.Generic.List<Settings.Log.Log>;
using ReadOnlyLogs = System.Collections.ObjectModel.ReadOnlyCollection<Settings.Log.Log>;
using CollapsedIndex = System.Collections.Generic.Dictionary<int, int>;
using CollapsedLogs = System.Collections.Generic.List<Settings.Log.LogAndCount>;
using ReadOnlyCollapsedLogs = System.Collections.ObjectModel.ReadOnlyCollection<Settings.Log.LogAndCount>;

namespace Settings.Log
{
    internal class Organizer
    {
        private readonly Stash _stash;
        private Mask _lastMask;
        private readonly Logs _lastLogs;
        private readonly ReadOnlyLogs _lastLogsReadOnly;
        private Mask _lastCollapsedMask;
        private readonly CollapsedIndex _lastCollapsedIndex;
        private readonly CollapsedLogs _lastCollapsedLogs;
        private readonly ReadOnlyCollapsedLogs _lastCollapsedLogsReadOnly;

        public Organizer(Stash stash)
        {
            const int reserve = 256;
            _stash = stash;
            _lastMask.SetAllTrue();
            _lastLogs = new Logs(reserve);
            _lastLogsReadOnly = _lastLogs.AsReadOnly();
            _lastCollapsedMask.SetAllTrue();
            _lastCollapsedIndex = new CollapsedIndex();
            _lastCollapsedLogs = new CollapsedLogs(reserve);
            _lastCollapsedLogsReadOnly = _lastCollapsedLogs.AsReadOnly();
        }

        public void Internal_AddToCache(Log log)
        {
            if (!_lastMask.IsAllTrue && _lastMask.Check(log.Type))
                _lastLogs.Add(log);
            AddToCache_Collapsed(log);
        }

        private void AddToCache_Collapsed(Log log)
        {
            if (!_lastCollapsedMask.Check(log.Type)) return;
            var rawLog = new RawLog(log.Type, log.Message, log.Stacktrace);
            var hash = rawLog.GetHashCode();
            int index;
            if (_lastCollapsedIndex.TryGetValue(hash, out index))
            {
                var logAndCount = _lastCollapsedLogs[index];
                logAndCount.Inc();
                _lastCollapsedLogs[index] = logAndCount;
            }
            else
            {
                _lastCollapsedIndex.Add(hash, _lastCollapsedLogs.Count);
                _lastCollapsedLogs.Add(new LogAndCount(rawLog));
            }
        }

        public ReadOnlyLogs Filter(Mask mask)
        {
            if (mask.IsAllTrue)
            {
                _lastMask = mask;
                _lastLogs.Clear();
                return _stash.All();
            }

            if (_lastMask == mask)
                return _lastLogsReadOnly;

            _lastMask = mask;
            _lastLogs.Clear();

            if (mask.IsAllFalse)
                return _lastLogsReadOnly;

            foreach (var log in _stash.All())
                if (mask.Check(log.Type))
                    _lastLogs.Add(log);
            return _lastLogsReadOnly;
        }

        public ReadOnlyCollapsedLogs FilterCollapsed(Mask mask)
        {
            if (_lastMask == mask)
                return _lastCollapsedLogsReadOnly;

            _lastMask = mask;
            _lastCollapsedIndex.Clear();
            _lastCollapsedLogs.Clear();

            if (_lastMask.IsAllFalse)
                return _lastCollapsedLogsReadOnly;

            foreach (var log in _stash.All())
                AddToCache_Collapsed(log);
            return _lastCollapsedLogsReadOnly;
        }

        public void Clear()
        {
            _lastLogs.Clear();
        }
    }
}