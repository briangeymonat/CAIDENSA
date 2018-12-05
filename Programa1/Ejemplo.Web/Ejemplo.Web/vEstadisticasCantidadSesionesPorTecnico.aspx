<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEstadisticasCantidadSesionesPorTecnico.aspx.cs" Inherits="Ejemplo.Web.vEstadisticasCantidadSesionesPorTecnico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Cantidad de sesiones de técnicos según tipo de sesión
    </h3>
    <table>
        <tr>
            <td>
                <asp:TextBox ID="txtBuscarEspecialista" runat="server" PlaceHolder="Buscar técnico"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdTecnicosCantidadSesion" runat="server"></asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
