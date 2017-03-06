namespace Test.Log
{
    public class Mask : ITest
    {
        public Mask()
            : base("LogMask")
        { }

        protected override void TestImpl()
        {
            var target = new Dashboard.Log.Mask();
            // TODO
        }
    }
}