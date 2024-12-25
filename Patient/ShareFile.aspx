<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/PatientMaster.Master" AutoEventWireup="true" CodeBehind="ShareFile.aspx.cs" Inherits="OccupancyDetectionWeb.Patient.ShareFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="container-fluid">
        <br />
        <br />
        <br />
        <div class="card">
            <div class="card-header">
                <asp:Label ID="lblTitle" runat="server" Font-Size="Large">Share File</asp:Label><br />
                <label>Select doctor to share file with</label>&nbsp;
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-md-4">
                       <label runat="server">File</label>&nbsp;
                       <asp:RequiredFieldValidator runat="server" ControlToValidate="ddFile" ErrorMessage="Required" 
                           ForeColor="Red" InitialValue="0" />
                       <asp:DropDownList ID="ddFile" runat="server" CssClass="form-control" 
                           OnSelectedIndexChanged="ddFile_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                       <label runat="server">Doctor</label>&nbsp;
                       <asp:RequiredFieldValidator runat="server" ControlToValidate="ddDoctor" ErrorMessage="Required" 
                           ForeColor="Red" InitialValue="0" />
                       <asp:DropDownList ID="ddDoctor" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-12">
                        <label>Description</label>&nbsp;
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtDesc" ErrorMessage="Required" ForeColor="Red" />--%>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
