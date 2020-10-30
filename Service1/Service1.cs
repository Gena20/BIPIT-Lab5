using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private string connectionString { get; } = "workstation id=autoTrips.mssql.somee.com;packet size=4096;user id=Gena20_SQLLogin_1;pwd=hlzsx7f1ej;data source=autoTrips.mssql.somee.com;persist security info=False;initial catalog=autoTrips";

        public void DoWork()
        {
        }

        public DataTable GetData()
        {
            string query = @"SELECT dbo.trips.id, dbo.cars.name AS car, dbo.objects.name AS obj_from, objects_1.name AS obj_to, dbo.trips.date_from, dbo.trips.date_to, DATEDIFF(hh, dbo.trips.date_from, dbo.trips.date_to) AS hours
                               FROM dbo.trips 
                         INNER JOIN dbo.objects              ON dbo.trips.object_form_id = dbo.objects.id
                         INNER JOIN dbo.objects AS objects_1 ON dbo.trips.object_to_id = objects_1.id
                         INNER JOIN dbo.cars                 ON dbo.trips.car_id = dbo.cars.id ";
            using (var connection = new SqlConnection(connectionString))
            {
                var dataAdapter = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand(query)
                    {
                        Connection = connection
                    }
                };
                var table = new DataTable() { TableName = "Trips" };
                dataAdapter.Fill(table);

                return table;
            }
        }

        public bool Insert(int car_id, int obj_from_id, int obj_to_id, string date_from, string date_to)
        {
            if (obj_from_id == obj_to_id) return false;
            var query = $"INSERT INTO dbo.trips VALUES ({car_id},{obj_from_id},{obj_to_id},'{date_from}','{date_to}','12:00')";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query) { Connection = connection };
                command.ExecuteNonQuery();
            }
            return true;
        }

        public DataTable GetSelectData(string tableName)
        {
            string query = $"SELECT * FROM dbo.{tableName}";
            using (var connection = new SqlConnection(connectionString))
            {
                var dataAdapter = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand(query)
                    {
                        Connection = connection
                    }
                };
                var table = new DataTable() { TableName = tableName };
                dataAdapter.Fill(table);

                return table;
            }
        }
    }
}
