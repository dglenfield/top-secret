using TopSecret.Common.Models;

ApplicationSettings settings = new()
{
    DatabaseFileLocation = Environment.CurrentDirectory,
    DatabaseFileName = "TopSecret.db"
};

Console.WriteLine($@"TopSecret database file: {settings.DatabaseFileLocation}\{settings.DatabaseFileName}");

