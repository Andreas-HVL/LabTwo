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
        public string password { get; private set; }
        public List<Product> _cart { get; private set; }
        public string premiumLevel { get; private set; }
        
        public Customer(string userName, string password, string premiumLevel = "Base")
        {
            this.userName = userName;
            this.password = password;
            this.premiumLevel = premiumLevel;
            _cart = new List<Product>();
        }
        public void PrintInfo()
        {
            Console.WriteLine("Here's your account information:");
            Console.WriteLine($"Current User: {userName}");
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Premium Level: {premiumLevel}");
        }
        public void PrintCart() => Cart.CartManager(_cart);
        public void CartItemAdd(Product input)
        {
            _cart.Add(input);
        }
    }

    public class PremiumCustomer : Customer
    {
        private string premiumLevel { get; set; }
        private int discount {  get; set; }
        public void printCart() => Cart.CartManager(_cart, discount);
        public PremiumCustomer(string userName, string password, string premiumLevel) : base(userName, password, premiumLevel)
        {
            if (premiumLevel == "Bronze")
            {
                this.discount = 5;
            }else if (premiumLevel == "Silver")
            {
                this.discount = 10;
            }else if (premiumLevel == "Gold")
            {
                this.discount = 15;
            }
        }
        public void PrintCart() => Cart.CartManager(_cart, discount);
    }

}
