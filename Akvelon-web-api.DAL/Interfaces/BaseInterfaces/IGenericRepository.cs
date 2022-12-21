using System.Linq.Expressions;

namespace Akvelon_web_api.DAL.Interfaces.BaseInterfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<bool> Create(TEntity entity);
    Task<TEntity> FindById(int id);
    Task<List<TEntity>> Get();
    Task<List<TEntity>> Get(Func<TEntity, bool> predicate);
    Task<bool> Remove(TEntity entity);
    Task<bool> Update(TEntity entity);
    Task<List<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<List<TEntity>> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
}