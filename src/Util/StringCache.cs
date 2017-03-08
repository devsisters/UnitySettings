using Cache = System.Collections.Generic.Dictionary<int, string>;

namespace Settings.Util
{
    internal class StringCache
    {
        private readonly Cache _cache = new Cache(128);

        public string Cache(string queryStr)
        {
            if (queryStr == null)
            {
                L.SomethingWentWrong();
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
