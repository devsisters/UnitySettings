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
            Log.I("[" + _name + "] starts...");
            TestImpl();
            Log.I("[" + _name + "] done");
        }

        protected abstract void TestImpl();
    }
}