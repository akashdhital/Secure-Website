using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
namespace Cybersecurity
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            lblErrorMessage.Text = "";


            if (Page.IsValid)
            {
                try
                {
                    int intUserID = 0;
                    int intPasswordID = 0;

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

                    // Insert User first and get back ID
                    using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Users(full_name, email_address) OUTPUT INSERTED.ID VALUES(@full_name,@email_address)", con))
                        {
                            cmd.Parameters.AddWithValue("@full_name", txtFullName.Text);
                            cmd.Parameters.AddWithValue("@email_address", txtEmail.Text);
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                try
                                {
                                    con.Open();
                                    intUserID = Convert.ToInt32(cmd.ExecuteScalar());
                                }
                                catch (Exception ex)
                                {
                                    //Debug.Print(ex.Message);
                                    lblErrorMessage.Text = "There is an error in form or email address already used! Please try again?";
                                }                                
                            }
                        }                        
                    }

                    // Insert password if user successfully inserted to Users table
                    if (intUserID > 0)
                    {
                        using(SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO  Password(user_id, password, password_date) OUTPUT INSERTED.ID VALUES(@userID,@password,@date)", con2))
                            {
                                cmd.Parameters.AddWithValue("@userID", intUserID);
                                cmd.Parameters.AddWithValue("@password", EncPassword);
                                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                                using(SqlDataAdapter sda = new SqlDataAdapter(cmd))
                                {
                                    con2.Open();
                                    intPasswordID = Convert.ToInt32(cmd.ExecuteScalar());
                                }                                
                            }                            
                        }
                    }

                    if (intPasswordID > 0)
                    {
                        // Display dialog and redirect to login page
                        Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Registered...');if(alert){ window.location='Login.aspx';}</script>");
                        
                        //lblErrorMessage.Text = "Successfully registered";
                        //Response.Redirect("Login.aspx");
                    }
                }
                catch // Email address field is unique in database, so if try to use an exists one give error message
                {
                    lblErrorMessage.Text = "There is an error in form or email address already used! Please try again?";
                }
            }
            else
            {
                lblErrorMessage.Text = "Invalid input in form!";
            }

        }

    }
}