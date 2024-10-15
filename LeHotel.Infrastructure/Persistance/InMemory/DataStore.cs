using LeHotel.Application.Common.Interfaces.Persistance;
using System.Linq.Expressions;

namespace LeHotel.Infrastructure.Persistance.InMemory
{
    public class DataStore<TEntity> : IDataStore<TEntity>
        where TEntity : class
    {
        private readonly List<TEntity> _entities;

        public DataStore(IEnumerable<TEntity>? initialData = null)
        {
            _entities = initialData?.ToList() ?? new List<TEntity>();
        }

        public IQueryable<TEntity> GetAll(Enum? findOptions = null)
        {
            return _entities.AsQueryable();
        }

        public TEntity? FindOne(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null)
        {
            return _entities.AsQueryable().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null)
        {
            return _entities.AsQueryable().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            var existing = _entities.FirstOrDefault(e => e.Equals(entity));
            if (existing != null)
            {
                _entities.Remove(existing);
                _entities.Add(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = _entities.AsQueryable().Where(predicate).ToList();
            foreach (var entity in entitiesToDelete)
            {
                _entities.Remove(entity);
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AsQueryable().Any(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AsQueryable().Count(predicate);
        }

        public IEnumerable<TEntity> SkipAndTakeSpecific(int skip, int take, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _entities.AsQueryable();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.Skip(skip).Take(take).ToList();
        }
    }
}
