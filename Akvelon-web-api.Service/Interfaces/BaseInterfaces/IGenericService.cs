using Akvelon_web_api.Domain.Response;
using Akvelon_web_api.Service.Implementations;

namespace Akvelon_web_api.Service.Interfaces.BaseInterfaces;

public interface IGenericService<TEntity> where TEntity : class
{
    Task<IBaseResponse<TEntity>> FindById(int id);
    Task<IBaseResponse<IEnumerable<TEntity>>> Get();
    Task<IBaseResponse<TEntity>> Create(TEntity entity);
    Task<IBaseResponse<bool>> Remove(int id);
    Task<IBaseResponse<TEntity>> Update(int id, TEntity entity);
}