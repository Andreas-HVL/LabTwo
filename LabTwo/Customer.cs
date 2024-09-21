using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using LabTwo;

namespace LabTwo
{
    public class Customer
    {
        public string userName { get; private set; }
        private string password { get; set; }
        private List<Product> _cart { get; set; }
        public Customer(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            _cart = new List<Product>();
        }
        public void PrintInfo()
        {
            Console.WriteLine("Here's your account information:");
            Console.WriteLine($"Current User: {userName}");
            Console.WriteLine($"Premium Level: Base");
            Console.WriteLine($"Password: {password}");
        }
        public void PrintCart()
        {
            if (_cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }
            Console.WriteLine("Products in Cart:");
            {
                foreach (Product item in _cart)
                {
                    Console.WriteLine($"Item: {item.itemName}, Price: ${item.price}");
                }
            }
        }
        public void CartItemAdd(Product input)
        {
            _cart.Add(input);
        }
    }

}
