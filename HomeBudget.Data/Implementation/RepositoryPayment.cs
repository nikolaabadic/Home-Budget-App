using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data.Implementation
{
    public class RepositoryPayment: IRepositoryPayment
    {
        private BankContext context;
        public RepositoryPayment(BankContext context)
        {
            this.context = context;
        }

        public void Add(Payment payment)
        {
            context.Payments.Add(payment);
        }

        public void Delete(Payment payment)
        {
            context.Payments.Remove(payment);
        }

        public Payment FindByID(int id, params int[] ids)
        {
            return context.Payments.Find(id);
        }

        public List<Payment> GetAll()
        {
            return context.Payments.ToList();
        }

        public List<Payment> Search(Expression<Func<Payment, bool>> pred)
        {
            return context.Payments.Where(pred).ToList();
        }
    }
}
