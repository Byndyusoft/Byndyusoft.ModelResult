namespace Byndyusoft.ModelResult.Common
{
    using ModelResults;

    public static class CommonErrorResultExtensions
    {
        public static bool IsNotFound(this ModelResult modelResult)
        {
            return modelResult.IsError() && modelResult.GetError().Code == CommonErrorCodes.NotFound;
        }

        public static bool IsNotFound<T>(this ModelResult<T> modelResult)
        {
            return IsNotFound(modelResult.AsSimple());
        }
    }
}