using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Admin
{
    public partial class Request : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string luminosity, temp, humidity, co2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    luminosity = Request.QueryString["luminosity"].ToString();
                    temp = Request.QueryString["temp"].ToString();
                    humidity = Request.QueryString["humidity"].ToString();
                    co2 = Request.QueryString["co2"].ToString();

                    con = new SqlConnection(constr);
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    cmd = new SqlCommand("insert into trnLog(Luminosity,Temp,Humidity,Co2,LogDate) " +
                        "values (@Luminosity,@Temp,@Humidity,@Co2,cast(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') as datetime))", con);
                    cmd.Parameters.AddWithValue("@Luminosity", luminosity);
                    cmd.Parameters.AddWithValue("@Temp", temp);
                    cmd.Parameters.AddWithValue("@Humidity", humidity);
                    cmd.Parameters.AddWithValue("@Co2", co2);
                    int insResult = cmd.ExecuteNonQuery();
                    if (insResult == 1)
                        Response.Write("success=0");
                    else
                        Response.Write("success=1");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}