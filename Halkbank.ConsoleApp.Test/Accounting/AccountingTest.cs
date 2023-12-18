using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Halkbank.ConsoleApp.Test.Accounting
{

  // Her test kodu çalıştırılırken tekrar tekrar Accouting test sınıfı instance alındığı için referans değerler tekrar ilk durumlarına dönüyor.
  public class AccountingTest
  {

    [Fact] // Fact attribute ile test methodu dışarıdan parametre almadığı durumlarda test ediliyor.
    // parametreik çalıştığımız durumlarda [Teory] bu attrinbute kullanılıyor.
    public void WhenAccountClosed()
    {
      // Arrange işlemi test edieceğimiz sınıfı hazırlama işlemi
      // Setup Data hazırlık işlemi
      HalkBank.ConsoleApp.Accounting.Account account = new HalkBank.ConsoleApp.Accounting.Account("324324324"
        , "TL");
      //account.Close("Test Purposes");
      account.SetBalance(5000);
    
      // Act yani test etmek için test edilecek methodun çağırımı
      account.WithDraw(1000);

      // Assert işlemi

      // Assert.True(!account.Closed); // bu para çekme işlemini yapabilmek için hesabın kapalı yada bloke olmaması lazım.

      Assert.Equal(4000, account.Balance);
      
    }

    // gönderilen parametrelere göre yapılan işlem miktarı üzerinden ne kadarlık bir balance yani hesap bakiyesi kalacağınız parametrik olarak test etmek istiyoruz
    // 3 farklı parametre ile para çekme işlemi testi.
    [Theory]
    [InlineData(10000,50000)]
    [InlineData(15000, 50000)]
    [InlineData(35000, 50000)]
    public void DailyLimitCheck(decimal amount, decimal balance)
    {

      // Arrange işlemi test edieceğimiz sınıfı hazırlama işlemi
      // Setup Data hazırlık işlemi
      HalkBank.ConsoleApp.Accounting.Account account = new HalkBank.ConsoleApp.Accounting.Account("324324324"
        , "TL");
      //account.Close("Test Purposes");
      account.SetBalance(balance);


      // bakimizi 50,000 olarak setup ettiğimizde çıkacak sonuçları parametre olarak yazıyoruz.


      // Act yani test etmek için test edilecek methodun çağırımı
      account.WithDraw(amount);

      //account.WithDraw(10000);
      //account.WithDraw(15000);
      //account.WithDraw(7000);

      // Assert işlemi
      Assert.True(account.Balance == (balance - amount)); // testen geçer.


    }


    [Fact] // Gün içerisinde 3 defa para çekme işlemi testi.
           // 32.000 tlilik günk bir işlem yaptığımız dönecek olan sonucu bakalım.
           // 3 adet örneklem alınarak test method çalıştırılır.
    public void DailyLimitCheck3WithDraw()
    {

      // Arrange işlemi test edieceğimiz sınıfı hazırlama işlemi
      // Setup Data hazırlık işlemi
      HalkBank.ConsoleApp.Accounting.Account account = new HalkBank.ConsoleApp.Accounting.Account("324324324"
        , "TL");
      //account.Close("Test Purposes");
      account.SetBalance(50000);

      // bakimizi 50,000 olarak setup ettiğimizde çıkacak sonuçları parametre olarak yazıyoruz.

      account.WithDraw(10000);
      account.WithDraw(15000);
      account.WithDraw(7000);

      // Assert işlemi
      Assert.True(account.Balance == 18000); // testen geçer.


    }
  }
}
