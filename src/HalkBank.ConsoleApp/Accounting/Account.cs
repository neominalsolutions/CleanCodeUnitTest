using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalkBank.ConsoleApp.Accounting
{
  public class Account
  {
    public string AccountNumber { get; init; }
    public decimal Balance { get; private set; }
    public string Currency { get;  init; }
    public string CloseReason { get; private set; }
    public bool Closed { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ClosedAt { get; private set; }

    // hesaba yapılan günlük işlemleri kontrol edeceğimiz sınıf.
    private List<AccountTransaction> transactions = new List<AccountTransaction>();

    //public Customer CurrentCustomer { get; set; } // bidirectional association çift taraflı ilişkilendirme yaklamışı law of demetter yasasına göre yanlış bir geliştirme tekniğidir. Nesneler arasındaki ilişkiler unidirectional assocication yapısını önerir. 


    public IReadOnlyList<AccountTransaction> Transactions { get; set; }

    public Account(string accountNumber, string currency)
    {
      AccountNumber = accountNumber;
      Balance = 0;
      Currency = currency;
      CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// Test için bakiyeyi belirli bir değerde ayarlamak için tanımladık.
    /// </summary>
    /// <param name="amount"></param>
    public void SetBalance(decimal amount)
    {
      Balance = amount;
    }

    // para çekme

    /// <summary>
    /// Test Case
    /// Günlük para çekme limiti 30.000 olsun
    /// Hesap kapalı iken para çekilemez
    /// hesap bakiyesi çekilecek miktardan düşük ise yetersiz bakiye hatası döndürelim
    /// </summary>
    /// <param name="amount">Çekilecek Tutar</param>
    public void WithDraw(decimal amount)
    {
      if (Closed)
        throw new Exception("Kapalı Hesaptan para çekilemez");

      if (amount > Balance)
        throw new Exception("Yetersiz bakiye");

      // günlük para çekme limitini aştık mı ?

      decimal dailyTotal = transactions
        .Where(x => x.TransactionAt.Date == DateTime.Now.Date && x.Type == (int)TransactionType.WithDraw)
        .Sum(x => x.Amount);

      if ((dailyTotal + amount) > 30000)
        throw new Exception("Günlük para çekme limitini aştınız");

      // buraya da yapılan işleme ait transaction ekleme kodu koyalım.

      var transaction = new AccountTransaction(this.AccountNumber, amount, TransactionType.WithDraw);
      transactions.Add(transaction);


      Balance -= amount;
    }

    // para yatırma
    public void Deposit(decimal amount)
    {
      Balance += amount;
    }

    public void Close(string closeReason)
    {
      CloseReason = closeReason;
      Closed = true;
      ClosedAt = DateTime.Now;
    }
  }
}
