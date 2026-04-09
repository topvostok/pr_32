using System;
using System.Collections.Generic;
using System.Text;

namespace PR_32.Classes
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public static IEnumerable<Manufacturer> AllManufactures()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            DataTable recordQuery = DBConnection.Connection("SELECT * FROM [dbo].[Manufacturer]");
            foreach (DataRow row in recordQuery.Rows)
                manufacturers.Add(new Manufacturer()
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = row[1].ToString(),
                    CountryCode = Convert.ToInt32(row[2]),
                    Phone = row[3].ToString(),
                    Mail = row[4].ToString()
                });
            return manufacturers;
        }
        public void Save(bool Update = false)
        {
            if (Update == false)
            {
                DBConnection.Connection($"INSERT INTO [dbo].[Manufacturer]([Name], [CountryCode], [Phone], [Mail]) " +
                    $"VALUES (N'{this.Name}', {this.CountryCode}, N'{this.Phone}', N'{this.Mail}');");
                this.Id = AllManufactures().Where(x => x.Name == this.Name &&
                                                x.CountryCode == this.CountryCode &&
                                                x.Phone == this.Phone &&
                                                x.Mail == this.Mail).First().Id;
            }
            else
            {
                DBConnection.Connection($"UPDATE [dbo].[State] SET " +
                    $"[Name] = N'{this.Name}', " +
                    $"[CountryCode] = {this.CountryCode}, " +
                    $"[Phone] = N'{this.Phone}', " +
                    $"[Mail] = N'{this.Mail}' " +
                    $"WHERE [Id] = {this.Id};");
            }
        }

        public void Delete()
        {
            DBConnection.Connection($"DELETE FROM [dbo].[Manufactures] WHERE [Id] = {this.Id};");
        }
    }
}
