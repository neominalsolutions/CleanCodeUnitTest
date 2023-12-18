using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalkBank.ConsoleApp.Accounting
{
  public enum TransactionType
  {
    WithDraw,
    Deposit
  }

  public class AccountTransaction
  {
    public string AccountNumber { get; init; }

    public decimal Amount { get; init; } // işleme ait ödeme yada para çekme bilgisi.

    public int Type { get;  init; } // +, -

    public DateTime TransactionAt { get; private set; }

    public AccountTransaction(string accountNumber,decimal amount, TransactionType type)
    {
      AccountNumber = accountNumber;
      Type = (int)type;
      TransactionAt = DateTime.Now;
      Amount = amount;
    }

  }
}
