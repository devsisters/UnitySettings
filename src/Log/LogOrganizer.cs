using Logs = System.Collections.Generic.List<Settings.Log.Log>;
using ReadOnlyLogs = System.Collections.ObjectModel.ReadOnlyCollection<Settings.Log.Log>;

namespace Settings.Log
{
    internal class Organizer
    {
        private readonly Stash _stash;
        private readonly ReadOnlyLogs _empty;
        private Mask _lastMask;
        private readonly Logs _lastLogs;
        private readonly ReadOnlyLogs _lastLogsReadOnly;

        public Organizer(Stash stash)
        {
            _stash = stash;
            _empty = new ReadOnlyLogs(new Logs());
            _lastMask.SetAllTrue();
            _lastLogs = new Logs(256);
            _lastLogsReadOnly = _lastLogs.AsReadOnly();
        }

        public void Internal_AddToCache(Log log)
        {
            if (_lastMask.IsAllTrue
                || _lastMask.IsAllFalse)
                return;
            if (_lastMask.Check(log.Type))
                _lastLogs.Add(log);
        }

        public ReadOnlyLogs Filter(Mask mask)
        {
            var oldMask = _lastMask;
            _lastMask = mask;

            if (mask.IsAllTrue)
            {
                _lastLogs.Clear();
                return _stash.All();
            }

            if (mask.IsAllFalse)
            {
                _lastLogs.Clear();
                return _empty;
            }

            if (oldMask == mask)
                return _lastLogsReadOnly;

            _lastLogs.Clear();
            foreach (var log in _stash.All())
                if (mask.Check(log.Type))
                    _lastLogs.Add(log);
            return _lastLogsReadOnly;
        }

        public void Clear()
        {
            _lastLogs.Clear();
        }
    }
}