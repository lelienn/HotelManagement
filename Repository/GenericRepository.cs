using System.Collections.Generic;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelManagement.Repository
{
    public class GenericRepasitory<T> : IGenericRepository<T> where T : class
    {
        private HotelManagementContext _context;
        private DbSet<T> table = null;

        public GenericRepasitory(HotelManagementContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Update(obj);
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }
    }
}
