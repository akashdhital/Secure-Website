using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cybersecurity
{
    public partial class Reset : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // Check email is valid and get user ID
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE email_address=@gmail", con))
                {
                    cmd.Parameters.AddWithValue("@gmail", txtEmail.Text);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            sda.Fill(dt);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message);
                        } 
                    }
                }
            }
            

            if (dt.Rows.Count > 0) // Email is valid
            {
                lblErrorMessage.Text = "";

                // Generate a new password end encrypt
                int length = 10;
                const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!£$%^&*()_-=+{}:@<>?";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < length--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                string strNewPassword = res.ToString();
                string EncPassword = "";

                byte[] data = UTF8Encoding.UTF8.GetBytes(strNewPassword);
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
                
                
                int iUserID = 0;
                if (dt.Rows.Count > 0) // Email is valid get Id
                {
                    iUserID = Convert.ToInt32(dt.Rows[0]["Id"]);
                }

                // Save new password to database 
                // Delete user's previous password and insert new one to temp password table
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
                {
                    using (SqlCommand cmd3 = new SqlCommand("DELETE FROM TempPassword WHERE user_id=@user_id; INSERT INTO  TempPassword(user_id, password, password_date) VALUES(@user_id,@password,@password_date)", con))
                    {
                        cmd3.Parameters.AddWithValue("@user_id", iUserID);
                        cmd3.Parameters.AddWithValue("@password", EncPassword);
                        cmd3.Parameters.AddWithValue("@password_date", DateTime.Now);
                        try
                        {
                            con.Open();
                            cmd3.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message);
                        } 
                        
                    }
                }

                // Email new password to user
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(txtEmail.Text);
                mail.From = new MailAddress("mjdai4831@gmail.com", "Password Reset", System.Text.Encoding.UTF8);
                mail.Subject = "This mail is send from Secure System application";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Your password reset on Secure System Site. <br />This password will only be valid for 24 hours! Please don't share these password with others";
                mail.Body += "<br /> The new password is: " + strNewPassword;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("mjdai4831@gmail.com", "ycbtugstyrkzqyxy");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                try
                {
                    client.Send(mail);
                    // Display sent dialog
                    Page.RegisterStartupScript("UserMsg", "<script>alert('New password successfully sent by email...');if(alert){ window.location='Login.aspx';}</script>");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);

                    // Display error dialog
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='Reset.aspx';}</script>");
                }

            }
            else
            {
                lblErrorMessage.Text = "Email address isn't registered in our system!";
            }            
        }
    }
}