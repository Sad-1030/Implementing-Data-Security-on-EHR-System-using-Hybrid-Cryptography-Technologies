<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OccupancyDetectionWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
     <style>
            .card{
            position:fixed;
            top: 45%;
            left: 50%;
            width:31em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
            box-shadow: 0 19px 38px rgba(0,0,0,0.30), 0 15px 12px rgba(0,0,0,0.22);
        }
     </style>
</head>
<body>
<form id="form1" runat="server">
        <div>
            <div style="background-color:navy;height:50px">
                   <p style="line-height:40px;font-family:'Arial Rounded MT';font-size:x-large;color:white">
                        &nbsp;&nbsp;Medical
                    </p>
               </div>
            <div class="card">
                 <div class="card-header">
                             <h3 class="card-title" style="font-family:'Arial Rounded MT';line-height:10px">
                     <svg width="1.5em" height="1.5em" viewBox="0 0 16 16" class="bi bi-person" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                      <path fill-rule="evenodd" d="M10 5a2 2 0 1 1-4 0 2 2 0 0 1 4 0zM8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm6 5c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z"/>
                     </svg>&nbsp;&nbsp;Sign in
                                 </h3>
                 </div>
                 <div class="card-body">
                     <div class="form-group">
                         <label class="control-label"><b>Login as</b></label>
                         <asp:DropDownList ID="ddType" runat="server" CssClass="form-control">
                             <asp:ListItem Value="1">Doctor</asp:ListItem>
                             <asp:ListItem Value="2">Patient</asp:ListItem>
                         </asp:DropDownList>
                     </div>
                     <div class="form-group">
                         <label class="control-label"><b>Email ID</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator> 
                         <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail"></asp:TextBox>
                     </div>
                     <div class="form-group">
                         <label class="control-label"><b>Password</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtPass" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator> 
                         <asp:TextBox CssClass="form-control" runat="server" ID="txtPass" TextMode="Password"></asp:TextBox>
                     </div>
                 </div>
                 <div class="card-footer">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" BackColor="navy" ForeColor="White" OnClick="btnLogin_Click"/>
                    <asp:Label runat="server" ForeColor="Red" Visible="false" ID="lblError"> &nbsp;Invalid username or password</asp:Label>
                    <asp:LinkButton ID="lnkSignup" runat="server" Text="New User ? Register Here" href="Signup.aspx"></asp:LinkButton>
                 </div>   
            </div>
        </div>
    </form>
</body>
</html>
