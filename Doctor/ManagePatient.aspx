<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="ManagePatient.aspx.cs" Inherits="OccupancyDetectionWeb.Doctor.ManagePatient" %>
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
   
    <h4 style="text-align: center">Manage Patient</h4>
    <h6 style="margin-top:7%;margin-left:2%"><a href="AddPatient.aspx?action=add" style="color:black"><u>Add Patient</u></a></h6>
    <asp:GridView ID="gvPatient" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="Patient_id"
        HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f96432" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Full_Name" HeaderText="Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Contact_No" HeaderText="Contact" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Email_Id" HeaderText="Email ID" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Date" HeaderText="Creation Date" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>
