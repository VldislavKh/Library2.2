using FluentValidation;
using FluentValidation.Results;
using Library2._2.CustomExceptionMiddleware.CustomExceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace Library2._2.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int code = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            string s;

            List<ErrorBody> errors = new();

            switch (exception)
            {
                case ValidationException validationEx:
                    code = StatusCodes.Status400BadRequest;
                    message = "Validation failed!";

                    foreach(var error in validationEx.Errors)
                    {
                        errors.Add(new ErrorBody { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage });
                    }

                    break;

                case NotFoundException notFoundEx:
                    code = StatusCodes.Status404NotFound;
                    message = "Not found";
                    break;
            }

            context.Response.StatusCode = code;
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = code,
                Message = message,
                Errors = errors
            }.ToString());  
        }
    }
}
