using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo
{
    public class Cart
    {
        private List<Product> cart { get; set; }
        public Cart()
        {
            cart = new List<Product>();

        }
        public class GroupedProduct
        {
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public double TotalPrice { get; set; }
        }
        private static List<GroupedProduct> GroupCartByProduct(List<Product> cart)
        {
            return cart.GroupBy(product => product.itemName)
                .Select(group => new GroupedProduct
                {
                    ItemName = group.Key,
                    Quantity = group.Count(),
                    TotalPrice = group.Sum(product => product.price) // Sum the prices of the grouped items
                })
                .ToList();
        }
        
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
            Console.WriteLine("Please select the desired currency");
            Console.WriteLine("1: Swedish Krone (SEK)");
            Console.WriteLine("2: American Dollars (USD)");
            Console.WriteLine("3. Euro (EUR)");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
            }
            while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3');
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
            if (discount == 0)
            {
                Console.WriteLine("Products in Cart:");
                foreach (var item in groupedCart)
                {
                    Console.WriteLine($"{item.Quantity} x {item.ItemName}, Price: {currency} {Math.Round(item.TotalPrice - (item.TotalPrice * discount / 100)),2}");
                }
                Console.WriteLine($"Total Price {currency} {totalCartPrice}");
            }
            else
            {
                Console.WriteLine("Products in Cart:");
                foreach (var item in groupedCart)
                {
                    Console.WriteLine($"{item.Quantity} x {item.ItemName}, Price: {currency}{Math.Round(item.TotalPrice - (item.TotalPrice * discount / 100)), 2}");
                }
                Console.WriteLine($"Total Price {currency}{totalCartPrice - (totalCartPrice * discount / 100)}");
            }
            
        }
        private static List<Product> ConvertCartPrices(List<Product> cart, Func<double, double> conversionFunc)
        {
            return cart.Select(product => new Product(
                product.itemName,        
                conversionFunc(product.price) 
            )).ToList();
        }            
        private static double ConvertSEKToUSD(double sekPrice)
        {
            const double SEKtoUSDRate = 0.098;
            return Math.Round((sekPrice * SEKtoUSDRate), 2);
        }
        private static double ConvertSEKToEUR(double sekPrice)
        {
            const double SEKtoEURRate = 0.088;
            return Math.Round((sekPrice * SEKtoEURRate), 2);
        }

    }   
}

