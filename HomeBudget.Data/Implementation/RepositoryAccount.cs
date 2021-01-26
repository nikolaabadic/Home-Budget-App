using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBudget.Data.Implementation
{
    public class RepositoryAccount : IRepositoryAccount
    {
        private BankContext context;
        public RepositoryAccount(BankContext context)
        {
            this.context = context;
        }
        public void Add(Account account)
        {
            context.Accounts.Add(account);
        }

        public void Delete(Account account)
        {
            context.Accounts.Remove(account);
        }

        public Account FindByID(params int[] id)
        {
            return context.Accounts.Find(id);
        }

        public List<Account> GetAll()
        {
            return context.Accounts.ToList();
        }
    }
}
