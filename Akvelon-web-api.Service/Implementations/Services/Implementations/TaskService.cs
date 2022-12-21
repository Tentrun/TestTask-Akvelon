using Akvelon_web_api.DAL.Interfaces.BaseInterfaces;
using Akvelon_web_api.Domain.Entity;

namespace Akvelon_web_api.Service.Implementations;

public class TaskService : GenericService<TaskEntity>
{
    private readonly IGenericRepository<TaskEntity> _repository;

    public TaskService(IGenericRepository<TaskEntity> repository) : base(repository)
    {
        _repository = repository;
    }
}