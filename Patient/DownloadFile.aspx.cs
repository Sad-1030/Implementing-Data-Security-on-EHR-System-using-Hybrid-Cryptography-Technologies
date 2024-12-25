using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Patient
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string FileID, PatientID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientID"] == null)
                Response.Redirect("../Login.aspx");

            FileID = Request.QueryString["Id"].ToString();
            PatientID = Session["PatientID"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select * from file_master where id=@id and key_value=@key", con);
                cmd.Parameters.AddWithValue("@id", FileID);
                cmd.Parameters.AddWithValue("@key", txtKey.Text);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string path = "~/Doctor/Files/" + dt.Rows[0]["file_name"].ToString() + "";
                    string fname = dt.Rows[0]["file_name"].ToString();

                    String[] spearator = { "." };
                    String[] strlist = fname.Split(spearator,
                       StringSplitOptions.RemoveEmptyEntries);
                    string fname1 = strlist[0];
                    string fileExtension = "." + strlist[1];
                    string path1 = "~/Doctor/Files/" + fname1 + "_enc" + fileExtension;
                    string input1 = Server.MapPath(path1);

                    string output3 = Server.MapPath("../Doctor/Files/") + fname1 + "_dec" + fileExtension;
                    Decrypt_AES(input1, output3, txtKey.Text);
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    string filePath = path;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=" + fname1 + "_dec" + fileExtension);
                    byte[] data = req.DownloadData(Server.MapPath(filePath));
                    response.BinaryWrite(data);
                    response.End();
                    response.Redirect("ViewFile.aspx");
                }
                else
                    Response.Write("<script>alert('Invalid Key')</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decrypt_AES(string inputFilePath, string outputfilePath, string key)
        {
            string EncryptionKey = key;
            //byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
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
                    // return data;
                }
            }

        }
    }


}