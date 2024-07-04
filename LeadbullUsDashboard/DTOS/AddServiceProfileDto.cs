using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DTOS
{
    public class AddServiceProfileDto
    {
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
