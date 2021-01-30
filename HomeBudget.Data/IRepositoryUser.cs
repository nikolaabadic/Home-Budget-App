using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data
{
    public interface IRepositoryUser : IRepository<User>
    {
        public User GetByUsernameAndPinCode(User user);
    }
}
