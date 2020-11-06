namespace ModelResult.Common
{
    using ModelResults;

    public static class CommonErrorResult
    {
        public static ErrorModelResult NotFound { get; } = new ErrorModelResult(CommonErrorCodes.NotFound, CommonErrorMessages.NotFound);
    }
}