using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Akvelon_web_api.Domain.Enum;

namespace Akvelon_web_api.Domain.Entity;

public class ProjectEntity
{
    public int Id { get; set; }
    
    public string ProjectName { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime? CompleteDate { get; set; }
    
    public ProjectStatusEnum Status { get; set; }

    public int Priority { get; set; }
    
    [JsonIgnore]
    public List<TaskEntity>? Tasks { get; set; }
}