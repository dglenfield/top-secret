using Microsoft.Data.Sqlite;
using TopSecret.Common.Logging;
using TopSecret.Common.Models;

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
    /// Asynchronously creates the database file and initializes its structure if it does not already exist.
    /// </summary>
    /// <remarks>This method ensures that the database file and its parent directory are created if they do
    /// not exist. If the database file already exists, the method returns immediately without making any changes. If
    /// the database file is created, it initializes the database by creating a `dbInfo` table and inserting a record.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
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
            CREATE TABLE IF NOT EXISTS dbInfo (
                version TEXT NOT NULL
            );
            INSERT INTO dbInfo (version) VALUES ('0.2');
        ";
        command.ExecuteNonQueryAsync().Wait();

        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS secrets (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                createdOn TEXT NOT NULL DEFAULT (datetime('now')),
                description TEXT,
                notes TEXT,
                password TEXT,
                updatedOn TEXT,
                username TEXT
            );
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

    /// <summary>
    /// Asynchronously retrieves information about the database, including its version.
    /// </summary>
    /// <remarks>This method attempts to open a connection to the database file specified by the connection
    /// string and queries the database for its version. If the database file does not exist, a  <see
    /// cref="FileNotFoundException"/> is thrown. If the database connection cannot be opened,  a <see
    /// cref="SqliteException"/> is thrown.</remarks>
    /// <returns>A <see cref="DatabaseInfo"/> object containing the database version if the query succeeds; otherwise, <see
    /// langword="null"/> if no version information is found.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the database file specified by the connection string does not exist.</exception>
    public async Task<DatabaseInfo?> GetDatabaseInfoAsync()
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
        command.CommandText = "SELECT version FROM dbInfo LIMIT 1;";
        
        await using var reader = command.ExecuteReaderAsync().Result;
        if (await reader.ReadAsync())
        {
            return new DatabaseInfo { Version = reader.GetString(0) };
        }
        return null; // No version found
    }
}
