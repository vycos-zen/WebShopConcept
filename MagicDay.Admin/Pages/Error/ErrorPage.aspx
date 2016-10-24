<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="MagicDay.Admin.ErrorPage" %>

<asp:Content ID="ErrorPageHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="ErrorPageBody" ContentPlaceHolderID="ContentBody" runat="server">
    <h2 class="largeTitle">Error Page</h2>
    <div id="shopBody">
        <div class="pageSection hasLabel">
            <p>
                <asp:Label runat="server" ID="lblErrorTitle" />
                <br />
                <asp:Label runat="server" ID="lblError" CssClass="errorText" />
            </p>
        </div>
    </div>
</asp:Content>
