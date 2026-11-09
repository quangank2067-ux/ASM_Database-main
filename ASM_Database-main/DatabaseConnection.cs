using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_Database_Final
{
    internal class DatabaseConnection
    {
        private static string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CeramicStoreDB;
                                                    Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
            }
            catch (Exception ex) 
            {
               MessageBox.Show("Error while connecting to the database","Warning",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return connection;
        }
    }
}
