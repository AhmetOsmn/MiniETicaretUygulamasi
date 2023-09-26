using MiniETicaretAPI.Application.Settings.Sections;

namespace MiniETicaretAPI.Application.Settings
{
    public class ConfigurationSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Storage Storage { get; set; }
        public string BaseStorageUrl { get; set; }
        public Token Token { get; set; }
    }
}
