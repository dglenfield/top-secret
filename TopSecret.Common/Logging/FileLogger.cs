namespace TopSecret.Common.Logging;

public class FileLogger
{
    private readonly string _logFilePath;

    private static readonly object _lock = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="FileLogger"/> class,  setting the default log file path to a file
    /// named "topsecret.log" in the application's base directory.
    /// </summary>
    /// <remarks>The default log file path is constructed using the application's base directory.  Ensure the
    /// application has write permissions to this directory to avoid runtime errors.</remarks>
    public FileLogger()
    {
        _logFilePath = Path.Combine(AppContext.BaseDirectory, "topsecret.log");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileLogger"/> class with the specified log file path.
    /// </summary>
    /// <remarks>The specified file path should be writable by the application. If the file does not exist, it
    /// will be created.</remarks>
    /// <param name="logFilePath">The path to the file where log messages will be written. This must be a valid file path and cannot be null or
    /// empty.</param>
    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    /// <summary>
    /// Logs an error message asynchronously, optionally including exception details.
    /// </summary>
    /// <param name="message">The error message to log. This cannot be <see langword="null"/> or empty.</param>
    /// <param name="ex">An optional exception to include in the log. If <see langword="null"/>, only the message is logged.</param>
    /// <returns>A task that represents the asynchronous logging operation.</returns>
    public async Task LogErrorAsync(string message, Exception? ex = null)
    {
        var errorMessage = ex == null
            ? message
            : $"{message}{Environment.NewLine}{ex}";
        await LogAsync("ERROR", errorMessage);
    }

    /// <summary>
    /// Logs an informational message asynchronously.
    /// </summary>
    /// <remarks>This method logs the provided message with an "INFO" log level.  It is intended for general
    /// informational messages that do not indicate errors or warnings.</remarks>
    /// <param name="message">The message to log. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous logging operation.</returns>
    public async Task LogInfoAsync(string message)
    {
        await LogAsync("INFO", message);
    }

    /// <summary>
    /// Asynchronously logs a message with the specified log level to the log file.
    /// </summary>
    /// <remarks>The log entry is timestamped using the current UTC time and written to the log file specified
    /// by the internal configuration. This method is thread-safe.</remarks>
    /// <param name="level">The severity level of the log entry (e.g., "INFO", "ERROR", "DEBUG").</param>
    /// <param name="message">The message to log. Cannot be null or empty.</param>
    /// <returns></returns>
    private async Task LogAsync(string level, string message)
    {
        string logEntry = $"[{DateTime.UtcNow:u}] [{level}] {message}{Environment.NewLine}";
        lock (_lock)
        {
            File.AppendAllText(_logFilePath, logEntry);
        }
        await Task.CompletedTask;
    }
}