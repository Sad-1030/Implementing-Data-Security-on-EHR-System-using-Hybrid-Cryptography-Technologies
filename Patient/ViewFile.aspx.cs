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
    public partial class ViewFile : System.Web.UI.Page
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
                cmd = new SqlCommand("select ROW_NUMBER() over (order by f.[id] desc) as [SrNo], f.[Id], d.[Full_Name] as [Doctor], d.[Phone], f.[File_Name], f.[File_Path], f.[Description], format(f.[Date],'dd-MMM-yyyy') as [Date]  from file_master f Inner Join Doctor_Master d on f.[doc_id] = d.[Id] where f.[Patient_Id] = @Patient_id", con);
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

        protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string FileID = e.CommandArgument.ToString();
                string CommandName = e.CommandName.ToString();
                if (CommandName.Equals("DownloadTable"))
                {
                    Response.Redirect("DownloadFile.aspx?Id=" + FileID);
                }
                else if (CommandName.Equals("MatchTable"))
                {
                    Response.Redirect("MatchHash.aspx?Id=" + FileID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}