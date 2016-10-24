<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoriesEdit.aspx.cs" Inherits="MagicDay.Admin.CategoriesEdit" %>

<asp:Content ID="CategoriesEditHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="CategoriesEditBody" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:ScriptManagerProxy ID="categoriesEditScriptProxy" runat="server" />
    <h2 class="largeTitle">Categories</h2>
    <div id="shopBody">
        <asp:UpdatePanel ID="upnlCategoriesEdit" runat="server" UpdateMode="Always">
            <Triggers>
            </Triggers>
            <ContentTemplate>
                <div class="pageSection">
                    <asp:Label runat="server"
                        Text="Category Edit"
                        CssClass="smallTitle" />
                    <asp:FormView ID="frmvCategoryForm" runat="server"
                        ItemType="MagicDay.DataModel.ProductCategory"
                        DataKeyNames="CategoryID"
                        SelectMethod="frmvCategoryForm_GetItem"
                        UpdateMethod="frmvCategoryForm_UpdateItem"
                        InsertMethod="frmvCategoryForm_InsertItem"
                        OnItemCreated="frmvCategoryForm_ItemCreated"
                        DefaultMode="Insert">
                        <ItemTemplate>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                        </EmptyDataTemplate>
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <EditItemTemplate>
                            <div class="form-group hasLabel hasTextBox">
                                <asp:Label ID="categoryNameLabel" runat="server"
                                    Text="Category Name"
                                    AssociatedControlID="categoryName" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox ID="categoryName" runat="server"
                                                CausesValidation="True"
                                                CssClass="form-control"
                                                Text='<%#: BindItem.CategoryName %>'
                                                AutoPostBack="true" />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:CustomValidator ID="categoryExistsValidator" runat="server"
                                                ControlToValidate="categoryName"
                                                ErrorMessage="Category name allready exists."
                                                ToolTip="Category name allready exists."
                                                Text="*"
                                                OnServerValidate="categoryExistsValidator_ServerValidate"
                                                CssClass="errorText control-label"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator ID="categoryNameRequired" runat="server"
                                                ToolTip="Name Required."
                                                ControlToValidate="categoryName"
                                                Text="*"
                                                ErrorMessage="Name Required"
                                                CssClass="errorText control-label"
                                                Display="Dynamic" />
                                        </div>
                                    </li>
                                </ul>
                                <div class="hasCheckBox">
                                    <asp:CheckBox ID="hasParentCategory" runat="server"
                                        Text="Has Parent Category"
                                        OnCheckedChanged="hasParentCategory_CheckedChanged"
                                        AutoPostBack="True"
                                        Checked='<%# (Eval("ParentCategoryID") !=null)%>' />
                                </div>
                                <asp:Panel ID="parentCategoryPanel" runat="server"
                                    Visible='<%# (Eval("ParentCategoryID") !=null)%>'
                                    CssClass="hasTextBox">
                                    <asp:Label ID="selectParentCategory" runat="server"
                                        Text="Select Parent Category"
                                        AssociatedControlID="parentCategoryDrop" />
                                    <br />
                                    <ul class="horizontalUl">
                                        <li>
                                            <div class="horizontalUlLiFormField">
                                                <asp:DropDownList ID="parentCategoryDrop" runat="server"
                                                    AutoPostBack="True"
                                                    ItemType="MagicDay.DataModel.ProductCategory"
                                                    SelectMethod="parentCategoryDrop_GetData"
                                                    DataTextField="CategoryName"
                                                    DataValueField="CategoryID"
                                                    CssClass="form-control"
                                                    OnDataBound="parentCategoryDrop_DataBound" />
                                        </li>
                                        <li>
                                            <div class="horizontalUlLiFormFieldError">
                                                <asp:CustomValidator ID="parentCategoryValid" runat="server"
                                                    ErrorMessage="Invalid parent category selection."
                                                    Text="*" ToolTip="Invalid parent category selection."
                                                    CssClass="errorText control-label"
                                                    Display="Dynamic"
                                                    OnServerValidate="parentCategoryValid_ServerValidate" />
                                            </div>
                                        </li>
                                    </ul>
                                    <br />
                                </asp:Panel>
                            </div>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <div class="form-group hasLabel hasTextBox">
                                <asp:Label ID="categoryNameLabel" runat="server"
                                    Text="Category Name"
                                    AssociatedControlID="categoryName" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox ID="categoryName" runat="server"
                                                CausesValidation="True"
                                                CssClass="form-control"
                                                Text='<%# BindItem.CategoryName %>'
                                                AutoPostBack="true" />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:CustomValidator ID="categoryExistsValidator" runat="server"
                                                ErrorMessage="Category name allready exists."
                                                Text="*"
                                                ToolTip="Category name allready exists."
                                                ControlToValidate="categoryName"
                                                OnServerValidate="categoryExistsValidator_ServerValidate"
                                                CssClass="errorText control-label"
                                                Display="Dynamic" />
                                            <asp:RequiredFieldValidator ID="categoryNameRequired" runat="server"
                                                ToolTip="Name Required."
                                                ControlToValidate="categoryName"
                                                Text="*"
                                                ErrorMessage="Name Required"
                                                CssClass="errorText control-label"
                                                Display="Dynamic" />
                                        </div>
                                    </li>
                                </ul>
                                <div class="hasCheckBox">
                                    <asp:CheckBox ID="hasParentCategory" runat="server"
                                        Text="Has Parent Category"
                                        OnCheckedChanged="hasParentCategory_CheckedChanged"
                                        AutoPostBack="True"
                                        Checked='<%# (Eval("ParentCategoryID") !=null) %>' />
                                </div>
                                <asp:Panel ID="parentCategoryPanel" runat="server"
                                    Visible='<%# (Eval("ParentCategoryID") !=null)%>'
                                    CssClass="hasTextBox">
                                    <asp:Label ID="selectParentCategory" runat="server"
                                        Text="Select Parent Category"
                                        AssociatedControlID="parentCategoryDrop" />
                                    <br />
                                    <ul class="horizontalUl">
                                        <li>
                                            <div class="horizontalUlLiFormField">
                                                <asp:DropDownList ID="parentCategoryDrop" runat="server"
                                                    AutoPostBack="True"
                                                    ItemType="MagicDay.DataModel.ProductCategory"
                                                    SelectMethod="parentCategoryDrop_GetData"
                                                    DataTextField="CategoryName"
                                                    DataValueField="CategoryID"
                                                    CssClass="form-control"
                                                    OnDataBound="parentCategoryDrop_DataBound" />
                                            </div>
                                        </li>
                                        <li>
                                            <div class="horizontalUlLiFormFieldError">
                                                <asp:CustomValidator ID="parentCategoryValid" runat="server"
                                                    ErrorMessage="Invalid parent category selection."
                                                    ToolTip="Invalid parent category selection."
                                                    Text="*"
                                                    CssClass="errorText control-label"
                                                    Display="Dynamic"
                                                    OnServerValidate="parentCategoryValid_ServerValidate" />
                                            </div>
                                        </li>
                                    </ul>
                                    <br />
                                </asp:Panel>
                            </div>
                        </InsertItemTemplate>
                    </asp:FormView>
                    <asp:ValidationSummary runat="server" CssClass="errorText" DisplayMode="BulletList" />
                    <asp:LinkButton runat="server" ID="btnSubmitCategory" Text="Submit" CssClass="btn mdButton" OnClick="btnSubmitCategory_Click" />
                    <asp:LinkButton runat="server" ID="btnCancelSubmit" Text="Cancel" CausesValidation="false" CssClass="btn mdButton" OnClick="btnCancelSubmit_Click" />
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
