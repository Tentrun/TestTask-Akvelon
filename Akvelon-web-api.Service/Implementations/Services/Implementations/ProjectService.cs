using Akvelon_web_api.DAL.Interfaces.BaseInterfaces;
using Akvelon_web_api.Domain.Entity;
using Akvelon_web_api.Service.Interfaces.BaseInterfaces;
using Microsoft.Extensions.Logging;

namespace Akvelon_web_api.Service.Implementations;

public class ProjectService : GenericService<ProjectEntity>, IProjectService
{
    private readonly IGenericRepository<ProjectEntity> _repository;

    public ProjectService(IGenericRepository<ProjectEntity> repository) : base(repository)
    {
        _repository = repository;
    }
}