<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="OccupancyDetectionWeb.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card {
            position: fixed;
            top: 35%;
            left: 40%;
            width: 45em;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
            box-shadow: 0 19px 38px rgba(0,0,0,0.30), 0 15px 12px rgba(0,0,0,0.22);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="background-color: navy; height: 50px">
                <p style="line-height: 40px; font-family: 'Arial Rounded MT'; font-size: x-large; color: white">
                    &nbsp;&nbsp;Medical
                </p>
            </div>
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title" style="font-family: 'Arial Rounded MT'; line-height: 10px">
                        <svg width="1.5em" height="1.5em" viewBox="0 0 16 16" class="bi bi-person" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" d="M10 5a2 2 0 1 1-4 0 2 2 0 0 1 4 0zM8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm6 5c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z" />
                        </svg>&nbsp;&nbsp;Sign up
                    </h3>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label class="control-label"><b>Name</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtName"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group ">
                            <label class="control-label"><b>Degree</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtDegree" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtDegree"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label class="control-label"><b>Email</b></label>&nbsp;&nbsp;
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ErrorMessage="Invalid email id " ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label class="control-label"><b>Mobile No</b></label>&nbsp;&nbsp;
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContact" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtContact" ValidationExpression="^[6-9]\d{9}$" ErrorMessage="Invalid mobile no " ForeColor="Red" ></asp:RegularExpressionValidator>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtContact"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label"><b>Password</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtPass" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPass" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label"><b>Confirm Password</b></label>&nbsp;&nbsp;<asp:CompareValidator runat="server" ControlToValidate="txtCPass" ControlToCompare="txtPass" ErrorMessage="Passwords do not match" ForeColor="Red"></asp:CompareValidator>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtCPass" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label"><b>Address</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtAddress"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label"><b>Specialist In</b></label>&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtSpec" ErrorMessage="Invalid" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtSpec"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="btnSignup" runat="server" Text="Signup" CssClass="btn" BackColor="navy" ForeColor="White" OnClick="btnSignup_Click" />
                    <asp:Label runat="server" ForeColor="Red" Visible="false" ID="lblError"> &nbsp;Invalid username or password</asp:Label>
                    <asp:LinkButton ID="lnkLogin" runat="server" Text="Already Registered ? Login Here" href="Login.aspx"></asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
