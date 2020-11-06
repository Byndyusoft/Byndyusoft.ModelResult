namespace ModelResult.AspNetCore.Dtos
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ErrorInfoItemDto
    {
        [NotNull]
        public string? PropertyName { get; set; }

        [NotNull]
        public string? Error { get; set; }

        public ErrorInfoItemDto()
        {
        }

        public ErrorInfoItemDto(ErrorInfoItem errorInfoItem)
        {
            PropertyName = errorInfoItem.PropertyName;
            Error = errorInfoItem.Error;
        }

        public ErrorInfoItem ToErrorInfoItem()
        {
            if (PropertyName == null)
                throw new InvalidOperationException("Property name is null");

            if (Error == null)
                throw new InvalidOperationException("Error is null");

            return new ErrorInfoItem(PropertyName, Error);
        }
    }
}