using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace kursach.Model
{
    internal class CustomerService
    {
        private readonly string _filePath = "customers.json";

        public CustomerService()
        {
            EnsureFileExists();
        }
        public async Task<List<Customer>> LoadCustomersAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Customer>();

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<Customer>>(json) ?? new List<Customer>();
            }
        }
        public async Task SaveCustomersAsync(List<Customer> customers)
        {
            string json = JsonConvert.SerializeObject(customers, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                await writer.WriteAsync(json);
            }
        }
        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(new List<Customer>(), Newtonsoft.Json.Formatting.Indented));
            }
        }
    }
}
