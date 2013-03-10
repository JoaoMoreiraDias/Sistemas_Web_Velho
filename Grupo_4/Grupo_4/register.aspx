<%@ Page Title="Register" Language="VB" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" MasterPageFile="~/Site.Master" %>
  
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Label ID="errorLbl" runat="server" />
<table border="0px" >
    <tr>
        <td>
            Name        
        </td>
        <td>
            <asp:TextBox id="uNameTxtbox" runat="server" ToolTip="InputName" />
            <td>
            <asp:RequiredFieldValidator ID="uNameTxtboxValidReq" runat="server" ControlToValidate="uNameTxtbox" ForeColor="Red" Font-Size="Larger" Text="*" ErrorMessage="Insert name" CssClass="alerta" Width="10"/>
        <asp:RegularExpressionValidator runat="server" id="uNameTxtboxValid" ControlToValidate="uNameTxtbox" ErrorMessage="Name coulnt not contain other symbols than letters[A-Z], numbers [0-9] and symbols [._] and should be longer than 2" ValidationExpression="([a-zA-Z0-9_\. ]){2,}" />
        
        </td>
        </td>
    </tr>
    <tr>
        <td>
            Nick
        </td>
        <td>
            <asp:TextBox ID="uNickTxtbox" runat="server" />
            <td>
            <asp:RequiredFieldValidator ID="uNickTxtboxValidReq" runat="server" ControlToValidate="uNickTxtbox" ForeColor="Red" Font-Size="Larger" Text="*" ErrorMessage="Insert Nick" CssClass="alert" Width="10"/>
            <asp:RegularExpressionValidator runat="server" id="uNickTxtboxValid" ControlToValidate="uNickTxtbox" ErrorMessage="Nick coulnt not contain other symbols than letters[A-Z], numbers [0-9] and symbols [._] and should be longer than 2" ValidationExpression="([a-zA-Z0-9_\. ]){2,}" />            
            </td>
        </td>
    </tr>
    <tr>
        <td>
        Password:
        </td>
        <td>
            <asp:TextBox ID="uPasswordTxtbox" runat="server" TextMode="Password"  />
            <td>
                <asp:RequiredFieldValidator ID="uPasswordTxtboxValidReq" runat="server" ControlToValidate="uPasswordTxtbox" ForeColor="Red" Font-Size="Larger" Text="*" ErrorMessage="Insert password" CssClass="alert" Width="10" />
            </td>
        </td>
    </tr>
    <tr>
        <td>
            Address:
        </td>
        <td>
            <asp:TextBox ID="uAddressTxtbox" runat="server" TextMode="MultiLine" Height="70"/>
            <td>
                <asp:RequiredFieldValidator ID="uAddressTxtboxValidReq" runat="server" ControlToValidate="uAddressTxtbox" ForeColor="Red" Font-Size="Larger" Text="*" ErrorMessage="Insert address" CssClass="alert" />
         </td>
            </td>
            
    </tr>
    <tr>
        <td>
            <asp:Button ID="uSubmitFormBtn" runat="server" OnClick="check_form" Text="Registrate" /> 
        </td>
    </tr>
    <tr>
        
        <td>&nbsp</td>
        <td>
            <asp:ValidationSummary ID="summaryValidation" runat="server" HeaderText="Please Fix there errors" CssClass="alerta" />
        </td>
    </tr>
</table>
    
</asp:Content>