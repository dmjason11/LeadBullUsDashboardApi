using Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ServiceProfile:BaseEntity
    {
        public string ServiceName { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public AppUser user { get; set; }
        public ICollection<Lead> Leads { get; set; }
        public ICollection<Deal> Deals { get; set; }
        
    }
}
