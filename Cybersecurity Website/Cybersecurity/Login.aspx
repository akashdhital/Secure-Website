<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Cybersecurity.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Secure System</title>
   
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"/> 
<link rel="stylesheet" href="style.css"/>
</head>

<body>
<form id="form1" runat="server">

<div class="container">
	<div class="row">
		<div class="col-sm-3">
		</div>


		<div class="col-sm-6">
			<div class="login_form">


 	
  <div class="form-group">
      <center>
  <img src ="images/logo.png"
            alt="Secure System" class="logo" col-sm-12"/> <br>

            </center>


            
            <div class="form-group">
            <label for="txtEmail" class="label_txt">Email address:</label>        
            <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
           
            <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"  ErrorMessage="Please Enter Your Email" 
                        Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ClientValidationFunction="changeColor" ForeColor="Red" Display="Dynamic"
                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                </div>
  </div>









  <div class="form-group">
  <label for="txtPassword" class="label_txt">Password:</label>
  <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
  
  <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                        ControlToValidate="txtPassword"  ErrorMessage="Please Enter Your Password" ForeColor="Red"></asp:RequiredFieldValidator>
                 </div>


</div>





<div class="row">
    <div class="col-sm-2">
        </div>
                
                    <asp:Button ID="Button1" class="btn btn-primary " runat="server" Text="Log In" onclick="Button1_Click" />

                
                
                <div class="col-sm-4">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>



            <br />


   
       <div class="row"> 
    <div class="col-sm-8">
          <label for="btnForgottenPassword" class="col-sm col-form-label"><class ="label_txt"></> also can be used as one time password--></label>
    </div>
    
        <div class="col-sm-3">    <br>
            <asp:Button ID="Button2"  class="btn btn-warning" runat="server" Text="Password reset" onclick="btnForgottenPassword_Click" CausesValidation="False" />

        </div>
           
    
    
        
    <div class="col-sm-4"></div>
</div>
</div>
<br> 
    <p>Don't have an account? <a href="Register.aspx">Sign up</a> </p>
</div>
 </div>
</div>
 </div>
	
</form>

   
		
		<div class="col-sm-3"></div>
		
</body>
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js"></script>
</html>