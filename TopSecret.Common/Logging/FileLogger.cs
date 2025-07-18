namespace TopSecret.Common.Logging;

public class FileLogger
{
    private readonly string _logFilePath;
    private static readonly object _lock = new();

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath ?? throw new ArgumentNullException(nameof(logFilePath));
    }

    public async Task LogInfoAsync(string message)
    {
        await LogAsync("INFO", message);
    }

    public async Task LogErrorAsync(string message, Exception? ex = null)
    {
        var errorMessage = ex == null
            ? message
            : $"{message}{Environment.NewLine}{ex}";
        await LogAsync("ERROR", errorMessage);
    }

    private async Task LogAsync(string level, string message)
    {
        var logEntry = $"[{DateTime.UtcNow:u}] [{level}] {message}{Environment.NewLine}";
        lock (_lock)
        {
            File.AppendAllText(_logFilePath, logEntry);
        }
        await Task.CompletedTask;
    }
}