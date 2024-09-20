using LabTwo;
//Webstore
Customer john = new("John", "bob");
Product Apple = new("Apple", 20);
john.PrintInfo();
john.CartItemAdd(Apple);
john.CartItemAdd(Apple);
john.CartItemAdd(Apple);
john.PrintCart();
Console.ReadKey();
