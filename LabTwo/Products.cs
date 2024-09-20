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
        public string GetItemName()
        {
            string a = itemName.ToString();
            return a;
        }

    }
}
