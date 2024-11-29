using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    internal class Customer
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Customer() { }
        public Customer(string firstName, string lastName, string phoneNumber, string nickName, string password)
        { FirstName = firstName; SecondName = lastName; PhoneNumber = phoneNumber; NickName = nickName; Password = password; }
    }
}
