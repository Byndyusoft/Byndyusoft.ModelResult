﻿namespace ModelResult
{
    using System;

    public class ErrorInfo
    {
        public string Code { get; private set; }

        public string Message { get; private set; }

        public ErrorInfoItem[] Items { get; private set; }

        public ErrorInfo(string code, string message, ErrorInfoItem[]? items = null)
        {
            Code = code;
            Message = message;
            Items = items ?? Array.Empty<ErrorInfoItem>();
        }
    }
}