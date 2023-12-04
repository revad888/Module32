namespace Module32.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
  
    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
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
    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        

        ConsoleLog(context);
        await FileLog(context);
        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
}