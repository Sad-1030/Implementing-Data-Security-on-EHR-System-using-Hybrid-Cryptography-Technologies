<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="DownloadFile.aspx.cs" Inherits="OccupancyDetectionWeb.Doctor.Otp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <br />
        <br />
        <br />
        <div class="card">
            <div class="card-header">
                <asp:Label ID="lblTitle" runat="server" Font-Size="Large">Enter OTP to download file</asp:Label><br />
                <label>A One-Time Password has been sent to your registered Email ID</label>&nbsp;
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-md-6">

                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-6">
                        <label>Enter OTP</label>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOtp" ErrorMessage="Required" ForeColor="Red" />
                        <asp:TextBox ID="txtOtp" runat="server" CssClass="form-control"></asp:TextBox>
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
