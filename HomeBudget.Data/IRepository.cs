using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Data
{
    public interface IRepository<T>
    {
        void Add(T param);
        List<T> GetAll();
        T FindByID(params int[] id);
        void Delete(T param);
    }
}
