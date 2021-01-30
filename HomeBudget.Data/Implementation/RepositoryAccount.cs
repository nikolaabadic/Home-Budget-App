using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public Account FindByID(int id,params int[] ids)
        {
            return context.Accounts.Find(id);
        }

        public List<Account> GetAll()
        {
            return context.Accounts.ToList();
        }

        public List<Account> Search(Expression<Func<Account,bool>> pred)
        {
            return context.Accounts.Where(pred).ToList();
        }
    }
}
