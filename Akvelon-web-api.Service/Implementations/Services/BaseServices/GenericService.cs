using Akvelon_web_api.DAL;
using Akvelon_web_api.DAL.Interfaces.BaseInterfaces;
using Akvelon_web_api.DAL.Interfaces.Implementations;
using Akvelon_web_api.Domain.Response;
using Akvelon_web_api.Service.Interfaces.BaseInterfaces;
using Microsoft.Extensions.Logging;

namespace Akvelon_web_api.Service.Implementations;

public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;

    protected GenericService(IGenericRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IBaseResponse<TEntity>> FindById(int id)
    {
        List<string> errors = new();
        var response = new BaseResponse<TEntity>
        {
            Data = await _repository.FindById(id)
        };

        // If data is missing assert error (text)
        if (response.Data == null)
        {
            errors.Add(ErrorsList.NotFounded);
        }
        response.BrokenRules = errors;

        // If errors list is empty returning positive result
        if (response.BrokenRules.Count == 0)
        {
            response.IsSuccessful = true;
        }
        return response;
    }

    public async Task<IBaseResponse<IEnumerable<TEntity>>> Get()
    {
        List<string> errors = new();
        var response = new BaseResponse<IEnumerable<TEntity>>
        {
            Data = await _repository.Get()
        };

        // If data is missing assert error (text)
        if (response.Data == null)
        {
            errors.Add(ErrorsList.NotFounded);
        }
        response.BrokenRules = errors;
        
        // If errors list is empty returning positive result
        if (response.BrokenRules.Count == 0)
        {
            response.IsSuccessful = true;
        }
        return response;
    }

    public async Task<IBaseResponse<TEntity>> Create(TEntity entity)
    {
        var response = new BaseResponse<TEntity>
        {
            IsSuccessful = await _repository.Create(entity),
            Data = entity
        };

        if (!response.IsSuccessful)
        {
            throw new Exception("Something wrong...");
        }

        return response;
    }

    public async Task<IBaseResponse<bool>> Remove(int id)
    {
        List<string> errors = new();
        var response = new BaseResponse<bool>();
        
        // Finding removing entity by id
        var entity = await _repository.FindById(id);

        if (entity != null)
        {
            // Removing founded entity and returning operation status
            response.IsSuccessful = await _repository.Remove(entity);
            return response;
        }
        
        // If data is missing assert error (text)
        errors.Add(ErrorsList.NotFounded);
        response.BrokenRules = errors;
        
        return response;
    }

    public async Task<IBaseResponse<TEntity>> Update(int id, TEntity entity)
    {
        var response = new BaseResponse<TEntity>
        {
            IsSuccessful = await _repository.Update(entity),
            Data = entity
        };
        
        return response;
    }
}