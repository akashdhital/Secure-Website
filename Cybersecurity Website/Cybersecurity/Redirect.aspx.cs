using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cybersecurity
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            // User's status saved to session, if someone try to open this site without login, it redirect to Login site
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else 
            {
                // Get user state from session
                string sLogin = Session["Login"].ToString().Trim();

                // Redirect user if he's/she's state logged out in session
                if (sLogin.Equals("false"))
                {
                    Response.Redirect("Login.aspx");
                }                
            }
        }
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("Update.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Set user state to logged out in session
            Session["Login"] = "false";

            Response.Redirect("Login.aspx");
        }
    }
}