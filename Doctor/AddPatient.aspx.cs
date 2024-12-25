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
    public partial class AddPatient : System.Web.UI.Page
    {

        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string operation, PatientID, DoctorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DoctorID"] == null)
                Response.Redirect("../Login.aspx");

            DoctorID = Session["DoctorID"].ToString();
            operation = Request.QueryString["action"].ToString();

            if (operation.Trim() == "edit")
                PatientID = Request.QueryString["id"].ToString();

            if (!IsPostBack)
            {
                if (operation.Trim() == "edit")
                    getDetails(PatientID);
            }
        }

        private void getDetails(string Patient_id)
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select * from patient_master where Patient_id = @Patient_id", con);
                cmd.Parameters.AddWithValue("@Patient_id", Convert.ToInt32(Patient_id));
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Full_Name"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email_Id"].ToString();
                    txtMobileNo.Text = dt.Rows[0]["Contact_No"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtDesc.Text = dt.Rows[0]["Description"].ToString();
                    txtRemarks.Text = dt.Rows[0]["Remark"].ToString();
                    txtEmail.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(constr);
                if (con.State != ConnectionState.Open)
                    con.Open();


                if (operation == "edit")
                {
                    cmd = new SqlCommand("update patient_master set Full_Name=@Full_Name, Email_Id=@Email_Id, Contact_No=@Contact_No, Address=@Address, " +
                        "Description = @Description, Remark = @Remark where Patient_Id = @Patient_Id", con);

                    cmd.Parameters.AddWithValue("@Patient_Id", PatientID);
                    cmd.Parameters.AddWithValue("@Full_Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Contact_No", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        Response.Redirect("ManagePatient.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Something went wrong')</script>");
                    }
                }
                else if (operation == "add")
                {
                    string ranpass = RandomString(8);

                    cmd = new SqlCommand("insert into patient_master (Doc_Id,Full_Name,Password,Contact_No,Email_Id,Address,Description,Remark,Date) values " +
                        "(@Doc_Id,@Full_Name,@Password,@Contact_No,@Email_Id,@Address,@Description,@Remark,getdate())", con);

                    cmd.Parameters.AddWithValue("@Doc_Id", DoctorID);
                    cmd.Parameters.AddWithValue("@Full_Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Password", ranpass);
                    cmd.Parameters.AddWithValue("@Contact_No", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);

                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        string eemail = txtEmail.Text;

                        SendMail(eemail, ranpass);


                        Response.Redirect("ManagePatient.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Something went wrong')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendMail(string eemail, string ranpass)
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

                mail.Subject = "OTP";
                mail.Body = "OTP : " + ranpass;
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }
    }
}