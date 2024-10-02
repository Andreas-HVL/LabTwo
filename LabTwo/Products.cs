using LabTwo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo
{
    public class Product
    {
        public string itemName { get; private set; }
        public double price { get; private set; }
        
        public Product(string itemName, double price)
        {
            this.itemName = itemName;
            this.price = price;
        }
        public static void AddToCart(Customer cart)
        {
            ConsoleKeyInfo cki;
            bool add = true;

            Product[] products = Manager.LoadProducts();
            int discount = 0;
            if (cart is PremiumCustomer premiumCustomer)
            {
                discount = premiumCustomer.discount;  // Get the discount from PremiumCustomer
            }

            while (add)
            {
                Console.Clear();
                for (int i = 0; i < products.Length; i++)
                {
                    double discountedPrice = products[i].price * (1 - discount / 100.0);
                    Console.WriteLine($"{i + 1}) {products[i].itemName} - Your Price: {discountedPrice:C}, Original Price: {products[i].price}");
                }
                Console.Write("\nEnter the number of the product you would like to add to your cart:  ");
                do
                {
                    cki = Console.ReadKey(true);
                }
                while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3');
                int productIndex;


                if (int.TryParse(cki.KeyChar.ToString(), out productIndex))
                {
                    productIndex -= 1;

                    if (productIndex >= 0 && productIndex < products.Length)
                    {
                        Product selectedProduct = products[productIndex];

                        Console.Write($"\nHow many {selectedProduct.itemName}(s) would you like to add?:  ");

                        string quantityInput = Console.ReadLine();

                        int quantity;

                        if (int.TryParse(quantityInput, out quantity))
                        {
                            cart.CartItemAdd(selectedProduct, quantity);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("Invalid Quantity. Please Enter a real number.");

                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Invalid product selection. Please enter a valid product number.");

                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine("invalid Input please enter a valid number..");

                    Thread.Sleep(1000);
                }
                Console.Write("\nPress 1 to continue shopping, otherwise press any key to return back to main menu. ");
                cki = Console.ReadKey(true);
                if (cki.KeyChar != '1')
                {
                    Console.Clear();
                    Console.WriteLine("returning to main menu...");
                    Thread.Sleep(1000);
                    add = false;
                    break;
                }
                else
                {
                    add = true;
                }
            }
        }
    }
}