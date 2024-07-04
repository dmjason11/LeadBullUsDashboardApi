using Core.Identity;

using System.ComponentModel.DataAnnotations.Schema;


namespace Core
{
    public class UserTask : BaseEntity
    {
        public string DocumentUrl { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
