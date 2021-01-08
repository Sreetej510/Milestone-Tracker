﻿using Newtonsoft.Json.Linq;
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
            Directory.CreateDirectory(Folder);

            if (!File.Exists(FilePath))
            {
                string text = "";
                
                if (jsonType == "dashboard")
                {
                    text = "{\"listNames\":[], \"list\": []}";
                }
                else if (jsonType == "list")
                {
                    text = "{\"onGoing\": []}";
                }

                JObject jObject = JObject.Parse(text);
                
                File.WriteAllText(FilePath, jObject.ToString());
                
            }

        }
    

        public JObject ReadJson()
        {
        //    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
        //    Stream stream = assembly.GetManifestResourceStream("Milestone_Tracker.Data.Dashboard.json");
        //    string text = "";
        //    using (var reader = new StreamReader(stream))
        //    {
        //        text = reader.ReadToEnd();
        //    }
        //    var jObject = JObject.Parse(text);

        //    File.WriteAllText(FilePath, jObject.ToString());

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
