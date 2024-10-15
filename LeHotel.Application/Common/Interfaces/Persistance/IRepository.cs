using System.Linq.Expressions;

namespace LeHotel.Application.Common.Interfaces.Persistance
{
    public interface IRepository<TEntity, TId> 
        where TEntity : class
        where TId : class
    {
        Task<IQueryable<TEntity>> GetAll(Enum? findOptions = null);
        Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null);
        Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, Enum? findOptions = null);
        Task<IEnumerable<TEntity>> SkipAndTakeSpecific(int skip, int take, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task Add(TEntity entity);
        Task AddMany(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task DeleteMany(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
    }
}
