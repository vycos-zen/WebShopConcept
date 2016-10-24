<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsView.aspx.cs" Inherits="MagicDay.Admin.ProductsView" Async="true" %>

<%--Trace="true"--%>
<asp:Content ID="ProductsViewHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="ProductsViewBody" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:ScriptManagerProxy ID="productsViewScriptProxy" runat="server" />
    <h2 class="largeTitle">Shop Management</h2>
    <div id="shopBody">
        <asp:UpdatePanel ID="productImagesUpdatePanel" runat="server" RenderMode="Inline">
            <Triggers>
            </Triggers>
            <ContentTemplate>
                <div class="pageSection">
                    <asp:Label runat="server"
                        Text="Products"
                        CssClass="smallTitle" />
                    <br />
                    <asp:GridView ID="productsGrid" runat="server"
                        AllowPaging="True"
                        AllowCustomPaging="true"
                        AllowSorting="False" 
                        PageSize="10"
                        AutoGenerateColumns="False"
                        ItemType="MagicDay.DataModel.ProductView"
                        SelectMethod="productsGrid_GetData"
                        DeleteMethod="productsGrid_DeleteItem"
                        DataKeyNames="ID"
                        CssClass="table table-hover table-responsive gridStyle"
                        HeaderStyle-CssClass="gridheader"
                        RowStyle-CssClass="gridRows hasTextBox"
                        OnPageIndexChanging="productsGrid_PageIndexChanging"
                        OnRowDeleting="productsGrid_RowDeleting"
                        OnRowEditing="productsGrid_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name" ItemStyle-Width="180">
                                <ItemTemplate>
                                    <asp:Label ID="productNameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Category" SortExpression="Category" ItemStyle-Width="180">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCategory" runat="server" Text='<%# Eval("Category") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="Description" ItemStyle-Width="180">
                                <ItemTemplate>
                                    <asp:Label ID="productDescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" SortExpression="Code" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="productCodeLabel" runat="server" Text='<%# Eval("Code") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price" SortExpression="Price" ItemStyle-Width="50">
                                <ItemTemplate>
                                    <asp:Label ID="productPriceLabel" runat="server" Text='<%# Eval("Price") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Images" SortExpression="ImageCount">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server"
                                        ID="btnNumberOfImages"
                                        Text='<%# Eval("ImageCount") %>'
                                        CommandName="ViewImages"
                                        CommandArgument='<%# Eval("ID") %>'
                                        OnCommand="btnNumberOfImages_Command" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="true"
                                ButtonType="Link"
                                ControlStyle-CssClass="btn mdButton"
                                ItemStyle-HorizontalAlign="Right"
                                CausesValidation="true"
                                EditText="Edit"
                                ShowEditButton="true"
                                DeleteText="Delete" />
                        </Columns>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridRows" />
                    </asp:GridView>
                    <asp:LinkButton ID="btnAddProduct" runat="server"
                        Text="Add Product"
                        OnClick="btnAddProduct_Click"
                        CausesValidation="false"
                        CssClass="btn mdButton" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
