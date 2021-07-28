using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffModelsLibrary.interfaces;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace StaffModelsLibrary
{
    public class XmlStorage : InMemory, ISerialize
    {

        public XmlStorage()
        {
            staffList = this.Deserialize(@"C:\Users\user\Documents\c#\StaffProject\staff\Xmlstorage.xml");
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
            XmlStorage x = new XmlStorage();
        }
    }
}
