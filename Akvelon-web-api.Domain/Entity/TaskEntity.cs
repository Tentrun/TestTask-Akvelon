using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Akvelon_web_api.Domain.Enum;

namespace Akvelon_web_api.Domain.Entity;

public class TaskEntity
{
    public int Id { get; set; }
    
    public string TaskName { get; set; }
    
    public string TaskDescription { get; set; }

    public TaskStatusEnum Status { get; set; }
    
    public int Priority { get; set; }
    
    public int ProjectId { get; set; }
    [JsonIgnore]
    public ProjectEntity? Project { get; set; }
}