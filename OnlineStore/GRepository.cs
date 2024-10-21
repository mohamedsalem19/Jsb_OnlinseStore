using OnlineStore.Entities;

namespace OnlineStore
{
    public class GRepository<T> : IGRepository<T> where T : class
    {
        private readonly StoreDbContext _dbContext;

        public GRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Orders))
            {
                return (IReadOnlyList<T>)await _dbContext.Set<Orders>()
                    .Include(E => E.OrdersProducts)
                    .ThenInclude(x => x.Product).ToListAsync();
            }

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Orders))
            {
                return await _dbContext.Set<Orders>().Where(e => e.Id == id)
                    .Include(e => e.OrdersProducts)
                    .ThenInclude(x => x.Product).FirstOrDefaultAsync() as T;
            }

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }
    }

}
