using MySql.Data.MySqlClient;
using _sql = frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_MySql.FL_MySqlExecutor;

namespace repository
{
    public static class general_repo
    {
        public static int Head_Count_Salhead(this MySqlCommand cmd, MySqlConnection con)
        {
            var count = 0;
            cmd.CommandText = "SELECT COUNT(salhead_name) as c FROM salary_head WHERE salhead_add_to_salinfo = 1;";
            cmd.Connection = con;
            _sql.Con_switch(con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = int.Parse(reader["c"].ToString());
            }
            reader.Close();
            _sql.Con_switch(con);
            return count;
        }

        public static string[] Get_Ids_Salhead(this MySqlCommand cmd, MySqlConnection con)
        {
            var ids = new string[cmd.Head_Count_Salhead(con)];
            cmd.CommandText = "SELECT salhead_name FROM salary_head WHERE salhead_add_to_salinfo = 1;";
            var i = 0;
            cmd.Connection = con;
            _sql.Con_switch(con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ids[i] = reader["salhead_name"].ToString();
                i++;
            }
            reader.Close();
            reader.Dispose();
            _sql.Con_switch(con);
            return ids;
        }

        public static string[] get_ControlIds_Salhead(this MySqlCommand cmd, MySqlConnection con)
        {
            var controlids = new string[cmd.Head_Count_Salhead(con)];
            cmd.CommandText = "SELECT salhead_name FROM salary_head WHERE salhead_add_to_salinfo = 1;";
            var i = 0;
            cmd.Connection = con;
            _sql.Con_switch(con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                controlids[i] = reader["salhead_name"].ToString().Replace(" ", "");
                i++;
            }
            reader.Close();
            reader.Dispose();
            _sql.Con_switch(con);
            return controlids;
        }

        public static void get_SalHead_ControlIds(this MySqlCommand cmd, MySqlConnection con, string[] controlids = null, string[] ids = null)
        {
            controlids = cmd.get_ControlIds_Salhead(con);
            ids = cmd.Get_Ids_Salhead(con);
        }
    }
}