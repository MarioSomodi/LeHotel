using System.Linq.Expressions;

namespace LeHotel.Application.Common.Interfaces.Persistance
{
    public interface IDataStore<TEntity> 
        where TEntity : class
    {
        IQueryable<TEntity> GetAll(Enum? findOptions = null);
        TEntity? FindOne(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null);
        void Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SkipAndTakeSpecific(int skip, int take, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    }
}
