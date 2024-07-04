using Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UserLogger:BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string LogText { get; set; }
    }
}
