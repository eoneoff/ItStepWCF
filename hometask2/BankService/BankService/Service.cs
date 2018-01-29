using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BankService
{
    class Service : IBankService
    {
        //определение, существует ли в базе счет с данным номером
        public bool AccountExists(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                return db.accounts.Any(a => a.account == accountNo);
            }
        }

        //баналс по счету с данным номером
        public decimal Balance(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    return db.accounts.First(a=>a.account==accountNo).balance;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        //изменение баланса по счету
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void Change(int accountNo, decimal sum)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    accounts account = db.accounts.First(a=>a.account==accountNo);
                    transactions transaction = new transactions { accounts = account, transAmount = sum, transDate = DateTime.Now };
                    db.transactions.Add(transaction);
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        //создание счета
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void Create(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (!db.accounts.Any(a => a.account == accountNo))
                {
                    accounts account = new accounts { account = accountNo, balance = 0 };
                    db.accounts.Add(account);
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        //удаление счета
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void Delete(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    accounts account = db.accounts.First(a=>a.account==accountNo);
                    db.accounts.Remove(account);
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        //выписка движение по счету
        public IEnumerable<TransactionData> Movement(int accountNo)
        {
            using (AccountEntities db = new AccountEntities())
            {
                if (db.accounts.Any(a => a.account == accountNo))
                {
                    int accId = db.accounts.First(a => a.account == accountNo).id;
                    var movement = db.transactions.
                        Where(t => t.accId == accId).
                        OrderBy(t => t.transDate).ToList();

                    List<TransactionData> result = new List<TransactionData>();

                    foreach(var t in movement)
                    {
                        result.Add(new TransactionData { transDate = t.transDate, transAmount = t.transAmount });
                    }

                    return result;

                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }

}
