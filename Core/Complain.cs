using Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Complain:BaseEntity
    {
        public string Address { get; set; }
        public int Status { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
