<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vTareas.aspx.cs" Inherits="Ejemplo.Web.vTareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Tareas
    </h3>
    <asp:Panel ID="PanelTaerasAdministrativas" runat="server">
        <h4>Tareas administrativas</h4>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Sesiones sin marcar asistencia:"></asp:Label>
                    <asp:GridView ID="grdSesionesPasadasDelDia" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesPasadasDelDia_SelectedIndexChanging" OnRowCreated="grdSesionesPasadasDelDia_RowCreated"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Beneficiarios que tienen planes cercanos al vencimiento:"></asp:Label>
                    <asp:GridView ID="grdPlanesPorVencerse" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True" OnRowCreated="grdPlanesPorVencerse_RowCreated" OnSelectedIndexChanging="grdPlanesPorVencerse_SelectedIndexChanging"></asp:GridView>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" Text="Especialistas con informes pendientes:"></asp:Label>
                    <asp:GridView ID="grdEspecialistasConInformesPendientes" runat="server" OnRowCreated="grdEspecialistasConInformesPendientes_RowCreated" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Beneficiarios que el plan no tiene fecha de vencimiento:"></asp:Label>
                    <asp:GridView ID="grdBeneficiariosConPlanSinFechaVencimiento" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdBeneficiariosConPlanSinFechaVencimiento_RowCreated" OnSelectedIndexChanging="grdBeneficiariosConPlanSinFechaVencimiento_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelTaerasEspecialistas" runat="server">
        <h4>Tareas de especialistas</h4>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Sesiones del día:"></asp:Label>
                    <asp:GridView ID="grdSesionesDelDia" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesDelDia_SelectedIndexChanging" ShowHeaderWhenEmpty="True" OnRowCreated="grdSesionesDelDia_RowCreated"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Sesiones para realizar observaciones:"></asp:Label>
                    <asp:GridView ID="grdObservacionesDeSesiones" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdObservacionesDeSesiones_SelectedIndexChanging" ShowHeaderWhenEmpty="True" OnRowCreated="grdObservacionesDeSesiones_RowCreated"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Informes pendientes:"></asp:Label>
                    <asp:GridView ID="grdInformesPendientes" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdInformesPendientes_RowCreated" OnSelectedIndexChanging="grdInformesPendientes_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Informes en proceso:"></asp:Label>
                    <asp:GridView ID="grdInformesEnProceso" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdInformesEnProceso_RowCreated" OnSelectedIndexChanging="grdInformesEnProceso_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Sesiones con observaciones realizadas:"></asp:Label>
                    <asp:GridView ID="grdSesionesObservacionesRealizadas" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesObservacionesRealizadas_SelectedIndexChanging" OnRowCreated="grdSesionesObservacionesRealizadas_RowCreated"></asp:GridView>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Filtros:"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Beneficiario:"></asp:Label>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBeneficiario" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Rango de fechas:"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Desde:"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Hasta:"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAplicar" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
