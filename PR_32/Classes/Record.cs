using System;
using System.Collections.Generic;
using System.Text;

namespace PR_32.Classes
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Format { get; set; }
        public int Size { get; set; }
        public int IdManufacturer { get; set; }
        public float Price { get; set; }
        public int IdState { get; set; }
        public string Description { get; set; }

        public static IEnumerable<Record> AllRecords()
        {
            List<Record> records = new List<Record>();
            DataTable recordQuery = DBConnection.Connection("SELECT * FROM [dbo].[Record]");
            foreach (DataRow row in recordQuery.Rows)
                records.Add(new Record()
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = row[1].ToString(),
                    Year = Convert.ToInt32(row[2]),
                    Format = Convert.ToInt32(row[3]),
                    Size = Convert.ToInt32(row[4]),
                    IdManufacturer = Convert.ToInt32(row[5]),
                    Price = float.Parse(row[6].ToString()),
                    IdState = Convert.ToInt32(row[7]),
                    Description = row[8].ToString()
                });
            return records;
        }
        public void Save(bool Update = false)
        {
            string CorrectPrice = this.Price.ToString().Replace(",", ".");
            if (Update == false)
            {
                DBConnection.Connection($"INSERT INTO [dbo].[Record](" +
                    $"[Name], " +
                    $"[Year], " +
                    $"[Format], " +
                    $"[Size], " +
                    $"[IdManufacturer], " +
                    $"[Price], " +
                    $"[IdState], " +
                    $"[Description]) " +
                    $"VALUES(" +
                    $"N'{this.Name}', " +
                    $"{this.Year}, " +
                    $"{this.Format}, " +
                    $"{this.Size}, " +
                    $"{this.IdManufacturer}, " +
                    $"{this.Price}, " +
                    $"{this.IdState}, " +
                    $"N'{this.Description}');");
                this.Id = Record.AllRecords().Where(x => x.Name == this.Name &&
                                                x.Year == this.Year &&
                                                x.Format == this.Format &&
                                                x.Size == this.Size &&
                                                x.IdManufacturer == this.IdManufacturer &&
                                                x.Price == this.Price &&
                                                x.IdState == this.IdState &&
                                                x.Description == this.Description).First().Id;
            }
            else
            {
                DBConnection.Connection($"UPDATE [dbo].[Record] " +
                    $"SET [Name] = N'{this.Name}', " +
                    $"[Year] = {this.Year}, " +
                    $"[Format] = {this.Format}, " +
                    $"[Size] = {this.Size}, " +
                    $"[IdManufacturer] = {this.IdManufacturer}, " +
                    $"[Price] = {this.Price}, " +
                    $"[IdState] = {this.IdState}, " +
                    $"[Description] = N'{this.Description}' " +
                    $"WHERE [Id] = {this.Id}");
            }
        }

        public void Delete()
        {
            DBConnection.Connection($"DELETE FROM [dbo].[Record] WHERE [Id] = {this.Id};");
        }

        public static void Export(string filePath, IEnumerable<Record> records)
        {
            // Загружаем справочники для отображения имён вместо ID
            var manufacturers = Manufacturer.AllManufactures().ToDictionary(m => m.Id, m => m.Name);
            var states = State.AllState().ToDictionary(s => s.Id, s => s.Name);

            var csv = new StringBuilder();

            // Заголовки (разделитель — точка с запятой для русскоязычного Excel)
            csv.AppendLine("\"ID\";\"Название\";\"Год\";\"Формат\";\"Размер\";\"Производитель\";\"Цена\";\"Состояние\";\"Описание\"");

            // Данные
            foreach (var record in records)
            {
                string format = record.Format == 0 ? "Моно" : "Стерео";
                string manufacturer = manufacturers.TryGetValue(record.IdManufacturer, out var mfr) ? mfr : "Неизвестно";
                string state = states.TryGetValue(record.IdState, out var st) ? st : "Неизвестно";

                csv.AppendLine(
                    $"\"{record.Id}\";" +
                    $"\"{record.Name}\";" +
                    $"\"{record.Year}\";" +
                    $"\"{format}\";" +
                    $"\"{record.Size}\";" +
                    $"\"{manufacturer}\";" +
                    $"\"{record.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)}\";" +
                    $"\"{state}\";" +
                    $"\"{record.Description}\""
                );
            }

            // Сохраняем с UTF-8 BOM для корректного отображения кириллицы в Excel
            File.WriteAllText(filePath, csv.ToString(), new UTF8Encoding(true));
        }
    }
}
