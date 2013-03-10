<%@ Page Title="Cart" Language="VB" AutoEventWireup="false" CodeFile="Cart.aspx.vb" Inherits="_Default" MasterPageFile="~/Site.Master" %>

    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Label ID="errorLbl" runat="server" />
<asp:Label ID="loginname" runat="server" />
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true"/>
    <asp:Panel ID="LoginPnl" runat="server" OnLoad="chechlogin" >
    </asp:Panel>
    <asp:panel ID="cart" runat="server" OnLoad="show_cart_items">
        <%--<asp:Label ID="lblCookie" runat="server" Text=":" />
        <asp:TextBox ID="cooky" runat="server" TextMode="MultiLine" />
        <asp:Button ID="redco" runat="server" Text="Read shit" OnClick="ReadCookie" />--%>
        


    </asp:panel>

    
    
</asp:Content>