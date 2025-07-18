using TopSecret.Common.Data;
using TopSecret.Common.Models;

ApplicationSettings settings = new()
{
    //DatabaseFileLocation = Environment.CurrentDirectory,
    DatabaseFileLocation = @"c:\temp2",
    DatabaseFileName = "TopSecret.db"
    //DatabaseFileName = @"test\TopSecret.db"
};

//string settingsPath = @"c:\temp2\appsettings.json";
string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

// Save settings
await settings.SaveSettingsAsync(settingsPath);

// Load settings
//settingsPath = @"c:\temp2\appsettings2.json";
ApplicationSettings? loadedSettings = await ApplicationSettings.LoadSettingsAsync(settingsPath);

if (loadedSettings == null || 
    string.IsNullOrWhiteSpace(loadedSettings.DatabaseFileLocation) || 
    string.IsNullOrWhiteSpace(loadedSettings.DatabaseFileName))
{
    WriteError("Failed to load application settings.");
    return;
}

SecretsDb secretsDb = new(loadedSettings.DatabaseFileLocation, loadedSettings.DatabaseFileName);
//SecretsDb secretsDb = new("", "");
WriteInfo($"TopSecret database file: {secretsDb.FullDatabaseFilePath}");

try
{
    await secretsDb.CreateAsync();
}
catch (Exception ex)
{
    WriteError($"\n** ERROR CREATING DATABASE: {ex.Message} **");
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
