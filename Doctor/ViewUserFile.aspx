<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="ViewUserFile.aspx.cs" Inherits="OccupancyDetectionWeb.Doctor.ViewUserFile" %>

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
    <h4 style="text-align: center">User Files</h4>
    <asp:GridView ID="gvFile" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="gvFile_RowCommand"
        HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f96432" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="patient_name" HeaderText="Patient" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Contact_no" HeaderText="Contact" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="file_name" HeaderText="File Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Descripion" HeaderText="Descripion" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Date" HeaderText="Creation Date" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnDownload" runat="server" Text="Download" CommandName="DownloadTable"
                        CausesValidation="true" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary"
                        BackColor="#2c92e6" ForeColor="White" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
