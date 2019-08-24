using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcProjesi.Repositories
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression);

        void Insert(T obj);
        void Delete(T obj);
        void Edit(T obj);

        void Dispose();
    }
}