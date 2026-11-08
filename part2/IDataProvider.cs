using System.Collections.Generic;

namespace StringApp.DAL
{
    public interface IDataProvider
    {
        void Save(string filePath, List<CustomStringEntity> items);
        List<CustomStringEntity> Load(string filePath);
    }
}