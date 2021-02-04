using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepositoryUser User { get; set; }
        public IRepositoryAccount Account { get; set; }
        public IRepositoryPayment Payment { get; set; }
        public IRepositoryCategory Category { get; set; }
        public IRepositoryBelonging Belonging { get; set; }
        void Commit();
    }
}
