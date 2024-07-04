using Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Lead:BaseEntity
    {
        public string sheetIdentifier { get; set; }
        [ForeignKey("ServiceProfile")]
        public int ServiceProfileId { get; set; }
        public ServiceProfile ServiceProfile { get; set; }
    }
}
