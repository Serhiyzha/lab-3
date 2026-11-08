using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StringApp.DAL
{
    public class JsonDataProvider : IDataProvider
    {
        public void Save(string filePath, List<CustomStringEntity> items)
        {
            string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public List<CustomStringEntity> Load(string filePath)
        {
            if (!File.Exists(filePath)) return new List<CustomStringEntity>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<CustomStringEntity>>(json) ?? new List<CustomStringEntity>();
        }
    }
}