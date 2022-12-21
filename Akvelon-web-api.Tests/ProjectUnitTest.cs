using Akvelon_web_api.Domain.Entity;
using Akvelon_web_api.Domain.Enum;


namespace Akvelon_web_api.Tests;

public class ProjectUnitTest
{
    [Fact]
    public async void CreateAndRead_Project()
    {
        var helper = new TestHelper();
        var service = helper.GetInMemoryProjectService();
        
        await service.Create(helper.GetMockProject());

        var result = await service.FindById(2);
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Data.Id);
        Assert.Equal("TestProject2", result.Data.ProjectName);
        Assert.Equal(3, result.Data.Priority);
        Assert.Equal(ProjectStatusEnum.Active, result.Data.Status);
    }

    [Fact]
    public async void GetAll_Projects()
    {
        var helper = new TestHelper();
        var service = helper.GetInMemoryProjectService();

        var projects = helper.GetMockProjects();

        foreach (var project in helper.GetMockProjects())
        {
            await service.Create(project);
        }

        var result = await service.Get();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Data.Count());
    }

    [Fact]
    public async void Remove_Project()
    {
        var helper = new TestHelper();
        var service = helper.GetInMemoryProjectService();
        
        await service.Create(helper.GetMockProject());
        
        var result = await service.Remove(2);
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async void Edit_Project()
    {
        var helper = new TestHelper();
        var service = helper.GetInMemoryProjectService();

        await service.Create(helper.GetMockProject());

        var projectToEdit = new ProjectEntity()
        {
            Id = 4,
            Priority = 10,
            StartDate = DateTime.Now.AddDays(30),
            CompleteDate = DateTime.Now.AddDays(60),
            ProjectName = "EditedProject",
            Status = ProjectStatusEnum.Completed
        };

        var result = await service.Get();
    }

    [Fact]
    public async void GetById_Project()
    {
        var helper = new TestHelper();
        var service = helper.GetInMemoryProjectService();

        await service.Create(helper.GetMockProject());

        var result = await service.FindById(2);
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Data.Id);
        Assert.Equal("TestProject2", result.Data.ProjectName);
        Assert.Equal(3, result.Data.Priority);
        Assert.Equal(ProjectStatusEnum.Active, result.Data.Status);
    }
}