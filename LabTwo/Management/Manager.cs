using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using LabTwo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using LabTwo.Functionality;
using LabTwo.Management;


namespace LabTwo.Management
{ 
    public static class Manager
    {
        private static readonly MongoClient client = new MongoClient("mongodb+srv://hijaker:r0pCX7VMLNuQ9e0w@cluster0.phpb6.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"); // Update with your connection string
        private static readonly IMongoDatabase database = client.GetDatabase("StoreDatabase");
        private static readonly IMongoCollection<BsonDocument> productCollection = database.GetCollection<BsonDocument>("Products");
        private static readonly IMongoCollection<BsonDocument> customerCollection = database.GetCollection<BsonDocument>("Customers");

        public static Product[]? Products { get; private set; }
        public static Customer[]? Customers { get; private set; }



        // Load products from MongoDB and convert to Product[]
        public static Product[] LoadProducts()
        {
            try
            {
                // Retrieve documents from the products collection
                var bsonDocs = productCollection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

                // Convert BSON to JSON and deserialize into Products array
                var jsonData = bsonDocs.ToJson();
                Products = JsonSerializer.Deserialize<Product[]>(jsonData) ?? Array.Empty<Product>();

                return Products;
            }
            catch (Exception ex)
            {
                // Log the error and throw a fatal exception to terminate the program
                Console.WriteLine("Critical Error: Failed to load products from the database.");
                Console.WriteLine($"Details: {ex.Message}");
                throw new InvalidOperationException("Unable to initialize the Products list. The program will now terminate.", ex);
            }
        }

        public static Customer[] LoadUsers()
        {
            try
            {
                // Retrieve documents from the customers collection
                var bsonDocs = customerCollection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

                // Convert BSON to JSON and deserialize into Customers array
                var jsonData = bsonDocs.ToJson();
                Customers = JsonSerializer.Deserialize<Customer[]>(jsonData) ?? Array.Empty<Customer>();

                return Customers;
            }
            catch (Exception ex)
            {
                // Log the error and throw a fatal exception to terminate the program
                Console.WriteLine("Critical Error: Failed to load customers from the database.");
                Console.WriteLine($"Details: {ex.Message}");
                throw new InvalidOperationException("Unable to initialize the Customers list. The program will now terminate.", ex);
            }
        }



        public static bool DoesCustomerExist(string username)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Username", username);
            return customerCollection.Find(filter).Any();
        }

        public static void SaveCustomer(Customer newCustomer)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Username", newCustomer.Username);
            var bsonDoc = BsonDocument.Parse(JsonSerializer.Serialize(newCustomer));
            customerCollection.ReplaceOne(filter, bsonDoc, new ReplaceOptions { IsUpsert = true });
        }

        public static void AddProduct()
        {
            Console.WriteLine("Please enter the name of the product to be added:");
            string name = Console.ReadLine();

            // Check if an item with the same itemName already exists in the runtime list
            if (Products != null && Products.Any(p => p.itemName.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"A product with the name '{name}' already exists in the runtime list. Please use a different name.");
                MenuWriter.AnyKeyReturn(); // Pause and return to menu
                return; // Exit the method
            }

            Console.WriteLine("Please enter the price:");
            double price;
            while (true)
            {
                string input = Console.ReadLine();

                // Try to parse the input as a double
                if (double.TryParse(input, out price))
                {
                    if (price > 0)
                    {
                        break; // Exit the loop if input is valid
                    }
                }
                Console.WriteLine("Invalid input. Please enter a valid positive number.");
            }

            // Create the new product
            Product newProduct = new Product(name, price);

            try
            {
                // Add to the runtime list
                var productList = Products.ToList(); // Convert to list for modification
                productList.Add(newProduct);
                Products = productList.ToArray(); // Update the runtime list

                // Insert the product into the database
                var bsonDoc = BsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(newProduct));
                productCollection.InsertOne(bsonDoc);

                Console.WriteLine("Item successfully added to the runtime list and database.");
                MenuWriter.AnyKeyReturn(); // Pause and return to menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add item to the database: {ex.Message}");
                MenuWriter.AnyKeyReturn(); // Pause and return to menu
            }
        }

        public static void RemoveProduct()
        {
            Console.WriteLine("Please enter the name of the property to search for (e.g., itemName):");
            string propertyName = Console.ReadLine();

            Console.WriteLine($"Please enter the value of the {propertyName} to search for:");
            string propertyValue = Console.ReadLine();

            try
            {
                // Build a filter to match documents where the specified property equals the given value
                var filter = Builders<BsonDocument>.Filter.Eq(propertyName, propertyValue);

                // Remove from the runtime list
                if (Products != null)
                {
                    var productList = Products.ToList(); // Convert to a list to allow modification

                    var productToRemove = productList.FirstOrDefault(p =>
                    {
                        // Use reflection to dynamically access the property value
                        var propertyInfo = p.GetType().GetProperty(propertyName);
                        if (propertyInfo != null)
                        {
                            var value = propertyInfo.GetValue(p)?.ToString();
                            return value == propertyValue;
                        }
                        return false;
                    });

                    if (productToRemove != null)
                    {
                        productList.Remove(productToRemove);
                        Products = productList.ToArray(); // Update the array after modification
                    }
                    else
                    {
                        Console.WriteLine($"No matching item found in the runtime list with {propertyName} = {propertyValue}.");
                        MenuWriter.AnyKeyReturn();
                    }
                }

                // Remove from the database
                var result = productCollection.DeleteOne(filter);

                if (result.DeletedCount > 0)
                {
                    Console.WriteLine("Item successfully removed from the database.");
                    MenuWriter.AnyKeyReturn();
                }
                else
                {
                    Console.WriteLine($"No matching item found in the database with {propertyName} = {propertyValue}.");
                    MenuWriter.AnyKeyReturn();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to remove item: {ex.Message}");
            }
        }
    }



}
