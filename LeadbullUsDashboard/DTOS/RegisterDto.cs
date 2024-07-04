using System.ComponentModel.DataAnnotations;

namespace Api.DTOS
{
    public class RegisterDto
    {
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "email is not correct")]
        public string Email { get; set; }
        [RegularExpression(@"^[a-zA-Z]\w{3,14}$", ErrorMessage = "")]
        public string Password { get; set; }
    }
}
