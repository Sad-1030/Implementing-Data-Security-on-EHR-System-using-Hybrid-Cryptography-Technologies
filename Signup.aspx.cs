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
    public partial class Signup : System.Web.UI.Page
    {

        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(constr);

                cmd = new SqlCommand("select * from doctor_master where email=@email", con);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Email ID already exists')</script>");
                }
                else
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd = new SqlCommand("insert into doctor_master(full_name,degree,Specialist,email,phone,password,address) values(@name,@deg,@sep,@email,@phone,@pass,@address)", con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@deg", txtDegree.Text);
                    cmd.Parameters.AddWithValue("@sep", txtSpec.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtContact.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                        Response.Write("<script>alert('Account created');" +
                             "window.location ='Login.aspx';</script>");
                    else
                        Response.Write("<script>alert('Something went wrong')</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}