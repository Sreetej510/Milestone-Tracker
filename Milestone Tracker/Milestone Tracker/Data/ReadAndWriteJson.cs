using Milestone_Tracker.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Milestone_Tracker.Data
{
    class ReadAndWriteJson
    {
        public string FilePath { get; set; }
        
        public ReadAndWriteJson(string fileName)
        {
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName + ".json");
        }

        public JObject ReadJson()
        {
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            //Stream stream = assembly.GetManifestResourceStream("Milestone_Tracker.Data.Fortnite.json");
            //string text = "";
            //using (var reader = new StreamReader(stream))
            //{
            //    text = reader.ReadToEnd();
            //}
            //var jObject = JObject.Parse(text);
            //File.WriteAllText(FilePath, jObject.ToString());


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
