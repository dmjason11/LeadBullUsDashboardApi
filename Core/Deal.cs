using Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Deal:BaseEntity
    {
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        [ForeignKey("ServiceProfile")]
        public int ServiceProfileId { get; set; }
        public ServiceProfile ServiceProfile { get; set; }

    }
}
