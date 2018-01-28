using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankService
{
    class Service : IBankService
    {
        public decimal Balance(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if(db.accounts.Any(a=>a.account==accountNo))
                {
                    return db.accounts.Find(accountNo).balance;
                }
                else
                    throw new ArgumentOutOfRangeException($"Счет № {accountNo} отсутствует в базе");
            }
        }

        public void Change(int accountNo, decimal sum)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    accounts account = db.accounts.Find(accountNo);
                    transactions transaction = new transactions { accounts = account, transAmount = sum, transDate = DateTime.Now };
                    db.transactions.Add(transaction);
                    db.SaveChanges();
                }
                else
                    throw new ArgumentOutOfRangeException($"Счет № {accountNo} отсутствует в базе");
            }
        }

        public void Create(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    accounts account = new accounts { account = accountNo, balance = 0 };
                    db.accounts.Add(account);
                    db.SaveChanges();
                }
                else
                    throw new ArgumentOutOfRangeException($"Счет № {accountNo} отсутствует в базе");
            }
        }

        public void Delete(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    accounts account = db.accounts.Find(accountNo);
                    db.accounts.Remove(account);
                    db.SaveChanges();
                }
                else
                    throw new ArgumentOutOfRangeException($"Счет № {accountNo} отсутствует в базе");
            }
        }

        public IEnumerable<TransactionData> Movement(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    var trans = db.transactions.Where(t => t.id == accountNo).OrderBy(t => t.transDate).Select(t => new TransactionData { transDate = t.transDate, transAmount = t.transAmount }).ToList();
                    return trans;
      
                }
                else
                    throw new ArgumentOutOfRangeException($"Счет № {accountNo} отсутствует в базе");
            }
        }
    }
}
