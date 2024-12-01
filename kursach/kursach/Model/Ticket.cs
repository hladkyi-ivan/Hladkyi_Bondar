using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    public class Ticket
    {
        public string Destination { get; set; } 
        public DateTime Date { get; set; } 
        public string HotelName { get; set; } 
        public string HotelImage { get; set; } 
        public string Period { get; set; } 
        public string TicketAvailability { get; set; } 
        public string Description { get; set; } 
        public string Price { get; set; }
        public string Departure { get; set; }
    }
}
