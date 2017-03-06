namespace Test
{
    public static class MainTest
    {
        public static void Main(string[] args)
        {
            new Log.Mask().Test();
            new Log.Stash().Test();
            new Util.CircleGesture().Test();
            new Util.StringCache().Test();
        }
    }
}
