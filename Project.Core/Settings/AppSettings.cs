using Newtonsoft.Json;

namespace Project.Core.Settings
{
    public class AppSettings
    {
        private const string JsonFilePath = "..\\Project.Core\\Settings\\settings.json";

        private AppSettings()
        {

        }
        static AppSettings()
        {
            ReloadSettings();
        }

        //Json faylındakı obyektlərin qarşılığı =>
        public DbConnectionModel ProjectAppDbConnectionModel { get; set; }
        public JwtOptions JwtOptions { get; set; }
        public EmailOptions EmailOptions { get; set; }
        public string[] CorsPolicyOrigins { get; set; }
        public string DefaultLanguage { get; set; }
        public string GlobalKey { get; set; }
        public OctosApiCredentials OctosApiCredentials { get; set; }
        public AgtApiCredentials AgtApiCredentials { get; set; }



        public static AppSettings Settings { get; private set; }


        public static void ReloadSettings()
        {
            JsonSerializer serializer = new JsonSerializer();

            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directoryPath = Path.GetDirectoryName(location);
            string settingsPath;

            if(File.Exists(Path.Combine(directoryPath, JsonFilePath)))
                settingsPath = Path.Combine(directoryPath, JsonFilePath);
            else
            {
                settingsPath = JsonFilePath;
            }

            using (StreamReader sr = new StreamReader(settingsPath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        Settings = serializer.Deserialize<AppSettings>(reader);
                    }
                }
            }
        }
    }
}
