namespace Byndyusoft.ModelResult.Dtos
{
    using System;

    public class ErrorInfoItemDto
    {
        public ErrorInfoItemDto()
        {
        }

        public ErrorInfoItemDto(ErrorInfoItem errorInfoItem)
        {
            PropertyName = errorInfoItem.PropertyName;
            Error = errorInfoItem.Error;
        }

        public string PropertyName { get; set; } = default!;

        public string Error { get; set; } = default!;

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