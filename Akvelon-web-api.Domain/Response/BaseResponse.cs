using System.Net;

namespace Akvelon_web_api.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public List<string>? BrokenRules { get; set; }
    
    public bool IsSuccessful { get; set; }
    
    public T? Data { get; set; }
}

public interface IBaseResponse<T>
{
    public List<string>? BrokenRules { get; }

    public bool IsSuccessful { get; }
    
    T? Data { get; }
}