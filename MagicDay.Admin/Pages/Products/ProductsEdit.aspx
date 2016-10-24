<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsEdit.aspx.cs" Inherits="MagicDay.Admin.ProductsEdit" %>

<asp:Content ID="ProductsEditHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="ProductsEditBody" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:ScriptManagerProxy ID="productsEditScriptProxy" runat="server" />

    <h2 class="largeTitle">Product Edit</h2>
    <div id="shopBody">

        <asp:UpdatePanel runat="server" ID="upnlProductEdit" UpdateMode="Always">
            <ContentTemplate>
                <div class="pageSection">
                    <asp:FormView ID="frmvProductEdit" runat="server"
                        DefaultMode="Insert"
                        ItemType="MagicDay.DataModel.Product"
                        SelectMethod="frmvProductEdit_GetItem"
                        InsertMethod="frmvProductEdit_InsertItem"
                        UpdateMethod="frmvProductEdit_UpdateItem">
                        <ItemTemplate>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <div class="form-group hasLabel hasTextBox">
                                <asp:Label runat="server"
                                    ID="lblProductName"
                                    Text="Product Name"
                                    AssociatedControlID="txtProductName" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductName"
                                                Text='<%# BindItem.ProductName %>'
                                                MaxLength="100"
                                                CssClass="form-control" />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server"
                                                ControlToValidate="txtProductName"
                                                ErrorMessage="Name Required."
                                                ToolTip="Name Required."
                                                Text="*"
                                                CssClass="errorText control-label"
                                                SetFocusOnError="True"
                                                Display="Static" />
                                        </div>
                                    </li>
                                </ul>
                                <asp:Label runat="server"
                                    Text="Product Category"
                                    AssociatedControlID="cboProductCategory" />
                                <div class="horizontalUlLiFormField">
                                    <asp:DropDownList ID="cboProductCategory" runat="server"
                                        ItemType="MagicDay.DataModel.ProductCategory"
                                        DataTextField="CategoryName"
                                        DataValueField="CategoryID"
                                        SelectMethod="cboProductCategory_GetData"
                                        CssClass="form-control" />
                                </div>
                                <asp:Label runat="server"
                                    Text="Product Code"
                                    AssociatedControlID="txtProductCode" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductCode"
                                                Text='<%# BindItem.ProductCode %>'
                                                MaxLength="20"
                                                CssClass="form-control" />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:RequiredFieldValidator runat="server"
                                                ID="productCodeRequiredValidator"
                                                ControlToValidate="txtProductCode"
                                                ErrorMessage="Product Code Requried."
                                                ToolTip="Product Code Requried."
                                                Text="*"
                                                CssClass="errorText"
                                                Display="Static"
                                                SetFocusOnError="True" />
                                        </div>
                                    </li>
                                </ul>
                                <asp:Label runat="server"
                                    Text="Product Description"
                                    AssociatedControlID="txtProductDescription" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductDescription"
                                                Rows="5"
                                                TextMode="MultiLine"
                                                MaxLength="1000"
                                                CssClass="form-control"
                                                Text='<%# BindItem.Description %>' />
                                        </div>
                                    </li>
                                </ul>
                                <asp:Label runat="server"
                                    ID="productDescriptionLimit"
                                    Text="" />
                                <asp:Label runat="server"
                                    Text="Product Price" AssociatedControlID="txtProductPrice" />

                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductPrice"
                                                TextMode="Number"
                                                MaxLength="10"
                                                CssClass="form-control"
                                                Text='<%# BindItem.Price %>' />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:RequiredFieldValidator ID="priceRequiredValidator" runat="server"
                                                ControlToValidate="txtProductPrice"
                                                ToolTip="Product Price Required."
                                                ErrorMessage="Product Price Required."
                                                Text="*"
                                                CssClass="errorText control-label"
                                                Display="Static"
                                                SetFocusOnError="True" />
                                        </div>
                                        <asp:RangeValidator ID="priceRangeValidator" runat="server"
                                            ControlToValidate="txtProductPrice"
                                            ToolTip="Price must be between 0-1000000"
                                            ErrorMessage="Price must be between 0-1000000"
                                            Text="*"
                                            CssClass="errorText control-label"
                                            Display="Static"
                                            MaximumValue="1000000"
                                            MinimumValue="0"
                                            SetFocusOnError="True"
                                            Type="Currency" />
                                    </li>
                                </ul>
                                <asp:CheckBox runat="server" ID="chkProceedToProductImages" Text="Edit Images" Checked="false" />
                            </div>
                        </EditItemTemplate>

                        <InsertItemTemplate>
                            <div class="form-group hasLabel hasTextBox">
                                <asp:Label runat="server"
                                    ID="lblProductName"
                                    Text="Product Name"
                                    AssociatedControlID="txtProductName" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductName"
                                                Text='<%# BindItem.ProductName %>'
                                                MaxLength="100"
                                                CssClass="form-control" />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server"
                                                ControlToValidate="txtProductName"
                                                ErrorMessage="Name Required."
                                                ToolTip="Name Required."
                                                Text="*"
                                                CssClass="errorText control-label"
                                                SetFocusOnError="True"
                                                Display="Dynamic" />
                                            <asp:CustomValidator ID="productExistsValidator" runat="server"
                                                ControlToValidate="txtProductName"
                                                ErrorMessage="Product name allready exists."
                                                ToolTip="Product name allready exists."
                                                Text="*"
                                                OnServerValidate="productExistsValidator_ServerValidate"
                                                CssClass="errorText"
                                                Display="Dynamic" />
                                        </div>
                                    </li>
                                </ul>
                                <asp:Label runat="server"
                                    Text="Product Category"
                                    AssociatedControlID="cboProductCategory" />

                                <div class="horizontalUlLiFormField">
                                    <asp:DropDownList ID="cboProductCategory" runat="server"
                                        ItemType="MagicDay.DataModel.ProductCategory"
                                        DataTextField="CategoryName"
                                        DataValueField="CategoryID"
                                        SelectMethod="cboProductCategory_GetData"
                                        CssClass="form-control" />
                                </div>
                                <asp:Label runat="server"
                                    Text="Product Code"
                                    AssociatedControlID="txtProductCode" />

                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductCode"
                                                Text='<%# BindItem.ProductCode %>'
                                                MaxLength="20"
                                                CssClass="form-control" />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:RequiredFieldValidator runat="server"
                                                ID="productCodeRequiredValidator"
                                                ControlToValidate="txtProductCode"
                                                ErrorMessage="Product Code Requried."
                                                ToolTip="Product Code Requried."
                                                Text="*"
                                                CssClass="errorText"
                                                Display="Dynamic"
                                                SetFocusOnError="True" />
                                        </div>
                                    </li>
                                </ul>
                                <asp:Label runat="server"
                                    Text="Product Description"
                                    AssociatedControlID="txtProductDescription" />
                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductDescription"
                                                Rows="5"
                                                TextMode="MultiLine"
                                                MaxLength="1000"
                                                CssClass="form-control"
                                                Text='<%# BindItem.Description %>' />
                                        </div>
                                    </li>
                                </ul>
                                <asp:Label runat="server"
                                    ID="productDescriptionLimit"
                                    Text="" />
                                <asp:Label runat="server"
                                    Text="Product Price" AssociatedControlID="txtProductPrice" />

                                <ul class="horizontalUl">
                                    <li>
                                        <div class="horizontalUlLiFormField">
                                            <asp:TextBox runat="server"
                                                ID="txtProductPrice"
                                                TextMode="Number"
                                                MaxLength="10"
                                                CssClass="form-control"
                                                Text='<%# BindItem.Price %>' />
                                        </div>
                                    </li>
                                    <li>
                                        <div class="horizontalUlLiFormFieldError">
                                            <asp:RequiredFieldValidator ID="priceRequiredValidator" runat="server"
                                                ControlToValidate="txtProductPrice"
                                                ToolTip="Product Price Required."
                                                ErrorMessage="Product Price Required."
                                                Text="*"
                                                CssClass="errorText control-label"
                                                Display="Dynamic"
                                                SetFocusOnError="True" />
                                        </div>
                                        <asp:RangeValidator ID="priceRangeValidator" runat="server"
                                            ControlToValidate="txtProductPrice"
                                            ToolTip="Price must be between 0-1000000"
                                            ErrorMessage="Price must be between 0-1000000"
                                            Text="*"
                                            CssClass="errorText control-label"
                                            Display="Dynamic"
                                            MaximumValue="1000000"
                                            MinimumValue="0"
                                            SetFocusOnError="True"
                                            Type="Currency" />
                                    </li>
                                </ul>
                                <asp:CheckBox runat="server" ID="chkProceedToProductImages" Text="Add Images" Checked="true" />
                            </div>
                        </InsertItemTemplate>
                    </asp:FormView>
                    <asp:ValidationSummary runat="server"
                        CssClass="errorText"
                        DisplayMode="BulletList" />
                    <asp:LinkButton runat="server"
                        ID="btnSubmitProduct"
                        Text="Submit"
                        OnClick="btnSubmitProduct_Click"
                        CssClass="btn mdButton" />
                    <asp:LinkButton runat="server"
                        ID="btnCancelSubmit"
                        Text="Cancel"
                        CausesValidation="false"
                        CssClass="btn mdButton"
                        OnClick="btnCancelSubmit_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>