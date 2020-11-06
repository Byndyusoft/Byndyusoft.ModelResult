namespace ModelResult.AspNetCore.Dtos
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class ErrorInfoDto
    {
        [NotNull]
        public string? Code { get; set; }

        [NotNull]
        public string? Message { get; set; }

        public ErrorInfoItemDto[]? Items { get; set; }

        public ErrorInfoDto()
        {
        }

        public ErrorInfoDto(ErrorInfo errorInfo)
        {
            Code = errorInfo.Code;
            Message = errorInfo.Message;
            Items = errorInfo.Items.Any() == false ? null : errorInfo.Items.Select(i => new ErrorInfoItemDto(i)).ToArray();
        }

        public ErrorInfo ToErrorInfo()
        {
            if (Code == null)
                throw new InvalidOperationException("Code is null");

            if (Message == null)
                throw new InvalidOperationException("Message is null");

            return new ErrorInfo(Code, Message, Items?.Select(i => i.ToErrorInfoItem()).ToArray());
        }
    }
}