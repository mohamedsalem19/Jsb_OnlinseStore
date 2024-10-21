namespace OnlineStore
{
    public interface IGRepository<T> where T : class
    {

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}

