using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Patient
{
    public partial class ManageFiles : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string PatientID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null || Session["PatientID"] == null)
                Response.Redirect("../Login.aspx");

            PatientID = Session["PatientID"].ToString();
            if (!IsPostBack)
                fillGridview(PatientID);
        }

        private void fillGridview(string PatientID)
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select ROW_NUMBER() over (order by fm.id desc) as [SrNo], fm.[id], fm.[File_Name], dm.[Full_Name], dm.[Email], dm.[Phone], fm.[Descripion], fm.[Date] from User_File_Master fm Inner Join Doctor_Master dm on fm.[Doc_id] = dm.[Id] where Patient_id = @Patient_id", con);
                cmd.Parameters.AddWithValue("@Patient_id", PatientID);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gvFile.DataSource = dt;
                    gvFile.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}