 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vUsuarioAgregar.aspx.cs" Inherits="Ejemplo.Web.AgregarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left:30px">Agregar usuario</h2>
    <br />
        <asp:Table runat="server">
            <asp:TableRow > 
                <asp:TableCell style="padding-left:20px" >
                    <asp:Label ID="lblNickname" runat="server" Text="Nickname:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtNickName" runat="server"  Width="160px"></asp:TextBox><asp:Label ID="lblObligatorio1" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtNombres" runat="server" Width="160px"></asp:TextBox><asp:Label ID="lblObligatorio2" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtApellidos" runat="server" Width="160px"></asp:TextBox><asp:Label ID="lblObligatorio3" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtCi" runat="server" TextMode="Number" Width="160px"></asp:TextBox><asp:Label ID="lblObligatorio4" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:" Width="160px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtDomicilio" runat="server" Width="160px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date" Width="160px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblTel" runat="server" Text="Telefono:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtTelefono" runat="server"  TextMode="Number" Width="160px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblEmail" runat="server" Text="Correo electronico:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="160px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" Width="160px"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" Width="160px"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="padding-left:20px">
                    <asp:Label ID="Label1" runat="server" Text="Vínculo:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:RadioButtonList ID="rbTipoDeEmpleado" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Empleado</asp:ListItem>
                        <asp:ListItem>Contratado</asp:ListItem>
                        <asp:ListItem>Socio</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="6" style="padding-left:20px">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" style="padding-left:20px">
                    <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar" OnClick="btnAgregarUsuario_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</asp:Content>
