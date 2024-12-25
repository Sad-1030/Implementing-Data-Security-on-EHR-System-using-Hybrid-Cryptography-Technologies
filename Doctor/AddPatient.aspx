<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="AddPatient.aspx.cs" Inherits="OccupancyDetectionWeb.Doctor.AddPatient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <br /><br /><br />
        <div class="card">
            <div class="card-header">
                <asp:Label ID="lblTitle" runat="server" Font-Size="Large">Patient Details</asp:Label>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-md-6">
                        <label>Name</label>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="Required" ForeColor="Red" />
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-6">
                        <label> Email ID </label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Required"/>
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ErrorMessage="Invalid email id " ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-6">
                        <label> Mobile No </label>
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobileNo" ForeColor="Red" ErrorMessage="Required"/>
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMobileNo" ValidationExpression="^[6-9]\d{9}$" ErrorMessage="Invalid mobile no " ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-6">
                        <label>Address</label>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" ErrorMessage="Required" ForeColor="Red" />
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label>Description</label>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDesc" ErrorMessage="Required" ForeColor="Red" />
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-12">
                        <label>Remarks</label>&nbsp;
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" ErrorMessage="Required" ForeColor="Red" />--%>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ForeColor="White"
                    OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
