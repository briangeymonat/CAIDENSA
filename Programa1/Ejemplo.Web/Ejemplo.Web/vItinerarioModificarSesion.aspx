<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vItinerarioModificarSesion.aspx.cs" Inherits="Ejemplo.Web.vItinerarioModificarConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Modificar sesión de itinerario
    </h3>
    <table>
        <tr>
            <td>
                <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Tipo de sesión:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoSesion" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Día:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDias" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Localidad:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Juan Lacaze"></asp:ListItem>
                    <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Desde:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDesde" runat="server" TextMode="Number"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtHasta" runat="server" TextMode="Number"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Beneficiarios"></asp:Label>
            </td>
            <td colspan ="3">
                <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                        </td>
                        <td>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="grdBeneficiarios" runat="server"></asp:GridView>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAgregarBeneficiario" runat="server" Text="Agregar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnQuitarBeneficiario" runat="server" Text="Quitar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </td>
            <td>
                <asp:GridView ID="grdBeneficiariosCargados" runat="server"></asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Especialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEspecialidades" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Especialistas"></asp:Label>
                            <asp:GridView ID="grdTodosEspecialistas" runat="server"></asp:GridView>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAgregarEspecialista" runat="server" Text="Agregar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnQuitarEspecialista" runat="server" Text="Quitar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Especialistas agregados"></asp:Label>
                            <asp:GridView ID="grdEspecialistasAgregados" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Comentario:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Width="100%" Height="130px" ></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
