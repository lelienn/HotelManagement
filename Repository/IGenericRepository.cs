using System.Linq.Expressions;

namespace HotelManagement.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    }
}
