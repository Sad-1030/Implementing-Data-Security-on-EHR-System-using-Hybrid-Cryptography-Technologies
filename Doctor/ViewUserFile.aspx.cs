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

namespace OccupancyDetectionWeb.Doctor
{
    public partial class ViewUserFile : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string DoctorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null || Session["DoctorID"] == null)
                Response.Redirect("../Login.aspx");

            DoctorID = Session["DoctorID"].ToString();
            if(!IsPostBack)
                fillGridview(DoctorID);
        }

        private void fillGridview(string DoctorID)
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SELECT ROW_NUMBER() over (order by f.[Id] desc) as SrNo, f.[Id], p.[full_name] as [patient_name], p.[Contact_no] as [Contact_No], f.[file_name], f.[File_Path], f.[Descripion], f.[Date] from User_File_Master f Inner Join Patient_Master p on f.[Patient_Id] = p.[Patient_Id] where f.[doc_id]=@doc_id", con);
                cmd.Parameters.AddWithValue("@doc_id", DoctorID);
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

        protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string Id = e.CommandArgument.ToString();
                //Response.Write("<script>alert('"+CommandArgument+"')</script>");
                Response.Redirect("DownloadFile.aspx?Id=" + Id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}