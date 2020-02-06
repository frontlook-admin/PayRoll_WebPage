using MySql.Data.MySqlClient;
using _sql = frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_SqlExecutor;

namespace repository
{
    public static class attendence_calc
    {
        public static double attendence_month(MySqlConnection con, MySqlCommand cmd, int employeeId, string date)
        {
            cmd.CommandText = "SELECT COUNT(Attendance) as c FROM attendance WHERE id = " +
                              employeeId + " AND MONTH(date) = MONTH('" + date + "')";
            _sql.Con_switch(con);
            MySqlDataReader reader = cmd.ExecuteReader();
            double c = 0;
            while (reader.Read())
            {
                c = double.Parse(reader["c"].ToString());
            }
            reader.Dispose();
            reader.Close();
            _sql.Con_switch(con);
            return c;
        }

        public static double no_days_month(MySqlConnection con, MySqlCommand cmd, string date)
        {
            cmd.CommandText = "SELECT DAYOFMONTH(LAST_DAY('" + date + "')) as c;";
            _sql.Con_switch(con);
            MySqlDataReader reader = cmd.ExecuteReader();
            double c = 0;
            while (reader.Read())
            {
                c = double.Parse(reader["c"].ToString());
            }
            reader.Dispose();
            reader.Close();
            _sql.Con_switch(con);
            return c;
        }
    }
}