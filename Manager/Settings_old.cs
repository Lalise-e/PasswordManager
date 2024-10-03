using System.IO;
using System.Text.Json;

namespace Manager
{
    public class Settings_old
    {
        public Settings_old()
        {
            FontName = "Segoe UI";
            BackgroundLocation = null;
            Incognito = false;
        }
        public string FontName { get; set; }
        public string BackgroundLocation { get; set; }
        public bool Incognito { get; set; }
        public static Settings_old GetSettings(string file)
        {
            string data = File.ReadAllText(file);
            return JsonSerializer.Deserialize<Settings_old>(data);
        }
        public static void SaveSettings(string file, Settings_old settings)
        {
            string data = JsonSerializer.Serialize(settings);
            File.WriteAllText(file, data);
        }
    }
}
