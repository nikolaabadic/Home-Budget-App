using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HomeBudget.Data.Implementation
{
    public class RepositoryBelonging : IRepositoryBelonging
    {
        private BankContext context;
        public RepositoryBelonging(BankContext context)
        {
            this.context = context;
        }
        public void Add(Belonging belonging)
        {
            context.Belongings.Add(belonging);
        }

        public void Delete(Belonging belonging)
        {
            context.Belongings.Remove(belonging);
        }

        public Belonging FindByID(int id, params int[] ids)
        {
            return context.Belongings.Find(id,ids[0],ids[1],ids[2],ids[3]);
        }

        public List<Belonging> GetAll()
        {
            return context.Belongings.ToList();
        }

        public List<Belonging> Search(Expression<Func<Belonging, bool>> pred)
        {
            return context.Belongings.Where(pred).ToList();
        }
    }
}
