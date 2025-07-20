using System.Text.Json;

namespace TopSecret.Common.Models;

public class ApplicationSettings
{
    public required string DatabaseFileLocation { get; set; }
    public required string DatabaseFileName { get; set; }

    /// <summary>
    /// Asynchronously loads application settings from a JSON file.
    /// </summary>
    /// <remarks>This method reads the entire content of the specified file asynchronously and attempts to
    /// deserialize it  into an <see cref="ApplicationSettings"/> object. Ensure the file exists and contains valid JSON
    /// formatted to match the structure of <see cref="ApplicationSettings"/>.</remarks>
    /// <param name="filePath">The path to the JSON file containing the application settings. Must not be null or empty.</param>
    /// <returns>An <see cref="ApplicationSettings"/> object deserialized from the JSON file,  or <see langword="null"/> if the
    /// file content is empty or cannot be deserialized.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file specified by <paramref name="filePath"/> does not exist.</exception>
    public static async Task<ApplicationSettings?> LoadAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Settings file not found: {filePath}");

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<ApplicationSettings>(json);
    }

    /// <summary>
    /// Asynchronously saves the current application settings to a specified file in JSON format.
    /// </summary>
    /// <remarks>The settings are serialized to JSON with indented formatting for readability. Ensure that the
    /// application has write permissions to the specified file path.</remarks>
    /// <param name="filePath">The full path of the file where the settings will be saved. The directory must exist.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory specified in <paramref name="filePath"/> does not exist.</exception>
    public async Task SaveAsync(string filePath)
    {
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            throw new DirectoryNotFoundException($"Directory not found: {Path.GetDirectoryName(filePath)}");

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(this, options);
        await File.WriteAllTextAsync(filePath, json);
    }
}
