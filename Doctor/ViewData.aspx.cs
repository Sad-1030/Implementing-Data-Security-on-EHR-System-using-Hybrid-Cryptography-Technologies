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

namespace OccupancyDetectionWeb.Admin
{
    public partial class ViewData : System.Web.UI.Page
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
                cmd = new SqlCommand("select ROW_NUMBER() over (order by [LogID] desc) as [SrNo], LogID, Luminosity,Temp,Humidity,Co2, format(LogDate,'dd-MMM-yyyy hh:mm tt') as [LogDate] from trnLog", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gvData.DataSource = dt;
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExportGridToExcel()
        {
            
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string filename = "DataLog_" + indianTime.ToString("ddMMyyyyHHmmss")+".xls";

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            gvData.GridLines = GridLines.Both;
            gvData.HeaderStyle.Font.Bold = true;
            gvData.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}