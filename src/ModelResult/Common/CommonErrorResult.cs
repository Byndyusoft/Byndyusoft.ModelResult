namespace Byndyusoft.ModelResult.Common
{
    using ModelResults;

    public static class CommonErrorResult
    {
        public static ErrorModelResult NotFound { get; } = new ErrorModelResult(CommonErrorCodes.NotFound, CommonErrorMessages.NotFound);

        public static ErrorModelResult Forbidden { get; } = new ErrorModelResult(CommonErrorCodes.Forbidden, CommonErrorMessages.Forbidden);
    }
}