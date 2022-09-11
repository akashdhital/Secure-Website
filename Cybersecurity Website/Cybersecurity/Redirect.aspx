<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Redirect.aspx.cs" Inherits="Cybersecurity.Redirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title> My Account - Secure System</title>
        <meta name="viewport" content= "width=device-width, initial-scale=1.0"/>
        <link rel = "stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css">
        <link rel="stylesheet" href="style.css"/>
        <link href="Content/bootstrap.min.css" rel="stylesheet"/>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-3">
</div>
<div class= "col-sm-6">
    
        <div class= "login_form">
            <img src ="images/logo.png"
            alt="Secure System" class="logo img-fluid"> <br>
            
          <br>
           
            <center>
           
           
               
            
            
            
            <p> CONGRATULATION YOU ARE LOGIN! <span style="color:#33CC00"></span></p>
            </center>
            
            
            <div>

                
       
            <div style="float:left;">
        <asp:Button ID="btnUpdatePassword" class="btn btn-primary" runat="server" Text="Password change"  CausesValidation="False" OnClick="btnUpdatePassword_Click" />
            </div>
        
        
            <div style="float:right">
        <asp:Button ID="btnLogout" class="btn btn-warning" runat="server" Text="Logout" OnClick="btnLogout_Click"  />
           
        </div> <div style="clear:both"></div>
        </div>
        </div>
        <div class="col-sm-3"></div>
    </div>
</div></form>
</body>
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src ="https://cdn.jsdelivr.net/npm/bootstrap24.5.3/dist/js/bootstrap.bundle.min.js"></script>
</html>

                
    