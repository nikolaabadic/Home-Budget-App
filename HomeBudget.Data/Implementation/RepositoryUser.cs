using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data.Implementation
{
    public class RepositoryUser : IRepositoryUser
    {
        private BankContext context;
        public RepositoryUser(BankContext context)
        {
            this.context = context;
        }
        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public User FindByID(int id, params int[] ids)
        {
            return context.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }
        
        public User GetByUsernameAndPinCode(User user)
        {
            return context.Users.Single(u => u.Username == user.Username && u.PINCode == user.PINCode);
        }

        

    }
}
