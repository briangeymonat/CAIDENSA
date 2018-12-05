<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vUsuarioAgregar.aspx.cs" Inherits="Ejemplo.Web.AgregarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Agregar usuario</h3>
    <br />
        <table class="table">
            <tr>
                <td>
                    <asp:Label ID="lblNickname" runat="server" Text="Nickname:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCi" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDomicilio" runat="server"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTel" runat="server" Text="Telefono:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server"  TextMode="Number"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Correo electronico:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoUsuario" runat="server"></asp:DropDownList>
                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server"></asp:DropDownList>
                </td>
                <td>

                </td>
                <td>
                    <asp:RadioButtonList ID="rbTipoDeEmpleado" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Empleado</asp:ListItem>
                        <asp:ListItem>Contratado</asp:ListItem>
                        <asp:ListItem>Socio</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar" OnClick="btnAgregarUsuario_Click"/>
                </td>
            </tr>
        </table>
</asp:Content>
