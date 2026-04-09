using System;
using System.Collections.Generic;
using System.Text;

namespace PR_32.Classes
{
    public class Supply
    {
        public int Id { get; set; }
        public int IdManufacturer { get; set; }
        public int IdRecord { get; set; }
        public string DateDelivery { get; set; }
        public int Count { get; set; }
        public static IEnumerable<Supply> AllSupplies()
        {
            List<Supply> supples = new List<Supply>();
            DataTable recordQuery = DBConnection.Connection("SELECT * FROM [dbo].[Supple]");
            foreach (DataRow row in recordQuery.Rows)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(row[3].ToString(), out dt);
                string CorrectDate = dt.Year + "-" + dt.Month + "-" + dt.Day;
                supples.Add(new Supply()
                {
                    Id = Convert.ToInt32(row[0]),
                    IdManufacturer = Convert.ToInt32(row[1]),
                    IdRecord = Convert.ToInt32(row[2]),
                    DateDelivery = CorrectDate,
                    Count = Convert.ToInt32(row[4])
                });
            }
            return supples;
        }
        public void Save(bool Update = false)
        {
            if (Update == false)
            {
                DBConnection.Connection($"INSERT INTO [dbo].[Supple]([IdManufacturer], [IdRecord], [DateDelivery], [Count]) " +
                    $"VALUES ({this.IdManufacturer}, {this.IdRecord}, '{this.DateDelivery}', {this.Count});");
                this.Id = Supply.AllSupplies().Where(x => x.IdManufacturer == this.IdManufacturer &&
                                                x.IdRecord == this.IdRecord &&
                                                x.DateDelivery == this.DateDelivery &&
                                                x.Count == this.Count).First().Id;
            }
            else
            {
                DBConnection.Connection($"UPDATE [dbo].[Supple] SET " +
                    $"[IdManufacturer] = {this.IdManufacturer}, " +
                    $"[IdRecord] = {this.IdRecord}, " +
                    $"[DateDelivery] = '{this.DateDelivery}', " +
                    $"[Count] = {this.Count} " +
                    $"WHERE [Id] = {this.Id};");
            }
        }

        public void Delete()
        {
            DBConnection.Connection($"DELETE FROM [dbo].[Supple] WHERE [Id] = {this.Id};");
        }

    }
}
