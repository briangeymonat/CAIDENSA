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
                <asp:GridView ID="grdTecnicosCantidadSesion" runat="server" ShowHeaderWhenEmpty="true">
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
    </table>
</asp:Content>
