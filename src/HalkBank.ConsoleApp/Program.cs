// See https://aka.ms/new-console-template for more information
using HalkBank.ConsoleApp.Accounting;

Console.WriteLine("Hello, World!");


Customer c = new Customer("324324", "324324");
//c.FirstName = "Ali";
//Console.WriteLine(c.FullName);
//c.LastName = "sadsadsa";
//c.Accounts.Add(new Account()); // invalid kullanım

//AccountNumberGenerator ac = new AccountNumberGenerator();
//string accountNumber = ac.Generate();
c.AddNewAccount("234234332432"); // müşteriye yeni bir hesap ekleme

Account ac = new Account("32432432", "TL");
ac.WithDraw(100);



//c.SetName("ali");
