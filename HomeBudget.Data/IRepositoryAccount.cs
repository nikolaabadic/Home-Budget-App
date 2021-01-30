using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data
{
    public interface IRepositoryAccount : IRepository<Account>
    {
        public List<Account> Search(Expression<Func<Account, bool>> pred);
    }
}
