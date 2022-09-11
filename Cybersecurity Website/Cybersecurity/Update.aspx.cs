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
    public partial class Update : System.Web.UI.Page
    {
        private int iUserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            // Check email and password matching           
                       
            // Encrypt old password
            string EncPassword = "";

            byte[] data = UTF8Encoding.UTF8.GetBytes(txtCurrentPassword.Text);
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

            // Check main password table first
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT  Users.Id, Users.email_address, Password.password, Password.password_date FROM Users INNER JOIN Password ON Users.ID=Password.user_id where email_address=@email and password=@password", con))
                {
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", EncPassword);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
            {
                // Then check temp password table for reset
                using (SqlCommand cmd2 = new SqlCommand("SELECT Users.Id, Users.email_address, TempPassword.password, TempPassword.password_date FROM Users INNER JOIN TempPassword ON Users.ID=TempPassword.user_id WHERE Users.email_address=@email and TempPassword.password=@password", con))
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
                lblErrorMessage.Text = "";
                iUserID = Convert.ToInt32(dt.Rows[0]["Id"]);
                updatePassword();               

            }
            else if (dt2.Rows.Count > 0) // Temp password match
            {
                // Check password not older than 24h
                DateTime passwordDate = Convert.ToDateTime(dt2.Rows[0]["password_date"].ToString());

                if ((DateTime.Now - passwordDate).TotalHours < 24)
                {
                    lblErrorMessage.Text = "";
                    iUserID = Convert.ToInt32(dt2.Rows[0]["Id"]);
                    updatePassword(); 
                }
            }
            else
            {
                lblErrorMessage.Text = "You're username and password is incorrect";
            }
        }

        private void updatePassword()
        {
            try
            {
                // Encrypt passwords
                string OldPassword = "";

                byte[] data = UTF8Encoding.UTF8.GetBytes(txtCurrentPassword.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["MyHash"]));
                    using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripleDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        OldPassword = Convert.ToBase64String(results);
                    }
                }

                string NewPassword = "";

                byte[] data2 = UTF8Encoding.UTF8.GetBytes(txtNewPassword.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["MyHash"]));
                    using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripleDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data2, 0, data2.Length);
                        NewPassword = Convert.ToBase64String(results);
                    }
                }

                DataTable dt = new DataTable();

                // Check new password in old password table, it didn't use in last year
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT  OldPassword.password, OldPassword.password_date FROM Users INNER JOIN OldPassword ON Users.ID=OldPassword.user_id WHERE Users.email_address=@email and OldPassword.password=@password", con))
                    {
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@password", NewPassword);
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count > 0) // Password's been used
                {
                    lblErrorMessage.Text = "New password has been used in last 12 months. Please eneter an other one?";
                }
                else
                {
                    lblErrorMessage.Text = "";

                    // Delete current password from main and temp password table
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database.mdf"].ConnectionString))
                    {
                        using (SqlCommand cmd1 = new SqlCommand("DELETE FROM TempPassword WHERE user_id=@user_id; DELETE FROM Password WHERE user_id=@user_id;", con))
                        {
                            cmd1.Parameters.AddWithValue("@user_id", iUserID);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                        }

                        // Save old password to old password table but delete all older than 1 year old passwords
                        using (SqlCommand cmd2 = new SqlCommand("DELETE FROM OldPassword WHERE DATEADD(year, 1, password_date) < @password_date; INSERT INTO  OldPassword(user_id, password, password_date) VALUES(@user_id,@password,@password_date)", con))
                        {
                            cmd2.Parameters.AddWithValue("@password_date", DateTime.Now);
                            cmd2.Parameters.AddWithValue("@password", OldPassword);
                            cmd2.Parameters.AddWithValue("@user_id", iUserID);;
                            cmd2.ExecuteNonQuery();
                        }


                        // Save new password to database
                        using (SqlCommand cmd3 = new SqlCommand("INSERT INTO  Password(user_id, password, password_date) OUTPUT INSERTED.ID VALUES(@userID,@password,@password_date)", con))
                        {
                            cmd3.Parameters.AddWithValue("@userID", iUserID);
                            cmd3.Parameters.AddWithValue("@password", NewPassword);
                            cmd3.Parameters.AddWithValue("@password_date", DateTime.Now);
                            cmd3.ExecuteNonQuery();
                        }

                        con.Close();
                    }

                    // Display dialog and redirect to update page
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Password updated. Please login?');if(alert){ window.location='Login.aspx';}</script>");
                }
            }
            catch
            {
                lblErrorMessage.Text = "An error occured, please try again?";
            }           
            
        }
    }
}