namespace ModelResult.Text.Json.Extensions
{
    using System.Text.Json;
    using Converters;

    public static class JsonSerializerOptionsExtensions
    {
        public static void AddErrorInfoConverter(this JsonSerializerOptions options)
        {
            options.Converters.Add(new ErrorInfoConverter());
            options.Converters.Add(new ErrorInfoItemConverter());
        }
    }
}