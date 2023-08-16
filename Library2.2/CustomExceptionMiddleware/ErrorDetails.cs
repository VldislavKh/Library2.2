using FluentValidation.Results;
using System.Text.Json;

namespace Library2._2.CustomExceptionMiddleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<ErrorBody>? Errors { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
