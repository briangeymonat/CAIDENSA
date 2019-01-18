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
                            <asp:GridView ID="grdDiagnosticoDuracion" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>

        <tr>
            <td>

                <table>
                    <tr>
                        <td>
                            <h3>Cantidad de beneficiarios por diagnóstico según año
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlAños" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAños_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Chart ID="Chart2" runat="server">
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
            </td>
        </tr>
        <tr>
            <td>

                <table>
                    <tr>
                        <td>
                            <h3>Cantidad de beneficiarios por año según diagnóstico
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlDiagnosticos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiagnosticos_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
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
            </td>
        </tr>
    </table>
</asp:Content>
