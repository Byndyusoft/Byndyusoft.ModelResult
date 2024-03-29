﻿namespace Byndyusoft.ModelResult.ModelResults
{
    public class ErrorModelResult : ModelResult
    {
        public ErrorModelResult(ErrorInfo errorInfo)
        {
            Error = errorInfo;
        }

        public ErrorModelResult(string code, string message, ErrorInfoItem[]? items = null) : this(new ErrorInfo(code, message, items))
        {
        }

        public ErrorInfo Error { get; }

        public string Code => Error.Code;

        public string Message => Error.Message;

        public ErrorInfoItem[] ErrorItems => Error.Items;

        public override ModelResultType Type => ModelResultType.Error;
    }

    public class ErrorModelResult<T> : ModelResult<T>
    {
        private ErrorModelResult(ErrorModelResult result) : base(result)
        {
        }

        public ErrorInfo Error => ((ErrorModelResult) InnerResult).Error;

        public string Code => Error.Code;

        public string Message => Error.Message;

        public ErrorInfoItem[] ErrorItems => Error.Items;

        public static explicit operator ErrorModelResult<T>(ErrorModelResult result)
        {
            return new ErrorModelResult<T>(result);
        }
    }
}