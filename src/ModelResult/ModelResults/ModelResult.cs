namespace Byndyusoft.ModelResult.ModelResults
{
    using System;

    public abstract class ModelResult : IModelResult
    {
        public abstract ModelResultType Type { get; }

        public bool IsOk() => Type == ModelResultType.Success;

        public bool IsError() => Type != ModelResultType.Success;

        public ErrorModelResult AsError()
        {
            if (IsOk())
                throw new InvalidOperationException("Result is not error");

            return (ErrorModelResult) this;
        }

        public ErrorInfo GetError() => AsError().Error;
    }

    public abstract class ModelResult<T> : IModelResult<T>
    {
        private readonly T _result;
        protected readonly ModelResult InnerResult;

        protected ModelResult(ModelResult modelResult, T result = default!)
        {
            InnerResult = modelResult ?? throw new ArgumentNullException(nameof(modelResult));
            _result = result;
        }

        public T Result
        {
            get
            {
                if (IsError())
                    throw new InvalidOperationException("Error result has no result");

                return _result;
            }
        }

        public ModelResultType Type => InnerResult.Type;

        public bool IsOk() => InnerResult.IsOk();

        public bool IsError() => InnerResult.IsError();

        public ModelResult AsSimple()
        {
            return InnerResult;
        }

        public ErrorModelResult<T> AsError()
        {
            if (IsOk())
                throw new InvalidOperationException("Result is not error");

            return (ErrorModelResult<T>) this;
        }

        public ErrorInfo GetError() => InnerResult.GetError();

        public static implicit operator ModelResult<T>(ModelResult modelResult)
        {
            if (modelResult is OkModelResult okModelResult)
                return okModelResult;

            if (modelResult is ErrorModelResult errorModelResult)
                return errorModelResult;

            throw new NotImplementedException(
                $"Conversion to generic from model result of type {modelResult.GetType().Name} is not implemented");
        }

        public static implicit operator ModelResult<T>(T result)
        {
            return new OkModelResult<T>(result);
        }

        public static implicit operator ModelResult<T>(OkModelResult modelResult)
        {
            return (OkModelResult<T>) modelResult;
        }

        public static implicit operator ModelResult<T>(ErrorModelResult modelResult)
        {
            return (ErrorModelResult<T>) modelResult;
        }
    }
}