using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    internal class Ticket
    {
        int price = 0;
        int id = 0;
        public int Price { get { return price; } set { price = value; } }
        public int Id { get { return id; } set { id = value; } }
    }
}
