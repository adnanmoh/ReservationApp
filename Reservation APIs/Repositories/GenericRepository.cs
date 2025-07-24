using Microsoft.EntityFrameworkCore;
using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using System.Linq.Expressions;

namespace Reservation_APIs.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ReservationAppContext context;

        public GenericRepository(ReservationAppContext context)
        {
            this.context = context;
        }

        public async Task<T> Add(T obj)
        {
            var result = await context.Set<T>().AddAsync(obj);

            return result.Entity;
        }

        public async Task<T?> Find(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<bool?> FindBool(Expression<Func<T, bool>> expression)
        {

            return (await context.Set<T>().FirstOrDefaultAsync(expression) != null); 
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().Where(expression).ToListAsync();
        }

        

        public IQueryable<T> GetAllIQueryable()
        {
            return context.Set<T>().AsQueryable();
        }

        public async Task<T?> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetById(params object[] keys)
        {
            return await context.Set<T>().FindAsync(keys);
        }

        public async Task<bool> ObjExists(int id)
        {
            return await context.Set<T>().FindAsync(id) != null;
        }

        public async Task<bool> ObjExists(params object[] keys)
        {
            return await context.Set<T>().FindAsync(keys) != null;
        }

        public bool Remove(T obj)
        {
            return context.Set<T>().Remove(obj) != null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public T Update(T obj)
        {
            var res = context.Set<T>().Update(obj);
            return res.Entity;
        }
    }
}
