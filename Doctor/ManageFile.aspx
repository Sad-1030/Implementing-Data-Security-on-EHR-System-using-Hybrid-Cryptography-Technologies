<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="ManageFile.aspx.cs" Inherits="OccupancyDetectionWeb.Doctor.ManageFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .gv {
            margin-left: 7%;
            width: 85%;
            margin-top: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 style="text-align: center">Manage Files</h4>
    <h6 style="margin-top:7%;margin-left:2%"><a href="UploadFile.aspx?action=add" style="color:black"><u>Upload File</u></a></h6>
    <asp:GridView ID="gvFile" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="ID"
        HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f96432" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="patient_name" HeaderText="Patient" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="file_name" HeaderText="File Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Date" HeaderText="Creation Date" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>
