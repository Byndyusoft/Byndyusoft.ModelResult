namespace Byndyusoft.ModelResult
{
    using System;
    using System.Linq;

    public class ErrorInfo
    {
        public ErrorInfo(string code, string message, ErrorInfoItem[]? items = null)
        {
            Code = code;
            Message = message;
            Items = items ?? Array.Empty<ErrorInfoItem>();
        }

        public string Code { get; private set; }

        public string Message { get; private set; }

        public ErrorInfoItem[] Items { get; private set; }

        public override string ToString()
        {
            return
                $"ErrorModelResult. Code - {Code}. Message - {Message}. Items - [ {string.Join(", ", Items.Select(i => i.ToString()))} ]";
        }
    }
}