<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GalleryManagement.aspx.cs" EnableEventValidation="true" Inherits="MagicDay.Admin.GalleryManagement" ClientIDMode="Predictable" Async="True" %>

<asp:Content ID="GalleryManagementContentHead" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="GalleryManagementContentBody" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:ScriptManagerProxy ID="galleryScriptManagerProxy" runat="server">
    </asp:ScriptManagerProxy>
    <asp:ObjectDataSource ID="ImageUploadRosterDataSource" runat="server"
        SelectMethod="GetAllImages"
        TypeName="MagicDay.ClassLibrary.Classes.ImageUploadRoster"
        OnObjectCreating="ImageUploadRosterDataSource_ObjectCreating"
        DataObjectTypeName="ImageUploadItem"
        DeleteMethod="RemoveImageFromUploadRoster" />
    <ef:EntityDataSource ID="ImageDataSource" runat="server"
        ConnectionString="name=MagicDayDBEntities"
        DefaultContainerName="MagicDayDBEntities"
        EnableDelete="True"
        EnableFlattening="False"
        EnableInsert="True"
        EnableUpdate="True"
        EntitySetName="GalleryImageEDs"
        EntityTypeFilter="GalleryImageED" />


    <div id="galleryDiv">
        <h2 class="largeTitle">Gallery Management</h2>
        <div id="imageUploadDiv" class="pageSection">
            <asp:UpdatePanel ID="imageUploadUpdatePanel" runat="server"
                Visible="true"
                RenderMode="Block"
                UpdateMode="Always">
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="imageUploadRoster" EventName="ItemCommand" />--%>
                    <%--  <asp:AsyncPostBackTrigger ControlID="uploadImage" EventName="Click" />--%>
                    <asp:PostBackTrigger ControlID="btnUploadImages" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label runat="server"
                        Text="Upload Images"
                        CssClass="smallTitle"
                        AssociatedControlID="btnNewUploadRosterToggle" />
                    <br />
                    <asp:Button ID="btnNewUploadRosterToggle" runat="server"
                        Text="New Upload"
                        OnClick="btnNewUploadRosterToggle_Click"
                        CssClass="btn mdButton" />
                    <br />
                    <br />
                    <asp:Panel ID="imageUploadPanel" runat="server"
                        Visible="false"
                        CssClass="hasLabel">
                        <ul class="horizontalUl">
                            <li style="width: 300px">
                                <asp:Label ID="uploadPrompt" runat="server"
                                    AssociatedControlID="imageUpload"
                                    Text="Select the images to be uploaded:" />
                                <asp:FileUpload ID="imageUpload" runat="server"
                                    AllowMultiple="true"
                                    CssClass="form-control" />
                            </li>
                            <li style="vertical-align: middle">
                                <asp:CustomValidator ID="imageUploadValidation" runat="server"
                                    ErrorMessage="Upload maximum 25 images."
                                    ToolTip="Upload maximum 25 images."
                                    ValidationGroup="maxNumberOfImagesValidation"
                                    OnServerValidate="imageUploadValidation_ServerValidate"
                                    CssClass="errorText"
                                    Display="Dynamic" />
                            </li>
                        </ul>
                        <asp:Label ID="uploadLimitLabel" runat="server"
                            Text="<%# NumberOfImagesInRoaster()%>"
                            ToolTip="Number of images to be uploaded."
                            AssociatedControlID="setImageNames" />
                        <br />

                        <asp:Button ID="btnUploadImages" runat="server"
                            Text="Add Images"
                            OnClick="btnUploadImages_Click"
                            ValidationGroup="maxNumberOfImagesValidation"
                            CssClass="btn mdButton" />
                        <br />
                        <br />

                        <asp:Label ID="setNamesPrompt" runat="server"
                            AssociatedControlID="imageUploadRoster"
                            Text="Set the names for the images and click Upload."
                            Visible="false" />
                        <br />
                        <asp:DataList ID="imageUploadRoster" runat="server"
                            DataSourceID="ImageUploadRosterDataSource"
                            RepeatDirection="Horizontal"
                            RepeatColumns="5"
                            OnDeleteCommand="imageUploadRoster_DeleteCommand"
                            OnEditCommand="imageUploadRoster_EditCommand"
                            OnUpdateCommand="imageUploadRoster_UpdateCommand">
                            <ItemTemplate>
                                <div class="imageSetNameDiv" runat="server">
                                    <ul class="imagesBoxUl">
                                        <li class="imageBoxText">
                                            <asp:Label ID="ImageNameLabel" runat="server"
                                                Text='<%# Eval("ImageName") %>' />
                                        </li>
                                        <li class="imageBoxImage">

                                            <asp:Image ID="ImageThumbnail" runat="server"
                                                Src='<%# mdOperations.DisplayImagePreUpload((byte[])Eval("ImageThumbnail")) %>' />
                                        </li>
                                        <li class="imageBoxTextInput">
                                            <asp:TextBox ID="ImageName" runat="server"
                                                Text='<%# Eval("ImageName") %>'
                                                CausesValidation="True"
                                                MaxLength="50"
                                                CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="imageNameRequired" runat="server"
                                                ControlToValidate="ImageName"
                                                ErrorMessage="*"
                                                ToolTip="Name Required"
                                                CssClass="errorText"
                                                SetFocusOnError="True"
                                                Display="Dynamic" />
                                        </li>
                                        <li class="imageBoxCommands">
                                            <asp:LinkButton ID="btnUpdateImageEdit" runat="server"
                                                Text="Update"
                                                CommandName="Update"
                                                CommandArgument='<%# Eval("ImageId") %>'
                                                CssClass="btn mdButton" />
                                            <asp:LinkButton ID="btnRemoveImage" runat="server"
                                                Text="Remove"
                                                CommandName="Delete"
                                                CausesValidation="false"
                                                CommandArgument='<%# Eval("ImageId") %>'
                                                CssClass="btn mdButton" />
                                        </li>
                                    </ul>
                                </div>
                            </ItemTemplate>
                            <%--      <FooterTemplate>
                                <asp:Label ID="numberOfImagesLabel" runat="server"
                                    Text='<%# NumberOfImagesInRoaster()%>' ToolTip="Number of images to be uploaded." />
                            </FooterTemplate>--%>
                        </asp:DataList>

                        <br />
                        <asp:Button ID="btnUploadImage" runat="server"
                            Text="Upload"
                            OnClick="uploadImage_Click"
                            CssClass="btn mdButton" />


                        <asp:Label ID="uploadStatus" runat="server" />


                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>

        <div class="pageSection">
            <div id="imageDisplayDiv" class="hasLabel">

                <asp:Label runat="server" AssociatedControlID="imageUploadRoster" Text="Gallery Images" />
                <asp:UpdatePanel ID="setNamesPanel" runat="server"
                    RenderMode="Block"
                    UpdateMode="Always">
                    <Triggers>
                        <%--   <asp:AsyncPostBackTrigger ControlID="uploadImage" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="imageList" EventName="ItemCommand" />--%>
                    </Triggers>
                    <ContentTemplate>


                        <asp:ListView ID="imageList" runat="server"
                            DataKeyNames="GalleryImageID"
                            DataSourceID="ImageDataSource"
                            GroupItemCount="5"
                            OnItemCommand="imageList_ItemCommand">
                            <GroupTemplate>
                                <tr id="itemPlaceholderContainer" runat="server">
                                    <td id="itemPlaceholder" runat="server"></td>
                                </tr>
                            </GroupTemplate>

                            <LayoutTemplate>
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                                <tr id="groupPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server">
                                            <div class="imageBoxCommands btn mdButton">
                                                <asp:DataPager ID="DataPager1" runat="server" PageSize="10">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Link"
                                                            ShowFirstPageButton="True"
                                                            ShowNextPageButton="False"
                                                            ShowPreviousPageButton="False" />
                                                        <asp:NumericPagerField />
                                                        <asp:NextPreviousPagerField ButtonType="Link"
                                                            ShowLastPageButton="True"
                                                            ShowNextPageButton="False"
                                                            ShowPreviousPageButton="False" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <td runat="server" style="">
                                    <div class="imageSetNameDiv" runat="server">
                                        <ul class="imagesBoxUl">
                                            <li class="imageBoxText">
                                                <asp:Label ID="GalleryImageNameLabel" runat="server"
                                                    Text=' <%# "Display Name: "+Eval("GalleryImageName") %>' />
                                            </li>
                                            <li class="imageBoxText">
                                                <asp:Label ID="GalleryImageUploadDateLabel" runat="server"
                                                    Text='<%# "  Uploaded: "+((DateTime)Eval("GalleryImageUploadDate")).ToShortDateString() %>' />
                                            </li>
                                            <li class="imageBoxImage">
                                                <asp:Image ID="GalleryImageLabel" runat="server"
                                                    Src='<%#mdOperations.ImageServerUrl()+"?source=gallery&type=thumbnail&guid="+ (Guid?)Eval("GalleryImageID") %>' />
                                            </li>
                                            <li class="imageBoxCommands">
                                                <asp:LinkButton ID="btnEditGalleryImage" runat="server"
                                                    Text="Edit Name"
                                                    CommandName="Edit"
                                                    CommandArgument='<%# Eval("GalleryImageID") %>'
                                                    CssClass="btn mdButton" />
                                                <asp:LinkButton ID="btnRemoveGalleryImage" runat="server"
                                                    Text="Remove"
                                                    CommandName="delete"
                                                    CausesValidation="false"
                                                    CommandArgument='<%# Eval("GalleryImageID") %>'
                                                    CssClass="btn mdButton" />
                                            </li>
                                        </ul>
                                    </div>
                                </td>
                            </ItemTemplate>

                            <SelectedItemTemplate>
                                <td runat="server" style="">
                                    <div class="imageSetNameDiv" runat="server">
                                        <ul class="imagesBoxUl">
                                            <li class="imageBoxImage">
                                                <asp:Image ID="GalleryImageLabel" runat="server"
                                                    Src='<%#mdOperations.ImageServerUrl()+"?source=gallery&type=thumbnail&guid="+ (Guid?)Eval("GalleryImageID") %>' />
                                            </li>
                                            <li class="imageBoxTextInput">
                                                <asp:TextBox ID="GalleryImageName" runat="server"
                                                    Text='<%# Eval("GalleryImageName") %>'
                                                    CausesValidation="True"
                                                    MaxLength="50"
                                                    CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="imageNameRequired" runat="server"
                                                    ControlToValidate="GalleryImageName"
                                                    ErrorMessage="*"
                                                    ToolTip="Name Required"
                                                    CssClass="errorText"
                                                    SetFocusOnError="True"
                                                    Display="Dynamic" />
                                            </li>
                                            <li class="imageBoxCommands">
                                                <asp:LinkButton ID="btnEditGalleryImage" runat="server"
                                                    Text="Update"
                                                    CommandName="update"
                                                    CommandArgument='<%# Eval("GalleryImageID") %>'
                                                    CssClass="btn mdButton" />
                                            </li>
                                        </ul>
                                    </div>
                                </td>
                            </SelectedItemTemplate>

                            <EditItemTemplate>
                                <td runat="server" style="">
                                    <div class="imageSetNameDiv" runat="server">
                                        <ul class="imagesBoxUl">
                                            <li class="imageBoxImage">
                                                <asp:Image ID="GalleryImageLabel" runat="server"
                                                    Src='<%#mdOperations.ImageServerUrl()+"?source=gallery&type=thumbnail&guid="+ (Guid?)Eval("GalleryImageID") %>' />
                                            </li>
                                            <li class="imageBoxTextInput">
                                                <asp:TextBox ID="GalleryImageName" runat="server"
                                                    Text='<%# Eval("GalleryImageName") %>'
                                                    CausesValidation="True"
                                                    MaxLength="50"
                                                    CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="imageNameRequired" runat="server"
                                                    ControlToValidate="GalleryImageName"
                                                    ErrorMessage="*"
                                                    ToolTip="Name Required"
                                                    CssClass="errorText"
                                                    SetFocusOnError="True"
                                                    Display="Dynamic" />
                                            </li>
                                            <li class="imageBoxCommands">
                                                <asp:LinkButton ID="btnEditGalleryImage" runat="server"
                                                    Text="Update" CommandName="Update"
                                                    CommandArgument='<%# Eval("GalleryImageID") %>'
                                                    CssClass="btn mdButton" />
                                            </li>
                                        </ul>
                                    </div>
                                </td>
                            </EditItemTemplate>
                        </asp:ListView>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
