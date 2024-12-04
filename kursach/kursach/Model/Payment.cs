using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    internal class Payment
    {
        public string Number { get; set; }
        public int? Code { get; set; }  
        public string Period { get; set; }
        public string OwnerName { get; set; }
        public string Adress { get; set; }
    }
}
