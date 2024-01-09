using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Configuration
{
    public string? Database { get; set; }
    public string? Password { get; set; }

    public void Save()
    {
        // Fetch existing file
        var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        var json = File.ReadAllText(path);

        // Convert file into a dynamic object so we preserve other settings
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new ExpandoObjectConverter());
        settings.Converters.Add(new StringEnumConverter());
        dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, settings)!;

        // Update configuration
        config.Configuration = this; // Store current config object

        // Save back to disk
        var update = JsonConvert.SerializeObject(config, Formatting.Indented, settings);
        File.WriteAllText(path, update);
    }

    public void Encrypt()
    {
        if (string.IsNullOrEmpty(ConfigurationSecret.Decrypt(Database)))
            Database = ConfigurationSecret.Encrypt(Database);

        if (string.IsNullOrEmpty(ConfigurationSecret.Decrypt(Password)))
            Password = ConfigurationSecret.Encrypt(Password);
    }

}