﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vDetallesSesion.aspx.cs" Inherits="Ejemplo.Web.DetallesSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <link href="~/Style/Style1.css" rel="stylesheet" type="text/css" />
    <title>Detalles de la sesión</title>
       <script type="text/javascript">
           function cerrar() {
               window.close();
           }
        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</head>
<body onload="setTimeout('cerrar()', 420000)">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
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
                                    <asp:Label ID="Label8" runat="server" Text="Localidad: " Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblLocalidad" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="vertical-align:top">
                    <td style="vertical-align:top">
                        <asp:Label ID="Label1" runat="server" Text="Beneficiarios" Font-Bold="true"></asp:Label>
                        <asp:GridView ID="grdBeneficiarios" runat="server" OnRowCreated="grdBeneficiarios_RowCreated">
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
                    <td style="vertical-align:top">
                        <asp:Label ID="Label2" runat="server" Text="Especialistas" Font-Bold="true"></asp:Label>
                        <asp:GridView ID="grdEspecialistas" runat="server" OnRowCreated="grdEspecialistas_RowCreated">
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
                    <td style="vertical-align:top">
                        <asp:Label ID="Label3" runat="server" Text="Comentario: " Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblComentario" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
