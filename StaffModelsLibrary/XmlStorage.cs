
using System.Collections.Generic;
using StaffModelsLibrary.interfaces;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace StaffModelsLibrary
{
    public class XmlStorage : InMemory, ISerialize
    {
        public XmlStorage()
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            var path = config["path"];

            staffList = this.Deserialize(path);
        }

        public List<Staff> Deserialize(string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staff>));
            FileStream fs = new FileStream(filepath, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            List<Staff> i;
            i = (List<Staff>)serializer.Deserialize(reader);
            fs.Close();
            return i;
        }
        public  void  Serialize(string path)
        {
            XmlSerializer xs = new XmlSerializer(staffList.GetType());
            TextWriter txtWriter = new StreamWriter(path);
            xs.Serialize(txtWriter,staffList);
            txtWriter.Close();
        }
    }
}
