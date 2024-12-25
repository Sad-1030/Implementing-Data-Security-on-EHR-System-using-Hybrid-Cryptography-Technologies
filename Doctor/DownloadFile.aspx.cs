using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Doctor
{
    public partial class Otp : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string FileID, DoctorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DoctorID"] == null)
                Response.Redirect("../Login.aspx");

            FileID = Request.QueryString["Id"].ToString();
            DoctorID = Session["DoctorID"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(constr);

                cmd = new SqlCommand("select * from user_file_master where id=@id", con);
                cmd.Parameters.AddWithValue("@id", FileID);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    string name = dt.Rows[0]["File_Name"].ToString();
                    cmd = new SqlCommand("select id from file_master where file_name=@name", con);
                    cmd.Parameters.AddWithValue("@name", name);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        string orgFileID = dt.Rows[0]["id"].ToString();

                        cmd = new SqlCommand("select otp from otp_master where file_id=@id and doc_id=@did and otp=@otp", con);
                        cmd.Parameters.AddWithValue("@id", orgFileID);
                        cmd.Parameters.AddWithValue("@otp", txtOtp.Text);
                        cmd.Parameters.AddWithValue("@did", DoctorID);
                        da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            cmd = new SqlCommand("select * from user_file_master where id=@id", con);
                            cmd.Parameters.AddWithValue("@id", FileID);
                            da = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                string path = "Files/" + dt.Rows[0]["File_Name"].ToString();
                                WebClient req = new WebClient();
                                HttpResponse response = HttpContext.Current.Response;
                                //string filePath = path;
                                response.Clear();
                                response.ClearContent();
                                response.ClearHeaders();
                                response.Buffer = true;
                                response.AddHeader("Content-Disposition", "attachment;filename=" + dt.Rows[0]["File_Name"].ToString());
                                byte[] data = req.DownloadData(Server.MapPath(path));
                                response.BinaryWrite(data);
                                response.End();
                                response.Redirect("ViewUserFile.aspx");
                            }
                        }
                        else
                            Response.Write("<script>alert('Invalid OTP')</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}