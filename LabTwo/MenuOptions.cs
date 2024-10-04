using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo
{
    // Class for storing the methods used to navigate the logged out Main Menu
    public class MenuOptions
    {
        public List<Customer> _customers;
        // Constructor for taking the list of registered customers to allow the program to run
        public MenuOptions()
        {
            _customers = Manager.LoadUsers().ToList();
        }
        public bool Login(out Customer? CurrentCustomer, out bool isLoggedIn)
        {
            string usernameInput = "";
            bool stayInLoginSubLoop = true; // Variable used to ensure Login-process continues until customer is correctly logged in, or chooses to leave
            isLoggedIn = false; // Variable used to navigate to the logged in part of the code
            bool stayInMenu = true; // Variable used to stay on the main menu, allowing the user to hop between different main menu options
            CurrentCustomer = null; // Variable used to store the logged in user for later use
            ConsoleKeyInfo cki; // Variable to take keypress inputs from the user

            // Do-While loop to ensure customer can attempt logging in several times over
            do
            {
                // Loop ensuring customer inputs a username that isn't null
                do
                {
                    Console.Clear();
                    Console.Write("Username: ");
                    usernameInput = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(usernameInput));

                // Parses through the list of customers, and looks for a valid match from the customer input
                CurrentCustomer = _customers.FirstOrDefault(c => c.Username.Equals(usernameInput, StringComparison.OrdinalIgnoreCase));
                if (CurrentCustomer != null)
                {
                    Console.Write("\nPassword: ");
                    string passwordInput = Console.ReadLine();
                    isLoggedIn = CurrentCustomer.Login(usernameInput, passwordInput);
                    if (isLoggedIn)
                    {
                        Console.WriteLine($"Welcome {CurrentCustomer.Username}");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Login failed. Incorrect username or password.");
                        Console.WriteLine("1: Return to Main Menu");
                        Console.WriteLine("2: Any key to try again");
                        cki = Console.ReadKey(true);
                        if (cki.KeyChar == '1')
                        {
                            stayInLoginSubLoop = false;
                        }
                        Console.Clear();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("User not found.");
                    Console.WriteLine("1: Return to Main Menu");
                    Console.WriteLine("2: Create user");
                    Console.WriteLine("Or press any other key to try again");
                    cki = Console.ReadKey(true);
                    if (cki.KeyChar == '1')
                    {
                        stayInLoginSubLoop = false;
                    } else if (cki.KeyChar == '2')
                    {
                        return CreateCustomer();
                    }
                    Console.Clear();
                }
            } while (!isLoggedIn && stayInLoginSubLoop);
            // On succesful login, leaves the login loop, and proceeds to logged-in main menu
            return isLoggedIn;
        }

        public bool CreateCustomer()
        {

            Console.Clear();
            ConsoleKeyInfo cki;
            Console.WriteLine("Select a username.");
            string newUserName = Console.ReadLine();

            // Parses through the list of customers, to either allow the user to progress with user-creation, or informs the user that a duplicate account exists
            if (!_customers.Any(c => c.Username.Equals(newUserName, StringComparison.OrdinalIgnoreCase)))
            { 
                Console.WriteLine("Select a password");
                string newPassword = Console.ReadLine();

                Console.WriteLine("Select Premium-Level");
                Console.WriteLine("1: Base");
                Console.WriteLine("2: Bronze");
                Console.WriteLine("3: Silver");
                Console.WriteLine("4: Gold");
                string premiumLevel = "";

                do
                {
                    cki = Console.ReadKey(true);
                }
                while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3' && cki.KeyChar != '4');
                switch (cki.KeyChar)
                {
                    case '1':
                        premiumLevel = "Base";
                        break;
                    case '2':
                        premiumLevel = "Bronze";
                        break;
                    case '3':
                        premiumLevel = "Silver";
                        break;
                    case '4':
                        premiumLevel = "Gold";
                        break;
                }
            
                Console.WriteLine();
                // Creates the customer as either a base customer or premium using ternary operator "?" (True) or ":" (False)
                // (if a then b else c) (a ? b : c)
                Customer newCustomer = premiumLevel == "Base"
                    ? new Customer(newUserName, newPassword, premiumLevel)   // Regular customer for Base level
                    : new PremiumCustomer(newUserName, newPassword, premiumLevel); // Premium customer for other levels

                _customers.Add(newCustomer);
                Manager.SaveCustomers(_customers.ToArray());
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Customer Already Exists. Try to log in instead.\n");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return false;
            }
            return true;
        }
        // Prints a list of all registered usernames
        public void CustomerList()
        {
            int i = 0;
            Console.Clear();
            foreach (Customer customer in _customers )
            {
                Console.WriteLine((i+1) + ": " + customer.Username);
                i++;
            }
        }
    }
}
