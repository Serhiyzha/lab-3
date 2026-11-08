using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace StringApp.DAL
{
    public class XmlDataProvider : IDataProvider
    {
        public void Save(string filePath, List<CustomStringEntity> items)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CustomStringEntity>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, items);
            }
        }

        public List<CustomStringEntity> Load(string filePath)
        {
            if (!File.Exists(filePath)) return new List<CustomStringEntity>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<CustomStringEntity>));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (List<CustomStringEntity>)serializer.Deserialize(fs)!;
            }
        }
    }
}