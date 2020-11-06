namespace Byndyusoft.ModelResult.Extensions
{
    using System.Linq;

    public static class StringExtensions
    {
        public static string ToFirstLowerChar(this string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return string.Empty;
            return propertyName.First().ToString().ToLower() + propertyName.Substring(1);
        }
    }
}