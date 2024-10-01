using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo
{
    public class MenuOptions
    {
        public List<Customer> _customers;
        public MenuOptions()
        {
            _customers = Manager.LoadUsers().ToList();
        }
        public bool Login(out Customer? CurrentCustomer, out bool isLoggedIn)
        {
            string usernameInput = "";
            bool stayInLoginSubLoop = true;
            isLoggedIn = false;
            bool stayInMenu = true;
            CurrentCustomer = null;
            ConsoleKeyInfo cki;
            do
            {
                do
                {
                    Console.Clear();
                    Console.Write("Username: ");
                    usernameInput = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(usernameInput));
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
                    Console.WriteLine("2: Any key to try again");
                    cki = Console.ReadKey(true);
                    if (cki.KeyChar == '1')
                    {
                        stayInLoginSubLoop = false;
                    }
                    Console.Clear();
                }
            } while (!isLoggedIn && stayInLoginSubLoop);
            return isLoggedIn;
        }

        public bool CreateCustomer()
        {

            Console.Clear();
            ConsoleKeyInfo cki;
            Console.WriteLine("Select a username.");
            string newUserName = Console.ReadLine();
            
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
            if (cki.KeyChar == '1')
            {
                premiumLevel = "Base";
            }
            else if (cki.KeyChar == '2')
            {
                premiumLevel = "Bronze";
            }
            else if (cki.KeyChar == '3')
            {
                premiumLevel = "Silver";
            }
            else if (cki.KeyChar == '4')
            {
                premiumLevel = "Gold";
            }
            Console.WriteLine();
            if (!_customers.Any(c => c.Username.Equals(newUserName, StringComparison.OrdinalIgnoreCase)))
            {
                Customer newCustomer = new Customer(newUserName, newPassword, premiumLevel);
                _customers.Add(newCustomer);
                Manager.SaveCustomers(_customers.ToArray());
            }
            else
            {
                Console.WriteLine($"Customer Already Exists. try to log in instead.");
                return false;
            }
            return true;
        }
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
