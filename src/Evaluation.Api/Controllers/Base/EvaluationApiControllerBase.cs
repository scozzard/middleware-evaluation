using Evaluation.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Api.Controllers.Base
{
    /// <summary>
    /// A base controller class that can be used by other controllers, and provides a means of
    /// sharing methods, generic responses etc.
    /// </summary>
    public class EvaluationApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Produces a generic "Not Found" response with a custom error payload.
        /// </summary>
        /// <param name="id">The id of the resource not found.</param>
        /// <returns></returns>
        public static ObjectResult ResourceNotFound(int id)
        {
            return new NotFoundObjectResult(new Error("Not Found", $"Resource with id '{id}' was not found."));
        }
    }
}