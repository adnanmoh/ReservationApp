using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Reservation_APIs.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        IQueryable<T> GetAllIQueryable();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression);
        Task<T?> GetById(int id);
        Task<T?> GetById(params object[] keys);
       
        Task<T?> Find(Expression<Func<T, bool>> expression);
        Task<bool?> FindBool(Expression<Func<T, bool>> expression);

        Task<bool> ObjExists(int id);
        Task<bool> ObjExists(params object[] keys);
        Task<T> Add(T obj);
        Task<int> SaveChangesAsync();
        T Update(T obj);
        bool Remove(T obj);
    }
}
