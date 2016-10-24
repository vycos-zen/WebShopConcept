<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductImageManager.aspx.cs" Inherits="MagicDay.Admin.ProductImageManager" %>

<asp:Content ID="ProductImagesHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="ProductImagesBody" ContentPlaceHolderID="ContentBody" runat="server">
    <h2 class="largeTitle">Product Images</h2>
    <div id="shopBody">


        <asp:UpdatePanel runat="server" ID="upnlProductImages" UpdateMode="Always" ChildrenAsTriggers="true">

            <ContentTemplate>
                <div class="pageSection">
                    <div class="smallTitle">
                        <asp:Label runat="server" ID="lblProductName" Text='<%# "Product: "+GetProductNameOfImages() %>' />
                    </div>
                    <asp:ListView runat="server"
                        ID="lstProductImages"
                        ItemType="MagicDay.DataModel.ProductImage"
                        DataKeyNames="ProductImageID"
                        SelectMethod="lstProductImages_GetData"
                        UpdateMethod="lstProductImages_UpdateItem"
                        DeleteMethod="lstProductImages_DeleteItem"
                        OnItemCanceling="lstProductImages_ItemCanceling"
                        OnItemDeleting="lstProductImages_ItemDeleting"
                        OnItemUpdating="lstProductImages_ItemUpdating">
                        <LayoutTemplate>
                            <ul runat="server" class="horizontalUl">
                                <li runat="server" id="groupPlaceholder"></li>
                            </ul>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <li runat="server" id="itemPlaceholder" />
                        </GroupTemplate>
                        <GroupSeparatorTemplate>
                        </GroupSeparatorTemplate>
                        <EmptyItemTemplate>
                        </EmptyItemTemplate>
                        <EmptyDataTemplate>
                        </EmptyDataTemplate>

                        <ItemTemplate>
                            <li>
                                <ul class="imagesBoxUl hasLabel">
                                    <li class="imageBoxTextCenter">
                                        <asp:Label runat="server"
                                            ID="lblImageNo"
                                            Text='<%# "("+Eval("ImageNo")+"/10)"%>' />
                                    </li>
                                    <li class="imageBoxImage">
                                        <asp:Image runat="server"
                                            ID="imgProductImages"
                                            ImageUrl='<%# DisplayProductThumbnail(Eval("ProductImageID").ToString() , Eval("ImageNo").ToString()) %>' />
                                    </li>
                                    <li class="imageBoxText hasLabel">
                                        <asp:Label runat="server"
                                            ID="lblImageDescription"
                                            Text='<%# Eval("ImageDesciption")%>' />
                                    </li>
                                    <li class="imageBoxCommands">
                                        <asp:LinkButton runat="server" Text="Edit" CommandName="Edit" />
                                        <asp:LinkButton runat="server" Text="Remove" CommandName="Delete" />
                                    </li>
                                </ul>
                            </li>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <li>
                                <ul class="imagesBoxUl hasLabel">
                                    <li class="imageBoxTextCenter">
                                        <asp:Label runat="server"
                                            ID="lblImageNo"
                                            Text='<%# "("+Eval("ImageNo")+"/10)"%>' />
                                    </li>

                                    <li class="imageBoxImage">
                                        <asp:Image runat="server"
                                            ImageUrl='<%# DisplayProductThumbnail(Eval("ProductImageID").ToString() , Eval("ImageNo").ToString()) %>' />
                                    </li>
                                    <li class="imageBoxTextInput hasTextBox">
                                        <asp:TextBox runat="server" ID="txtImageDescription" Text='<%# BindItem.ImageDesciption %>' CssClass="form-control" />
                                    </li>
                                    <li class="imageBoxCommands">
                                        <asp:LinkButton runat="server" Text="Update" CommandName="Update" />
                                        <asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" />
                                    </li>
                                </ul>
                            </li>

                        </EditItemTemplate>
                    </asp:ListView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ValidationSummary runat="server" CssClass="errorText" DisplayMode="BulletList" />
        <div class="pageSection">
            <asp:Label runat="server" Text="Select images to upload" />
            <br />
            <asp:FileUpload runat="server" ID="fplImageUpload" AllowMultiple="true" />
            <br />
            <ul class="horizontalUl">
                <li>
                    <asp:LinkButton runat="server"
                        ID="btnAddTempImages"
                        Text="Add Description"
                        OnClick="btnAddTempImages_Click"
                        CssClass="btn mdButton" />
                </li>
                <li>
                    <asp:LinkButton runat="server"
                        ID="btnSubmitUpload"
                        Text="Submit Images"
                        CssClass="btn mdButton"
                        OnClick="btnSubmitUpload_Click" />
                </li>
                <li>
                    <asp:LinkButton runat="server"
                        ID="btnCancelUpload"
                        Text="Cancel"
                        OnClick="btnCancelUpload_Click"
                        CssClass="btn mdButton"
                        CausesValidation="false" />
                </li>
            </ul>
            <br />
            <asp:UpdatePanel runat="server" ID="upnlTempImagesPanel" ChildrenAsTriggers="true" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAddTempImages" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:ListView runat="server" ID="lstTempImages"
                ItemType="MagicDay.BusinessLogic.ImageTasks.ImageUploadItem"
                DataKeyNames="ImageId"
                OnItemEditing="lstTempImages_ItemEditing"
                OnItemUpdating="lstTempImages_ItemUpdating"
                OnItemCanceling="lstTempImages_ItemCanceling"
                OnItemDeleting="lstTempImages_ItemDeleting"
                ViewStateMode="Disabled"
                GroupItemCount="5">
                <LayoutTemplate>
                    <ul runat="server" class="horizontalUl" style="width: 960px;">
                        <li runat="server" id="groupPlaceholder"></li>
                    </ul>
                </LayoutTemplate>
                <GroupTemplate>
                    <li runat="server" id="itemPlaceholder" />
                </GroupTemplate>
                <GroupSeparatorTemplate>
                </GroupSeparatorTemplate>
                <ItemTemplate>
                    <li>
                        <ul class="imagesBoxUl">
                            <li class="imageBoxImage">
                                <asp:Image runat="server" ID="imgTempImage" ImageUrl='<%# DisplayTempProductImageThumbnail(Eval("ImageID").ToString()) %>' />
                            </li>
                            <li class="imageBoxText hasLabel">
                                <asp:Label runat="server" Text="Description:" />
                                <br />
                                <asp:Label runat="server" ID="lblImageDescription" Text="<%# BindItem.ImageDescription %>" />
                            </li>
                            <li class="imageBoxCommands">
                                <asp:LinkButton runat="server" Text="Edit" CommandName="Edit" />
                                <asp:LinkButton runat="server" Text="Remove" CommandName="Delete" />
                            </li>
                        </ul>
                    </li>
                </ItemTemplate>
                <EditItemTemplate>
                    <li>
                        <ul class="imagesBoxUl">
                            <li class="imageBoxImage">
                                <asp:Image runat="server" ID="imgTempImage" ImageUrl='<%# DisplayTempProductImageThumbnail(Eval("ImageID").ToString()) %>' />
                            </li>
                            <li class="imageBoxTextInput hasLabel hasTextBox">
                                <asp:Label runat="server" Text="Description:" />
                                <br />
                                <asp:TextBox runat="server" ID="txtImageDescription" Text="<%# BindItem.ImageDescription %>" CssClass="form-control" />
                            </li>
                            <li class="imageBoxCommands">
                                <asp:LinkButton runat="server" Text="Update" CommandName="Update" />
                                <asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" />
                            </li>
                        </ul>
                    </li>
                </EditItemTemplate>
            </asp:ListView>


        </div>
        <asp:CustomValidator runat="server" ID="cvCheckMaxNumberOfImage" OnServerValidate="cvCheckMaxNumberOfImage_ServerValidate" />
    </div>
</asp:Content>
