using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace part1
{
    public enum Direction
    {
        Forward,
        Backward
    }

    [Serializable]
    public class CustomString
    {
        public string Value { get; set; } = string.Empty;
        public int Length => Value?.Length ?? 0;
        public int ShiftKey { get; set; }
        public Direction ShiftDirection { get; set; }

        public CustomString() { }

        public CustomString(string value, int shiftKey, Direction shiftDirection)
        {
            Value = value;
            ShiftKey = shiftKey;
            ShiftDirection = shiftDirection;
        }

        public string Encrypt()
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;

            char[] buffer = Value.ToCharArray();
            int delta = (ShiftDirection == Direction.Forward) ? ShiftKey : -ShiftKey;

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] + delta);
            }
            return new string(buffer);
        }

        public string Decrypt()
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;

            char[] buffer = Value.ToCharArray();
            int delta = (ShiftDirection == Direction.Forward) ? -ShiftKey : ShiftKey;

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] + delta);
            }
            return new string(buffer);
        }

        public override string ToString()
        {
            return $"[CustomString] Текст: \"{Value}\" | Довжина: {Length} | Ключ: {ShiftKey} | Напрямок: {ShiftDirection}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CustomString[] array = new CustomString[6]
            {
                new CustomString("Hello World", 3, Direction.Forward),
                new CustomString("C# Programming", 1, Direction.Backward),
                new CustomString("Data Serialization", 5, Direction.Forward),
                new CustomString("Object Array", 2, Direction.Backward),
                new CustomString("Variant 10", 4, Direction.Forward),
                new CustomString("Algorithm Test", 2, Direction.Forward)
            };

            Console.WriteLine("=== 1. Початкові 6 об'єктів ===");
            foreach (var item in array)
            {
                Console.WriteLine(item);
                Console.WriteLine($"   Шифрований: {item.Encrypt()}");
            }

            string jsonFile = "data.json";
            string jsonString = JsonSerializer.Serialize(array, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFile, jsonString);

            CustomString[] restoredFromJson = JsonSerializer.Deserialize<CustomString[]>(File.ReadAllText(jsonFile))!;
            Console.WriteLine("\n=== Відновлено з JSON ===");
            foreach (var item in restoredFromJson) Console.WriteLine(item);

            string xmlFile = "data.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomString[]));
            using (FileStream fs = new FileStream(xmlFile, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, array);
            }

            CustomString[] restoredFromXml;
            using (FileStream fs = new FileStream(xmlFile, FileMode.Open))
            {
                restoredFromXml = (CustomString[])xmlSerializer.Deserialize(fs)!;
            }
            Console.WriteLine("\n=== Відновлено з XML ===");
            foreach (var item in restoredFromXml) Console.WriteLine(item);

            string binFile = "data.bin";
            using (BinaryWriter writer = new BinaryWriter(File.Open(binFile, FileMode.Create)))
            {
                writer.Write(array.Length);
                foreach (var item in array)
                {
                    writer.Write(item.Value);
                    writer.Write(item.ShiftKey);
                    writer.Write((int)item.ShiftDirection);
                }
            }

            List<CustomString> restoredFromBin = new List<CustomString>();
            using (BinaryReader reader = new BinaryReader(File.Open(binFile, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string val = reader.ReadString();
                    int key = reader.ReadInt32();
                    Direction dir = (Direction)reader.ReadInt32();
                    restoredFromBin.Add(new CustomString(val, key, dir));
                }
            }
            Console.WriteLine("\n=== Відновлено з Бінарного файлу (Колекція List<T>) ===");
            foreach (var item in restoredFromBin) Console.WriteLine(item);

            string customFile = "data.txt";
            using (StreamWriter sw = new StreamWriter(customFile))
            {
                foreach (var item in array)
                {
                    sw.WriteLine($"{item.Value};{item.ShiftKey};{item.ShiftDirection}");
                }
            }

            List<CustomString> restoredFromCustom = new List<CustomString>();
            foreach (var line in File.ReadAllLines(customFile))
            {
                var parts = line.Split(';');
                if (parts.Length == 3)
                {
                    restoredFromCustom.Add(new CustomString(
                        parts[0],
                        int.Parse(parts[1]),
                        Enum.Parse<Direction>(parts[2])
                    ));
                }
            }
            Console.WriteLine("\n=== Відновлено з Кастомного файлу ===");
            foreach (var item in restoredFromCustom) Console.WriteLine(item);
        }
    }
}