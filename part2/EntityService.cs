using System.Collections.Generic;
using System.Linq;
using StringApp.DAL;

namespace StringApp.BLL
{
    public class EntityService
    {
        private List<CustomStringModel> _items = new List<CustomStringModel>();

        public void AddString(string value, int shiftKey, ShiftDirectionModel direction)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new StringDomainException("Рядок не може бути порожнім!");

            if (shiftKey < 0)
                throw new StringDomainException("Ключ зсуву не може бути від'ємним!");

            _items.Add(new CustomStringModel
            {
                Value = value,
                ShiftKey = shiftKey,
                ShiftDirection = direction
            });
        }

        public List<CustomStringModel> GetAll() => _items;

        public void SaveData(string filePath, IDataProvider provider)
        {
            EntityContext context = new EntityContext(provider);
            var entities = _items.Select(m => new CustomStringEntity
            {
                Value = m.Value,
                ShiftKey = m.ShiftKey,
                ShiftDirection = (DirectionEntity)m.ShiftDirection
            }).ToList();

            context.SaveToFile(filePath, entities);
        }

        public void LoadData(string filePath, IDataProvider provider)
        {
            EntityContext context = new EntityContext(provider);
            var entities = context.LoadFromFile(filePath);

            _items = entities.Select(e => new CustomStringModel
            {
                Value = e.Value,
                ShiftKey = e.ShiftKey,
                ShiftDirection = (ShiftDirectionModel)e.ShiftDirection
            }).ToList();
        }
    }
}