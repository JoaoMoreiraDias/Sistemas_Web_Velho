<%@ Page Title="Continue" Language="VB" AutoEventWireup="false" CodeFile="cart2.aspx.vb" Inherits="cart2" MasterPageFile="~/Site.Master"  %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Label ID="errorLbl" runat="server" />
    Discount Code
    <asp:TextBox ID="discountcodeTxtbox" runat="server" /><br />

    <asp:Button ID="deliveruseraddressBtn" runat="server"  Text="Deliver to your address" OnClick="show_submitbnt"/>
    <asp:Button ID="delivertoorheraddressBtn" runat="server" text="Deliver to other address" OnClick="showadditional_address" /><br />
    <asp:Label ID="otheraddressLbl" runat="server" Text="Input other address" visible="false" /> <br />
    <asp:TextBox ID="otheraddressTxtBox" Visible="false" runat="server" TextMode="MultiLine" /><br />

    <asp:Button ID="finishorderBtn" runat="server" Text="Finish order" Visible="false" OnClick="finishOrder" />


    
</asp:Content>