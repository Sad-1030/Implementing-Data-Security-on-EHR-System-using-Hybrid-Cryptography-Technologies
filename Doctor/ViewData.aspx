<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="ViewData.aspx.cs" Inherits="OccupancyDetectionWeb.Admin.ViewData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gv {
            margin-left: 10%;
            width: 85%;
            margin-top: 5%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <h4 style="text-align: center">View Data</h4>
    <div style="text-align:end;margin:10px">
            <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-primary" Text="Download Data" BackColor="Navy"
                OnClick="btnDownload_Click" />
    </div>

    <asp:GridView ID="gvData" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="LogID"
        HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f96432" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Luminosity" HeaderText="Luminosity" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Temp" HeaderText="Temp" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Humidity" HeaderText="Humidity" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Co2" HeaderText="Co2" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LogDate" HeaderText="LogDate" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>
