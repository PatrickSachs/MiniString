using System.Diagnostics;

namespace Tests
{
    public static class TestOutput
    {
        public static T WriteLine<T>(this T t)
        {
            Debug.WriteLine(t);
            return t;
        }
    }
}