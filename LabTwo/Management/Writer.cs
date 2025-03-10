﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabTwo.Models;

namespace LabTwo.Management
{
    // Class used to write out strings of text, to keep clutter in main low
    public class MenuWriter
    {
        public static void Welcome()
        {
            foreach (char c in "Welcome to the store \nPlease select from the below options")
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine("");
            Console.WriteLine("1: Login \n2: Create User \n3: View all Customers \n4: Close store");

        }
        public static void CustomerList(Customer[] customers)
        {
            int i = 0;
            foreach (Customer customer in customers)
            {
                Console.WriteLine(i + 1 + " " + customer.Username);
                i++;
            }
        }
        public static void LoggedInMenu()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Select from the below options");
            Console.WriteLine("1: View user info");
            Console.WriteLine("2: View cart");
            Console.WriteLine("3: Add items to cart");
            Console.WriteLine("4: Logout");
            Console.WriteLine("5: Add Products");
            Console.WriteLine("6: Remove Products");
            Console.WriteLine("7: Checkout");
        }
        public static void AnyKeyReturn()
        {
            Console.WriteLine("Press any key to return to Main Menu");
            Console.ReadKey();
        }
        public static void ExitMenu()
        {
            Console.WriteLine("Processing Payment");
            foreach (char c in ".......")
            {
                Console.Write(c);
                Thread.Sleep(200);
            }
            Console.WriteLine("\nThanks for shopping, goodbye!");
            Console.WriteLine("Please press any key to close the store");
            Console.ReadKey();
        }
    }
}
