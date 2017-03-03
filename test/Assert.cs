namespace Test
{
    internal static class Assert
    {
        public static void Equals<T>(T value1, T value2)
            where T : System.IComparable
        {
            if (value1.CompareTo(value2) != 0)
                Log.E(value1 + " is not same to " + value2);
        }

        public static void True(bool value)
        {
            if (value) Log.E(value + " is not true");
        }

        public static void False(bool value)
        {
            if (value) Log.E(value + " is not false");
        }
    }
}
