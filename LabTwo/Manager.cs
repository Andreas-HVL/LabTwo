using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace LabTwo
{
    // Class to mange the files needed for the program to save data between runs.
    public static class Manager
    {
        public static Product[]? products { get; private set; }
        public static Customer[]? customers { get; private set; }

        // Creates the user-list file with premade logins, if the file does not already exist on the machine.
        public static void UserListCreator()
        {
            List<Customer> users = new List<Customer>
            {
                new Customer("Knatte", "123"),
                new Customer("Fnatte", "321"),
                new PremiumCustomer("Tjatte", "213", "Gold")
            };

            // Generates a string containing a filepath for the json file to be stored.
            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string jsonFolderPath = Path.Combine(baseDirectory, "Json");
            string userFilePath = Path.Combine(baseDirectory, "Json", "Users.json");

            // If the folder and file are missing, creates them.
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

        // Creates the creates the product-list file, if the file does not already exist on the machine.
        public static void ProductListCreator()
        {
            List<Product> products = new List<Product>
        {
            new Product("Apple", 20),
            new Product("Banana", 15),
            new Product("Honey", 200)
        };

            // Generates a string containing a filepath for the json file to be stored.
            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string jsonFolderPath = Path.Combine(baseDirectory, "Json");
            string productFilePath = Path.Combine(baseDirectory, "Json", "Products.json");

        // If the folder and file are missing, creates them.
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

        // Method used to generate the filepath for the user-file, used in creating the file, and writing to the file
        private static string GetCustomersFilePath()
        {
            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            return Path.Combine(baseDirectory, "Json", "Users.Json");
        }
    
        // Function to read the Products-File, and load it into the program.
        public static Product[] LoadProducts()
        {
            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string productFilePath = Path.Combine(baseDirectory, "Json", "Products.json");
            string userFile = File.ReadAllText(productFilePath);
            Product[] result = JsonSerializer.Deserialize<Product[]>(userFile);
            products = result;
            return products;
        }

        // Function to read the users-File, and load it into the program.
        public static Customer[] LoadUsers()
        {
            string userFilePath = GetCustomersFilePath();
            string userFile = File.ReadAllText(userFilePath);

            using (JsonDocument doc = JsonDocument.Parse(userFile))
            {
                var root = doc.RootElement;

                List<Customer> result = new List<Customer>();

                // Iterate over each customer object in the array
                foreach (var element in root.EnumerateArray())
                {
                    string customerType = element.GetProperty("CustomerType").GetString();
                    string username = element.GetProperty("Username").GetString();
                    string password = element.GetProperty("Password").GetString();
                    string premiumLevel = element.GetProperty("premiumLevel").GetString();

                    // Creates customers based on PremiumLevel, as either Regular Customers, or Premium Customers
                    if (customerType == "PremiumCustomer")
                    {
                        result.Add(new PremiumCustomer(username, password, premiumLevel));
                    }
                    else
                    {
                        result.Add(new Customer(username, password, premiumLevel));
                    }
                }

                customers = result.ToArray();
                return customers;
            }
        }

        // Function to update the customer file after creating a new customer.
        public static void SaveCustomers(Customer[] customers)
        {
            string customersFilePath = GetCustomersFilePath();

            string JsonData = JsonSerializer.Serialize(customers);
            File.WriteAllText(customersFilePath, JsonData);
        }
       
    }
}
        