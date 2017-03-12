namespace Test
{
    public static class MainTest
    {
        public static void Main(string[] args)
        {
            new Util.CircleGesture().Test();
            new Util.StringCache().Test();
            new Extension.Log.MaskTest().Test();
            new Extension.Log.StashTest().Test();
        }
    }
}
