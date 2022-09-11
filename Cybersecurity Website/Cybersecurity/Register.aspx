
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Cybersecurity.Register" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="style.css"/>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/zxcvbn/4.2.0/zxcvbn.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet"/>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
<form id="form1" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-sm-4">
</div>
<div class="col-sm-4">

<div class = "col-sm-4">
        </div>
</div>
     <div class ="row">
        
           <div class="col-sm-4">
            
</div>
    
<div class="col-sm-4">
    <div class ="signup_form">
        <img src ="images/logo.png"
            alt="Secure System" class="logo" "img-fluid"/>


 
  <div class="form-group">
  	
        <label for="txtFullName" class="label_txt">Full name</label>
        <asp:TextBox ID="txtFullName" class="form-control" runat="server" required=""></asp:TextBox>
  
  <div class="col-sm-4">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                        ControlToValidate="txtFullName"  ErrorMessage="Please Enter Your Fullname" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexNameValid" runat="server" ValidationExpression="^([A-Z][a-z]*?)\s+([A-Z][a-z]*)$"
                        ClientValidationFunction="changeColor" ForeColor="Red" Display="Dynamic"
                        ControlToValidate="txtFullName" ErrorMessage="Invalid Full Name Format" style="background-color: #00FF99"></asp:RegularExpressionValidator>
                </div>
                </div>
  <div class="form-group">
    <label for="txtEmail" class="label_txt">Email </label>
    <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
    <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  Display="Dynamic"
                        ControlToValidate="txtEmail"  ErrorMessage="Please Enter Your Email" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ClientValidationFunction="changeColor" ForeColor="Red" Display="Dynamic"
                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                 </div>
  </div>

 
<div class="form-group">
    <label for="txtPassword" class="label_txt">Password </label>
    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
    <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  Display="Dynamic"
                        ControlToValidate="txtPassword"  ErrorMessage="Please Enter Your Password" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexPasswordValid" runat="server" ValidationExpression="^.*(?=.{6,})(?=.*[\d])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$"
                        ClientValidationFunction="changeColor" ForeColor="Red" ControlToValidate="txtPassword" Display="Dynamic"
                        ErrorMessage="Include uppercase and lowercase letters, numeric and symbol characters, and be at least 6 in length"></asp:RegularExpressionValidator>
                    <ajaxToolkit:PasswordStrength ID="txtPassword_PasswordStrength" runat="server"   
                        BehaviorID="txtPassword_PasswordStrength" TargetControlID="txtPassword"  
                        MinimumLowerCaseCharacters="1" MinimumNumericCharacters="1"   
                        MinimumUpperCaseCharacters="1" MinimumSymbolCharacters ="1"
                        PreferredPasswordLength="10" 
                        TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent" 
                        RequiresUpperAndLowerCaseCharacters="true" 
                        CalculationWeightings="50;15;15;20"/> 
                 </div>
  </div>



<div class="form-group">
    <label for="txtConfirm" class="label_txt">Confirm Password </label>
    <asp:TextBox ID="txtConfirm" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
    <div class="col-sm-4">
                    <asp:CompareValidator id="Compare1" 
                        ControlToValidate="txtPassword" 
                        ControlToCompare="txtConfirm"
                        ErrorMessage="Password and Re-Password doesn't match"
                        Display="Dynamic"
                        ClientValidationFunction="changeColor" 
                        ForeColor="Red" 
                        runat="server"/>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4" 
                        runat="server" 
                        Display="Dynamic"
                        ControlToValidate="txtConfirm"  
                        ErrorMessage="Please Confirm Your Password" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    
                 </div>

</div>




  <br>
                  
<div class="g-recaptcha" data-sitekey="6LfYYo0gAAAAAJLyeyOgqd9Tx6qGUNjL79_zouj7"></div>
        <br> 

<asp:Button ID="Button1" class="btn btn-success form_btn"  runat="server" Text="Register" onclick="Button1_Click" />
        

<p>Have an account? <a href="login.aspx">Log in</a></p>
<div class="col-sm-4">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
  </div>
    </div>
    </div>
         </div>
    </div>
    </div>

</form>
    
    

<div class="col-sm-4">
</div>
    


</body>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js">
</script>
</html>