using List = System.Collections.Generic.List<Dashboard.Log.Log>;

namespace Dashboard.Log
{
    internal class Stash
    {
        private readonly List _logs = new List(128);
        private readonly Util.StringCache _strCache = new Util.StringCache();

        public void Clear()
        {
            _logs.Clear();
            _strCache.Clear();
        }

        public void Add(RawLog raw, Sample sample)
        {
            var msg = _strCache.Cache(raw.Message);
            var stacktrace = _strCache.Cache(raw.Stacktrace);
            var newLog = new Log(raw.Type, msg, stacktrace, sample);
            _logs.Add(newLog);
        }
    }
}

// if (newLogAdded)
// {
//     calculateStartIndex();
//     int totalCount = currentLog.Count;
//     int totalVisibleCount = (int)(Screen.height * 0.75f / size.y);
//     if (startIndex >= (totalCount - totalVisibleCount))
//         scrollPosition.y += size.y;
// }
// if (TotalMemUsage > maxSize)
// {
//     clear();
//     Debug.Log("Memory Usage Reach" + maxSize + " mb So It is Cleared");
//     return;
// }
// public List Extract(Mask mask)
// {
//     var ret = new List(_logs.Count / 4);
//     foreach (var log in _logs)
//         if (mask.Check(log.Type))
//             ret.Add(log);
//     return ret;
// }

// public List Danger_All()
// {
//     return _logs;
// }

// public List Extract(Mask mask, string search)
// {
//     search = search.ToLower();
//     var ret = new List(_logs.Count / 4);
//     foreach (var log in _logs)
//     {
//         if (!mask.Check(log.Type)) continue;
//         if (log.Message.ToLower().Contains(search))
//             ret.Add(log);
//     }
//     return ret;
// }
