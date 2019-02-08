<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vNuevaContraseña.aspx.cs" Inherits="Ejemplo.Web.vNuevaContraseña" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Style/Style1.css" rel="stylesheet" type="text/css" />
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
            <asp:Image ID="Image2" runat="server" Height="80px" ImageUrl="/Img/HeaderLogo.jpg" runat="server" AlternateText="Imagen no disponible" ImageAlign="TextTop" />
            <asp:Image ID="Image3" runat="server" Height="80px" ImageUrl="/Img/HeaderNombre.png" runat="server" AlternateText="Imagen no disponible" ImageAlign="TextTop" />

            <hr style="border: 1px solid black" />

        </div>
        <div>
            <center>
                <h2>
                    Ingreso de nueva contraseña
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
                <asp:TableCell>
                    <asp:Label ID="Label3" runat="server" Text="Repetir contraseña:"></asp:Label>                    
                </asp:TableCell>
                <asp:TableCell>
                        <asp:TextBox ID="txtContrasenaRepetir" runat="server" TextMode="Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right"><br />
                        <asp:Button ID="btnAtras" runat="server" Text="Atras" Style="margin-right:20px" OnClick="btnAtras_Click"></asp:Button>
                        <asp:Button ID="btnConfirmarNuevaContrasena" runat="server" Text="Confirmar nueva contraseña" OnClick="btnConfirmarNuevaContrasena_Click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>    
        </center>
        </div>
    </form>
</body>
</html>
