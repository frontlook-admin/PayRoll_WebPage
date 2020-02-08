using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using frontlook_dotnetframework_library.FL_webpage.FL_Controls;
using MySql.Data.MySqlClient;
using _response = frontlook_dotnetframework_library.FL_webpage.FL_general.FL_response;
using _controls = frontlook_dotnetframework_library.FL_webpage.FL_Controls.FL_Control;
using frontlook_dotnetframework_library.FL_webpage.FL_DataBase;
using PayRoll.App_Data.repository;

namespace PayRoll
{
    public partial class Salinfo : System.Web.UI.Page
    {
        private static readonly string Constring = ConfigurationManager.ConnectionStrings["payrollConnectionString"].ConnectionString;

        private readonly MySqlConnection con =
            new MySqlConnection(Constring);

        private readonly MySqlCommand cmd = new MySqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                OnPageLoad();
            }
            Dynamiccontrols();
        }

        public void OnPageLoad()
        {
            Get_Elployees(emp);
        }

        private void Dynamiccontrols()
        {
            try
            {
                cmd.Connection = con;
                //cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='payroll_db' AND TABLE_NAME='salary_info' AND COLUMN_NAME NOT IN (SELECT 'id');";
                cmd.CommandText = "SELECT salhead_name FROM salary_head WHERE salhead_add_to_salinfo = 1;";
                con.Con_switch();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var a = reader["salhead_name"].ToString();
                    add_sec_salinfo.Controls.Add(FL_Label_TextBox.FL_label_textbox_default(a));
                }
                reader.Close();
                con.Con_switch();
            }
            catch (MySqlException)
            {
                Response.Write(_response.FL_message("Sorry..!! Unable to create the form. Please contact your developer for help."));
            }
        }

        private void Get_Elployees(ListControl dl)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT concat(IFNULL(CONCAT(employee.id,'     '),''),IFNULL(CONCAT(employee.fname,' '),''),IFNULL(CONCAT(employee.mname,' '),''),IFNULL(CONCAT(employee.lname,' '),'')) as name,id FROM employee;";
                /*DropDownList ddl = new DropDownList();
                add_sec_salinfo.Controls.Add(FL_Label_DropDownList.FL_form_create_dropdownlist1("Employee", con,cmd, 
                    "SELECT concat(IFNULL(CONCAT(employee.fname,' '),''),IFNULL(CONCAT(employee.mname,' '),''),IFNULL(CONCAT(employee.lname,' '),'')) as Employee,id FROM employee;",
                    "Employee","id"));*/
                dl.Items.Clear();
                var item1 = new ListItem
                {
                    Text = "-Select Employee-",
                    Value = "0"
                };
                dl.Items.Add(item1);

                con.Con_switch();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var item = new ListItem
                    {
                        Text = reader["name"].ToString(),
                        Value = reader["id"].ToString()
                    };
                    dl.Items.Add(item);
                }
                reader.Close();
                con.Con_switch();
            }
            catch (Exception e)
            {
                Response.Write(_response.FL_message("Message 3:" + e.Message));
            }
        }

        private string Selection_elements_builder(int count, IReadOnlyList<string> ids)
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
        }

        private void Get_data(string id)
        {
            var count = cmd.Head_Count_Salhead(con);

            var controlids = cmd.get_ControlIds_Salhead(con);
            var ids = cmd.Get_Ids_Salhead(con);

            var q = Selection_elements_builder(count, ids);

            if (emp.SelectedValue.Equals("0"))
            {
                for (var j = 0; j + 1 <= count; j++)
                {
                    ((TextBox)_controls.FL_GetChildControl(add_sec_salinfo, controlids[j])).Text = string.Empty;
                }
            }
            else
            {
                //Response.Write(r + "");
                cmd.CommandText = "SELECT " + q + " FROM salary_info WHERE id = " + id + ";";
                con.Con_switch();
                MySqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    if (emp.SelectedValue.Equals("0"))
                    {
                        for (int j = 0; j + 1 <= count; j++)
                        {
                            ((TextBox)_controls.FL_GetChildControl(add_sec_salinfo, controlids[j])).Text = string.Empty;

                        }
                    }
                    else
                    {
                        for (int j = 0; j + 1 <= count; j++)
                        {
                            string val = reader1[ids[j]].ToString();
                            ((TextBox)_controls.FL_GetChildControl(add_sec_salinfo, controlids[j])).Text = val;

                        }
                    }
                }
                reader1.Close();
                con.Con_switch();
            }
        }

        private string Queary_build_updatedata(int count, IReadOnlyList<string> controlids, IReadOnlyList<string> ids)
        {
            var q = "";
            const string a = ", `";
            const string b = "`=";
            const string c = "`";

            for (var j = 0; j <= (count - 1); j++)
            {
                string v;
                if (j == 0)
                {
                    if (count == 1)
                    {
                        v = _controls.FL_GetControlString(add_sec_salinfo, controlids[j]).Trim();
                        q = c + ids[j] + b + v;
                    }
                    else
                    {
                        v = _controls.FL_GetControlString(add_sec_salinfo, controlids[j]).Trim();
                        q = c + ids[j] + b + v + a;
                    }
                }
                else if (j > 0 && count > (j + 1))
                {
                    v = _controls.FL_GetControlString(add_sec_salinfo, controlids[j]).Trim();
                    q = q + ids[j] + b + v + a;
                }
                else if (j > 0 && count == (j + 1))
                {
                    v = _controls.FL_GetControlString(add_sec_salinfo, controlids[j]).Trim();
                    q = "UPDATE salary_info SET " + q + ids[j] + b + v + " WHERE id = " + emp.SelectedValue + "; ";
                }
            }
            return q;
        }

        protected void update_salinfo_Click(object sender, EventArgs e)
        {
            try
            {
                var count = cmd.Head_Count_Salhead(con);
                var controlids = cmd.get_ControlIds_Salhead(con);
                var ids = cmd.Get_Ids_Salhead(con);
                string queary = Queary_build_updatedata(count, controlids, ids);
                //Response.Write(queary);
                cmd.CommandText = queary;
                con.Con_switch();
                int r = cmd.ExecuteNonQuery();
                con.Con_switch();
                if (r == 1)
                {
                    Response.Write(_response.FL_message("Data Updated Successfully..!!"));
                    emp.ClearSelection();
                    emp.Items.FindByValue("0");
                    Get_data(emp.SelectedValue);
                }
                else
                {
                    Response.Write(
                        _response.FL_message("Something went wrong..!! Contact your system administrator for help..!!"));
                }
            }
            catch (MySqlException ex)
            {
                Response.Write(_response.FL_message(ex.Message));
            }
            /*catch (Exception exs)
            {
                Response.Write(_response.FL_message("Fields must not contain any characters or special characters other than dot..!!"));
            }*/
        }

        protected void emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_data(emp.SelectedValue);
        }
    }

    /*private string queary_build_savedata()
        {
            string q="";
            string a = "`, `";
            string g = ", ";
            string b = "(`";
            string c = "`) VALUES ();";
            string e;
            string v = "";
            string d = "`";
            string f = "";
            int count = 0;
            cmd.CommandText = "SELECT COUNT(salhead_name) as c FROM salary_head;";
            cmd.Connection = con;
            con.Con_switch();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = int.Parse(reader["c"].ToString());
                //Response.Write(_response.FL_message(count.ToString()));
            }
            reader.Close();
            con.Con_switch();

            cmd.CommandText = "SELECT salhead_name FROM salary_head;";
            int i = 0;
            cmd.Connection = con;
            con.Con_switch();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (i == 0)
                {
                    if (count == 1)
                    {
                        v = f+ _controls.FL_GetControlString(add_sec_salinfo, reader["salhead_name"].ToString().Replace(" ", "")).Trim() + f;
                        e = "`) VALUES (" + v + ");";
                        q = b + reader["salhead_name"].ToString() + e;
                    }
                    else
                    {
                        v = d+_controls.FL_GetControlString(add_sec_salinfo, reader["salhead_name"].ToString().Replace(" ", "")).Trim()+ g;
                        q = b + reader["salhead_name"].ToString() + a;
                    }
                }
                else if (i > 0 && count > (i + 1))
                {
                    v = v+ _controls.FL_GetControlString(add_sec_salinfo, reader["salhead_name"].ToString().Replace(" ", "")).Trim() + g;
                    q = q + reader["salhead_name"].ToString() + a;
                }
                else if (i > 0 && count == (i + 1))
                {
                    v =v+ _controls.FL_GetControlString(add_sec_salinfo, reader["salhead_name"].ToString().Replace(" ", "")).Trim() + f;
                    e = "`) VALUES (" + v + ") WHERE id = "+emp.SelectedValue+";";
                    q = "INSERT INTO salary_info "+q + reader["salhead_name"].ToString() + e;
                }

                i++;
            }
            reader.Close();
            con.Con_switch();
            Response.Write(q.ToString());
            return q;
        }*/

}