namespace GhostOfTheLibrary.Extensions
{
    public static class StringExtensions
    {
        public static string ClearStringFromParanthesesStart(this string text)
        {
            var splittedText = text.Split('(');

            return splittedText[0].TrimEnd();
        }
    }
}
