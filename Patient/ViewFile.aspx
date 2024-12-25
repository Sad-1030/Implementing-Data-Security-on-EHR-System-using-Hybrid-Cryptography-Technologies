<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/PatientMaster.Master" AutoEventWireup="true" CodeBehind="ViewFile.aspx.cs" Inherits="OccupancyDetectionWeb.Patient.ViewFile" %>
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
    <asp:GridView ID="gvFile" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="Id" 
        OnRowCommand="gvFile_RowCommand" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#2c92e6" 
        HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Doctor" HeaderText="Doctor" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Phone" HeaderText="Contact" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="file_name" HeaderText="File Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnDownload" runat="server" Text="Download" CommandName="DownloadTable"
                        CausesValidation="true" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary"
                        BackColor="#f96432" ForeColor="White" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnMatch" runat="server" Text="Match Hash" CommandName="MatchTable"
                        CausesValidation="true" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary"
                        BackColor="#f96432" ForeColor="White" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
