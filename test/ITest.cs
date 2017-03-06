namespace Test
{
    public abstract class ITest
    {
        private readonly string _name;

        public ITest(string name)
        {
            _name = name;
        }

        public void Test()
        {
            L.I("[" + _name + "] starts...");
            TestImpl();
            L.I("[" + _name + "] done");
        }

        protected abstract void TestImpl();
    }
}