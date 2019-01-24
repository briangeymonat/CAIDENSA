<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vNuevaContraseña.aspx.cs" Inherits="Ejemplo.Web.vNuevaContraseña" %>

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
                    Ingreso de nueva contraseña
                </h3>
        <table>
            <tr>
                <td>
<asp:Label ID="Label1" runat="server" Text="NickName:"></asp:Label>
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
                <td>
<asp:Label ID="Label3" runat="server" Text="Repetir contraseña:"></asp:Label>                    
                </td>
                <td>
<asp:TextBox ID="txtContrasenaRepetir" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
<asp:Button ID="btnConfirmarNuevaContrasena" runat="server" Text="Confirmar nueva contraseña" OnClick="btnConfirmarNuevaContrasena_Click"></asp:Button>
                </td>
            </tr>
        </table>    
        </center>
        </div>
    </form>
</body>
</html>
