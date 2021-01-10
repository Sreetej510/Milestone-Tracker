using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Milestone_Tracker.Data
{
    class ReadAndWriteJson
    {
        public string FilePath { get; set; }
        public string Folder { get; set; }

        public ReadAndWriteJson(string fileName, string directory, string jsonType)
        {
            Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directory);
            FilePath = Path.Combine(Folder, fileName + ".json");

            if (!File.Exists(FilePath))
            {
                
                JObject jObject = JObject.Parse("{ \"allCategories\":[],\"onGoing\": []}");
                
                File.WriteAllText(FilePath, jObject.ToString());
                
            }
        }
    

        public JObject ReadJson()
        {
            var jsonString = File.ReadAllText(FilePath);
            var jsonObject = JObject.Parse(jsonString);
            return jsonObject;
        }

        public void WriteJson(JObject jObject)
        {
            File.WriteAllText(FilePath, jObject.ToString());
        }


    }
}
