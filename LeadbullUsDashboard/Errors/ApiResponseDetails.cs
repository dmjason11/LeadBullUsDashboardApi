namespace Api.Errors
{
    public class ApiResponseDetails:ApiResponse
    {
        public string Detail { get; set; }
        public ApiResponseDetails() : base(400)
        {

        }
        public ApiResponseDetails(int code, string message, string detail) : base(code, message)
        {
            Detail = detail;
        }
    }
}
