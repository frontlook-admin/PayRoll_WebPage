namespace helpers
{
    public static class payroll_repo
    {
        public readonly static string database_name = "payroll_db";
        /*public static int Head_Count_DB(this MySqlCommand cmd, MySqlConnection con,
            string Table_Name, string Schema_Name, string Anti_Parameter = null)
        {
            var count = 0;
            string command = "";
            if (string.IsNullOrEmpty(Anti_Parameter))
            {
                command = "SELECT COUNT(COLUMN_NAME) as c FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='" +
                          Schema_Name + "' AND TABLE_NAME='" + Table_Name + "';";
            }
            else
            {
                command = "SELECT COUNT(COLUMN_NAME) as c FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='" +
                          Schema_Name + "' AND TABLE_NAME='" + Table_Name + "' AND COLUMN_NAME NOT IN (SELECT '" + Anti_Parameter + "');";
            }
            cmd.CommandText = command;
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

        public static string[] Get_Ids_DB(this MySqlCommand cmd, MySqlConnection con,
            string Table_Name, string schemaName, string antiParameter = null)
        {
            var ids = new string[cmd.Head_Count_DB(con,Table_Name,schemaName)];
            var command = "";
            if (string.IsNullOrEmpty(antiParameter))
            {
                command = "SELECT COLUMN_NAME as c FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='" +
                          schemaName + "' AND TABLE_NAME='" + Table_Name + "';";
            }
            else
            {
                command = "SELECT COLUMN_NAME as c FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='" +
                          schemaName + "' AND TABLE_NAME='" + Table_Name + "' AND COLUMN_NAME NOT IN (SELECT '"+antiParameter+"');";
            }
            cmd.CommandText = command;
            var i = 0;
            cmd.Connection = con;
            _sql.Con_switch(con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ids[i] = reader["c"].ToString();
                i++;
            }
            reader.Close();
            reader.Dispose();
            _sql.Con_switch(con);
            return ids;
        }

        public static string[] Get_ControlIds_DB(this MySqlCommand cmd, MySqlConnection con,
            string tableName, string schemaName, string antiParameter = null)
        {
            var count = cmd.Head_Count_DB(con, tableName, schemaName,antiParameter);
            var ids = cmd.Get_Ids_DB(con, tableName, schemaName,antiParameter);
            var controlids = new string[count];
            for (var b = 0; b <= (count - 1); b++)
            {
                controlids[b] = ids[b].ToString().Replace(" ", "");
            }
            return controlids;
        }

        public static void Get_Ids_ControlIds_DB(this MySqlCommand cmd, MySqlConnection con,
            string tableName, string schemaName, string[] controlids = null, string[] ids = null)
        {
            controlids = cmd.Get_ControlIds_DB(con, tableName, schemaName);
            ids = cmd.Get_Ids_DB(con, tableName, schemaName);
        }

        private static void Dynamiccontrols_DB(this MySqlCommand cmd, MySqlConnection con,
            string Table_Name, string Schema_Name,Control control, string Anti_Parameter = null)
        {
            var count = cmd.Head_Count_DB(con, Table_Name, Schema_Name, Anti_Parameter);
            var control_ids = cmd.Get_ControlIds_DB(con, Table_Name, Schema_Name, Anti_Parameter);
            for (var b = 0; b <= (count - 1); b++)
            {
                control.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox1(control_ids[b]));
            }
        }

        private static string Selection_elements_builder(int count, IReadOnlyList<string> ids)
        {
            const string c = "`";
            const string a = "`,`";
            var q = "";
            for (var b = 0; b <= (count - 1); b++)
            {
                if (b == 0)
                {
                    if (count == 1)
                    {
                        q = q + c + ids[b] + c;
                    }
                    else
                    {
                        q = q + c + ids[b] + a;
                    }
                }
                else if (b > 0 && count > (b + 1))
                {
                    q = q + ids[b] + a;
                }
                else if (b > 0 && count == (b + 1))
                {
                    q = q + ids[b] + c;
                }
                else
                {
                    break;
                }
            }
            return q;
        }*/
    }
}