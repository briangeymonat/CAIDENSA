<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vLogin.aspx.cs" Inherits="Ejemplo.Web.vLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
    <h2>
        Login
    </h2>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell><br />
                        <asp:Label ID="Label1" runat="server" Text="Nickname:"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell><br />
                        <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Contraseña:"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>                        
                        <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center"><br />
                        <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar sesión" OnClick="btnIniciarSesion_Click" style="height: 26px"></asp:Button>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="vNuevaContraseña.aspx">¿No tienes contraseña aún?</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            </center>
        </div>
    </form>
</body>
</html>
