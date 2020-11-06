namespace Byndyusoft.ModelResult.ModelResults
{
    public class ErrorModelResult : ModelResult
    {
        public ErrorInfo Error { get; }

        public string Code => Error.Code;

        public string Message => Error.Message;

        public ErrorInfoItem[] ErrorItems => Error.Items;

        public override ModelResultType Type { get; } = ModelResultType.Error;

        public ErrorModelResult(ErrorInfo errorInfo)
        {
            Error = errorInfo;
        }

        public ErrorModelResult(string code, string message, ErrorInfoItem[]? items = null) : this(new ErrorInfo(code, message, items))
        {
        }
    }

    public class ErrorModelResult<T> : ModelResult<T>
    {
        public ErrorInfo Error => ((ErrorModelResult)InnerResult).Error;

        public string Code => Error.Code;

        public string Message => Error.Message;

        public ErrorInfoItem[] ErrorItems => Error.Items;

        private ErrorModelResult(ErrorModelResult result) : base(result) { }

        public static explicit operator ErrorModelResult<T>(ErrorModelResult result)
        {
            return new ErrorModelResult<T>(result);
        }
    }
}