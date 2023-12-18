using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HalkBank.ConsoleApp.Accounting
{
  public class Customer
  {
    public Guid CustomerNumber { get; init; }
    public string FirstName { private get; init; }
    public string LastName { private get; init; }

    private List<Account> accounts = new List<Account>(); // custtomer account yönetmek için açtık.

    public IReadOnlyList<Account> Accounts => accounts;
    // code-defensing yapmak için Accounts kısmını sadece okunabilir bir proprty yaparak encapsulated ettik.
    // code-defensing kavramı geliştiricilerin hatalı kod kullanımları yapmasını engelleyen kodu gereksiz kullanımlara kapatan bir geliştirme şeklidir. 

    public string FullName { get
      {
        return $"{FirstName} {LastName}";
      } 
    }

    public DateTime DateOfBirth { get; set; }

    public Customer(string firstName,string lastName) // required alanları buraya neden yazarız. çünkü geliştiri setter değerini yanlışlıkla göndermeyi unutmasın diye yazıyor.
    {
      CustomerNumber = Guid.NewGuid();
      FirstName = firstName.Trim();
      LastName = lastName.Trim().ToUpper();
      //Customer c = new Customer("sadsad","sadsad");
      //c.FirstName = "wqeasd";
    }

    public void AddNewAccount(string accountNumber, string currency)
    {
      accounts.Add(new Account(accountNumber, currency));
    }

    //public void SetName(string name)
    //{
    //  //this.FirstName = name;
    //}

    //public void SetLastName(string lname)
    //{
    //  // bu değer sadece contructor içerisinde gönderilebilir.
    //  // this.LastName = lname;
    //}

    /// <summary>
    /// Güncel hesap üzerindeki belirli tarih aralarında hesap dökümü
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public IReadOnlyList<AccountTransaction> GetTransactionAt(string accountNumber,DateTime startDate, DateTime endDate)
    {
      var acc = GetCurrentAccount(accountNumber);

      return acc.Transactions.Where(x => x.TransactionAt.Date >= startDate.Date && x.TransactionAt.Date <= endDate.Date).ToList().AsReadOnly();

    }

    /// <summary>
    /// Şuan aktif hesap bilgisini döner.
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Account GetCurrentAccount(string accountNumber)
    {
      var account = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

      if (account is null)
        throw new Exception("Böyle bir hesap bulunamadı");

      return account;
    }



  }
}
