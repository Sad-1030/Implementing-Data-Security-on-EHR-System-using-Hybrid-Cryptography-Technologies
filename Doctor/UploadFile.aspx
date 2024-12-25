<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="OccupancyDetectionWeb.Doctor.UploadFile" %>
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
                        <label>Patient</label>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddPatient" ErrorMessage="Required" ForeColor="Red" InitialValue="0" />
                        <asp:DropDownList ID="ddPatient" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-12">
                        <label>Remarks</label>&nbsp;
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtDesc" ErrorMessage="Required" ForeColor="Red" />--%>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="image/x-png,image/gif,image/jpeg"/>
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
