using System;
using System.Collections.Generic;
using System.Text;

namespace PR_32.Classes
{
    public class DBConnection
    {
        public static DataTable Connection(string SQL)
        {
            DataTable dataTable = new DataTable("Datatable");
            SqlConnection sqlConnection = new SqlConnection("server=LAPTOP3019;Trusted_Connection=No;DataBase=VinylRecords;User=sa;PWD=1111");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = SQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
