<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="Cybersecurity.Reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title> Forgot Password</title>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"/> 
<link rel="stylesheet" href="style.css"/>
<link href="Content/bootstrap.min.css" rel="stylesheet"/>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div class="container">
	<div class="row">
		<div class="col-sm-4">
		</div>
		<div class="col-sm-4">
 	
    <div class="login_form">
  <div class="form-group">
 <img src="images/logo.png" alt="Techno Smarter" class="img-fluid logo"> 
      <div class="form-group">
 <label for="txtEmail" class="label_txt">Email address</label>
 <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
 <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtEmail"  ErrorMessage="Please Enter Your Username" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ClientValidationFunction="changeColor" ForeColor="Red" 
                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                </div>
</div>
<asp:Button ID="btnReset" class="btn btn-primary form_btn" runat="server" Text="Send email" OnClick="btnReset_Click" />
<asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" ></asp:Label>
  
</div>
   <p>Have an account? <a href="login.aspx">Login</a> </p>
    <p>Don't have an account? <a href="register.aspx">Sign up</a> </p> 
		</div>
		<div class="col-sm-4">
		</div>
	</div>
</div> 
</form>
  
</body>
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js"></script>
</html>