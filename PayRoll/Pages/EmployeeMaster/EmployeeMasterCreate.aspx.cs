using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _response = frontlook_dotnetframework_library.FL_webpage.FL_general.FL_response;
using frontlook_dotnetframework_library.FL_webpage.FL_Controls;
using frontlook_dotnetframework_library.FL_webpage.FL_DataBase;
using frontlook_dotnetframework_library.FL_webpage.FL_DataBase.FL_MySql;
using frontlook_dotnetframework_library.FL_webpage.FL_general;
using MySql.Data.MySqlClient;

namespace PayRoll.Pages.EmployeeMaster
{
    public partial class EmployeeMasterCreate : System.Web.UI.Page
    {
        private static readonly string Constring = ConfigurationManager.ConnectionStrings["payrollConnectionString"].ConnectionString;

        private readonly MySqlConnection con =
            new MySqlConnection(Constring);

        private readonly MySqlCommand cmd = new MySqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {

            }
            Dynamiccontrols();
        }

        private void Dynamiccontrols()
        {
            var count = cmd.FL_Get_ColumnCount(con, "payroll_db", "employee", "id");
            var columnNames = cmd.FL_Get_ColumnNames(con, "payroll_db", "employee", "id");
            
            for (var i = 0; i < count; i++)
            {
                if (columnNames[i] == "Designation")
                {
                    var q = "Select `Designation Id`,`Designation Name` FROM designation";
                    get_ddlitems(columnNames[i],q,"Designation Name","Designation Id");
                }
                else if (columnNames[i] == "Department")
                {
                    var q = "Select `Department Id`,`Department Name` FROM department";
                    get_ddlitems(columnNames[i], q, "Department Name", "Department Id");
                }
                else if (columnNames[i] == "Grade")
                {
                    var q = "Select `Grade Id`,`Grade Name` FROM grade";
                    get_ddlitems(columnNames[i], q, "Grade Name", "Grade Id");
                }
                else if (columnNames[i] == "Active")
                {
                    empdiv.Controls.Add(FL_Label_CheckBox.FL_label_checkbox(columnNames[i]));
                }
                else 
                {
                    empdiv.Controls.Add(FL_Label_TextBox.FL_label_textbox_default(columnNames[i]));
                }
                
                //Response.Write(columnNames[i].FL_printmessage_to_webpage());
            }

        }

        private void get_ddlitems(string columnName, string query, string value1, string value2)
        {
            empdiv.Controls.Add(FL_Label_DropDownList.FL_form_create_dropdownlist1(columnName));
            var Dl = (DropDownList) FL_Control.FL_GetChildControl(empdiv, columnName.Replace(" ", ""));
            try
            {
                cmd.Connection = con;
                cmd.CommandText = query;
                Dl.Items.Clear();
                var Item1 = new ListItem
                {
                    Text = "-Select "+columnName+"-",
                    Value = "0"
                };
                Dl.Items.Add(Item1);

                con.Con_switch();
                var Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    var Item = new ListItem
                    {
                        Text = Reader[value1].ToString(),
                        Value = Reader[value2].ToString()
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

        public void insert_data()
        {
            var count = cmd.FL_Get_ColumnCount(con, "payroll_db", "employee", "id");
            var columnNames = cmd.FL_Get_ColumnNames(con, "payroll_db", "employee", "id");
            var controlids = new string[count];
            for (var i = 0; i < count; i++)
            {
                controlids[i] = columnNames[i].Replace(" ", "");
            }
            var r = cmd.ExecuteCommand(con,
                FL_MySqlExecutor.FL_MySql_InsertQueryBuilder(count, columnNames, empdiv, controlids, "payroll_db",
                    "employee"));
            if (r == 0)
            {
                Response.Write("Data saved successfully..!!".FL_message("~/Pages/EmployeeMaster/EmployeeMasterIndex.aspx"));
            }
        }

        private string Queary_build_updatedata(int count, IReadOnlyList<string> ids)
        {
            var q = FL_MySqlExecutor.FL_MySql_ColumnValueElementBuilder(empdiv, count, ids);
            return q;
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            insert_data();
        }
    }
}