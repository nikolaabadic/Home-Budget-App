using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public User FindByID(params int[] id)
        {
            return context.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }
    }
}
