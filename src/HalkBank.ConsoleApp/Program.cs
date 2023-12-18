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
c.AddNewAccount("234234332432","TL"); // müşteriye yeni bir hesap ekleme

Account ac = new Account("32432432", "TL");
ac.WithDraw(100);


//var transaction = new AccountTransaction("32432", 500, TransactionType.WithDraw);
//ac.Transactions.Add(transaction);


//c.SetName("ali");


#region LawOfDemetter

Customer customer = new Customer("Ali", "Tan");
//customer.Accounts.Add(new Account("345354", "TL"));

// bir nesne üzerinden nesne ilişikileri (associations) üzerinden diğer alt nesnelere erişip kontrolsüz işlemler yapabildik. bu durum law of demetter yasasına uygun bir geliştirme şekli değildir. Bu sebeple nesneler arasındaki bu ilişkileri sınırlandırmalıyız.
//customer.Accounts.FirstOrDefault(x => x.AccountNumber == "345354").Transactions.Add(new AccountTransaction("345354", 45, TransactionType.WithDraw));

customer.GetCurrentAccount("343545").WithDraw(500);

// inDirection yapısının kullanılması
var alltranscations = customer.GetCurrentAccount("343545").Transactions.ToList();
var filteredTransactions = customer.GetTransactionAt("343545",DateTime.Now.AddDays(-7), DateTime.Now);


#endregion
