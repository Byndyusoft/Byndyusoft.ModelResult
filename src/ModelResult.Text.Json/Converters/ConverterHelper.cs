namespace Byndyusoft.ModelResult.Converters
{
    using System.Text.Json;

    public static class ConverterHelper
    {
        public static JsonEncodedText GetPropertyName(string name, JsonSerializerOptions options)
        {
            if (options.PropertyNamingPolicy != null)
                name = options.PropertyNamingPolicy.ConvertName(name);
            return JsonEncodedText.Encode(name, options.Encoder);
        }
    }
}