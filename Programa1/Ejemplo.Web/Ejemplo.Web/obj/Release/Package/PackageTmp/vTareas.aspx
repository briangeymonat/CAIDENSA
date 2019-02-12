<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vTareas.aspx.cs" Inherits="Ejemplo.Web.vTareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelTaerasAdministrativas" runat="server">
        <h1 style="padding-left: 10px">Tareas administrativas</h1>
        <asp:Table runat="server" Style="margin-left: 10px">
            <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label6" runat="server" Text="Sesiones sin marcar asistencia:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label5" runat="server" Text="Beneficiarios que tienen planes cercanos al vencimiento:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label8" runat="server" Text="Beneficiarios que el plan no tiene fecha de vencimiento:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label7" runat="server" Text="Especialistas con informes pendientes:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdSesionesPasadasDelDia" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnSelectedIndexChanging="grdSesionesPasadasDelDia_SelectedIndexChanging" OnRowCreated="grdSesionesPasadasDelDia_RowCreated" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdPlanesPorVencerse" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnRowCreated="grdPlanesPorVencerse_RowCreated" OnSelectedIndexChanging="grdPlanesPorVencerse_SelectedIndexChanging" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdBeneficiariosConPlanSinFechaVencimiento" runat="server" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnRowCreated="grdBeneficiariosConPlanSinFechaVencimiento_RowCreated" OnSelectedIndexChanging="grdBeneficiariosConPlanSinFechaVencimiento_SelectedIndexChanging" ShowHeaderWhenEmpty="True" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdEspecialistasConInformesPendientes" runat="server" ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnRowCreated="grdEspecialistasConInformesPendientes_RowCreated" ShowHeaderWhenEmpty="True" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>

                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <asp:Panel ID="PanelTaerasEspecialistas" runat="server">
        <h1 style="padding-left: 10px">Tareas de especialistas</h1>
        <asp:Table runat="server" Style="margin-left: 10px">
            <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Sesiones del día:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label3" runat="server" Text="Informes pendientes:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label4" runat="server" Text="Informes en proceso:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel5" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdSesionesDelDia" runat="server" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnSelectedIndexChanging="grdSesionesDelDia_SelectedIndexChanging" ShowHeaderWhenEmpty="True" OnRowCreated="grdSesionesDelDia_RowCreated" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>                
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel7" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdInformesPendientes" runat="server" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnRowCreated="grdInformesPendientes_RowCreated" OnSelectedIndexChanging="grdInformesPendientes_SelectedIndexChanging" ShowHeaderWhenEmpty="True" Width="100%">

                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel8" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdInformesEnProceso" runat="server" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnRowCreated="grdInformesEnProceso_RowCreated" OnSelectedIndexChanging="grdInformesEnProceso_SelectedIndexChanging" ShowHeaderWhenEmpty="True" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label9" runat="server" Text="Sesiones para realizar observaciones:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="Label10" runat="server" Text="Sesiones con observaciones realizadas:" Font-Bold="true" Font-Size="11"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel6" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdObservacionesDeSesiones" runat="server" AutoGenerateSelectButton="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            OnSelectedIndexChanging="grdObservacionesDeSesiones_SelectedIndexChanging" ShowHeaderWhenEmpty="True" OnRowCreated="grdObservacionesDeSesiones_RowCreated" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Panel ID="Panel9" runat="server" ScrollBars="Vertical" Height="250px">
                        <asp:GridView ID="grdSesionesObservacionesRealizadas" runat="server" ShowHeaderWhenEmpty="True"
                            ViewStateMode="Enabled" CellPadding="2" ForeColor="#333333" GridLines="Both"
                            AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdSesionesObservacionesRealizadas_SelectedIndexChanging" OnRowCreated="grdSesionesObservacionesRealizadas_RowCreated" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label11" runat="server" Text="Filtros:" Font-Bold="true" Font-Size="11"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <br />
                                <asp:Label ID="Label12" runat="server" Text="Beneficiario:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <br />
                                <asp:DropDownList ID="ddlBeneficiario" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <br />
                                <asp:Label ID="Label13" runat="server" Text="Rango de fechas:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <br />
                                <asp:Label ID="Label14" runat="server" Text="Desde:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <br />
                                <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label15" runat="server" Text="Hasta:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                <br />
                                <asp:Button ID="btnAplicar" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" />
                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>
