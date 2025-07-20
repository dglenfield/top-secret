using TopSecret.Common.Data;
using TopSecret.Common.Models;

string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

// Save settings
try
{
    ApplicationSettings settings = new()
    {
        DatabaseFileLocation = @"c:\temp2",
        DatabaseFileName = "TopSecret.db"
    };

    await settings.SaveAsync(settingsPath);
}
catch (DirectoryNotFoundException ex)
{
    WriteError(ex.Message);
    return;
}

// Load settings
ApplicationSettings? loadedSettings;
try
{
    loadedSettings = await ApplicationSettings.LoadAsync(settingsPath);
}
catch (FileNotFoundException ex)
{
    WriteError(ex.Message);
    return;
}

if (loadedSettings == null)
{
    WriteError("Failed to load application settings.");
    return;
}

SecretsDb secretsDb = new(loadedSettings.DatabaseFileLocation, loadedSettings.DatabaseFileName);
WriteInfo($"TopSecret database file: {secretsDb.FullDatabaseFilePath}");

try
{
    await secretsDb.CreateAsync();
}
catch (Exception ex)
{
    WriteError($"\n** ERROR CREATING DATABASE: {ex.Message} **");
    return;
}

await secretsDb.GetUsersAsync();

try
{
    await secretsDb.DeleteAsync();
}
catch (Exception ex)
{
    WriteError($"\n** ERROR DELETING DATABASE: {ex.Message} **");
}
