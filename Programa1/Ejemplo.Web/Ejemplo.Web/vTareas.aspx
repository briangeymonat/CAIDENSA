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
                    <asp:GridView ID="grdSesionesPasadasDelDia" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True"
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnSelectedIndexChanging="grdSesionesPasadasDelDia_SelectedIndexChanging" OnRowCreated="grdSesionesPasadasDelDia_RowCreated">
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
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Beneficiarios que tienen planes cercanos al vencimiento:"></asp:Label>
                    <asp:GridView ID="grdPlanesPorVencerse" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True"
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnRowCreated="grdPlanesPorVencerse_RowCreated" OnSelectedIndexChanging="grdPlanesPorVencerse_SelectedIndexChanging">
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
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" Text="Especialistas con informes pendientes:"></asp:Label>
                    <asp:GridView ID="grdEspecialistasConInformesPendientes" runat="server" ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowCreated="grdEspecialistasConInformesPendientes_RowCreated" ShowHeaderWhenEmpty="True">
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
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Beneficiarios que el plan no tiene fecha de vencimiento:"></asp:Label>
                    <asp:GridView ID="grdBeneficiariosConPlanSinFechaVencimiento" runat="server" AutoGenerateSelectButton="True" 
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowCreated="grdBeneficiariosConPlanSinFechaVencimiento_RowCreated" OnSelectedIndexChanging="grdBeneficiariosConPlanSinFechaVencimiento_SelectedIndexChanging" ShowHeaderWhenEmpty="True"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
    </asp:Panel>
    <asp:Panel ID="PanelTaerasEspecialistas" runat="server">
        <h4>Tareas de especialistas</h4>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Sesiones del día:"></asp:Label>
                    <asp:GridView ID="grdSesionesDelDia" runat="server" AutoGenerateSelectButton="True" 
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnSelectedIndexChanging="grdSesionesDelDia_SelectedIndexChanging" ShowHeaderWhenEmpty="True" OnRowCreated="grdSesionesDelDia_RowCreated">
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
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Sesiones para realizar observaciones:"></asp:Label>
                    <asp:GridView ID="grdObservacionesDeSesiones" runat="server" AutoGenerateSelectButton="True"
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnSelectedIndexChanging="grdObservacionesDeSesiones_SelectedIndexChanging" ShowHeaderWhenEmpty="True" OnRowCreated="grdObservacionesDeSesiones_RowCreated">
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
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Informes pendientes:"></asp:Label>
                    <asp:GridView ID="grdInformesPendientes" runat="server" AutoGenerateSelectButton="True" 
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                        OnRowCreated="grdInformesPendientes_RowCreated" OnSelectedIndexChanging="grdInformesPendientes_SelectedIndexChanging" ShowHeaderWhenEmpty="True">

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
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Informes en proceso:"></asp:Label>
                    <asp:GridView ID="grdInformesEnProceso" runat="server" AutoGenerateSelectButton="True"
                        ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                         OnRowCreated="grdInformesEnProceso_RowCreated" OnSelectedIndexChanging="grdInformesEnProceso_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Sesiones con observaciones realizadas:"></asp:Label>
                    <asp:GridView ID="grdSesionesObservacionesRealizadas" runat="server" ShowHeaderWhenEmpty="True"
                         ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                         AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesObservacionesRealizadas_SelectedIndexChanging" OnRowCreated="grdSesionesObservacionesRealizadas_RowCreated">
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
