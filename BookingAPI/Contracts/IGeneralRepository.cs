using System.Linq.Expressions;

namespace BookingAPI.Contracts
{
    public interface IGeneralRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        public IQueryable<T> Query();
    }
}
