
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="Cybersecurity.Update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Password Update</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="style.css"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/zxcvbn/4.2.0/zxcvbn.js"></script>
    
</head>
<body>
<form id="form1" runat="server">
        
<asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
       
            
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

<br /><br />
 
  <div class="form-group">
        <label for="txtEmail" class="label_txt">Email</label>
        <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox> 
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Enter Your Email" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ClientValidationFunction="changeColor" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" 
                            Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                </div>
      <br /><br />          
  <div class="form-group">
    <label for="txtCurrentPassword" class="label_txt">Current Password </label>
    <asp:TextBox ID="txtCurrentPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>  
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCurrentPassword" ErrorMessage="Please Enter Your Password" ForeColor="Red"></asp:RequiredFieldValidator>                       
                    </div>
  </div>

 
<div class="form-group">
    <label for="txtNewPassword" class="label_txt">New Password </label>
    <asp:TextBox ID="txtNewPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    
    <div class="col-sm-4">
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="Please Enter Your Password" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regexPasswordValid" runat="server" ValidationExpression="^.*(?=.{6,})(?=.*[\d])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$"
    ClientValidationFunction="changeColor" ForeColor="Red" ControlToValidate="txtNewPassword" Display="Dynamic"
    ErrorMessage="Include uppercase and lowercase letters, numeric and symbol characters, and be at least 6 in length"></asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ClientValidationFunction="changeColor" ControlToCompare="txtCurrentPassword" ControlToValidate="txtNewPassword" 
                            Display="Dynamic" Operator="NotEqual" ErrorMessage="Current and New Password won't be same!" ForeColor="Red" />
                        <ajaxToolkit:PasswordStrength ID="txtPassword_PasswordStrength" runat="server" BehaviorID="txtPassword_PasswordStrength" 
                            MinimumLowerCaseCharacters="1" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="10" TargetControlID="txtNewPassword"
                            TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent" RequiresUpperAndLowerCaseCharacters="true" CalculationWeightings="50;15;15;20" />
                    </div>
  </div>

<div class="form-group">
    <label for="txtConfirm" class="label_txt">Confirm Password </label>
    <asp:TextBox ID="txtConfrim" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    
                    <div class="col-sm-4">
                        <asp:CompareValidator ID="Compare1" runat="server" ClientValidationFunction="changeColor" ControlToCompare="txtConfrim" ControlToValidate="txtNewPassword" 
                            Display="Dynamic" ErrorMessage="Password and Confirmation Password doesn't match" ForeColor="Red" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfrim" ErrorMessage="Please Confirm Your Password" 
                            Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

</div> <br />            
  <asp:Button ID="btnUpdate" class="btn btn-success form_btn" runat="server" Text="Update" OnClick="btnUpdate_Click" />

<br />
        


<div class="col-sm-4">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
  </div>
    </div>
    </div>
         </div>
    </div>
    </div>

</form>
    
    

<div class="col-sm-11">
</div>
    


</body>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js">
</script>
</html>