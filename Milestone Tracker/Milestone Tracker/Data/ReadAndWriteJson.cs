using Milestone_Tracker.Views.Advanced_Lists;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace Milestone_Tracker.Data
{
    class ReadAndWriteJson
    {
        public string FilePath { get; set; }
        public string Folder{ get; set; }
        
        public ReadAndWriteJson(string fileName, string directory)
        {
            Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directory);
            FilePath = Path.Combine(Folder , fileName + ".json");
        }

        public JObject ReadJson()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Milestone_Tracker.Data.Fortnite.json");
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var jObject = JObject.Parse(text);
            Directory.CreateDirectory(Folder);
            File.WriteAllText(FilePath, jObject.ToString());


            var jsonString = File.ReadAllText(FilePath);
            var jsonObject = JObject.Parse(jsonString);
            return jsonObject;
        }
        
        public void WriteJson(JObject jObject)
        {
            File.WriteAllText(FilePath,jObject.ToString());
        }


    }
}
