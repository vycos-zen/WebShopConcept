<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MagicDay.Admin.Default" %>

<asp:Content ID="DefaultContentHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="DefaultContentBody" ContentPlaceHolderID="ContentBody" runat="server">
    <h2 class="largeTitle">Magic Day Web Admin</h2>
    <asp:Label runat="server" Text="Go To:" CssClass="smallTitle" />
    <br />
    <ul>
        <li class="mediumNormalText">
            <asp:HyperLink runat="server" NavigateUrl="~/Pages/Categories/CategoriesView.aspx" Text="Categories" />
        </li>
        <li class="mediumNormalText">
            <asp:HyperLink runat="server" NavigateUrl="~/Pages/Products/ProductsView.aspx" Text="Products" />
        </li>
    </ul>
    <br />

</asp:Content>
