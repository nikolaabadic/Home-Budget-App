using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBudget.Data.Implementation
{
    public class RepositoryCategory : IRepositoryCategory
    {
        private BankContext context;
        public RepositoryCategory(BankContext context)
        {
            this.context = context;
        }
        public void Add(Category category)
        {
            context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            context.Categories.Remove(category);
        }

        public Category FindByID(int id, params int[] ids)
        {
            return context.Categories.Find(id);
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }
    }
}
