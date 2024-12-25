<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/PatientMaster.Master" AutoEventWireup="true" CodeBehind="MatchHash.aspx.cs" Inherits="OccupancyDetectionWeb.Patient.MatchHash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <br />
        <br />
        <br />
        <div class="card">
            <div class="card-header">
                <asp:Label ID="lblTitle" runat="server" Font-Size="Large">Match Hash</asp:Label><br />
                <label>Upload Original File to Match Hash</label>&nbsp;
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-md-4">
                        <asp:Label runat="server">File Name</asp:Label><br />
                       <label id="lblFileName" runat="server"></label>&nbsp;
                    </div>
                    <div class="col-md-4">
                        <asp:Label runat="server">Hash Value</asp:Label><br />
                       <label id="lblHash" runat="server"></label>&nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="image/x-png,image/gif,image/jpeg"/>
                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label runat="server">Generated Hash</asp:Label><br />
                       <label id="lblGeneratedHash" runat="server"></label>&nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label><br />
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
