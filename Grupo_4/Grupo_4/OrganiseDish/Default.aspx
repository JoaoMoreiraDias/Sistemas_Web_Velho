<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" MasterPageFile="~/Site.Master" Inherits="OrganiseDish_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Button id="add_Dish_btn" Text="Add Dish" OnClick="show_form_for_add_dish" runat="server" />
   
   
   <asp:Label ID="dImgErrorLbl" runat="server" />
   <asp:Label ID="testlabel" runat="server" Text="" />
   
  

    <asp:panel id="pnlform" runat="server" Visible="false">  
    <table id="Table1" border="0px">
        <%-----------------------------------------------------%>
        <tr>
              <td>
                    <asp:Label id="dnamelbl" runat="server" Text="Dish name: "/>   
               </td>
               <td>
                    <asp:TextBox id="dNameTxtbox" runat="server" /> 
                </td>
                <td>
                    <asp:RegularExpressionValidator runat="server" id="dNameTxtboxValid" ControlToValidate="dNameTxtbox" ErrorMessage="Name coulnt not contain other symbols than letters[A-Z], numbers [0-9] and symbols [._] and should be longer than" ValidationExpression="([a-zA-Z0-9_\. ]){2,}" />
                
               </td>
           </tr>
            <tr>
                <td>
                    <asp:Label ID="dDescriptionLbl" runat="server" Text="Description: " />
                </td>
                <td>
                        <asp:TextBox ID="dDecriptionTxtbox" runat="server" Height="89px" 
                    Width="253px" MaxLength="500" TextMode="multiline" /> 
                </td>
                <td>
                    <%--<asp:RegularExpressionValidator runat="server" id="dDecriptionTxtboxValid" ControlToValidate="dDecriptionTxtbox" ErrorMessage="Text coulnt not contain [$%#@*\/]" ValidationExpression="([$%#@*\/]){2,}" />--%>
                
               </td>
            </tr>
                <tr>
                    <td>
                        <asp:Label ID="dAdditionainfoLbl" runat="server" Text="Additiona info" />
                    </td>
                    <td>
                        <asp:TextBox ID="dAdditioninfoTxtbox" runat="server" Height="89px" 
                        Width="253px" MaxLength="500" TextMode="multiline" />
                    </td>
                    <td>
                        <%--<asp:RegularExpressionValidator runat="server" id="dAdditioninfoTxtboxValid" ControlToValidate="dAdditioninfoTxtbox" ErrorMessage="Text coulnt not contain [$%#@*\/]" ValidationExpression="([#$%]{1})" />--%>
                
                     </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="dPriceLbl" runat="server" Text="Price" />
                    </td>
                    <td>
                        <asp:TextBox ID="dPriceTxtbox" runat="server" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator runat="server" id="dPriceTxtboxValid" ControlToValidate="dPriceTxtbox" ErrorMessage="Bad price format" ValidationExpression="(\d){1,}\.\d{0,2}" />
                
                    </td>             
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="dCategoryLbl" runat="server" Text="Category" />
                    </td>
                    <td>
                        <asp:DropDownList ID="dCategoryDropDownList" runat="server" />
                        <asp:TextBox ID="dCategoryAddMoreTxtbox" runat="server" visible="false" />
                        <asp:Button ID="dCategoriesAddMoreSubmitBtn" runat="server" Visible="false" OnClick="add_one_category" Text="Add Category" />
                        <asp:Button ID="dCategoriesAddMoreBtn" runat="server" OnClick="make_dCategoryAddMoreTxtbox_visible" Text="Add New Category" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="dPictureLbl" runat="server" Text="Add Pictures" /><br />
                        <asp:FileUpload ID="dImageLink1" runat="server" BorderStyle="Outset" /><br />                        
                        <asp:FileUpload ID="dImageLink2" runat="server" BorderStyle="Outset" Visible="False" /><br />
                        <asp:FileUpload ID="dImageLink3" runat="server" BorderStyle="Outset" Visible="false" /><br />

                        <asp:Button ID="addMoreImgBtn1" runat="server" Text="Add One more Img1" CommandName="Show" CommandArgument="1"  OnCommand="change_visibility_of_imagelink" />
                        <asp:Button ID="addMoreImgBtn2" runat="server" Text="Add One more Img2" CommandName="Show2" CommandArgument="2" Visible="false"   OnCommand="change_visibility_of_imagelink" />
                        <%--<INPUT id="oFile" type="file" runat="server">--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%--<asp:Label ID="dImgErrorLbl" runat="server" />--%>
                         <asp:ImageMap ID="ImageMap1" Height="250px" Width="250px" ImageUrl="" runat="server" Visible="false" >
                         
                        </asp:ImageMap>
                        <asp:Panel ID="imageHolderPlace1" runat="server" Visible="true" /> 
                        <asp:Panel ID="imageHolderPlace2" runat="server" Visible="true" />   
                        <asp:Panel ID="imageHolderPlace3" runat="server" Visible="true" />                          
                        
                       
                        
                    </td>
                </tr>         
     </table>
      <asp:Button ID="submitAddDishFormBtn" runat="server" Text="Submit image" OnClick="submit_dish_data_toDB" />
     
</asp:panel>

   

</asp:Content>