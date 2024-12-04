using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace kursach.Model
{
    [Serializable]
    public class Customer
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarPath { get; set; }
        [XmlArray("LikedTickets")]
        [XmlArrayItem("Ticket")]
        public List<Ticket> LikedTickets { get; set; }
        public Customer() 
        {
            AvatarPath = "Images/Без_названия-removebg-preview.png";
            LikedTickets = new List<Ticket>();
        }
        public Customer(string firstName, string lastName, string phoneNumber, string nickName, string password)
        { FirstName = firstName; SecondName = lastName; PhoneNumber = phoneNumber; NickName = nickName; Password = password; AvatarPath = "Images/Без_названия-removebg-preview.png"; LikedTickets = new List<Ticket>(); }
        public void AddLikedTicket(Ticket ticket)
        {
            if (!LikedTickets.Any(t => t.HotelName == ticket.HotelName && t.Date == ticket.Date))
            {
                LikedTickets.Add(ticket);
            }
        }
        public void RemoveLikedTicket(Ticket ticket)
        {
            var ticketToRemove = LikedTickets.FirstOrDefault(t => 
                t.HotelName == ticket.HotelName && 
                t.Date == ticket.Date);
            
            if (ticketToRemove != null)
            {
                LikedTickets.Remove(ticketToRemove);
            }
        }
    }
}
