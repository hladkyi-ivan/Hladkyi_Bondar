using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
namespace kursach.Model
{
    public class CustomerService
    {
        private const string FilePath = "C:\\Users\\ivann\\Desktop\\Курсова\\kursach\\kursach\\customers.xml";
        private List<Customer> _customers;
        public static bool IsUserLoggedIn { get; set; } = false;
        public CustomerService()
        {
            if (File.Exists(FilePath))
            {
                var serializer = new XmlSerializer(typeof(List<Customer>));
                using (var stream = File.OpenRead(FilePath))
                {
                    _customers = (List<Customer>)serializer.Deserialize(stream) ?? new List<Customer>();
                }
            }
            else
            {
                _customers = new List<Customer>();
                SaveChanges();
            }
        }
        public Customer GetByNickName(string nickName) =>
            _customers.FirstOrDefault(c => c.NickName.Equals(nickName, StringComparison.OrdinalIgnoreCase));
        public bool ValidateUserCredentials(string nickName, string password)
        {
            var customer = GetByNickName(nickName);
            if (customer != null && customer.Password == password)
            {
                IsUserLoggedIn = true; 
                return true;
            }
            IsUserLoggedIn = false;
            return false;
        }
        private void SaveChanges()
        {
            var serializer = new XmlSerializer(typeof(List<Customer>));
            using (var stream = File.Create(FilePath))
            {
                serializer.Serialize(stream, _customers);
            }
        }
    }
}