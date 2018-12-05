<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEstadisticasDiagnosticos.aspx.cs" Inherits="Ejemplo.Web.vEstadisticasDiagnosticos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <h3>Duración promedio de tratamiento según diagnóstico
                </h3>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBuscarDiagnostico" runat="server" PlaceHolder="Buscar diagnóstico"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdDiagnosticoDuracion" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <h3>
                    Cantidad de beneficiarios por año según diagnóstico
                </h3>
                <asp:Chart ID="Chart1" runat="server">
        <Series>
            <asp:Series Name="Series1"></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
            </td>
        </tr>
    </table>
</asp:Content>
