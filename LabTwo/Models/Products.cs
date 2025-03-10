﻿using LabTwo.Management;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo.Models
{
    // Product class, also handling adding products to the cart
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
            ConsoleKeyInfo cki; // Variable used for menu options
            bool add = true; // Variable used to allow the user to keep adding items until they want to stop
            Product[] products = Manager.LoadProducts(); // Imports the list of products available in the store

            // Discount used for Premium Customers, imported in case of the logged in user being a Premium customer
            int discount = 0;
            if (cart is PremiumCustomer premiumCustomer)
            {
                discount = premiumCustomer.discount;
            }

            // Loop to keep adding products
            while (add)
            {
                Console.Clear();
                // Lists all available products, and their price, for Premium Customers, also shows the un-discounted price.
                if (discount != 0)
                {
                    for (int i = 0; i < products.Length; i++)
                    {
                        double discountedPrice = products[i].price * (1 - discount / 100.0);
                        Console.WriteLine($"{i + 1}) {products[i].itemName} - Your Price: {discountedPrice:C}, Original Price: {products[i].price:C}");
                    }
                }
                else
                {
                    for (int i = 0; i < products.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}) {products[i].itemName} - Your Price: {products[i].price:C}");
                    }
                }

                Console.Write("\nEnter the number of the product you would like to add to your cart:  ");
                int productIndex;
                while (true)
                {
                    string input = Console.ReadLine();

                    // Try to parse the input as an integer
                    if (int.TryParse(input, out productIndex))
                    {
                        // Check if the number is within the valid range
                        if (productIndex >= 1 && productIndex <= products.Length)
                        {
                            productIndex -= 1; // Adjust for zero-based indexing
                            break; // Exit the loop if input is valid
                        }
                        else
                        {
                            Console.WriteLine($"Invalid input. Please enter a number between 1 and {products.Length}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                }


                Product selectedProduct = products[productIndex]; // Sets the current selected product as a new class, to add to the cart

                Console.Write($"\nHow many {selectedProduct.itemName}(s) would you like to add?:  ");
                string quantityInput = Console.ReadLine();
                int quantity;

                // checks if the customer added a valid integer, and adds the requested amount of the product to the cart
                if (int.TryParse(quantityInput, out quantity))
                {
                    cart.CartItemAdd(selectedProduct, quantity);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Invalid Quantity. Please Enter an integer.");

                    Console.ResetColor();
                }


                Console.Write("\nPress 1 to continue shopping, otherwise press any key to return back to main menu. ");
                cki = Console.ReadKey(true);
                if (cki.KeyChar != '1')
                {
                    Console.Clear();
                    add = false;
                    break;
                }
            }
        }
    }
}