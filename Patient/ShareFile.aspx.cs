using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Patient
{
    public partial class ShareFile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        string PatientID;
        DataTable dt;
        public static string DoctorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null || Session["PatientID"] == null)
                Response.Redirect("../Login.aspx");

            PatientID = Session["PatientID"].ToString();

            if (!IsPostBack)
                fillFileDropdownList();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string file_path, file_name;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select file_name, file_path from file_master where file_name=@name", con);
                cmd.Parameters.AddWithValue("@name", ddFile.SelectedItem.Text);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    file_path = dt.Rows[0]["file_path"].ToString();
                    file_name = dt.Rows[0]["file_name"].ToString();

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd = new SqlCommand("insert into user_file_master (file_name,file_path,patient_id,Descripion,date,doc_id) values(@fname,@fpath,@pid,@desc,getdate(),@doc_id)", con);
                    cmd.Parameters.AddWithValue("@fname", ddFile.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@fpath", file_path);
                    cmd.Parameters.AddWithValue("@pid", Session["PatientID"].ToString());
                    cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@doc_id", ddDoctor.SelectedValue);
                    int result = cmd.ExecuteNonQuery();
                    if(result == 1)
                    {
                        var chars = "QWERTYUIOPLKJHGFDSAZXCVBNMqwertyuioplkjhgfdsazxcvbnm0987654321";
                        var stringargs = new char[6];
                        var random = new Random();
                        for (int i = 0; i < stringargs.Length; i++)
                        {
                            stringargs[i] = chars[random.Next(chars.Length)];
                        }

                        string otp = new String(stringargs);

                        cmd = new SqlCommand("select * from otp_master where file_id=@fid and patient_id=@pid and doc_id=@did", con);
                        cmd.Parameters.AddWithValue("@fid", ddFile.SelectedValue);
                        cmd.Parameters.AddWithValue("@pid", Session["PatientID"].ToString());
                        cmd.Parameters.AddWithValue("@did", ddDoctor.SelectedValue);
                        da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            string existingOtp = dt.Rows[0]["otp"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showAlert(File Already shared and Otp is" + existingOtp + ");", true);
                        }
                        else
                        {
                            if (con.State != ConnectionState.Open)
                                con.Open();
                            cmd = new SqlCommand("insert into otp_master(file_id,patient_id,doc_id,otp) values(@fid,@pid,@did,@otp)", con);
                            cmd.Parameters.AddWithValue("@fid", ddFile.SelectedValue);
                            cmd.Parameters.AddWithValue("@pid", Session["PatientID"].ToString());
                            cmd.Parameters.AddWithValue("@did", ddDoctor.SelectedValue);
                            cmd.Parameters.AddWithValue("@otp", otp);
                            int insResult = cmd.ExecuteNonQuery();
                            if(insResult == 1)
                            {
                                cmd = new SqlCommand("select email from doctor_master where id=@id", con);
                                cmd.Parameters.AddWithValue("@id", ddDoctor.SelectedValue);
                                da = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    string doc_mail = dt.Rows[0]["email"].ToString();
                                    string body = "File name: "+ file_name +"\n"+
                                        "Otp:" + otp;

                                    SendMail(doc_mail, body);
                                    Response.Redirect("ManageFiles.aspx");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Something went wrong')</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Something went wrong')</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        void fillFileDropdownList()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select 0 as [Id], '---Select---' as [File_Name] union select [Id], [File_Name] from File_Master where [Patient_id] = @Patient_Id", con);
                cmd.Parameters.AddWithValue("@Patient_Id", PatientID);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    ddFile.DataValueField = "Id";
                    ddFile.DataTextField = "File_Name";
                    ddFile.DataSource = ds.Tables[0];
                    ddFile.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        void fillDoctorDropdownList()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select 0 as [Id], '---Select---' as [Full_Name] union select[Id], [Full_Name] from Doctor_Master where Id<>(select [doc_id] from File_Master where Id = @file_id)", con);
                cmd.Parameters.AddWithValue("@file_id", ddFile.SelectedValue);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    ddDoctor.DataValueField = "Id";
                    ddDoctor.DataTextField = "Full_Name";
                    ddDoctor.DataSource = ds.Tables[0];
                    ddDoctor.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SendMail(string eemail, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Credentials = new System.Net.NetworkCredential("grpno.32021@gmail.com", "waswmphtxtjzklya");
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                mail = new MailMessage();
                mail.From = new MailAddress("grpno.32021@gmail.com");
                string eid = eemail;
                mail.To.Add(eid);

                mail.Subject = "File Access details";
                mail.Body = body;
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void ddFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDoctorDropdownList();
        }
    }
}