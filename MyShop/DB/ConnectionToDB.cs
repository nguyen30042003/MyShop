using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyShop.DB
{
    public class ConnectionToDB
    {
        private static readonly Lazy<ConnectionToDB> instance = new Lazy<ConnectionToDB>(() => new ConnectionToDB());
        private readonly SqlConnection connection;
        private readonly string connectionString = @"
        Server=.; 
        Database=MyShopProject; 
        Trusted_Connection=yes; 
        TrustServerCertificate=True;
        ";
        private ConnectionToDB()
        {
            connection = new SqlConnection(connectionString);
        }

        public static ConnectionToDB Instance
        {
            get { return instance.Value; }
        }

        public SqlConnection GetConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

    }
}
