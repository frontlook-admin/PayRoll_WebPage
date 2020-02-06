using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using frontlook_dotnetframework_library.FL_universal;
using frontlook_dotnetframework_library.FL_webpage.FL_Controls;
using frontlook_dotnetframework_library.FL_webpage.FL_DataBase;
using frontlook_dotnetframework_library.FL_webpage.FL_general;
using JetBrains.Annotations;
using MySql.Data.MySqlClient;
using PayRoll.App_Data.repository;
using repository;
using _controls = frontlook_dotnetframework_library.FL_webpage.FL_Controls.FL_Control;
using _prr = repository.payroll_repo;
using _repo = repository;

namespace PayRoll
{
    public partial class Salgen : Page
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
            return cmd.FL_Check_Column_Exists(con, _prr.database_name, tableName, columnName);
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
            var query = "SELECT salhead_formula, salhead_group_id, group_name, group_code FROM salary_head LEFT JOIN head_group ho on salary_head.salhead_group_id = ho.group_id WHERE salhead_name='" +
                x + "';";
            var c = cmd.GetValue(query, con);
            Response.Write(c.FL_printmessage_to_webpage());
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

        [UsedImplicitly]
        private void test(string id)
        {
            Regex regex = new Regex(@"([a-z A-Z]+)*");
            foreach (Match x in regex.Matches(id))
            {
                Response.Write(x.Value.FL_printmessage_to_webpage());
            }
        }

        private string[] rectified_formula(string[] formula)
        {
            int count = formula.Length;
            for (var i = 0; i <= (count - 1); i++)
            {
                formula[i] = replace_formula(formula[i]);
                Response.Write(formula[i].FL_printmessage_to_webpage());
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
                        "'AND NOT(ho.group_code = 'N') ;";
                    con.Con_switch();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        formula[i] = reader["salhead_formula"].ToString();
                        sign[i] = reader["group_code"].ToString();
                    }

                    reader.Close();
                    reader.Dispose();
                    con.Con_switch();
                }

                formula = rectified_formula(formula);

                for (var i = 0; i <= (count - 1); i++)
                {
                    Response.Write(formula[i].FL_printmessage_to_webpage());
                    cmd.CommandText = "SELECT " + formula[i] + " AS `" + ids[i] + "` FROM salary_info WHERE id=" +
                                      int.Parse(id) + ";";
                    con.Con_switch();
                    var reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        var a = reader1[ids[i]].ToString();
                        if (string.IsNullOrEmpty(a))
                        {
                            a = "0.00";
                        }
                        amts[i] = Math.Round(double.Parse(a), 2, MidpointRounding.AwayFromZero);
                        Response.Write(("<br/>" + formula[i]).FL_printmessage_to_webpage() + "  " + amts[i]);
                    }

                    reader1.Close();
                    reader1.Dispose();
                    con.Con_switch();

                    if (!string.IsNullOrEmpty(amts[i].ToString()) && !string.Equals(amts[i].ToString(), "0"))
                    {
                        salgen.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox_default(ids[i]));
                        ((TextBox)_controls.FL_GetChildControl(salgen, controlids[i])).Text = amts[i].ToString();
                    }
                }

                var amt = "";
                var f = "";
                //double attendance = attendence_calc.attendence_month(con, cmd, int.Parse(id), set_date.Text.ToString());
                //double days = attendence_calc.no_days_month(con, cmd, set_date.Text);
                for (var i = 0; i < (count - 1); i = i + 2)
                {
                    /*if (i == 0)
                    {
                        amt = amt + sign[i] + precision_point(amts[i]) + sign[i + 1] + precision_point(amts[i + 1]);
                        f = f + sign[i] + formula[i] + sign[i + 1] + formula[i + 1];
                    }
                    else
                    {
                        amt = amt + sign[i] + precision_point(amts[i]) + sign[i + 1] + precision_point(amts[i + 1]);
                        f = f + sign[i] + formula[i] + sign[i + 1] + formula[i + 1];
                    }*/
                    amt = amt + sign[i] + precision_point(amts[i]) + sign[i + 1] + precision_point(amts[i + 1]);
                    f = f + sign[i] + formula[i] + sign[i + 1] + formula[i + 1];

                }
                Response.Write(amt.FL_printmessage_to_webpage());
                Response.Write(f.FL_printmessage_to_webpage());
                var val = FL_MathExpression.FL_Result(amt).ToString();

                double final_salary = double.Parse(val);
                if (!String.IsNullOrEmpty(amt) && !string.Equals(val, "0"))
                {
                    salgen.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox_default("Total Salary"));
                    ((TextBox)_controls.FL_GetChildControl(salgen, "TotalSalary")).Text = Math.Round(final_salary, 2, MidpointRounding.AwayFromZero).ToString();
                }

            }
        }

        private double precision_point(double amount, int precision = -1)
        {
            return Math.Round(amount, precision != -1 ? precision : 2, MidpointRounding.AwayFromZero);
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

                con.Con_switch();
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
                con.Con_switch();
            }
            catch (Exception e)
            {
                Response.Write(("Message 3:" + e.Message).FL_message());
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
                con.Con_switch();
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
                con.Con_switch();
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
                con.Con_switch();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string a = reader["salhead_name"].ToString();
                    salgen.Controls.Add(FL_Label_TextBox.FL_label_readonly_textbox_default(a));
                }
                reader.Close();
                con.Con_switch();
            }
            catch (MySqlException)
            {
                Response.Write("Sorry..!! Unable to create the form. Please contact your developer for help.".FL_message());
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            get_value(emp.SelectedValue);
        }
    }
}