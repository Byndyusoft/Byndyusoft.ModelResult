namespace Byndyusoft.ModelResult.AspNetCore.Extensions
{
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using ModelResults;

    public static class ControllerModelResultExtensions
    {
        public static ActionResult ToActionResult(this ModelResult modelResult)
        {
            if (modelResult.IsOk())
                return new OkResult();

            return GetErrorResult(modelResult);
        }

        public static ActionResult<T> ToActionResult<T>(this ModelResult<T> modelResult)
        {
            if (modelResult.IsOk())
                return modelResult.Result;

            return GetErrorResult(modelResult.AsSimple());
        }

        private static ActionResult GetErrorResult(ModelResult modelResult)
        {
            if (modelResult.IsNotFound())
                return new NotFoundResult();

            if (modelResult.IsForbidden())
                return new ForbidResult();

            return new BadRequestObjectResult(modelResult.GetError());
        }
    }
}