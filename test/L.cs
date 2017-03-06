namespace Test
{
    internal static class L
    {
        public enum Color
        {
            None, Reset, Red,
        }

        private static string AnsiForFg(Color c)
        {
            switch (c)
            {
                case Color.None: return "";
                case Color.Reset: return "\x1b[0m";
                case Color.Red: return "\x1b[31m";
                default: return "";
            }
        }

        private static void WriteToConsole(string message, bool newLine)
        {
            if (newLine) System.Console.WriteLine(message);
            else System.Console.Write(message);
        }

        public static void Print(string message, bool newLine = true, Color fgColor = Color.None)
        {
            WriteToConsole(AnsiForFg(fgColor), false);
            WriteToConsole(message, newLine);
            WriteToConsole(AnsiForFg(Color.Reset), false);
        }

        private static void PrintWithTag(string tag, string message, Color fgColor = Color.None)
        {
            Print(tag + ") " + message, fgColor: fgColor);
        }

        public static void I(string message)
        {
            PrintWithTag("I", message);
        }

        public static void E(string message)
        {
            PrintWithTag("E", message, fgColor: Color.Red);
            var stacktrace = new System.Diagnostics.StackTrace(1, true);
            Print(stacktrace.ToString());
        }
    }
}
