using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Message:BaseEntity
    {
        public Complain Complain { get; set; }
        public int Order { get; set; } //Message Order 
        public string Text { get; set; }
    }
}
