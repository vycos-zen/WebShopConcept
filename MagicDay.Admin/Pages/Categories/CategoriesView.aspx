<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoriesView.aspx.cs" Inherits="MagicDay.Admin.CategoriesView" %>

<asp:Content ID="CategoriesViewHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="CategoriesViewBody" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:ScriptManagerProxy ID="categoriesViewScriptProxy" runat="server" />
    <h2 class="largeTitle">Categories</h2>
    <div id="shopBody">
        <div class="pageSection">
            <asp:UpdatePanel runat="server" ID="productCategoriesUpdatePanel" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Label runat="server" Text="Category List" CssClass="smallTitle" />
                    <asp:GridView ID="productCategoriesGrid" runat="server"
                        AllowPaging="True"
                        AllowSorting="False"
                        AllowCustomPaging="true"
                        ItemType="MagicDay.DataModel.CategoryView"
                        SelectMethod="productCategoriesGrid_GetData"
                        DeleteMethod="productCategoriesGrid_DeleteItem"
                        DataKeyNames="ID"
                        AutoGenerateColumns="False" PageSize="5"
                        CssClass="table table-hover table-responsive gridStyle"
                        HeaderStyle-CssClass="gridheader"
                        RowStyle-CssClass="gridRows"
                        OnPageIndexChanging="productCategoriesGrid_PageIndexChanging"
                        OnRowDeleting="productCategoriesGrid_RowDeleting"
                        OnRowEditing="productCategoriesGrid_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="Category Name" SortExpression="Category">
                                <ItemTemplate>
                                    <asp:Label ID="productCategoryNameLbl" runat="server"
                                        Text='<%# Eval("Category") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parent Category" SortExpression="ParentCategory">
                                <ItemTemplate>
                                    <asp:Label runat="server"
                                        ID="lblParentCategoryName"
                                        Text='<%# Eval("ParentCategory") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Count" SortExpression="ProductCount">
                                <ItemTemplate>
                                    <asp:Label runat="server"
                                        ID="lblParentCategoryName"
                                        Text='<%# Eval("ProductCount") %>' />
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
                    <asp:Panel runat="server"
                        ID="pnlPageError"
                        Visible='<%# !ModelState.IsValid %>'>
                        <asp:ValidationSummary runat="server"
                            CssClass="errorText"
                            DisplayMode="BulletList" />
                        <a href="/Pages/Categories/CategoriesView.aspx">Ok</a>
                    </asp:Panel>
                    <asp:LinkButton runat="server"
                        ID="btnAddCategory"
                        Text="Add Category"
                        CssClass="btn mdButton"
                        OnClick="btnAddCategory_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
