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
    public partial class ManageFile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        string DoctorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null || Session["DoctorID"] == null)
                Response.Redirect("../Login.aspx");

            DoctorID = Session["DoctorID"].ToString();

            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SELECT ROW_NUMBER() over (order by f.[Id] desc) as SrNo, f.ID, f.[file_name],f.patient_id,p.full_name as patient_name,f.[description],format(f.[Date],'dd-MMM-yyyy') as [date] FROM File_Master f Inner Join Patient_Master p on f.[Patient_Id] = p.[Patient_Id] where f.doc_id=@id", con);
                cmd.Parameters.AddWithValue("@id", DoctorID);
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