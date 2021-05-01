using Microsoft.Extensions.Configuration;

namespace BulkImageDownloader.Cli.Config
{
    public class ConfigManager
    {
        private static string _value = string.Empty;
        private static readonly IConfigurationRoot _configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
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
    }
}
