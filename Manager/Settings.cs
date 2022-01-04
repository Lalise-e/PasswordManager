using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Manager
{
    public class Settings
    {
        public Settings()
        {
            FontName = "Segoe UI";
            BackgroundLocation = null;
            Incognito = false;
        }
        public string FontName { get; set; }
        public string BackgroundLocation { get; set; }
        public bool Incognito { get; set; }
        public static Settings GetSettings(string file)
        {
            string data = File.ReadAllText(file);
            return JsonSerializer.Deserialize<Settings>(data);
        }
        public static void SaveSettings(string file, Settings settings)
        {
            string data = JsonSerializer.Serialize(settings);
            File.WriteAllText(file, data);
        }
    }
}
