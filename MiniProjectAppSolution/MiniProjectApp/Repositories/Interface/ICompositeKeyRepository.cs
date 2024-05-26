namespace MiniProjectApp.Repositories.Interface
{
    public interface ICompositeKeyRepository  <K,T> where T : class
    {
        public Task<T> Add(T item);
        public Task<T> DeleteByKey(K key1, K key2);
        public Task<T> Update(T item);
        public Task<T> GetByKey(K key1, K key2);
        public Task<IEnumerable<T>> GetAll();

    }
}
