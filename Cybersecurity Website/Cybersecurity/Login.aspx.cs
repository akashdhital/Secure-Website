using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace Cybersecurity
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string EncPassword = "";

            byte[] data = UTF8Encoding.UTF8.GetBytes(txtPassword.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["MyHash"]));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    EncPassword = Convert.ToBase64String(results);
                }
            }

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
            {
                // Check main password table first
                using (SqlCommand cmd = new SqlCommand("SELECT  Users.email_address, Password.password, Password.password_date FROM Users INNER JOIN Password ON Users.ID=Password.user_id where email_address=@email and password=@password", con))
                {
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", EncPassword);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }


                // Then check temp password table for reset
                using (SqlCommand cmd2 = new SqlCommand("SELECT  Users.email_address, TempPassword.password, TempPassword.password_date FROM Users INNER JOIN TempPassword ON Users.ID=TempPassword.user_id WHERE Users.email_address=@email and TempPassword.password=@password", con))
                {
                    cmd2.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd2.Parameters.AddWithValue("@password", EncPassword);
                    using (SqlDataAdapter sda2 = new SqlDataAdapter(cmd2))
                    {
                        sda2.Fill(dt2);
                    }
                }
            }

            if (dt.Rows.Count > 0) // Main password macth
            {
                // Check password not older than a month
                DateTime passwordDate = Convert.ToDateTime(dt.Rows[0]["password_date"].ToString());

                if ((DateTime.Now - passwordDate).TotalDays > 30)
                {

                    Session["Login"] = "false";

                    // Display dialog and redirect to update page
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Your password older then 30 days. Please update ...');if(alert){ window.location='Update.aspx';}</script>");
                }
                else
                {
                    // Set user state to logged in in session
                    Session["Login"] = "true";

                    Response.Redirect("Redirect.aspx");
                }

            }
            else if (dt2.Rows.Count > 0) // Temp password match
            {

                // Check password not older than 24h
                DateTime passwordDate = Convert.ToDateTime(dt2.Rows[0]["password_date"].ToString());

                if ((DateTime.Now - passwordDate).TotalHours > 24)
                {
                    // Set user state to logged out in session
                    Session["Login"] = "false";

                    // Display dialog and redirect to login page
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Your temp password older than 24h !');if(alert){ window.location='Login.aspx';}</script>");
                }
                else
                {
                    // Set user state to logged out in session
                    Session["Login"] = "false";

                    // Display dialog and redirect to update page
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Please update your temporary password?');if(alert){ window.location='Update.aspx';}</script>");
                }
            }
            else
            {
                // Set user state to logged out in session
                Session["Login"] = "false";

                lblErrorMessage.Text = "Username or Password is Invalid";
            }

        }


        protected void btnForgottenPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reset.aspx");
        }


    }
}