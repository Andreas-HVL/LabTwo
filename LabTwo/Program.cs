using LabTwo;
using System;
using System.Text.Json;
//Webstore
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
Writer writer = new Writer();
writer.FastWrite("I removed this.input = input; since you’re not using it for anything currently. You can add it back if you decide to pass input when creating a Writer object.");

Console.ReadKey();