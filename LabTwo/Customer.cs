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
        public string premiumLevel { get; private set; }
        private List<Product> _cart { get; set; }
        public Customer(string userName, string password, string premiumLevel = "Base")
        {
            this.userName = userName;
            this.password = password;
            this.premiumLevel = premiumLevel;
            _cart = new List<Product>();
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Current User: {userName}");
            Console.WriteLine($"Premium Level: {premiumLevel}");
            Console.WriteLine($"Password: {password}");
        }
        public void PrintCart()
        {
            foreach(var item in _cart)
            {
                Product.GetItemName();
            }
        }
        public void CartItemAdd(Product input)
        {
            _cart.Add(input);
        }
    }

}
