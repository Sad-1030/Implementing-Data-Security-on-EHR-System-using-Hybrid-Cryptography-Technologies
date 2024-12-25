using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Doctor
{
    public partial class ManagePatient : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Name"] == null)
                Response.Redirect("../Login.aspx");

            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select row_number() over (order by [Date] desc) as [SrNo], [Patient_id], [Full_Name], [Contact_No], [Email_Id], [Address], format([Date],'dd-MMM-yyyy') as [Date] from patient_master", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gvPatient.DataSource = dt;
                    gvPatient.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}