<%@ Page Title="Login" Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" MasterPageFile="~/Site.Master"  %>
   
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Label ID="errorLbl" runat="server" />
<asp:LinkButton ID="pleaseRegisterLinkLbl" runat="server" />

<table>
    
    <tr>
        <td>
            Nick
        </td>
        <td>
            <asp:TextBox ID="LoginNameTxtbox" runat="server"/><br />
        </td>
    </tr>
    <tr>
        <td>
            Password
        </td>
        <td>
            <asp:TextBox  ID="PasswordTxtbox" runat="server" TextMode="Password" /><br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="check_login"/> <br />
        </td>
    </tr>
</table>
    
    
</asp:Content>