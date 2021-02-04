using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data
{
    public interface IRepositoryBelonging : IRepository<Belonging>
    {
        public List<Belonging> Search(Expression<Func<Belonging,bool>> pred);
    }
}
