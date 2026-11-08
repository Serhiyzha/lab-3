using System.Collections.Generic;

namespace StringApp.DAL
{
    public class EntityContext
    {
        private readonly IDataProvider _dataProvider;

        public EntityContext(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void SaveToFile(string filePath, List<CustomStringEntity> items)
        {
            _dataProvider.Save(filePath, items);
        }

        public List<CustomStringEntity> LoadFromFile(string filePath)
        {
            return _dataProvider.Load(filePath);
        }
    }
}