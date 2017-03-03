namespace Test.Util
{
    public class StringCache : ITest
    {
        public StringCache()
            : base("StringCache")
        { }

        protected override void TestImpl()
        {
            var target = new Dashboard.Util.StringCache();
            var orgStr = "some string";
            var tmpStr1 = "some ";
            var tmpStr2 = "string";
            var otherStr = tmpStr1 + tmpStr2;

            {
                Assert.Equals(orgStr, otherStr);
                Assert.RefNotEquals(orgStr, otherStr);
            }

            {
                var cacheStr = target.Cache(orgStr);
                Assert.RefEquals(orgStr, cacheStr);
            }

            {
                var cacheStr = target.Cache(otherStr);
                Assert.RefEquals(orgStr, cacheStr);
            }

            {
                var oldCache = target.Cache(orgStr);
                Assert.RefEquals(orgStr, oldCache);
                target.Clear();
                var newCache = target.Cache(otherStr);
                Assert.Equals(oldCache, newCache);
                Assert.RefEquals(newCache, otherStr);
            }
        }
    }
}
