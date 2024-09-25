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
        
        static public void CartPrinter(List<Product> cart, int discount = 0)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            var groupedCart = cart.GroupBy(product => product.itemName)
                .Select(group => new
                {
                    ItemName = group.Key,
                    Quantity = group.Count(),
                    TotalPrice = group.Sum(product => product.price)
                });
            double totalCartPrice = groupedCart.Sum(item => item.TotalPrice);
            if (discount == 0)
            {
                Console.WriteLine("Products in Cart:");
                foreach (var item in groupedCart)
                {
                    Console.WriteLine($"{item.Quantity} x {item.ItemName}, Price: ${item.TotalPrice}");
                }
                Console.WriteLine($"Total Price ${totalCartPrice}");
            }
            else
            {
                Console.WriteLine("Products in Cart:");
                foreach (var item in groupedCart)
                {
                    Console.WriteLine($"{item.Quantity} x {item.ItemName}, Price: ${item.TotalPrice - (item.TotalPrice * discount / 100)}");
                }
                Console.WriteLine($"Total Price ${totalCartPrice - (totalCartPrice * discount / 100)}");
            }
            
        }
    }   
}

