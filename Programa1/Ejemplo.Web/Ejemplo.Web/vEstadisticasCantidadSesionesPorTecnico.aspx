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
                
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Mes: "></asp:Label>
                <asp:DropDownList ID="ddlMeses" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Año: "></asp:Label>
                <asp:DropDownList ID="ddlAños" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAños_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:TextBox ID="txtBuscarEspecialista" runat="server" PlaceHolder="Buscar técnico" OnTextChanged="txtBuscarEspecialista_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdTecnicosCantidadSesion" runat="server" ShowHeaderWhenEmpty="true"></asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
