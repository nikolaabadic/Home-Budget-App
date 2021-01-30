using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data
{
    public interface IRepositoryPayment : IRepository<Payment>
    {
        public List<Payment> Search(Expression<Func<Payment, bool>> pred);
    }
}
