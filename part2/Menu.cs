using System;
using StringApp.BLL;
using StringApp.DAL;

namespace StringApp.PL
{
    public class Menu
    {
        private readonly EntityService _service = new EntityService();

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- ГОЛОВНЕ МЕНЮ (Варіант 10) ---");
                Console.WriteLine("1. Додати новий рядок");
                Console.WriteLine("2. Показати всі рядки (з шифруванням/дешифруванням)");
                Console.WriteLine("3. Зберегти у файл");
                Console.WriteLine("4. Завантажити з файлу");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine()!;
                try
                {
                    switch (choice)
                    {
                        case "1": AddItem(); break;
                        case "2": ShowItems(); break;
                        case "3": SaveToFile(); break;
                        case "4": LoadFromFile(); break;
                        case "0": return;
                        default: Console.WriteLine("Невірний вибір!"); break;
                    }
                }
                catch (StringDomainException ex)
                {
                    Console.WriteLine($"[Бізнес-помилка]: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Системна помилка]: {ex.Message}");
                }
            }
        }

        private void AddItem()
        {
            Console.Write("Введіть текст: ");
            string val = Console.ReadLine()!;
            Console.Write("Введіть ключ зсуву (число): ");
            int key = int.Parse(Console.ReadLine()!);
            Console.Write("Напрямок (0 - Forward, 1 - Backward): ");
            ShiftDirectionModel dir = (ShiftDirectionModel)int.Parse(Console.ReadLine()!);

            _service.AddString(val, key, dir);
            Console.WriteLine("Рядок успішно додано!");
        }

        private void ShowItems()
        {
            var list = _service.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                Console.WriteLine($"\n[{i + 1}] Текст: \"{item.Value}\" (Довжина: {item.Length})");
                Console.WriteLine($"    Шифрований: \"{item.Encrypt()}\"");
                Console.WriteLine($"    Дешифрований: \"{item.Decrypt()}\"");
            }
        }

        private void SaveToFile()
        {
            Console.Write("Введіть шлях до файлу: ");
            string path = Console.ReadLine()!;
            IDataProvider provider = SelectProvider();
            _service.SaveData(path, provider);
            Console.WriteLine("Дані успішно збережено!");
        }

        private void LoadFromFile()
        {
            Console.Write("Введіть шлях до файлу: ");
            string path = Console.ReadLine()!;
            IDataProvider provider = SelectProvider();
            _service.LoadData(path, provider);
            Console.WriteLine("Дані успішно завантажено!");
        }

        private IDataProvider SelectProvider()
        {
            Console.WriteLine("Оберіть формат серіалізації:");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. XML");
            Console.Write("Вибір: ");
            string type = Console.ReadLine()!;

            return type switch
            {
                "1" => new JsonDataProvider(),
                "2" => new XmlDataProvider(),
                _ => throw new Exception("Невідомий провайдер серіалізації!")
            };
        }
    }
}