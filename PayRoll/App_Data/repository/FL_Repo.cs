using System.Configuration;
using frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_MySql;
using MySql.Data.MySqlClient;

namespace PayRoll.App_Data.repository
{
    public class FL_Repo
    {
        private static readonly string Constring = ConfigurationManager.ConnectionStrings["payrollConnectionString"].ConnectionString;

        private static readonly MySqlConnection con =
            new MySqlConnection(Constring);

        private static readonly MySqlCommand cmd = new MySqlCommand();
        public static bool Column_Exists(string TableName, string ColumnName)
        {
            return cmd.FL_MySql_Check_Column_Exists(con, "payroll_db", TableName, ColumnName);
        }
    }
}