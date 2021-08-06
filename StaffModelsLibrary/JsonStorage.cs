
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
namespace StaffModelsLibrary
{
    class JsonStorage : InMemory,ISerialize
    {
        public JsonStorage()
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            var path = config["path"];

            staffList = this.Deserialize(path);
        }

        public List<Staff> Deserialize(string path)
        {

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            string jsonStrings = File.ReadAllText(path);
            List<Staff> list;
            list = JsonConvert.DeserializeObject<List<Staff>>(jsonStrings, settings);
            return list;
        }

        public void Serialize(string path)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            var jsonString = JsonConvert.SerializeObject(staffList, settings);
            File.WriteAllText(path, jsonString);
        }
    }
}
