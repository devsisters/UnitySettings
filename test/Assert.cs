using System;

namespace Test
{
    internal static class Assert
    {
        public static void Equals<T>(T value1, T value2)
            where T : IComparable
        {
            if (value1.CompareTo(value2) != 0)
                L.E(value1 + " is not same to " + value2);
        }

        public static void RefEquals(Object value1, Object value2)
        {
            if (!Object.ReferenceEquals(value1, value2))
                L.E(value1 + " is not same to " + value2);
        }

        public static void RefNotEquals(Object value1, Object value2)
        {
            if (Object.ReferenceEquals(value1, value2))
                L.E(value1 + " is same to " + value2);
        }

        public static void True(bool value)
        {
            if (!value) L.E(value + " is not true");
        }

        public static void False(bool value)
        {
            if (value) L.E(value + " is not false");
        }
    }
}
