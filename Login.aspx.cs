using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb
{
    public partial class Login : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                string email = txtEmail.Text.ToString().Trim();
                string password = txtPass.Text.ToString().Trim();
                string type = ddType.SelectedValue.ToString();

                con = new SqlConnection(constr);

                if(type.Equals("1"))
                    cmd = new SqlCommand("select * from Doctor_master where Email=@email and Password=@pass", con);
                else
                    cmd = new SqlCommand("select * from Patient_Master where Email_id=@email and Password=@pass", con);

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", password);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (type.Equals("1"))
                    {
                        Session["Name"] = dt.Rows[0]["Email"].ToString();
                        Session["DoctorID"] = dt.Rows[0]["Id"].ToString();
                        Response.Redirect("Doctor/Home.aspx");
                    }
                    else
                    {
                        Session["Name"] = dt.Rows[0]["Email_id"].ToString();
                        Session["PatientID"] = dt.Rows[0]["Patient_Id"].ToString();
                        Response.Redirect("Patient/Home.aspx");
                    }
                    lblError.Visible = false;
                }
                else
                    lblError.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}