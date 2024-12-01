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
        private static readonly string FilePath = Path.Combine(
         Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
         "customers.xml"
     );
        private List<Customer> _customers;
        public static bool IsUserLoggedIn { get; set; } = false;
        public static Customer CurrentCustomer { get; private set; }
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
                CurrentCustomer = customer;
                IsUserLoggedIn = true; 
                return true;
            }
            IsUserLoggedIn = false;
            CurrentCustomer = null;
            return false;
        }
        public void SaveCustomer(Customer customer)
        {
            _customers.Add(customer);
            SaveChanges();
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