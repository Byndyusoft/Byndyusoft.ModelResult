namespace Byndyusoft.ModelResult.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Extensions;
    using ModelResult;

    public class ErrorInfoConverter : JsonConverter<ErrorInfo?>
    {
        public override ErrorInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            var code = (string?) null;
            var message = (string?) null;
            var items = (ErrorInfoItem[]?) null;
            var itemsConverter = GetItemConverter(options);

            var codeName = GetCodeName(options);
            var codeNameLower = GetCodeNameLower(options);
            var messageName = GetMessageName(options);
            var messageNameLower = GetMessageNameLower(options);
            var itemsName = GetItemsName(options);
            var itemsNameLower = GetItemsNameLower(options);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                if (reader.ValueTextEquals(codeName.EncodedUtf8Bytes) || reader.ValueTextEquals(codeNameLower.EncodedUtf8Bytes))
                    code = GetStringValue(ref reader);
                else if (reader.ValueTextEquals(messageName.EncodedUtf8Bytes) || reader.ValueTextEquals(messageNameLower.EncodedUtf8Bytes))
                    message = GetStringValue(ref reader);
                else if (reader.ValueTextEquals(itemsName.EncodedUtf8Bytes) || reader.ValueTextEquals(itemsNameLower.EncodedUtf8Bytes))
                    items = GetErrorInfoItems(ref reader, options, itemsConverter);
                else
                    throw new JsonException();
            }

            if (code == null)
                throw new InvalidOperationException("Code is null");

            if (message == null)
                throw new InvalidOperationException("Message is null");

            var errorInfo = new ErrorInfo(code, message, items);
            return errorInfo;
        }

        private JsonEncodedText GetCodeName(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfo.Code), options);
        }

        private JsonEncodedText GetCodeNameLower(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfo.Code).ToFirstLowerChar(), options);
        }

        private JsonEncodedText GetMessageName(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfo.Message), options);
        }

        private JsonEncodedText GetMessageNameLower(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfo.Message).ToFirstLowerChar(), options);
        }

        private JsonEncodedText GetItemsName(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfo.Items), options);
        }

        private JsonEncodedText GetItemsNameLower(JsonSerializerOptions options)
        {
            return ConverterHelper.GetPropertyName(nameof(ErrorInfo.Items).ToFirstLowerChar(), options);
        }

        public JsonConverter<ErrorInfoItem> GetItemConverter(JsonSerializerOptions options)
        {
            return (JsonConverter<ErrorInfoItem>) options.GetConverter(typeof(ErrorInfoItem));
        }

        private string GetStringValue(ref Utf8JsonReader reader)
        {
            reader.Read();

            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            var value = reader.GetString();

            return value;
        }

        private ErrorInfoItem[]? GetErrorInfoItems(ref Utf8JsonReader reader, JsonSerializerOptions options,
            JsonConverter<ErrorInfoItem> itemsConverter)
        {
            reader.Read();

            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            var items = new List<ErrorInfoItem>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                var errorInfoItem = itemsConverter.Read(ref reader, typeof(ErrorInfoItem), options);
                items.Add(errorInfoItem);
            }

            return items.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, ErrorInfo? value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else
            {
                var itemsConverter = GetItemConverter(options);
                writer.WriteStartObject();
                writer.WriteString(GetMessageName(options), value.Message);
                writer.WriteString(GetCodeName(options), value.Code);
                WriteItems(writer, value.Items, options, itemsConverter);
                writer.WriteEndObject();
            }
        }

        private void WriteItems(Utf8JsonWriter writer, ErrorInfoItem[]? items, JsonSerializerOptions options,
            JsonConverter<ErrorInfoItem> itemsConverter)
        {
            if (items == null || items.Any() == false)
                writer.WriteNull(GetItemsName(options));

            else
            {
                writer.WriteStartArray(GetItemsName(options));
                foreach (var errorInfoItem in items)
                    itemsConverter.Write(writer, errorInfoItem, options);
                writer.WriteEndArray();
            }
        }
    }
}