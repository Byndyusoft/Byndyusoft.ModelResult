namespace ModelResult.ModelResults
{
    public interface IModelResult
    {
        bool IsOk();

        bool IsError();
    }

    public interface IModelResult<out T> : IModelResult
    {
        T Result { get; }

        ModelResultType Type { get; }
    }
}