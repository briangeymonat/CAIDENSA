<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vAsistencias.aspx.cs" Inherits="Ejemplo.Web.vAsistencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    
    <script type="text/javascript">
        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left:40px" >Asistencias mensuales:
    </h2>
    <asp:Table runat="server">
        <asp:TableRow VerticalAlign="Top" HorizontalAlign="Center">
            <asp:TableCell Width="80%" Style="padding-left: 30px;">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" PlaceHolder="Buscar por CI, Nombre o Apellido" OnTextChanged="txtBuscarBeneficiarios_TextChanged" Width="30%" AutoPostBack="true"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Panel ID="Panel1" runat="server">
                            </asp:Panel>
                            <asp:GridView ID="grdAsistencias" runat="server" ShowHeaderWhenEmpty="true">
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
                            <br />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell Width="20%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell> <asp:Label ID="Label1" runat="server" Text="Año: "></asp:Label>
                            <asp:DropDownList ID="ddlAños" runat="server" OnSelectedIndexChanged="ddlAños_SelectedIndexChanged" AutoPostBack="true" Width="80px"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell><asp:Label ID="Label6" runat="server" Text="Mes: "></asp:Label>
                            <asp:DropDownList ID="ddlMeses" runat="server" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged" AutoPostBack="true" Width="80px"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <br />
                            <asp:Label ID="Label9" runat="server" Text="Filtros:" Font-Bold="true" Font-Size="12"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <br />

                            <asp:Label ID="Label2" runat="server" Text="Localidad:"  Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbJuanLacaze" runat="server" Text="Juan Lacaze" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbNuevaHelvecia" runat="server" Text="Nueva Helvecia" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <br />
                            <asp:Label ID="Label3" runat="server" Text="Plan:"  Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbASSE" runat="server" Text="ASSE" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbAYEX" runat="server" Text="AYEX" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbCAMEC" runat="server" Text="CAMEC" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbCirculocatolico" runat="server" Text="Círculo Católico" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbMIDES" runat="server" Text="MIDES" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbParticular" runat="server" Text="Particular" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPolicial" runat="server" Text="Policial" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Rango de edad:"  Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Desde: "></asp:Label>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="Hasta: "></asp:Label>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <br />
                            <asp:Label ID="Label8" runat="server" Text="Especialidades a las que asiste:"  Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbFisioterapeuta" runat="server" Text="Fisioterapeuta" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbFonoaudiologo" runat="server" Text="Fonoaudiólogo" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPedagogo" runat="server" Text="Pedagogo" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPsicologo" runat="server" Text="Psicólogo" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPsicomotricista" runat="server" Text="Psicomotricista" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPsicopedagogo" runat="server" Text="Psicopedagogo" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" OnClick="btnAplicarFiltros_Click" />
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" OnClick="btnLimpiarFiltros_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
