<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vDetallesSesion.aspx.cs" Inherits="Ejemplo.Web.DetallesSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalles de la sesión</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Beneficiarios"></asp:Label>
                    <asp:ListView ID="ListView1" runat="server"></asp:ListView>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Especialistas"></asp:Label>
                    <asp:ListView ID="ListView2" runat="server"></asp:ListView>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Comentario"></asp:Label>
                    <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Localidad:"></asp:Label>
                    <asp:Label ID="lblLocalidad" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnComentar" runat="server" Text="Comentar" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" style="height: 26px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
