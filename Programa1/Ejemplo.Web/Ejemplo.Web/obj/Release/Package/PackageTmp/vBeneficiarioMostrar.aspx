<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioMostrar.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioMostrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 50px">Mostrar beneficiarios:
    </h2>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell Style="width: 300px; padding-left: 50px;">
                <br />
                <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" Width="100%" PlaceHolder="Buscar por CI, Nombre o Apellido" TextMode="Search" OnTextChanged="txtBuscarBeneficiarios_TextChanged" AutoPostBack="true">
                </asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Style="padding-left: 50px;">
                        <br />
                        <asp:Panel ID="PanelBeneficiarios" runat="server" ScrollBars="Vertical" Height="750px">
                            <asp:GridView ID="grdBeneficiarios" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateSelectButton="true" OnSelectedIndexChanging="grdBeneficiarios_SelectedIndexChanging" OnRowCreated="grdBeneficiarios_RowCreated"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            <asp:TableCell>
                <asp:Table runat="server" Style="width: 250px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="lblFiltros" runat="server" Text="Filtros:" Style="margin-left: 50px;" Font-Bold="true" Font-Size="12"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbActivos" runat="server" Text="Activos" Style="margin-left: 50px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPasivos" runat="server" Text="Pasivos" Style="margin-left: 50px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br>
                            <asp:Label ID="lblLocalidad" runat="server" Text="Localidad:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbJuanLacaze" runat="server" Text="Juan Lacaze" Style="margin-left: 50px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbNuevaHelvecia" runat="server" Text="Nueva Helvecia" Style="margin-left: 50px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br>
                            <asp:Label ID="lblSexo" runat="server" Text="Sexo:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="cblSexo" runat="server" Style="margin-left: 50px">
                                <asp:ListItem Text="Masculino"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br>
                            <asp:Label ID="lblPlan" runat="server" Text="Plan:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="cblPlan" runat="server" Style="margin-left: 50px">
                                <asp:ListItem Text="ASSE"></asp:ListItem>
                                <asp:ListItem Text="AYEX"></asp:ListItem>
                                <asp:ListItem Text="CAMEC"></asp:ListItem>
                                <asp:ListItem Text="Círculo Católico"></asp:ListItem>
                                <asp:ListItem Text="MIDES"></asp:ListItem>
                                <asp:ListItem Text="Particular"></asp:ListItem>
                                <asp:ListItem Text="Policial"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br>
                            <asp:Label ID="lblRangoDeEdad" runat="server" Text="Rango de edad:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Desde:" Style="margin-left: 50px"></asp:Label>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="Hasta:" Style="margin-left: 50px"></asp:Label>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br>
                            <asp:Label ID="lblEspecialidades" runat="server" Text="Especialidades a las que asiste:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="cblEspecialidad" runat="server" Style="margin-left: 50px">
                                <asp:ListItem Text="Fisioterapia"></asp:ListItem>
                                <asp:ListItem Text="Fonoaudiologia"></asp:ListItem>
                                <asp:ListItem Text="Pedadogia"></asp:ListItem>
                                <asp:ListItem Text="Psicologia"></asp:ListItem>
                                <asp:ListItem Text="Psicomotricidad"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br>
                            <asp:Label ID="lblDiagnostico" runat="server" Text="Diagnóstico:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlDiagnosticos" runat="server" Style="margin-left: 50px"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" OnClick="btnAplicarFiltros_Click" Style="margin-left: 50px" />
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar filtros" OnClick="btnLimpiarFiltros_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
