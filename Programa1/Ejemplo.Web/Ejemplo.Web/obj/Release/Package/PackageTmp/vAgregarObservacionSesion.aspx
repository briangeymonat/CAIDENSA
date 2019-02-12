<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vAgregarObservacionSesion.aspx.cs" Inherits="Ejemplo.Web.vAgregarObservacionSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    
    <link href="~/Style/Style1.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function cerrar() {
            window.close();
        }
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
    <title>Agregar observación a la sesión</title>
</head>
<body onload="setTimeout('cerrar()', 420000)">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Fecha: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblFecha" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="    Hora de inicio: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblHoraInicio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="    Hora de fin: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblHoraFin" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="   Tipo de sesión: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblTipoSesion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Localidad: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblLocalidad" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="vertical-align:top;">
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Beneficiarios" Font-Bold="true"></asp:Label>
                        <asp:GridView ID="grdBeneficiarios" runat="server" Enabled="False" OnRowCreated="grdBeneficiarios_RowCreated">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Especialistas" Font-Bold="true"></asp:Label>
                        <asp:GridView ID="grdESpecialistas" runat="server" Enabled="False" OnRowCreated="grdESpecialistas_RowCreated">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <table >
                            <tr style="vertical-align:top;">
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Comentario:" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr style="vertical-align:top;">
                                <td>
                                    <asp:Label ID="lblComentario" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;">
                        <asp:Label ID="lblAgregarObservacion" runat="server" Text="Agregar observación sobre la sesión dada:" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="vertical-align:top;" colspan="2">
                        <asp:TextBox ID="txtObservacionSesion" runat="server" TextMode="MultiLine" Width="329px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" />
                        <asp:Button ID="btnDescartar" runat="server" Text="Descartar sin realizar la observación" OnClick="btnDescartar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnRealizar" runat="server" Text="Realizar observación" OnClick="btnRealizar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
