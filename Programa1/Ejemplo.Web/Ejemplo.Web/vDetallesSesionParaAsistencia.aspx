<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vDetallesSesionParaAsistencia.aspx.cs" Inherits="Ejemplo.Web.vDetallesSesionParaAsistencia" %>

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
                        <asp:Label ID="Label5" runat="server" Text="Fecha::"></asp:Label>
                        <asp:Label ID="lblFecha" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="    Hora de inicio: "></asp:Label>
                                    <asp:Label ID="lblHoraInicio" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <asp:Label ID="Label9" runat="server" Text="    Hora de fin: "></asp:Label>
                                    <asp:Label ID="lblHoraFin" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="   Tipo de sesión: "></asp:Label>
                                    <asp:Label ID="lblTipoSesion" runat="server"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                    <asp:Label ID="Label8" runat="server" Text="Localidad: "></asp:Label>
                                    <asp:Label ID="lblLocalidad" runat="server"></asp:Label>
                                </td>
                                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Especialistas: "></asp:Label>
                        <asp:Label ID="lblEspecialistas" runat="server"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Comentario"></asp:Label>
                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Beneficiarios:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario1" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario2" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario2" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario3" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario3" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario4" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario4" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario5" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario5" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario6" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario6" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario7" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario7" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreBeneficiario8" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblBeneficiario8" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Asistió" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No asistió"></asp:ListItem>
                            <asp:ListItem Text="Reprogramada"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td colspan="4">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
