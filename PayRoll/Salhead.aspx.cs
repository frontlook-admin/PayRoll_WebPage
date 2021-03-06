﻿using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using _response = frontlook_dotnetframework_library.FL_webpage.FL_general.FL_response;
using _color = frontlook_dotnetframework_library.FL_webpage.FL_general.FL_Color;
using _sql = frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_MySql.FL_MySqlExecutor;
using frontlook_dotnetframework_library.FL_webpage.FL_general;
using MySql.Data.MySqlClient;
using repository;
using _repo = repository.FL_Repo;
using System.Web.UI;

namespace PayRoll
{
    public partial class Salhead : System.Web.UI.Page
    {
        private static readonly string Constring = ConfigurationManager.ConnectionStrings["payrollConnectionString"].ConnectionString;

        private readonly MySqlConnection con =
            new MySqlConnection(Constring);

        private readonly MySqlCommand cmd = new MySqlCommand();


        //Salhead_repo get_data = new Salhead_repo();
        //Salhead_repo persistant_data = new Salhead_repo();

        private string spaces, enter;

        protected void Page_Load(object sender, EventArgs e)
        {

            cmd.Connection = con;
            if (!IsPostBack)
            {
                Onpageload();
            }
            else
            {
                //Get_addgroupitems();
                //Get_salheadids();
                //Get_editgroupitems();
                //Modify_fetch_data();
                cmd.Connection = con;
            }

            //Response.Write(_response.FL_printmessage_to_webpage(""+_repo.Column_Exists("salary_info", "Basic 1Pay")));
        }

        private void add_controls_clear()
        {
            var gdata = new Salhead_repo { _code = "", _formula = "", _name = "", _groupcode = "", _startdate = DateTime.Today };
            add_code.Text = gdata._code;
            add_name.Text = gdata._name;
            add_ddl_group.ClearSelection();
            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                add_ddl_group.Items.FindByValue(gdata._groupcode).Selected = true;

            }
            catch (Exception cv)
            {
                Console.WriteLine(cv.Message + enter + spaces + cv.StackTrace + enter + spaces + cv.Data + enter + spaces + cv.HelpLink +
                                  enter + spaces + cv.Source + enter + spaces + cv.HResult + enter + spaces + cv.InnerException);
            }

            add_formula.Text = gdata._formula;
            add_startdate.Text = gdata._startdate.ToString("yyyy-MM-dd");
        }

        private void Listing_add_ddl()
        {
            Get_group(add_ddl_group);
            Get_formula_list(add_formula_list);
            add_controls_clear();
        }

        private void Listing_edit_ddl()
        {
            Get_salheadids(salheadid);
            Get_formula_list(edit_formula_list);
            Get_group(edit_ddl_group);
        }

        private void Get_salheadids(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                var item1 = new ListItem
                {
                    Text = "-Select Name-",
                    Value = "0"
                };
                dl.Items.Add(item1);
                cmd.CommandText = "SELECT salhead_id,salhead_code,salhead_name FROM salary_head;";
                _sql.Con_switch(con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var item = new ListItem
                    {
                        Text = reader["salhead_name"].ToString(),
                        Value = reader["salhead_id"].ToString()
                    };
                    dl.Items.Add(item);
                }
                reader.Close();
                _sql.Con_switch(con);
            }
            catch (Exception e)
            {
                Response.Write(_response.FL_message("Message 3:" + e.Message));
            }
        }

        private void Get_group(DropDownList dl)
        {
            try
            {
                using (cmd)
                {
                    cmd.CommandText = "SELECT group_id,group_name FROM head_group;";
                    dl.Items.Clear();
                    _sql.Con_switch(con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var item = new ListItem
                        {
                            Text = reader["group_name"].ToString(),
                            Value = reader["group_id"].ToString()
                        };
                        dl.Items.Add(item);
                    }
                    reader.Close();
                    _sql.Con_switch(con);
                }
            }
            catch (Exception e)
            {
                Response.Write(_response.FL_message("Message 2:" + e.Message));
            }
        }

        private void Onpageload()
        {
            spaces = Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;");
            enter = Server.HtmlDecode("<br/><br/>");

            active_add_salaryhead_div.BackColor = "#0066FF".FL_Color_Code();
            active_add_salaryhead_div.ForeColor = Color.White;
            active_edit_salaryhead_div.BackColor = Color.Silver;
            active_edit_salaryhead_div.ForeColor = Color.Black;
            add_sec_salhead.Visible = true;
            editdel_sec_salhead.Visible = false;
            Listing_add_ddl();
            add_startdate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void Insert_data(Salhead_repo ins)
        {
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO salary_head (salhead_code, salhead_name, salhead_group_id, salhead_formula,salhead_add_to_salinfo, salhead_start_date) VALUES " +
                              "('" + ins._code + "','" + ins._name + "','" + ins._groupcode + "','" + ins._formula + "'," + ins._add_to_salinfo + ",'"
                              + ins._startdate.ToString("yyyy-MM-dd") + "');";

            _sql.Con_switch(con);
            int r = cmd.ExecuteNonQuery();
            cmd.CommandText = "";
            _sql.Con_switch(con);

            if (r == 1)
            {
                if (add_checkbox.Checked.Equals(true))
                {
                    try
                    {
                        cmd.CommandText = "ALTER TABLE salary_info ADD COLUMN `" + ins._name + "` DECIMAL(20,2);";
                        _sql.Con_switch(con);
                        var q = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);
                        cmd.CommandText = "ALTER TABLE salary_generate ADD COLUMN `" + ins._name + "` DECIMAL(20,2);";
                        _sql.Con_switch(con);
                        var t = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);
                        if (q == t && q == 1)
                        {
                            Response.Write(_response.FL_message("Salary Head " + ins._name.ToUpper() + " Successfully Created...!!!",
                                "salaryhead.aspx';"));
                        }
                    }
                    catch (MySqlException x)
                    {
                        Response.Write(_response.FL_message(x.Code + "\\n\\n" + x.SqlState + "\\n\\n" + x.StackTrace + "\\n\\n" + x.Message));
                    }
                    catch (Exception ex)
                    {
                        Response.Write(_response.FL_message("Message 1:" + ex.Message));
                    }

                }
                else
                {
                    Response.Write(_response.FL_message("Salary Head " + ins._name.ToUpper() + " Successfully Created...!!!",
                        "salaryhead.aspx';"));
                }
            }
            else
            {
                Response.Write(_response.FL_message("Salary Head Is Already Present With Name " +
                                                      add_name.ToString().ToUpper()));
            }



        }

        private void Update_data(Salhead_repo set)
        {
            Response.Write(set._id);
            cmd.Connection = con;
            var cmdtxt = "UPDATE salary_head SET " +
                         "salhead_code = '" + set._code + "', salhead_name = '" + set._name + "', salhead_group_id = '" +
                         set._groupcode + "', " +
                         "salhead_formula = '" + set._formula + "', salhead_add_to_salinfo = " + set._add_to_salinfo + "," +
                         " salhead_start_date = '" + set._startdate.ToString("yyyy-MM-dd") + "' WHERE salhead_id = " + set._id + "; ";
            /*cmd.CommandText = "CALL salary_head_update(" + set._id + ",'" + set._code + "','" + set._name + "','" +
                              set._groupcode + "','" + set._formula + "',"+set._add_to_salinfo+",'" + set._startdate.ToString("yyyy-MM-dd") + "');";*/
            cmd.CommandText = cmdtxt;

            _sql.Con_switch(con);
            var r = cmd.ExecuteNonQuery();
            _sql.Con_switch(con);
            cmd.CommandText = "";

            if (r == 1)
            {
                if (edit_checkbox.Checked.Equals(true))
                {
                    if (_repo.Column_Exists("salary_info", edit_oldname.Text) && _repo.Column_Exists("salary_generate", edit_oldname.Text))
                    {
                        var q = 0;
                        var t = 0;

                        cmd.CommandText = "ALTER TABLE salary_info CHANGE COLUMN `" + edit_oldname.Text + "` `" + set._name + "` DECIMAL(20,2);";
                        _sql.Con_switch(con);
                        q = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);

                        cmd.CommandText = "ALTER TABLE salary_generate CHANGE COLUMN `" + edit_oldname.Text + "` `" + set._name + "` DECIMAL(20,2);";
                        _sql.Con_switch(con);
                        t = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);

                        if (q == t && q == 1)
                        {
                            Listing_edit_ddl();
                            Response.Write(_response.FL_message("Salary Head Column " + edit_oldname.Text.ToUpper() + " Changed To" + set._name.ToUpper() +
                                                                " Successfully...!!!", "~/salaryhead.aspx"));
                        }
                    }
                    else
                    {
                        try
                        {
                            var q = 0;
                            var t = 0;

                            if (_repo.Column_Exists("salary_info", edit_oldname.Text))
                            {
                                cmd.CommandText = "ALTER TABLE salary_info CHANGE COLUMN `" + edit_oldname.Text + "` `" + set._name + "` DECIMAL(20,2);";
                                _sql.Con_switch(con);
                                q = cmd.ExecuteNonQuery();
                                _sql.Con_switch(con);
                            }
                            else
                            {
                                cmd.CommandText = "ALTER TABLE salary_info ADD COLUMN `" + set._name + "` DECIMAL(20,2);";
                                _sql.Con_switch(con);
                                q = cmd.ExecuteNonQuery();
                                _sql.Con_switch(con);
                            }

                            if (_repo.Column_Exists("salary_generate", edit_oldname.Text))
                            {
                                cmd.CommandText = "ALTER TABLE salary_generate CHANGE COLUMN `" + edit_oldname.Text + "` `" + set._name + "` DECIMAL(20,2);";
                                _sql.Con_switch(con);
                                t = cmd.ExecuteNonQuery();
                                _sql.Con_switch(con);
                            }
                            else
                            {
                                cmd.CommandText = "ALTER TABLE salary_generate ADD COLUMN `" + set._name + "` DECIMAL(20,2);";
                                _sql.Con_switch(con);
                                t = cmd.ExecuteNonQuery();
                                _sql.Con_switch(con);
                            }

                            if (q == t && t == 1)
                            {
                                Response.Write(_response.FL_message("Salary Head " + set._name.ToUpper() + " Successfully Created...!!!",
                                    "salaryhead.aspx';"));
                            }
                        }
                        catch (MySqlException x)
                        {
                            Response.Write(_response.FL_message(x.Code + "\\n\\n" + x.SqlState + "\\n\\n" + x.StackTrace + "\\n\\n" + x.Message));
                        }
                        catch (Exception ex)
                        {
                            Response.Write(_response.FL_message("Message 1:" + ex.Message));
                        }
                    }
                }
                else
                {
                    /*if (_repo.Column_Exists("salary_info", edit_oldname.Text))
                    {
                        cmd.CommandText = "ALTER TABLE salary_info drop COLUMN `" + edit_oldname.Text + "`;";
                        _sql.Con_switch(con);
                        var q = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);
                        cmd.CommandText = "ALTER TABLE salary_generate drop COLUMN `" + edit_oldname.Text + "`;";
                        _sql.Con_switch(con);
                        var t = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);
                        if (q == t && t == 1)
                        {
                            Response.Write(_response.FL_message("Salary Head Column " + edit_oldname.Text.ToUpper() +
                                                                " Changed To" + set._name.ToUpper() +
                                                                " Successfully...!!!", "~/salaryhead.aspx"));
                        }
                    }*/
                    var q = 0;
                    var t = 0;
                    if (_repo.Column_Exists("salary_info", edit_oldname.Text))
                    {
                        cmd.CommandText = "ALTER TABLE salary_info drop COLUMN `" + edit_oldname.Text + "`;";
                        _sql.Con_switch(con);
                        q = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);
                    }

                    if (_repo.Column_Exists("salary_generate", edit_oldname.Text))
                    {
                        cmd.CommandText = "ALTER TABLE salary_generate drop COLUMN `" + edit_oldname.Text + "`;";
                        _sql.Con_switch(con);
                        t = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);

                    }
                    if (q == t && t == 1)
                    {
                        Response.Write(_response.FL_message("Salary Head Column " + edit_oldname.Text.ToUpper() +
                                                            " Changed To" + set._name.ToUpper() +
                                                            " Successfully...!!!", "~/salaryhead.aspx"));
                    }
                }
            }
            else
            {

                Response.Write(_response.FL_message("Salary Head Is Already Present With Name " +
                set._name.ToUpper()));
            }


        }

        private void Delete_data()
        {
            cmd.Connection = con;
            try
            {
                cmd.CommandText = "CALL salary_head_delete(" + int.Parse(salheadid.SelectedValue) + ");";
                _sql.Con_switch(con);
                var r = cmd.ExecuteNonQuery();
                _sql.Con_switch(con);
                if (r == 1)
                {
                    var q = 0;
                    var t = 0;
                    if (_repo.Column_Exists("salary_info", edit_oldname.Text))
                    {
                        cmd.CommandText = "ALTER TABLE salary_info drop COLUMN `" + edit_oldname.Text + "`;";
                        _sql.Con_switch(con);
                        q = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);
                    }

                    if (_repo.Column_Exists("salary_generate", edit_oldname.Text))
                    {
                        cmd.CommandText = "ALTER TABLE salary_generate drop COLUMN `" + edit_oldname.Text + "`;";
                        _sql.Con_switch(con);
                        t = cmd.ExecuteNonQuery();
                        _sql.Con_switch(con);

                    }
                    if (q == t && t == 1)
                    {
                        Response.Write(_response.FL_message(edit_oldname.Text.ToUpper(), "salaryhead.aspx"));
                    }
                }
                else
                {
                    Response.Write(_response.FL_message("Something went wrong while deleting " +
                                                        edit_oldname.Text.ToUpper() + "...!!"));
                }
            }
            catch (Exception e)
            {
                Response.Write(_response.FL_message(e.Message));
            }
        }

        private void Set_data_for_saving()
        {
            char[] a = new char[]
            {
            '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '`', '|'
            ,';','"',':','>','?','<',',','.','{','}','[',']','/','*','-','+'
            };
            var setData = new Salhead_repo
            {
                _name = add_name.Text,
                _code = add_name.Text.Trim(a).Replace(" ", string.Empty),
                _groupcode = add_ddl_group.SelectedItem.Value,
                _formula = add_formula.Text,
                _add_to_salinfo = add_checkbox.Checked,
                _startdate = DateTime.Parse(add_startdate.Text)
            };


            Insert_data(setData);


            Listing_add_ddl();
        }

        private void Set_data_for_updating()
        {
            char[] a = new char[]
            {
            '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '`', '|'
            ,';','"',':','>','?','<',',','.','{','}','[',']','/','*','-','+'
            };
            var set = new Salhead_repo
            {
                _name = edit_name.Text,
                _code = edit_name.Text.Trim(a).Replace(" ", string.Empty),
                _formula = edit_formula.Text,
                _groupcode = edit_ddl_group.SelectedValue,
                _id = int.Parse(salheadid.SelectedValue),
                _add_to_salinfo = edit_checkbox.Checked,
                _oldname = edit_oldname.Text,
                _startdate = DateTime.ParseExact(edit_startdate.Text, "yyyy-MM-dd", null)
            };
            Update_data(set);
        }

        private Salhead_repo Modify_data_allocation()
        {
            var gdata = new Salhead_repo { _id = int.Parse(salheadid.SelectedValue) };
            cmd.CommandText = "SELECT salhead_code,salhead_name,salhead_group_id,salhead_formula,salhead_add_to_salinfo,salhead_start_date FROM salary_head WHERE salhead_id = " + gdata._id + ";";
            _sql.Con_switch(con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                gdata._code = reader["salhead_code"].ToString();
                gdata._name = reader["salhead_name"].ToString();
                gdata._groupcode = reader["salhead_group_id"].ToString();
                gdata._formula = reader["salhead_formula"].ToString();
                var a = reader["salhead_add_to_salinfo"].ToString();
                if (a == "1")
                {
                    gdata._add_to_salinfo = true;
                }
                else
                {
                    gdata._add_to_salinfo = false;
                }
                gdata._startdate = DateTime.Parse(reader["salhead_start_date"].ToString());
            }
            _sql.Con_switch(con);
            gdata._oldname = gdata._name;
            return gdata;
        }

        private void Modify_fetch_data()
        {
            var gdata = Modify_data_allocation();

            edit_code.Text = gdata._code;
            edit_name.Text = gdata._name;
            edit_oldname.Text = gdata._oldname;
            edit_ddl_group.ClearSelection();
            Response.Write(gdata._groupcode);
            try
            {
                edit_ddl_group.Items.FindByValue(gdata._groupcode).Selected = true;
            }
            catch (Exception cv)
            {
                Console.WriteLine(cv.Message + enter + spaces + cv.StackTrace + enter + spaces + cv.Data + enter + spaces + cv.HelpLink +

                                  enter + spaces + cv.Source + enter + spaces + cv.HResult + enter + spaces + cv.InnerException);
            }

            edit_formula.Text = gdata._formula;
            edit_checkbox.Checked = gdata._add_to_salinfo;
            edit_startdate.Text = gdata._startdate.ToString("yyyy-MM-dd");

        }

        protected void Active_add_salaryhead_div_Click(object sender, EventArgs e)
        {
            active_add_salaryhead_div.BackColor = "#0066FF".FL_Color_Code();
            active_add_salaryhead_div.ForeColor = Color.White;
            active_edit_salaryhead_div.BackColor = Color.Silver;
            active_edit_salaryhead_div.ForeColor = Color.Black;
            add_sec_salhead.Visible = true;
            editdel_sec_salhead.Visible = false;
            Listing_add_ddl();
        }

        protected void Active_edit_salaryhead_div_Click(object sender, EventArgs e)
        {
            active_edit_salaryhead_div.BackColor = "#0066FF".FL_Color_Code();
            active_edit_salaryhead_div.ForeColor = Color.White;
            active_add_salaryhead_div.BackColor = Color.Silver;
            active_add_salaryhead_div.ForeColor = Color.Black;
            editdel_sec_salhead.Visible = true;
            add_sec_salhead.Visible = false;
            Listing_edit_ddl();
            Modify_fetch_data();
        }

        protected void Save_salhead_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(add_name.Text) &&
                           !string.IsNullOrEmpty(add_ddl_group.SelectedValue))
            {
                Set_data_for_saving();
            }
            else
            {
                Response.Write(_response.FL_message("No Fields Can Be Empty..!!"));
            }
        }

        protected void Salheadid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Write(salheadid.SelectedValue);
            Modify_fetch_data();
        }

        protected void Update_salhead_Click(object sender, EventArgs e)
        {

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse

            if (!string.IsNullOrEmpty(edit_name.Text))
            {
                Set_data_for_updating();
            }
            else
            {
                Response.Write(_response.FL_message("No Fields Can Be Empty..!!"));
            }

            Listing_edit_ddl();
            Modify_fetch_data();
        }

        protected void Del_salhead_Click(object sender, EventArgs e)
        {
            Delete_data();
            Listing_edit_ddl();
            Modify_fetch_data();
        }

        private void Get_formula_list(ListBox lb)
        {
            try
            {

                using (cmd)
                {
                    cmd.CommandText = "SELECT salhead_name,salhead_formula FROM salary_head;";
                    lb.Items.Clear();
                    var item0 = new ListItem
                    {
                        Text = "CONDITIONAL FORMULA",
                        Value = "(IF ((CONDITION),(TRUE),(FALSE)))"
                    };
                    lb.Items.Add(item0);
                    _sql.Con_switch(con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var item = new ListItem
                        {
                            Text = reader["salhead_name"].ToString(),
                            Value = reader["salhead_formula"].ToString()
                        };
                        lb.Items.Add(item);
                    }
                    reader.Close();
                    _sql.Con_switch(con);
                }
            }
            catch (Exception e)
            {
                Response.Write(_response.FL_message("Message 2:" + e.Message));
            }
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        private void add_text_to_cursor_position(TextBox TextBox, string input)
        {
            string jsFunc = $"insertAtCursor(" + TextBox.ID + "," + input + ")";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "insertAtCursor", jsFunc, true);
            ClientScript.RegisterClientScriptBlock(GetType(), "id", jsFunc, true);
        }

        protected void add_formula_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            add_formula.Text = add_formula.Text + add_formula_list.SelectedValue;

            var insertText = add_formula_list.SelectedValue;
            add_text_to_cursor_position(add_formula, insertText);
            string jsFunc = $"insertAtCursor(" + add_formula.ID + "," + insertText + ")";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);
            ScriptManager.RegisterClientScriptBlock(this.add_formula_list, GetType(), "insertAtCursor", jsFunc, true);
        }

        protected void edit_formula_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            edit_formula.Text = edit_formula.Text + edit_formula_list.SelectedValue;
        }
    }
}