using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using LabTwo;

namespace LabTwo
{
    public struct userData
    {
        string pw;
        string premLvl;
    }
    public class Customer
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Product> _cart { get; private set; }
        public string premiumLevel { get; private set; }

       
        public Customer(string username, string password, string premiumLevel = "Base")
        {
            this.Username = username;
            this.Password = password;
            this.premiumLevel = premiumLevel;
            _cart = new List<Product>();
        }

        public void PrintInfo()
        {
            Console.WriteLine("Here's your account information:");
            Console.WriteLine($"Current User: {Username}");
            Console.WriteLine($"Password: {Password}");
            Console.WriteLine($"Premium Level: {premiumLevel}");
        }

        public void PrintCart() => Cart.CartPrinter(_cart);

        public void CartItemAdd(Product input, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                _cart.Add(input);
            }
        }
        public void CartItemRemove(Product input)
        {
            _cart.Remove(input);
        }
        public bool Login(string inputUsername, string inputPassword)
        {
            return this.Username.Equals(inputUsername, StringComparison.OrdinalIgnoreCase) && this.Password == inputPassword;
        }
    }

    public class PremiumCustomer : Customer
    {
        private int discount {  get; set; }
        public void printCart() => Cart.CartPrinter(_cart, discount);
        public PremiumCustomer(string Username, string password, string premiumLevel) : base(Username, password, premiumLevel)
        {
            discount = premiumLevel switch
            {
                "Bronze" => 5,
                "Silver" => 10,
                "Gold" => 15,
                _ => 0
            };
        }
        public void PrintCart() => Cart.CartPrinter(_cart, discount);
    }

}
