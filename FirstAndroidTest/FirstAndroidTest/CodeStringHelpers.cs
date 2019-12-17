namespace FirstAndroidTest
{
    public static class CodeStringHelpers
    {
        public static string UpperFirst(this string name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        }
    }
}
