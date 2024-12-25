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

namespace OccupancyDetectionWeb.Doctor
{
    public partial class UploadFile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        string DoctorID;
        public static string data;
        public static string data1;
        public static string data2;
        public static string data3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null || Session["DoctorID"] == null)
                Response.Redirect("../Login.aspx");

            DoctorID = Session["DoctorID"].ToString();

            if (!IsPostBack)
                fillDropdownList();
        }

        private void fillDropdownList()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select 0 as [Patient_Id], '---Select---' as [Full_Name] union select [Patient_Id], [Full_Name] from Patient_Master where Doc_Id = @doc_id", con);
                cmd.Parameters.AddWithValue("@doc_id", DoctorID);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    ddPatient.DataValueField = "Patient_id";
                    ddPatient.DataTextField = "Full_Name";
                    ddPatient.DataSource = dt;
                    ddPatient.DataBind();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string fileName, fileExtension, file_path, input1, file_name1, output, input = "";
            string EmailID;

            try
            {
                if(!FileUpload1.HasFile)
                    Response.Write("<script>alert('Please upload file')</script>");
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                    fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    file_path = Path.GetFileName(FileUpload1.PostedFile.FileName);

                    input = Server.MapPath("Files/") + file_path;

                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("select top 1 1 from File_Master where file_name = @file_name", con);
                    cmd.Parameters.AddWithValue("@file_name", file_path);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if(dt.Rows.Count > 0)
                        Response.Write("<script>alert('File Name already exists')</script>");
                    else
                    {
                        Stream inputStream = FileUpload1.PostedFile.InputStream;
                        Byte[] data3;

                        using (var streamReader = new MemoryStream())
                        {
                            inputStream.CopyTo(streamReader);
                            data3 = streamReader.ToArray();
                            data1 = Convert.ToBase64String(data3);
                            data2 = Encoding.UTF8.GetString(data3, 0, data3.Length);
                        }

                        string hashData = performHash(data2);

                        var chars = "QWERTYUIOPLKJHGFDSAZXCVBNMqwertyuioplkjhgfdsazxcvbnm0987654321";
                        var stringargs = new char[8];
                        var random = new Random();
                        for (int i = 0; i < stringargs.Length; i++)
                        {
                            stringargs[i] = chars[random.Next(chars.Length)];
                        }
                        string key = new String(stringargs);
                        //Build the File Path for the original (input) and the encrypted (output) file.
                        input1 = Server.MapPath("Files/") + fileName + fileExtension;
                        file_name1 = fileName + "_enc" + fileExtension;


                        output = Server.MapPath("Files/") + fileName + "_enc" + fileExtension;
                        //Save the Input File, Encrypt it and save the encrypted file in output path.

                        FileUpload1.SaveAs(input);

                        StreamReader reader = new StreamReader(FileUpload1.FileContent);
                        do
                        {
                            string textLine = reader.ReadLine();
                            //string.Concat(data,textLine);
                            if (data == string.Empty)
                                data = textLine;
                            else
                                data = data + "\n" + textLine;
                        } while (reader.Peek() != -1);
                        reader.Close();

                        //encrytp_aes
                        this.Encrypt_AES(input1, output, key);

                        if (con.State != ConnectionState.Open)
                            con.Open();

                        cmd = new SqlCommand("insert into file_master(file_name, file_path, patient_id, description, date, doc_id, file_enc, key_value,hash_match) values(@fname, @fpath, @pid, @desc, getdate(), @doc_id, @file_enc, @key,@hash_match)", con);
                        cmd.Parameters.AddWithValue("@fname", file_path);
                        cmd.Parameters.AddWithValue("@fpath", input);
                        cmd.Parameters.AddWithValue("@pid", ddPatient.SelectedValue);
                        cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
                        cmd.Parameters.AddWithValue("@doc_id", DoctorID);
                        cmd.Parameters.AddWithValue("@file_enc", output);
                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@hash_match", hashData);

                        int result =  cmd.ExecuteNonQuery();
                        if (result == 1)
                        {

                            cmd = new SqlCommand("select * from Patient_Master", con);
                            da = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            if(dt.Rows.Count > 0)
                            {
                                EmailID = dt.Rows[0]["Email_Id"].ToString();

                                string body = "File has been shared to your account\n" +
                                    "File Name: " + file_path + "\n" +
                                    "Key: " + key;

                                MailMessage mail1 = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                                mail1.From = new MailAddress("grpno.32021@gmail.com");
                                mail1.To.Add(EmailID);
                                mail1.Subject = "Key  Details";
                                mail1.Body = body;
                                SmtpServer.EnableSsl = true;
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential("grpno.32021@gmail.com", "waswmphtxtjzklya");
                                SmtpServer.Send(mail1);
                                Response.Redirect("ManageFile.aspx");
                            }
                            else
                                Response.Write("<script>alert('Something went wrong')</script>");
                        }
                        else
                            Response.Write("<script>alert('Something went wrong')</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Encrypt_AES(string inputFilePath, string outputfilePath, string key)
        {
            string EncryptionKey = key;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        private string performHash(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}