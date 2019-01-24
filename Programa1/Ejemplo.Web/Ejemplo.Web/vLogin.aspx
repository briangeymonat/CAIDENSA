<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vLogin.aspx.cs" Inherits="Ejemplo.Web.vLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script type="text/javascript">
        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
    <h3>
        Login
    </h3>
            <table>
                <tr>
                    <td>
<asp:Label ID="Label1" runat="server" Text="Nickname:"></asp:Label>
                    </td>
                    <td>
<asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
<asp:Label ID="Label2" runat="server" Text="Contraseña:"></asp:Label>

                    </td>
                    <td>
                        
                        <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar sesión" OnClick="btnIniciarSesion_Click" style="height: 26px"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="vNuevaContraseña.aspx">¿No tienes contraseña aún?</asp:HyperLink>
                    </td>
                </tr>
            </table>
            </center>
        </div>
    </form>
</body>
</html>
