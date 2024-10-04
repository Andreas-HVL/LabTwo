using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using LabTwo;

namespace LabTwo
{ 
    // Class to handle the customer information and cart + login function
    public class Customer
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Product> _cart { get; private set; }
        public string premiumLevel { get; private set; }
        public string CustomerType { get; protected set; } = "Customer";


        public Customer(string username, string password, string premiumLevel = "Base")
        {
            this.Username = username;
            this.Password = password;
            this.premiumLevel = premiumLevel;
            _cart = new List<Product>();
        }

        // Simple function that prints out the customer information
        public void PrintInfo()
        {
            Console.WriteLine("Here's your account information:");
            Console.WriteLine($"Current User: {Username}");
            Console.WriteLine($"Password: {Password}");
            Console.WriteLine($"Premium Level: {premiumLevel}");
        }

        // Takes the current instance of the customer's cart, and prints it out using the function in the Cart Class
        public virtual void PrintCart() => Cart.CartPrinter(_cart);

        // Function used to add items to the cart, used in the Product Class
        public void CartItemAdd(Product input, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                _cart.Add(input);
            }
        }
       
        // Login function
        public bool Login(string inputUsername, string inputPassword)
        {
            return this.Username.Equals(inputUsername, StringComparison.OrdinalIgnoreCase) && this.Password == inputPassword;
        }
    }

    // Subclass of Customer, used to separate customers from premium customers, limited usability currently due to only difference being a discount.
    public class PremiumCustomer : Customer
    {
        public int discount {  get; set; }
        
        public PremiumCustomer(string Username, string password, string premiumLevel) : base(Username, password, premiumLevel)
        {
            this.CustomerType = "PremiumCustomer"; 

            discount = premiumLevel switch
            {
                "Bronze" => 5,
                "Silver" => 10,
                "Gold" => 15,
                _ => 0
            };
        }

        // Slightly difference function used to print the cart for premium users
        public override void PrintCart() => Cart.CartPrinter(_cart, discount);
    }

}
