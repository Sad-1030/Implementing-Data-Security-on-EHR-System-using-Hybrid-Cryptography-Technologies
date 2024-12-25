<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/PatientMaster.Master" AutoEventWireup="true" CodeBehind="ManageFiles.aspx.cs" Inherits="OccupancyDetectionWeb.Patient.ManageFiles" %>
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
    <h4 style="text-align: center">View Files</h4>
    <h6 style="margin-top:7%;margin-left:2%"><a href="ShareFile.aspx" style="color:black"><u>Share File</u></a></h6>
    <asp:GridView ID="gvFile" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="Id" 
        HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#2c92e6" 
        HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="file_name" HeaderText="File Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="full_name" HeaderText="Doctor" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Phone" HeaderText="Mobile" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Descripion" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>
