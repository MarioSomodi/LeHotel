using LeHotel.Application.Common.Interfaces.Persistance;
using System.Linq.Expressions;

namespace LeHotel.Infrastructure.Persistance.InMemory.Repositories
{
    public class MemoryBaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
        where TId : class
    {
        private readonly IDataStore<TEntity> _dataStore;

        public MemoryBaseRepository(IDataStore<TEntity> dataStore) {
            _dataStore = dataStore;
        }

        public async Task Add(TEntity entity)
        {
            _dataStore.Add(entity);
        }

        public async Task AddMany(IEnumerable<TEntity> entities)
        {
            _dataStore.AddMany(entities);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dataStore.Any(predicate);  
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dataStore.Count(predicate);
        }

        public async Task Delete(TEntity entity)
        {
            _dataStore.Delete(entity);
        }

        public async Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            _dataStore.DeleteMany(predicate);
        }

        public async Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null)
        {
            return _dataStore.Find(predicate, findOptions);
        }

        public async Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null)
        {
            return _dataStore.FindOne(predicate, findOptions);
        }

        public async Task<IQueryable<TEntity>> GetAll(Enum? findOptions = null)
        {
            return _dataStore.GetAll(findOptions);
        }

        public Task<IEnumerable<TEntity>> SkipAndTakeSpecific(int skip, int take, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            throw new NotImplementedException();
        }

        public async Task Update(TEntity entity)
        {
            _dataStore.Update(entity);
        }
    }
}
