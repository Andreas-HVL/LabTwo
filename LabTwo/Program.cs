using LabTwo;
using System.Text.Json;
//Webstore
/*
PremiumCustomer Bob = new("Bob", "John", "Gold");
Customer john = new("John", "bob");
Product Apple = new("Apple", 20);
Product Banana = new("Banana", 40);

john.PrintInfo();
john.CartItemAdd(Apple);
john.CartItemAdd(Apple);
john.CartItemAdd(Apple);
john.CartItemAdd(Banana);
john.CartItemAdd(Banana);
john.PrintCart();
Console.WriteLine("");
Console.WriteLine("");

Bob.PrintInfo();
Bob.CartItemAdd(Apple);
Bob.CartItemAdd(Apple);
Bob.CartItemAdd(Apple);
Bob.CartItemAdd(Banana);
Bob.CartItemAdd(Banana);
Bob.CartItemAdd(Banana);
Bob.PrintCart();
Console.ReadKey();
*/

var products = new List<Product>
{
    new Product("Apple", 20),
    new Product("Banana", 15)
};

string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
string productFilePath = Path.Combine(baseDirectory, "Json", "Products.json");
string jsonFolderPath = Path.Combine(baseDirectory, "Json");
if (!Directory.Exists(jsonFolderPath))
{
    Directory.CreateDirectory(jsonFolderPath);
}
Console.WriteLine(jsonFolderPath);
string jsonData = JsonSerializer.Serialize(products);
File.WriteAllText(productFilePath, jsonData);
Console.ReadKey();