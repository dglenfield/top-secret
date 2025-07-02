using TopSecret.Common.Models;

ApplicationSettings settings = new()
{
    Password = "this!sn0tTHePassword"
};

Console.WriteLine($"TopSecret password: {settings.Password}");
