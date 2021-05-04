using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace BulkImageDownloader.Cli.Config
{
    public class ConfigManager
    {
        private static string _value = string.Empty;

        private static readonly IConfigurationRoot _configurationRoot = new ConfigurationBuilder().AddJsonFile(GetSettingsFilePath(), optional: true, reloadOnChange: true).Build();
        public static IConfigurationRoot GetAppSettings() => _configurationRoot;
        public static IConfigurationSection GetSection(string key) => _configurationRoot.GetSection(key);
        public string this[string key]
        {
            set
            {
                _value = value;
            }
            get
            {
                return _configurationRoot.GetSection(_value)[_value];
            }
        }
        public static string GetSettingsFilePath() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        public static void UpdateAppSettings<T>(string key, T value)
        {
            Console.WriteLine(GetSettingsFilePath());
            try
            {
                string filePath = GetSettingsFilePath();
                var jsonFile = File.ReadAllText(filePath);
                dynamic objectFile = JsonConvert.DeserializeObject(jsonFile);

                var sectionPath = key.Split(":")[0];

                if (string.IsNullOrEmpty(sectionPath))
                    return;


                var keyPath = key.Split(":")[1];

                objectFile[sectionPath][keyPath] = value;

                string output = JsonConvert.SerializeObject(objectFile, Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Save! Please Restart the app: " + e);
            }
        }
    }
}
