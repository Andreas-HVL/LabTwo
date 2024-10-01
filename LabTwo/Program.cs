using LabTwo;
using System;
using System.Runtime.InteropServices;
using System.Text.Json;

/// Serialise User/password, som private i customer class, send til manager.
/// 

//Webstore
Manager.UserListCreator();
Manager.ProductListCreator();
MenuOptions program = new MenuOptions();

ConsoleKeyInfo cki;
bool isLoggedIn = false;
bool stayInMenu = true;
Customer? CurrentCustomer = null;

do
{
    MenuWriter.Welcome();
    do
    {
        cki = Console.ReadKey(true);
    }
    while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3');
    switch (cki.KeyChar)
    {
        case '1':
            Console.Clear();
            isLoggedIn = program.Login(out CurrentCustomer, out isLoggedIn);
            if (isLoggedIn)
            {
                stayInMenu = false;
            }
            break;
        case '2':
            program.CreateCustomer();
            break;
        case '3':
            program.CustomerList();
            break;
    }
} while (stayInMenu);
while (isLoggedIn)
{
    Console.Clear();
    MenuWriter.LoggedInMenu();
    do
    {
        cki = Console.ReadKey(true);
    }
    while (cki.KeyChar != '1' && cki.KeyChar != '2' && cki.KeyChar != '3' && cki.KeyChar != '4');
    switch(cki.KeyChar)
    {
        case '1':
            Console.Clear();
            CurrentCustomer.PrintInfo();
            MenuWriter.AnyKeyReturn();
            break;
        case '2':
            Console.Clear();
            CurrentCustomer.PrintCart();
            MenuWriter.AnyKeyReturn();
            break;
        case '3':
            Console.Clear();
            Product.AddToCart(CurrentCustomer);
            break;
        case '4':
            Console.Clear();
            MenuWriter.ExitMenu();
            isLoggedIn = false;
            break;
    }
}

/// 1: View user info
/// 2: View Cart
/// 3: Add/remove from cart
/// 4: Checkout




