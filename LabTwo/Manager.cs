using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace LabTwo
{
    public static class Manager
    {
        public static Product[]? products { get; private set; }
        public static Customer[]? customers { get; private set; }

        public static Product[] LoadProducts()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string productFilePath = Path.Combine(baseDirectory, "Json", "Products.json");
            string jsonFolderPath = Path.Combine(baseDirectory, "Json");
            string userFile = File.ReadAllText(productFilePath);
            Product[] result = JsonSerializer.Deserialize<Product[]>(userFile);
            products = result;
            return products;
        }

        public static Customer[] LoadUsers()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string userFilePath = Path.Combine(baseDirectory, "Json", "Users.json");
            string jsonFolderPath = Path.Combine(baseDirectory, "Json");
            string userFile = File.ReadAllText(userFilePath);
            Customer[] result = JsonSerializer.Deserialize<Customer[]>(userFile);
            customers = result;
            return customers;
        }

        public static void UserListCreator()
        {
            List<Customer> users = new List<Customer>
            // Dictionary<string, string> = 
        {
            new Customer("Knatte", "123"),
            new Customer("Fnatte", "321"),
            new Customer("Tjatte", "213")
        };
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string userFilePath = Path.Combine(baseDirectory, "Json", "Users.json");
            string jsonFolderPath = Path.Combine(baseDirectory, "Json");
            if (!Directory.Exists(jsonFolderPath))
            {
                Directory.CreateDirectory(jsonFolderPath);
            }
            string jsonData = JsonSerializer.Serialize(users);
            if (!File.Exists(userFilePath))
            {
                File.WriteAllText(userFilePath, jsonData);
            }
        }
        public static void ProductListCreator()
        {
            List<Product> products = new List<Product>
        {
            new Product("Apple", 20),
            new Product("Banana", 15),
            new Product("Honey", 200)
        };
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string productFilePath = Path.Combine(baseDirectory, "Json", "Products.json");
            string jsonFolderPath = Path.Combine(baseDirectory, "Json");
            if (!Directory.Exists(jsonFolderPath))
            {
                Directory.CreateDirectory(jsonFolderPath);
            }
            string jsonData = JsonSerializer.Serialize(products);
            if (!File.Exists(productFilePath))
            {
                File.WriteAllText(productFilePath, jsonData);
            }
            
        }
    }
}
        