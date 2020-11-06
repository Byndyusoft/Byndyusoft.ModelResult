namespace Byndyusoft.ModelResult.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Extensions;
    using ModelResult;

    public class ErrorInfoItemConverter : JsonConverter<ErrorInfoItem?>
    {
        public override ErrorInfoItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            var propertyNameName = GetPropertyNameName(options);
            var propertyNameNameLower = GetPropertyNameNameLower(options);
            var errorName = GetErrorName(options);
            var errorNameLower = GetErrorNameLower(options);

            var property = (string?) null;
            var error = (string?) null;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                if (reader.ValueTextEquals(propertyNameName.EncodedUtf8Bytes) || reader.ValueTextEquals(propertyNameNameLower.EncodedUtf8Bytes))
                    property = GetValue(ref reader);
                else if (reader.ValueTextEquals(errorName.EncodedUtf8Bytes) || reader.ValueTextEquals(errorNameLower.EncodedUtf8Bytes))
                    error = GetValue(ref reader);
                else
                    throw new JsonException();
            }

            if (property == null)
                throw new InvalidOperationException("Property is null");

            if (error == null)
                throw new InvalidOperationException("Error is null");

            var errorInfoItem = new ErrorInfoItem(property, error);
            return errorInfoItem;
        }

        private JsonEncodedText GetPropertyNameName(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfoItem.PropertyName), options);
        }

        private JsonEncodedText GetPropertyNameNameLower(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfoItem.PropertyName).ToFirstLowerChar(), options);
        }

        private JsonEncodedText GetErrorName(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfoItem.Error), options);
        }

        private JsonEncodedText GetErrorNameLower(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfoItem.Error).ToFirstLowerChar(), options);
        }

        private string GetValue(ref Utf8JsonReader reader)
        {
            reader.Read();

            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            var value = reader.GetString();

            return value;
        }

        public override void Write(Utf8JsonWriter writer, ErrorInfoItem? value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else
            {
                writer.WriteStartObject();
                writer.WriteString(GetPropertyNameName(options), value.PropertyName);
                writer.WriteString(GetErrorName(options), value.Error);
                writer.WriteEndObject();
            }
        }
    }
}