using Cache = System.Collections.Generic.Dictionary<int, string>;

namespace Dashboard.Util
{
    internal class StringCache
    {
        private readonly Cache _cache = new Cache(128);

        // public int Count { get { return _cache.Count; } }

        public string Cache(string queryStr)
        {
            if (queryStr == null)
            {
                // something went wrong.
                return string.Empty;
            }

            if (queryStr.Length == 0)
                return string.Empty;

            var hash = queryStr.GetHashCode();
            string cachedStr;
            if (_cache.TryGetValue(hash, out cachedStr))
                return cachedStr;

            _cache.Add(hash, queryStr);
            return queryStr;
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
