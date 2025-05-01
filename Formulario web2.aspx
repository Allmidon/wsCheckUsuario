<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="Formulario web2.aspx.cs" Inherits="wsCheckUsuario.Formulario_web2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">    
    <br />
    <asp:Label ID="Label1" runat="server" Text="Gestión de Usuarios" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"></asp:Label>
    <br /><br />

    <table width="70%" border="0">
        <tr>
            <td width="30%">

                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="Small" Text="Clave:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox1" runat="server" MaxLength="3" Width="50px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td width="30%">

                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="Small" Text="Nombre:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox2" runat="server" MaxLength="150" Width="300px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td width="30%">

                <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="Small" Text="Apellido Paterno:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox3" runat="server" MaxLength="40" Width="300px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td width="30%">

                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="Small" Text="Apellido Materno:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox4" runat="server" MaxLength="40" Width="300px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td width="30%">

                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="Small" Text="Usuario:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox5" runat="server" MaxLength="8" Width="297px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td width="30%">

                <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="Small" Text="Contraseña:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox6" runat="server" MaxLength="8" Width="297px"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td width="30%">

                <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="Small" Text="Ruta Foto:"></asp:Label>

            </td>
            <td width="70%">

                <asp:TextBox ID="TextBox7" runat="server" MaxLength="30" Width="294px"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td width="30%">

                <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="Small" Text="Tipo Usuario:"></asp:Label>

            </td>
            <td width="70%">


                <asp:DropDownList ID="DropDownList1" runat="server" Width="166px">
                </asp:DropDownList>


            </td>
        </tr>

        <tr>
            <td colspan="2" align="middle">
                <br />
                <asp:Button ID="Button1" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="Button1_Click" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" Text="Modificar" CssClass="btn btn-warning" />
                &nbsp;
                <asp:Button ID="Button3" runat="server" Text="Eliminar"  CssClass="btn btn-danger" />
                <br />
            </td>
        </tr>
    </table>

    <br /><br />
</div>

</asp:Content>