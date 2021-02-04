using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data
{
    public interface IRepositoryAdmin : IRepository<Admin>
    {
        public Admin Search(Expression<Func<Admin, bool>> pred);
        public Admin GetByUsernameAndPinCode(Admin admin);
    }
}
