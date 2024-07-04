
namespace Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse() {
        
        }
        public ApiResponse(int statusCode , string message = null)
        {
            StatusCode = statusCode;
            Message = message?? getMessage(statusCode);
        }

        private string getMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorize, you are not",
                404 => "Resourse is not found",
                500 => "Internal server error",
                _ => "UnExpected Error"

            };
        }
    }
}
