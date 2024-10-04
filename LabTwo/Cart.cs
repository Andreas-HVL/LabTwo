using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo
{
    // Class to handle the creation and functionality of the shopping cart of a logged in user.
    public class Cart
    {
        private List<Product> cart { get; set; }
        public Cart()
        {
            cart = new List<Product>();
        }

        // Groups the items in the cart for use in the CartPrinter method
        private static List<(string ItemName, int Quantity, double TotalPrice)> GroupCartByProduct(List<Product> cart)
        {
            return cart.GroupBy(product => product.itemName) // Group by product name
                .Select(group => (
                    ItemName: group.Key,                    // The name of the product
                    Quantity: group.Count(),                // Count the number of products in this group
                    TotalPrice: group.Sum(product => product.price) // Sum the total price for this group
                ))
                .ToList();
        }
        
        // Used to convert the individual prices of items in the cart for currency conversion
        private static List<Product> ConvertCartPrices(List<Product> cart, Func<double, double> conversionFunc)
        {
            return cart.Select(product => new Product(
                product.itemName,
                conversionFunc(product.price)
            )).ToList();
        }


        // Used to print the contents of the cart of the logged in user
        static public void CartPrinter(List<Product> cart, int discount = 0)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }
            
            string currency = "";
            var groupedCart = GroupCartByProduct(cart);
            double totalCartPrice = groupedCart.Sum(item => item.TotalPrice);
            ConsoleKeyInfo cki;

            Console.WriteLine("Please select the desired currency");
            Console.WriteLine("1: Swedish Krona (SEK)");
            Console.WriteLine("2: American Dollars (USD)");
            Console.WriteLine("3. Euro (EUR)");
            
            // Awaits a valid input from the user
            do
            {
                cki = Console.ReadKey(true);
            }
            while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3');
            
            // Handles the selection of currency to be used for showing the cart.
            switch (cki.KeyChar)
            {
                case '1':
                    currency = "SEK";
                    break;
                case '2':
                    totalCartPrice = ConvertSEKToUSD(totalCartPrice);
                    cart = ConvertCartPrices(cart, ConvertSEKToUSD);
                    groupedCart = GroupCartByProduct(cart);
                    currency = "$";
                    break;
                case '3':
                    totalCartPrice = ConvertSEKToEUR(totalCartPrice);
                    cart = ConvertCartPrices(cart, ConvertSEKToEUR);
                    groupedCart = GroupCartByProduct(cart);
                    currency = "$";
                    break;
            }
            Console.Clear();

            Console.WriteLine("Products in Cart:");
            // Uses the earlier groupedCart variable to group the products in the cart together, to have a neat list of each individual product.
            // If the customer is premium, it adds the discount to the items and calculates price before and after discount.
            if (discount == 0)
            {
                foreach (var item in groupedCart)
                {
                    Console.WriteLine($"{item.Quantity} x {item.ItemName}, Price: {currency} {Math.Round(item.TotalPrice, 2)}, Price per item: {item.TotalPrice / item.Quantity}");
                }
                Console.WriteLine($"Total Price {currency} {Math.Round(totalCartPrice, 2)}");
            }
            else
            {
                foreach (var item in groupedCart)
                {
                    double discountedPrice = Math.Round((item.TotalPrice - (item.TotalPrice * discount / 100)), 2);
                    Console.WriteLine($"{item.Quantity} x {item.ItemName}, Price: {currency} {discountedPrice}, Price per item: {discountedPrice / item.Quantity}");
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Total Price: {currency} {Math.Round(totalCartPrice - (totalCartPrice * discount / 100))}\n");
                Console.ResetColor();
                Console.WriteLine($"Original Total without your premium discount: {currency} {Math.Round(totalCartPrice, 2)}\n");
            }
            
        }
        
        
        // Functions to convert the currency from SEK to USD or EUR
        public static double ConvertSEKToUSD(double sekPrice)
        {
            const double SEKtoUSDRate = 0.098;
            return Math.Round((sekPrice * SEKtoUSDRate), 2);
        }
        public static double ConvertSEKToEUR(double sekPrice)
        {
            const double SEKtoEURRate = 0.088;
            return Math.Round((sekPrice * SEKtoEURRate), 2);
        }

    }   
}

