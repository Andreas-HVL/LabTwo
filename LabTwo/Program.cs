using LabTwo;
using System;
using System.Runtime.InteropServices;
using System.Text.Json;

/// Serialise User/password, som private i customer class, send til manager.
/// 

//Webstore
Writer writer = new Writer();
Manager.UserListCreator();
Manager.ProductListCreator();
Customer[] customers = Manager.LoadUsers();
Product[] products = Manager.LoadProducts();
ConsoleKeyInfo cki;

/// Generating Files and loading them
/*
Manager.UserListCreator();
Manager.ProductListCreator();
Customer[] customers = Manager.LoadUsers();
Product[] products = Manager.LoadProducts();
foreach (Customer customer in customers)
{
    Console.WriteLine(customer.userName);
}
Console.WriteLine();
Console.WriteLine();
foreach (Product product in products)
{
    Console.WriteLine(product.itemName);
}
*/

/// How to select customer and product from arrays
/*
Customer[] customers = Manager.LoadUsers();
Product[] products = Manager.LoadProducts();
Customer? CurrentCustomer = customers.FirstOrDefault(c => c.userName == "Knatte");
Product? CurrentProduct = products.FirstOrDefault(c => c.itemName == "Apple");
CurrentCustomer.PrintInfo();
CurrentCustomer.CartItemAdd(CurrentProduct);
CurrentCustomer.CartItemAdd(CurrentProduct);
CurrentCustomer.CartItemAdd(CurrentProduct);
*/

/// Writer Tests
/*

writer.FastWrite("Dette er en lang test som jeg bruger til at undersøge om min Writer-Klasse fungerer som den skal, og om jeg skal være forsigtig med hvor meget den skriver ift. linjebrud eller hvordan og hvorledes.");
writer.SlowWrite("Ik?");
*/

/// ReadKey Setup for Menu Handling
/*
ConsoleKeyInfo cki;
Console.WriteLine("Tryk 1,2 eller 3");
do
{
    cki = Console.ReadKey(true);
}
while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3');
Console.WriteLine("There we go");
*/

/// Current Actual Program
/*
writer.FastWrite("Welcome to the store");
writer.FastWrite("Press 1 to login:");
writer.FastWrite("Press 2 to create a user:");
do
{
    cki = Console.ReadKey(true);
}
while (cki.KeyChar != '1' && cki.KeyChar != '2');
*/

/*
string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
string userFilePath = Path.Combine(baseDirectory, "Json", "Users.json");
string jsonFolderPath = Path.Combine(baseDirectory, "Json");
Console.WriteLine(userFilePath);
string tempFilePath = "../json/Users.Json";
Console.WriteLine(tempFilePath);

Customer? CurrentCustomer = customers.FirstOrDefault(c => c.userName == "Knatte");
Product? CurrentProduct = products.FirstOrDefault(c => c.itemName == "Apple");
foreach product in pproduct { 
    "create as Clas I can call by name"
}

CurrentCustomer.CartItemAdd(Apple);

1{ if 1 }
Product? CurrentProduct = products.FirstOrDefault(c => c.itemName == "Apple");
CurrentCustomer.CartItemAdd(CurrentProduct);
if 2{
    Product? CurrentProduct = products.FirstOrDefault(c => c.itemName == "Banana");
    CurrentCustomer.CartItemAdd(CurrentProduct);
}
*/
int i = 0;
foreach (Customer customer in customers)
{ 
    Console.WriteLine(i + " " + customer.userName);
    i++;
}
i = 0;
Console.WriteLine();
Console.WriteLine();
foreach (Product product in products)
{
    Console.WriteLine(i + " " + product.itemName);
    i++;
}

do
{
    cki = Console.ReadKey(true);
}
while (cki.KeyChar != '1' && cki.KeyChar != '2');