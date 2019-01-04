<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vTareas.aspx.cs" Inherits="Ejemplo.Web.vTareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Tareas
    </h3>
    <asp:Panel ID="PanelTaerasAdministrativas" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Sesiones que pasaron del día:"></asp:Label>
                    <asp:GridView ID="grdSesionesPasadasDelDia" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesPasadasDelDia_SelectedIndexChanging"></asp:GridView>
                    <asp:Button ID="btnDetallesSesion" runat="server" Text="Ver detalles de la sesion" OnClick="btnDetallesSesion_Click" />
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Beneficiarios que tienen planes cercanos al vencimiento:" ></asp:Label>
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
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Sesiones del día:"></asp:Label>
                    <asp:GridView ID="grdSesionesDelDia" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesDelDia_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Observaciones de sesiones:"></asp:Label>
                    <asp:GridView ID="grdObservacionesDeSesiones" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdObservacionesDeSesiones_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Informes pendientes:"></asp:Label>
                    <asp:GridView ID="grdInformesPendientes" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdInformesPendientes_RowCreated" OnSelectedIndexChanging="grdInformesPendientes_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Informes en proceso:"></asp:Label>
                    <asp:GridView ID="grdInformesEnProceso" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdInformesEnProceso_RowCreated" OnSelectedIndexChanging="grdInformesEnProceso_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Informes terminados:"></asp:Label>
                    <asp:GridView ID="grdInformesTerminados" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdInformesTerminados_RowCreated" OnSelectedIndexChanging="grdInformesTerminados_SelectedIndexChanging" ShowHeaderWhenEmpty="True"></asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
