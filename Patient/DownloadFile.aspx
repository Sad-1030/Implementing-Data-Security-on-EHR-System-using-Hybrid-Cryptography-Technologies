<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/PatientMaster.Master" AutoEventWireup="true" CodeBehind="DownloadFile.aspx.cs" Inherits="OccupancyDetectionWeb.Patient.DownloadFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <br />
        <br />
        <br />
        <div class="card">
            <div class="card-header">
                <asp:Label ID="lblTitle" runat="server" Font-Size="Large">Enter Key to download file</asp:Label><br />
                <label>A the key that has been sent to your registered Email ID</label>&nbsp;
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-md-6">

                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-6">
                        <label>Enter Key</label>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtKey" ErrorMessage="Required" ForeColor="Red" />
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
           <div class="card-footer">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-secondary" ForeColor="White"
                    OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
