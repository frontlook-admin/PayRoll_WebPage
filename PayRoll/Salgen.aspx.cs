﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using frontlook_dotnetframework_library.FL_universal;
using _response = frontlook_dotnetframework_library.FL_webpage.FL_general.FL_response;
using _controls = frontlook_dotnetframework_library.FL_webpage.FL_Controls.FL_GetControl;
using _sql = frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_MySql.FL_MySqlExecutor;
using MySql.Data.MySqlClient;
using repository;
using _prr = repository.payroll_repo;
using _repo = repository;
using frontlook_dotnetframework_library.FL_webpage.FL_Controls;
using System.Text.RegularExpressions;

namespace PayRoll
{
    public partial class Salgen : System.Web.UI.Page
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

            /*
            var x = "((`Ta`+`Main Salary`)*12/100)+1";

            bool b = check_formula_all(x);
            while (!b)
            {
                x = replace_formula(x);
                Response.Write(_response.FL_printmessage_to_webpage(x));
                b = check_formula_all(x);
            }
            Response.Write(_response.FL_printmessage_to_webpage(replace_formula(x)));

            */
        }

        private bool check_formula(string columnName, string tableName)
        {
            return _sql.FL_Check_Column_Exists(con, cmd, _prr.database_name, tableName, columnName);
        }

        private bool check_formula_all(string formula)
        {
            bool bo = true;
            Regex regex = new Regex(@"([a-z A-Z]+)*");
            foreach (Match x in regex.Matches(formula))
            {
                if (!string.IsNullOrEmpty(x.Value))
                {
                    bo = bo && check_formula(x.Value, "salary_info");
                }
            }
            return bo;
            /*var regex = new Regex(@"([a-z A-Z]+)*");
            return regex.Matches(formula).Cast<Match>().Aggregate(true, (Current, X) => Current && check_formula(X.Value, "salary_info"));*/
        }

        private string return_formula(string x)
        {
            var c = "";
            cmd.CommandText =
                "SELECT salhead_formula, salhead_group_id, group_name, group_code FROM salary_head LEFT JOIN head_group ho on salary_head.salhead_group_id = ho.group_id WHERE salhead_name='" +
                x + "';";
            _sql.Con_switch(con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                c = reader["salhead_formula"].ToString();
            }
            reader.Close();
            reader.Dispose();
            _sql.Con_switch(con);
            Response.Write(_response.FL_printmessage_to_webpage(c));
            return c;
        }

        private string replace_formula(string formula)
        {
            bool b = check_formula_all(formula);
            Regex regex = new Regex(@"([a-z A-Z]+)*");
            while (!b)
            {
                foreach (Match x in regex.Matches(formula))
                {
                    if (!string.IsNullOrEmpty(x.Value))
                    {
                        if (!check_formula(x.Value, "salary_info"))
                        {
                            var formula1 = return_formula(x.Value);
                            formula1 = "(" + formula1 + ")";
                            //Response.Write(_response.FL_printmessage_to_webpage(formula1));
                            formula = formula.Replace("`" + x.Value + "`", formula1);
                        }
                        //Response.Write(_response.FL_printmessage_to_webpage("" + check_formula_all(formula)));

                    }
                }
                b = check_formula_all(formula);
            }
            return formula;
        }

        private void test(string id)
        {
            Regex regex = new Regex(@"([a-z A-Z]+)*");
            foreach (Match x in regex.Matches(id))
            {
                Response.Write(_response.FL_printmessage_to_webpage(x.Value));
            }
        }

        private string[] rectified_formula(string[] formula)
        {
            int count = formula.Length;
            for (var i = 0; i <= (count - 1); i++)
            {
                formula[i] = replace_formula(formula[i]);
                Response.Write(_response.FL_printmessage_to_webpage(formula[i]));
            }

            return formula;
        }

        private void get_value(string id)
        {
            if (!String.Equals(id, "0"))
            {
                var count = cmd.Head_Count_Salhead(con);
                var controlids = cmd.get_ControlIds_Salhead(con);
                var ids = cmd.Get_Ids_Salhead(con);
                var groups = new string[count];
                var sign = new string[count];
                var amts = new double[count];
                var formula = new string[count];

                for (var i = 0; i <= (count - 1); i++)
                {
                    cmd.CommandText =
                        "SELECT salhead_formula, salhead_group_id, group_name, group_code FROM salary_head LEFT JOIN head_group ho on salary_head.salhead_group_id = ho.group_id WHERE salhead_name = '" + ids[i] +
                        "';";
                    _sql.Con_switch(con);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        formula[i] = reader["salhead_formula"].ToString();
                        sign[i] = reader["group_code"].ToString();
                    }

                    reader.Close();
                    reader.Dispose();
                    _sql.Con_switch(con);
                }

                formula = rectified_formula(formula);

                for (var i = 0; i <= (count - 1); i++)
                {
                    Response.Write(_response.FL_printmessage_to_webpage(formula[i]));
                    cmd.CommandText = "SELECT " + formula[i] + " AS `" + ids[i] + "` FROM salary_info WHERE id=" +
                                      int.Parse(id) + ";";
                    _sql.Con_switch(con);
                    var reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        var a = reader1[ids[i]].ToString();
                        if (string.IsNullOrEmpty(a))
                        {
                            a = "0.00";
                        }
                        amts[i] = Math.Round(double.Parse(a), 2, MidpointRounding.AwayFromZero);
                        Response.Write(_response.FL_printmessage_to_webpage("<br/>" + formula[i]) + "  " + amts[i]);
                    }

                    reader1.Close();
                    reader1.Dispose();
                    _sql.Con_switch(con);

                    if (!string.IsNullOrEmpty(amts[i].ToString()) && !string.Equals(amts[i].ToString(), "0"))
                    {
                        salgen.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox_default(ids[i]));
                        ((TextBox)_controls.FL_GetChildControl(salgen, controlids[i])).Text = amts[i].ToString();
                    }
                }

                var amt = "";
                double attendance = attendence_calc.attendence_month(con, cmd, int.Parse(id), set_date.Text.ToString());
                double days = attendence_calc.no_days_month(con, cmd, set_date.Text);
                for (var i = 0; i < (count - 1); i = i + 2)
                {
                    if (i == 0)
                    {
                        amt = amt + sign[i] + precision_point((amts[i] * attendance / days)) + sign[i + 1] + precision_point(amts[i + 1]);
                    }
                    else
                    {
                        amt = amt + sign[i] + precision_point(amts[i]) + sign[i + 1] + precision_point(amts[i + 1]);
                    }

                }
                Response.Write(amt);
                var val = FL_MathExpression.FL_Result(amt).ToString();

                double final_salary = double.Parse(val);
                if (!String.IsNullOrEmpty(amt) && !string.Equals(val, "0"))
                {
                    salgen.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox_default("Total Salary"));
                    ((TextBox)_controls.FL_GetChildControl(salgen, "TotalSalary")).Text = Math.Round(final_salary, 2, MidpointRounding.AwayFromZero).ToString();
                }

            }
        }

        public double precision_point(double amount, int precision = -1)
        {
            if (precision != -1)
            {
                return Math.Round(amount, precision, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            }
        }

        public void OnPageLoad()
        {
            Get_Elployees(emp);
            set_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void Get_Elployees(ListControl dl)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT concat(IFNULL(CONCAT(employee.id,'     '),''),IFNULL(CONCAT(employee.fname,' '),''),IFNULL(CONCAT(employee.mname,' '),''),IFNULL(CONCAT(employee.lname,' '),'')) as name,id FROM employee;";

                dl.Items.Clear();
                var item1 = new ListItem
                {
                    Text = "-Select Employee-",
                    Value = "0"
                };
                dl.Items.Add(item1);

                _sql.Con_switch(con);
                MySqlDataReader reader = cmd.ExecuteReader();

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
                _sql.Con_switch(con);
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

            if (!emp.SelectedValue.Equals("0"))
            {
                //Dynamiccontrols();
                cmd.CommandText = "SELECT " + q + " FROM salary_info WHERE id = " + id + ";";
                _sql.Con_switch(con);
                MySqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    if (!emp.SelectedValue.Equals("0"))
                    {
                        for (int j = 0; j + 1 <= count; j++)
                        {
                            string val = reader1[ids[j]].ToString();
                            if (!String.IsNullOrEmpty(val) || !String.Equals(val, "0"))
                            {
                                ((TextBox)_controls.FL_GetChildControl(salgen, controlids[j])).Text = val;
                            }
                        }
                    }
                }
                reader1.Close();
                _sql.Con_switch(con);
            }
            /*else
            {
                for (var j = 0; j + 1 <= count; j++)
                {
                    ((TextBox)_controls.FL_GetChildControl(salgen, controlids[j])).Text = string.Empty;
                }
            }*/
        }

        private void Dynamiccontrols()
        {
            try
            {
                cmd.Connection = con;
                //cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='payroll_db' AND TABLE_NAME='salary_info' AND COLUMN_NAME NOT IN (SELECT 'id');";
                cmd.CommandText = "SELECT salhead_name FROM salary_head;";
                _sql.Con_switch(con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string a = reader["salhead_name"].ToString();
                    salgen.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox_default(a));
                }
                reader.Close();
                _sql.Con_switch(con);
            }
            catch (MySqlException)
            {
                Response.Write(_response.FL_message("Sorry..!! Unable to create the form. Please contact your developer for help."));
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            get_value(emp.SelectedValue);
        }
    }
}