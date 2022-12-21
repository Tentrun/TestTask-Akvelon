using Akvelon_web_api.DAL;
using Akvelon_web_api.DAL.Interfaces.BaseInterfaces;
using Akvelon_web_api.DAL.Interfaces.Implementations;
using Akvelon_web_api.Domain.Entity;
using Akvelon_web_api.Domain.Enum;
using Akvelon_web_api.Service.Implementations;
using Akvelon_web_api.Service.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Akvelon_web_api.Tests;

    public class TestHelper
    {
        private readonly ApplicationDbContext dbContext;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "TestDbInMemory");
            builder.EnableSensitiveDataLogging(true);
            var dbContextOptions = builder.Options;

            dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        public IGenericRepository<ProjectEntity> GetInMemoryProjectRepository()
        {
            return new GenericRepository<ProjectEntity>(dbContext);
        }

        public ProjectService GetInMemoryProjectService()
        {
            return new ProjectService(GetInMemoryProjectRepository());
        }
        
        public IGenericRepository<TaskEntity> GetInMemoryTaskRepository()
        {
            return new GenericRepository<TaskEntity>(dbContext);
        }

        public TaskService GetInMemoryTaskService()
        {
            return new TaskService(GetInMemoryTaskRepository());
        }

        public IEnumerable<ProjectEntity> GetMockProjects()
        {
            return new List<ProjectEntity>()
            {
                { new ProjectEntity(){ Id = 1, StartDate = DateTime.Now, CompleteDate = DateTime.Now.AddDays(3), ProjectName = "TestProject1", Priority = 1, Status = ProjectStatusEnum.Active} },
                { new ProjectEntity(){ Id = 2, StartDate = DateTime.Now.AddHours(3), ProjectName = "TestProject2", Priority = 5, Status = ProjectStatusEnum.NotStarted} },
            };
        }
        
        public IEnumerable<TaskEntity> GetMockTasks()
        {
            return new List<TaskEntity>()
            {
                { new TaskEntity(){ Id = 1, Priority = 5, Status = TaskStatusEnum.ToDo, TaskName = "FirstTestTask", TaskDescription = "You need to make it right now", ProjectId = 5} },
                { new TaskEntity(){ Id = 2, Priority = 1, Status = TaskStatusEnum.ToDo, TaskName = "SecondTestTask", TaskDescription = "You need to make it later", ProjectId = 5} },
            };
        }

        public ProjectEntity GetMockProject()
        {
            return new ProjectEntity()
            {
                Id = 2, 
                StartDate = DateTime.Now, 
                CompleteDate = DateTime.Now.AddDays(3), 
                ProjectName = "TestProject2", 
                Priority = 3, 
                Status = ProjectStatusEnum.Active,
            };
        }
    }