using kursach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.ViewModel
{
    internal class BuyTicket
    {
        public BuyTicket(int price) 
        {
            price += 1;
        }
    }
}
