using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OccupancyDetectionWeb.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
                Response.Redirect("../Login.aspx");

            lblName.Text = "Welcome " + Session["Name"].ToString();
        }
    }
}