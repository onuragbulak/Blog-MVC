using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MvcProjesi.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T: BaseClass
    {
        internal MvcProjesiContext db = null;
        internal DbSet<T> table = null;

        public GenericRepository()
        {
            db = new MvcProjesiContext();
            table = db.Set<T>();
        }

        public GenericRepository(MvcProjesiContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return table.Where(expression);
        }
        public void Insert(T obj)
        {
            obj.Validate();
            table.Add(obj);
        }
        public void Delete(T obj)
        {
            obj.Validate();
            var existing = table.Find(obj);
            table.Remove(existing);
        }
        public void Edit(T obj)
        {
            obj.Validate();
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}