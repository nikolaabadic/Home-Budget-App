using HomeBudget.Data.Implementation;
using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Data.UnitOfWork
{
    public class AdminUnitOfWork : IAdminUnitOfWork
    {
        private AdminContext context;
                
        public AdminUnitOfWork(AdminContext context)
        {
            this.context = context;
            Admin = new RepositoryAdmin(context);
        } 
        public IRepositoryAdmin Admin { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
