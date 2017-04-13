<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TesteDeBanco2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="LabelEmail" runat="server" Text="E-mail: "></asp:Label>
        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
    <br /><br />
        <asp:Label ID="LabelPassword" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
    <br /><br />
    <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />
        </div>
    
</asp:Content>
