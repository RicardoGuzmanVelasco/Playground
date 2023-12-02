namespace Commits.Runtime
{
    public static class Formatting
    {
        public static string OutOf(this float num, float den) => $"{num:0.00} / {den:0.00}";
    }
}