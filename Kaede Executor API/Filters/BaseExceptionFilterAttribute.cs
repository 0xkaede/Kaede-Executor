using Kaede_Executor_API.Models.Exceptions;
using Kaede_Executor_API.Models.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Kaede_Executor_API.Filters
{
    public class BaseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not BaseException)
            {
                Console.WriteLine($"Unhandled exception: {context.Exception.Message}");

                // Unhandled exception
                context.Exception = new UnhandledErrorException(Guid.NewGuid().ToString());
            }

            var exception = (BaseException)context.Exception;
            context.Result = new JsonResult(exception)
            {
                StatusCode = exception.StatusCode
            };
        }
    }
}
