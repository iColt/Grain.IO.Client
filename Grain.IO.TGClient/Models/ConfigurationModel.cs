namespace Grain.IO.TGClient.Models
{
    public interface IConfigurationModel
    {
        string AuthToken { get; set; }
        string TamdhuWhiskyPath { get; set; }
    }

    public class ConfigurationModel : IConfigurationModel
    {
        public string TamdhuWhiskyPath { get; set; } = string.Empty;

        public string AuthToken { get; set; } = string.Empty;
    }
}
