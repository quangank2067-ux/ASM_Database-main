
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASM_Database_Final
{
    internal class DatabaseConnection
    {
        private static string _connectionString =
            @"Data Source=localhost;Initial Catalog=CeramicStoreDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(_connectionString);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Database connection error: " + ex.Message,
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return null;
            }
        }
    }
}
