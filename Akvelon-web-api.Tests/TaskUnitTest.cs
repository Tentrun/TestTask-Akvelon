namespace Akvelon_web_api.Tests;

public class TaskUnitTest
{
    [Fact]
    public async void GetTasksByProject()
    {
        var helper = new TestHelper();
        var projectService = helper.GetInMemoryProjectService();
        var taskService = helper.GetInMemoryTaskService();

        var project = helper.GetMockProject();
        project.Id = 5;
        
        await projectService.Create(project);

        foreach (var task in helper.GetMockTasks())
        {
            await taskService.Create(task);
        }

        var result = await taskService.Get();
        var tasks = result.Data.Where(t => t.ProjectId == 5);
        
        Assert.NotNull(tasks);
        Assert.Equal(2, tasks.Count());
    }
}