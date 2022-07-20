using System.Net;

namespace WebAPI.Exceptions
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationError(string property, string error)
        {
            PropertyName = property;
            ErrorMessage = error;
        }

    }
    public class AppValidationException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public IEnumerable<ValidationError> Errors { get; set; }
        public AppValidationException(
            ValidationError error,
            string message = "Validation failed",
            HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
            Errors = new List<ValidationError>(){
                error
            };
        }
        public AppValidationException(
            IEnumerable<ValidationError> errors,
            string message = "Validation failed",
            HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public IDictionary<string, string[]> ToDictionary()
        {
            return Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
        }
    }
}
