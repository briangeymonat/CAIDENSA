<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeMostrar.aspx.cs" Inherits="Ejemplo.Web.vInformeMostrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Todos los informes</h3>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBuscarInforme" runat="server" PlaceHolder="Buscar por Nombre, Apellido o CI de beneficiario"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdInformes" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdInformes_RowCreated" OnSelectedIndexChanging="grdInformes_SelectedIndexChanging"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <h5>Filtros:</h5>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Tipos:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipos" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Diagnósticos:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDiagnosticos" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Rango de edad:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Desde:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Rango de fechas:"></asp:Label>
                         </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Desde:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaInicial" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Hasta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaFinal" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" OnClick="btnAplicarFiltros_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
