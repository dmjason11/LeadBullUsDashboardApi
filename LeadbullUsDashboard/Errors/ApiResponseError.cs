namespace Api.Errors
{
    public class ApiResponseError:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiResponseError() : base(400)
        {

        }
        public ApiResponseError(int code, string message, List<string> errors) : base(code, message)
        {
            Errors = errors;
        }
    }
}
