using Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Payment:BaseEntity
    {
        public string InvoiceNo { get; set; }
        public decimal InvoiceVal { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime SentDateTime { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
