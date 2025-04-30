<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="Formulario_web1.aspx.cs" Inherits="wsCheckUsuario.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="App_Themes/Principal/Principal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Reporte de Usuarios Registrados" CssClass="tituloContenido"></asp:Label>
    <br /> <br />

    <!-- El TextBox para ingresar el filtro -->
    <asp:TextBox ID="TextBox1" runat="server" placeholder="Buscar por nombre o usuario"></asp:TextBox>
    
    <!-- El ImageButton que ejecuta el filtrado -->
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/icon_logalum.GIF" style="height: 16px" OnClick="ImageButton1_Click" />
    
    <!-- El GridView para mostrar los resultados -->
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="5">
        <AlternatingRowStyle BackColor="#CCFFFF" Font-Names="Arial" Font-Size="Small" />
        <HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Candara" Font-Size="Medium" ForeColor="White" />
        <PagerStyle BackColor="#000066" Font-Bold="True" Font-Names="Candara" Font-Size="Medium" ForeColor="White" />
        <RowStyle BackColor="#99CCFF" Font-Names="Arial" Font-Size="Small" ForeColor="Black" />
    </asp:GridView>
</asp:Content>
