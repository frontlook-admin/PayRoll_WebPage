using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using frontlook_dotnetframework_library.FL_webpage.FL_Controls;
using frontlook_dotnetframework_library.FL_webpage.FL_DataBase;
using MySql.Data.MySqlClient;
using _response = frontlook_dotnetframework_library.FL_webpage.FL_general.FL_response;
using _controls = frontlook_dotnetframework_library.FL_webpage.FL_Controls.FL_Control;

namespace PayRoll.Pages.AttendanceModule
{
    public partial class attendance : Page
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

        private void OnPageLoad()
        {
            Get_Elployees(emp);
        }

        private void Get_Elployees(ListControl Dl)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT concat(IFNULL(CONCAT(employee.id,'     '),''),IFNULL(CONCAT(employee.fname,' '),''),IFNULL(CONCAT(employee.mname,' '),''),IFNULL(CONCAT(employee.lname,' '),'')) as name,id FROM employee;";

                Dl.Items.Clear();
                var Item1 = new ListItem
                {
                    Text = "-Select Employee-",
                    Value = "0"
                };
                Dl.Items.Add(Item1);

                con.Con_switch();
                var Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    var Item = new ListItem
                    {
                        Text = Reader["name"].ToString(),
                        Value = Reader["id"].ToString()
                    };
                    Dl.Items.Add(Item);
                }
                Reader.Close();
                con.Con_switch();
            }
            catch (Exception E)
            {
                Response.Write(_response.FL_message("Message 3:" + E.Message));
            }
        }

        private void Dynamiccontrols()
        {
            try
            {
                var Count = cmd.Head_Count_DB(con, "attendance", "payroll_db", "id");
                var ControlIds = cmd.Get_ControlIds_DB(con, "attendance", "payroll_db", "id");
                for (var B = 0; B <= (Count - 1); B++)
                {
                    if (ControlIds[B].Equals("Attendance"))
                    {
                        attendance_form.Controls.Add(FL_Label_CheckBox.FL_label_readonly_checkbox(ControlIds[B]));
                    }
                    else if (ControlIds[B].Equals("Date"))
                    {
                        attendance_form.Controls.Add(FL_Label_TextBox.FL_label_textbox_date(ControlIds[B]));
                        _controls.FL_SetControlString(attendance_form, ControlIds[B], DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                }
            }
            catch (Exception)
            {
                Response.Write(_response.FL_message("Sorry..!! Unable to create the form. Please contact your developer for help."));
            }
        }

        private string command_builder()
        {
            var Count1 = cmd.Head_Count_DB(con, "attendance", "payroll_db");
            var Count = cmd.Head_Count_DB(con, "attendance", "payroll_db", "id");
            var Ids = cmd.Get_Ids_DB(con, "attendance", "payroll_db");
            var ControlIds = cmd.Get_ControlIds_DB(con, "attendance", "payroll_db", "id");
            var queary = "INSERT INTO attendance (" + FL_ControlId_Dynamic.Selection_Input_Builder(Count1, Ids) +
                         ") VALUES (" + emp.SelectedValue + "," +
                         FL_ControlId_Dynamic.Selection_elements_builder(Count, ControlIds, attendance_form) + ");";

            return queary;
        }

        protected void update_attendence_Click(object sender, EventArgs e)
        {
            if (!emp.SelectedValue.Equals("0"))
            {
                cmd.CommandText = command_builder();
                con.Con_switch();
                int r = cmd.ExecuteNonQuery();
                con.Con_switch();
                if (r.Equals(1))
                {
                    Response.Write(_response.FL_message("Attendence given for " + emp.SelectedItem.Text + "..!!", "/attendance"));
                }
            }
        }
    }
}