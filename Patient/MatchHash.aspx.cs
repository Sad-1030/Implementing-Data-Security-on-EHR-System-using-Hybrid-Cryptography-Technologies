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
    public partial class MatchHash : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string PatientID, FileID;

        public string file_path, key, fileName, fileExtension, output, input_file;
        public static string input, input1, input2, input3;
        public static string output1, output2, output3;
        public static string data;
        public static string data1;
        public static string data2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null || Session["PatientID"] == null)
                Response.Redirect("../Login.aspx");

            FileID = Request.QueryString["Id"].ToString();

            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("select * from file_master where id=@id", con);
                cmd.Parameters.AddWithValue("@id", FileID);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    lblFileName.InnerText = dt.Rows[0]["File_Name"].ToString();
                    lblHash.InnerText = dt.Rows[0]["hash_match"].ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FileUpload1.HasFile)
                    Response.Write("<script>alert('Please select file')</script>");
                else
                {
                    Stream inputStream = FileUpload1.PostedFile.InputStream;
                    Byte[] data;

                    using (var streamReader = new MemoryStream())
                    {
                        inputStream.CopyTo(streamReader);
                        data = streamReader.ToArray();
                        data1 = Convert.ToBase64String(data);
                        data2 = Encoding.UTF8.GetString(data, 0, data.Length);
                    }


                    string hashData = performHash(data2);
                    lblGeneratedHash.InnerText = hashData;
                    if (lblGeneratedHash.InnerText == lblHash.InnerText)
                    {
                        lblMsg.Text = "Hash Value Matched";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMsg.Text = "Hash Value Not Matched";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
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