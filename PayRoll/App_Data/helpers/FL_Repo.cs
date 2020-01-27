using System.Configuration;
using MySql.Data.MySqlClient;
using _sql = frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_MySql.FL_MySqlExecutor;

namespace helpers
{
    public class FL_Repo
    {
        private static readonly string Constring = ConfigurationManager.ConnectionStrings["payrollConnectionString"].ConnectionString;

        private static readonly MySqlConnection con =
            new MySqlConnection(Constring);

        private static readonly MySqlCommand cmd = new MySqlCommand();
        public static bool Column_Exists(string TableName, string ColumnName)
        {
            return _sql.FL_Check_Column_Exists(con, cmd, "payroll_db", TableName, ColumnName);
        }
    }
}