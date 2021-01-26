using HomeBudget.Data.Implementation;
using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Data.UnitOfWork
{
    public class BankUnitOfWork : IUnitOfWork
    {
        private BankContext context;

        public BankUnitOfWork(BankContext context)
        {
            this.context = context;
            User = new RepositoryUser(context);
            Account = new RepositoryAccount(context);
            Payment = new RepositoryPayment(context);
            Category = new RepositoryCategory(context);
        }

        public IRepositoryUser User { get ; set; }
        public IRepositoryAccount Account { get; set; }
        public IRepositoryPayment Payment { get; set; }
        public IRepositoryCategory Category { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
