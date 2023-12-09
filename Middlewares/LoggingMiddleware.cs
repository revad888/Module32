using Module32.Models.Db;

namespace Module32.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRequestRepository _requestRepository;

    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next, IRequestRepository requestRepository)
    {
        _next = next;
        _requestRepository= requestRepository;
    }
    
    public void ConsoleLog(HttpContext context)
    {
        Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
    }
    public async Task FileLog(HttpContext context)
    {
        string logString = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}\n";
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Logs.txt");
        await File.AppendAllTextAsync(logFilePath, logString);
    }
    public async Task RequestLog(HttpContext context, IRequestRepository requestRepository)
    {

        Request request = new Request()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Url = $"http://{context.Request.Host.Value + context.Request.Path}"

        };
        await requestRepository.AddReuest(request);
        //await _next.Invoke(context);
    }
    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        

        ConsoleLog(context);
        await FileLog(context);
        await RequestLog(context, _requestRepository);
        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
}