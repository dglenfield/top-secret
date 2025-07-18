using System.Text.Json;

namespace TopSecret.Common.Models;

public class ApplicationSettings
{
    public string? DatabaseFileLocation { get; set; }
    public string? DatabaseFileName { get; set; }

    // Serialize ApplicationSettings to a JSON file
    public async Task SaveSettingsAsync(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(this, options);
        await File.WriteAllTextAsync(filePath, json);
    }

    // Serialize ApplicationSettings to a JSON file (static method for explicit settings)
    public static async Task SaveSettingsAsync(ApplicationSettings settings, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(settings, options);
        await File.WriteAllTextAsync(filePath, json);
    }

    // Deserialize ApplicationSettings from a JSON file
    public static async Task<ApplicationSettings?> LoadSettingsAsync(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<ApplicationSettings>(json);
    }
}
