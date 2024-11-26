using LabTwo.Functionality;
using LabTwo.Management;
using LabTwo.Models;
using System;
using System.Runtime.InteropServices;
using System.Text.Json;

try
{
    // Initialize runtime data
    Manager.LoadProducts();
    Manager.LoadUsers();
    
}
catch (Exception ex)
{
    Console.WriteLine("The program encountered a critical error during initialization and must shut down.");
    Console.WriteLine($"Error: {ex.Message}");
    Environment.Exit(1); // Force the program to exit with an error code
}

MenuOptions program = new MenuOptions(); // Instantiates a class of the MenuOptions to call different methods for running the program
bool isLoggedIn = false; // Variable used to navigate to the logged in part of the code
bool stayInMenu = true; // Variable used to stay on the main menu, allowing the user to hop between different main menu options
bool programRunning = true; // Variable to allow the user to log back out without closing the program
Customer? CurrentCustomer = null; // Variable used to store the logged in user for later use
ConsoleKeyInfo cki; // Variable to take keypress inputs from the user

// Loop keeping the program running until the user requests closing the program
do
{
    // Loop for before the user is logged in, letting the customer hop between different menu options
    do
    {
        Console.Clear();
        MenuWriter.Welcome();
        do
        {
            cki = Console.ReadKey(true);
        }
        while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3' && cki.KeyChar != '4');
        switch (cki.KeyChar)
        {
            case '1': // Login function
                Console.Clear();
                isLoggedIn = program.Login(out CurrentCustomer, out isLoggedIn);
                if (isLoggedIn)
                {
                    stayInMenu = false;
                }
                break;
            case '2': // Create Customer function
                program.CreateCustomer();
                break;
            case '3': // Prints the customer list
                program.CustomerList();
                MenuWriter.AnyKeyReturn();
                break;
            case '4': // Closes the program
                Console.Clear();
                Console.WriteLine("Thanks for stopping by. Hope to see you soon.");
                stayInMenu = false;
                programRunning = false;
                break;
        }
    } while (stayInMenu);
    // Loop for when the user is logged in, allowing hopping between different logged in menu options
    while (isLoggedIn)
    {
        Console.Clear();
        MenuWriter.LoggedInMenu();
        var menuChoice = InputReader.SingleKey(7);
        switch (menuChoice)
        {
            case '1': // Prints out the customer's user info
                Console.Clear();
                CurrentCustomer.PrintInfo();
                MenuWriter.AnyKeyReturn();
                break;
            case '2': // Prints out the customer's cart
                Console.Clear();
                CurrentCustomer.PrintCart();
                MenuWriter.AnyKeyReturn();
                break;
            case '3': // Adding items to the customers cart
                Console.Clear();
                Product.AddToCart(CurrentCustomer);
                break;
            case '4': // Logout function
                Console.Clear();
                isLoggedIn = false;
                break;
            case '5':
                Console.Clear();
                Manager.AddProduct();
                break;
            case '6':
                Console.Clear();
                Manager.RemoveProduct();
                break;
            case '7': // Make-Believe Checkout function, closing the program
                Console.Clear();
                MenuWriter.ExitMenu();
                isLoggedIn = false;
                programRunning = false;
                break;
        }
    }
} while (programRunning);