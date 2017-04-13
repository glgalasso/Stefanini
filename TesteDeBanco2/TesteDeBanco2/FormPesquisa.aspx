<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPesquisa.aspx.cs" Inherits="TesteDeBanco2.FormPesquisa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="formpesquisa" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <div>
            <asp:Label ID="LabelName" runat="server" Text="Name:  "></asp:Label>
            <asp:TextBox ID="Name" runat="server" Width="220px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;       
            <asp:Label ID="LabelGender" runat="server" Text="Gender:  "></asp:Label>
            <asp:DropDownList ID="Gender" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
            <br /><br />
            <asp:Label ID="LabelCity" runat="server" Text="City:  "></asp:Label>
            <asp:DropDownList ID="City" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LabelRegion" runat="server" Text="Region:  "></asp:Label>
            <asp:DropDownList ID="Region" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Clear" runat="server" Text="Clear Fields" OnClick="Clear_Click" />
            <br /><br />
            <asp:Label ID="LabelLastPurchase" runat="server" Text="Last Purchase:  "></asp:Label>
            <asp:TextBox ID="Lastpurchaseini" runat="server" class="X"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="Lastpurchaseini_CalendarExtender" runat="server" BehaviorID="Lastpurchaseini_CalendarExtender" PopupButtonID="Calendar1" TargetControlID="Lastpurchaseini"></ajaxToolkit:CalendarExtender>
            <asp:ImageButton ID="Calendar1" runat="server" ImageUrl="~/img/calendar.png" ImageAlign="Middle" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LabelUntil" runat="server" Text="until  "></asp:Label>
            <asp:TextBox ID="Lastpurchasefim" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="Lastpurchasefim_CalendarExtender" runat="server" BehaviorID="Lastpurchasefim_CalendarExtender" PopupButtonID="Calendar2" TargetControlID="Lastpurchasefim"></ajaxToolkit:CalendarExtender>
            <asp:ImageButton ID="Calendar2" runat="server" ImageUrl="~/img/calendar.png" ImageAlign="Middle" />
            <br /><br />
            <asp:Label ID="LabelClassification" runat="server" Text="Classification:  "></asp:Label>
            <asp:DropDownList ID="Classification" runat="server"></asp:DropDownList>
            
            <asp:Panel ID="pnlSeller" runat="server" style="display: inline; padding-left: 14px" Visible="false">
            <asp:Label ID="LabelSeller" runat="server" Text="Seller:  "></asp:Label>
            <asp:DropDownList ID="Seller" runat="server"></asp:DropDownList>
                </asp:Panel>
            
            <br /><br />
            <asp:GridView ID="Result" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
