

#  Secure login and register system 
This project is designed to impement cyber security features in the system and it is for educational purpose. 
The project is built with ASP.NET Core MVC. The user register and login on the Application can be assessed directly on the web browser.

The Database is created using SQL Server through Microsoft SQL Management Studio. In the SQL Server, a Database is created for the Application for the storage of users data.

To get users data as the register, Sql Server engine is implemented and the connection string for the data source is added to the web.config in the application.
Password are encrypted when user enter on the form and google recaptcha is use for bots scanning so that the system will be secure.


##Microsoft Visual Studio Community 2019
Link to download Microsoft Visual Studio Community 2019: -
https://visualstudio.microsoft.com/vs/

##External packages which are used in .Net Core Project
=>Dapper ORM
=>Microsoft.EntityFrameworkCore
=>System.Data.SqlClient
=>Microsoft.EntityFrameworkCore.SqlServer

##Microsoft SQL Server 2019
Link to download SQL Server Express: -
https://www.microsoft.com/en-us/sql-server/sql-server-downloads

## How to run the project
=> Checkout this project to a location in your disk.  
=> Open the solution file using the Visual Studio.  
=> Restore the NuGet packages by rebuilding the solution.  
=> Run the project.

##Features
=> Registration
=> Login
=> Change Password (Password expire in 30 days, Cannot use old password upto 1 year)
=> Reset Password (One time password will be send to email which can be used upto 24 hours)
=> Sending Email for Reset Password.

For Demo I have used gmail. (Application is Configured with Gmail For Sending Email enter details in Reset.aspx.cs)

##Configure Gmail to send email as smtp server
=> Open your Google Admin console (admin.google.com).
=> Click Security > Basic settings .
=> Under Less secure apps, select Go to settings for less secure apps .
=> In the subwindow, select the Enforce access to less secure apps for all users radio button. ...
+> Click the Save button.

Enable less secure app on gmail for sending email from localhost :- https://hotter.io/docs/email-accounts/secure-app-gmail/


##🔨Getting Started:
##****Registation Process****##
When User register clicks on submit button on 
Client-Side SHA-256 Hash of the password is created 
and sent to the server on, on serverside salt is 
generated and combination [ SHA-256 Hash + Salt ] is 
stored in User Table and salt is stored in UserTokens Table.

##****Login Process****##
When User Log into Application using Username 
and password, according to Username we get UserDetails 
of the User from User Table and on based of UserId we 
get User Salt which is stored in UserTokens Table. 
Next, we are going to combine Posted User Password 
SHA-256 Hash with Stored User Salt and compare with Stored Hash in User Table.

##****Change Password Process****##
Next in Change Password Process, this process is done
after login into the application there we are going to
ask the user to enter Current password and New Password.
All Password History is Maintained in PasswordHistory Table.

##Screenshot 
**Register Page** : https://drive.google.com/file/d/1G3_TAxrVyfVBipGOC9M-O5FvROwwy1kq/view?usp=sharing

**Login Page** : https://drive.google.com/file/d/1hCicFMwpplCuAEhqFpMm5I5_zDgtO66K/view?usp=sharing

**Reset Password** : https://drive.google.com/file/d/1d5V8xWs0UiJ5NvV6wGAk_7ddjgK1Nl4o/view?usp=sharing

**Password Update** : https://drive.google.com/file/d/1QCu9c0Krgc5Djqg5CJYXKVv5tBo9EqSn/view?usp=sharing


## 🔨 Used technologies
ASP.NET CORE 6.0 MVC  
ASP.NET Core areas  
Entity Framework CORE 6.4.4  
HtmlSanitizer  
Bootstrap 5.1.3  
Popper.js  
AJAX Control Toolkit 20.1.0  
jQuery and any kind of jQuery plugins (bootstrap-select)  
JavaScript and JS animations  
Microsoft Configuration  


### Copyright and license
Code and documentation copyright 2022 **Akash Dhital(Student of Sunderland University)**.