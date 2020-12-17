namespace Byndyusoft.ModelResult.ModelResults
{
    public class OkModelResult : ModelResult
    {
        public override ModelResultType Type { get; } = ModelResultType.Success;
    }

    public class OkModelResult<T> : ModelResult<T>
    {
        private OkModelResult(OkModelResult modelResult) : base(modelResult)
        {
        }

        public OkModelResult(T result) : base(new OkModelResult(), result)
        {
        }

        public static explicit operator OkModelResult<T>(OkModelResult result)
        {
            return new OkModelResult<T>(result);
        }
    }
}