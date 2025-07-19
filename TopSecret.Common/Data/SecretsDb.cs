using Microsoft.Data.Sqlite;
using TopSecret.Common.Logging;

namespace TopSecret.Common.Data;

public class SecretsDb
{
    public readonly string DatabaseFileLocation;
    public readonly string DatabaseFileName;
    
    public string FullDatabaseFilePath => Path.IsPathRooted(DatabaseFileName) ? DatabaseFileName : Path.Combine(DatabaseFileLocation, DatabaseFileName);
    public string FullLogFilePath => Path.Combine(DatabaseFileLocation, "secretsdb-errors.log");
    
    private readonly string _connectionString;
    private readonly FileLogger _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SecretsDb"/> class with the specified database file location and
    /// name.
    /// </summary>
    /// <remarks>The constructor sets up the database connection string and initializes a logger for the
    /// database operations. Ensure that the provided file location and name are valid and accessible.</remarks>
    /// <param name="databaseFileLocation">The directory path where the database file is located. This value cannot be null or empty.</param>
    /// <param name="databaseFileName">The name of the database file, including its extension. This value cannot be null or empty.</param>
    public SecretsDb(string databaseFileLocation, string databaseFileName)
    {
        DatabaseFileLocation = databaseFileLocation;
        DatabaseFileName = databaseFileName;
        _logger = new FileLogger(FullLogFilePath);
        _connectionString = $"Data Source={FullDatabaseFilePath};Pooling=False";
    }

    /// <summary>
    /// Creates a new SQLite database file and initializes it with a default schema and data, if it does not already
    /// exist.
    /// </summary>
    /// <remarks>This method ensures that the database file is created at the specified location and
    /// initializes it with a default schema, including a "users" table with a sample user. If the database file already
    /// exists, the method returns without making any changes.</remarks>
    /// <returns>A task that represents the asynchronous operation of creating and initializing the database.</returns>
    /// <exception cref="ArgumentException">Thrown if the database file location or name is not provided (i.e., is null, empty, or consists only of
    /// whitespace).</exception>
    public async Task CreateAsync()
    {
        if (!Directory.Exists(DatabaseFileLocation))
        {
            Directory.CreateDirectory(DatabaseFileLocation);
            _logger.LogInfoAsync($"Created directory: {DatabaseFileLocation}").Wait();
        }

        if (File.Exists(FullDatabaseFilePath))
        {
            return; // Database file already exists, no need to create it again
        }

        _logger.LogInfoAsync($"Database file does not exist. Creating database at {FullDatabaseFilePath}").Wait();

        await using SqliteConnection connection = new(_connectionString);
        
        try
        {
            await connection.OpenAsync();
        }
        catch (SqliteException ex)
        {
            string logMessage = $"Error opening database connection: {connection.ConnectionString}";
            _logger.LogErrorAsync(logMessage, ex).Wait();
            throw;
        }

        // Create the users table and insert a sample user
        await using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS users (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL
            );
            INSERT INTO users (name) VALUES ('Danny');
        ";
        command.ExecuteNonQueryAsync().Wait();
    }

    /// <summary>
    /// Deletes the database file if it exists.
    /// </summary>
    /// <remarks>This method checks for the existence of the database file at the path specified by 
    /// <c>FullDatabaseFilePath</c>. If the file exists, it attempts to delete it.  A brief delay is introduced to
    /// ensure the file lock is released by the operating system.  If the deletion is successful, a log entry is created
    /// to indicate success.  If an error occurs during deletion, the error is logged, and the exception is
    /// rethrown.</remarks>
    /// <returns></returns>
    public async Task DeleteAsync()
    {
        if (File.Exists(FullDatabaseFilePath))
        {
            await Task.Delay(100); // Give the OS a moment to release the file lock
            try
            {
                File.Delete(FullDatabaseFilePath);
                _logger.LogInfoAsync($"Database file '{FullDatabaseFilePath}' deleted successfully.").Wait();
            }
            catch (Exception ex)
            {
                string logMessage = $"Error deleting database file '{FullDatabaseFilePath}'";
                _logger.LogErrorAsync(logMessage, ex).Wait();
                throw;
            }   
        }
    }

    public async Task GetUsersAsync()
    {
        if (!File.Exists(FullDatabaseFilePath))
        {
            throw new FileNotFoundException($"Database file not found: {FullDatabaseFilePath}");
        }

        await using SqliteConnection connection = new(_connectionString);
        
        try
        {
            await connection.OpenAsync();
        }
        catch (SqliteException ex)
        {
            string logMessage = $"Error opening database connection: {connection.ConnectionString}";
            _logger.LogErrorAsync(logMessage, ex).Wait();
            throw;
        }

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, name FROM users;";
        
        await using var reader = command.ExecuteReaderAsync().Result;
        while (reader.ReadAsync().Result)
        {
            Console.WriteLine($"User: {reader.GetInt32(0)} - {reader.GetString(1)}");
        }
    }
}
