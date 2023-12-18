using HalkBank.ConsoleApp.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalkBank.ConsoleApp.Services
{

  // DDD- Domain Driven Design, Domain Centric.
  // Data Centric 
  public class AccountRepo
  {
    public void Save(Account account)
    {

    }
  }

  // Account Behavior işlemleri yönetimi servis üzerinden Account class da herhangi bir davranış olmayacaktı.
  // Account class sadece propertylerden oluşacaktı.
  // bu tarz sınıfın 2 veya daha fazla sınıfa bölünüp yönetildiği durumlarda Account sınıfı Anemic Domain ismini alıcaktı. Anemic Domain herhangi bir davranış göstermeyip sadece içerisinde propertyler ile veri tutumamızı sağlayan data structure olarak classların kullanıldığı gelkiştirme şekli. Bu geliştirme şekli Data Centric Model olarak geçiyor. 
  public class AccountService
  {


    private readonly AccountRepo accountRepo = new AccountRepo();

    public void WithDraw(decimal amount, string accountNumber)
    {
      // Repodanm Find ettik
      Accounting.Account acc = new Accounting.Account(accountNumber, "TL");
      acc.WithDraw(amount);

      accountRepo.Save(acc);


      //if (acc.Closed)
      //  throw new Exception("Kapalı Hesaptan para çekilemez");

      //if (amount > acc.Balance)
      //  throw new Exception("Yetersiz bakiye");

      //// günlük para çekme limitini aştık mı ?

      //decimal dailyTotal = acc.Transactions
      //  .Where(x => x.TransactionAt.Date == DateTime.Now.Date && x.Type == (int)TransactionType.WithDraw)
      //  .Sum(x => x.Amount);

      //if ((dailyTotal + amount) > 30000)
      //  throw new Exception("Günlük para çekme limitini aştınız");


      acc.SetBalance(acc.Balance - amount); // Yeni balance

    }
  }
}
