<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeFile="Home.aspx.vb" Inherits="_Default" %>
    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <script type="text/javascript">
        function setCookie(c_name, value, exdays) {            
            var dropdownname = "MainContent_" + c_name;
            var e = document.getElementById(dropdownname);
            var strUser = e.options[e.selectedIndex].value;
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = "Order" + c_name + "=" + strUser;
            alert("Dish Ordered");
        }
        
 </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   <asp:Panel ID="mainHolderPnl" runat="server" OnLoad="load_all_dish" HorizontalAlign="Center">
   <asp:Label ID="errorLbl" runat="server" Text=""/>
   
   
   
   
   </asp:Panel>
</asp:Content>