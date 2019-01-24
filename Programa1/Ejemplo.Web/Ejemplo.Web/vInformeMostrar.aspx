<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeMostrar.aspx.cs" Inherits="Ejemplo.Web.vInformeMostrar" %>

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
    <h2 style="padding-left: 50px">Todos los informes
    </h2>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell Style="width: 300px; padding-left: 50px;" runat="server">
                <br />
                <asp:TextBox ID="txtBuscarInforme" runat="server" PlaceHolder="Buscar por Nombre, Apellido o CI de beneficiario" Width="100%">
                </asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Style="padding-left: 50px;">
                        <br />
                        <asp:GridView ID="grdInformes" runat="server" AutoGenerateSelectButton="True"
                            OnRowCreated="grdInformes_RowCreated" OnSelectedIndexChanging="grdInformes_SelectedIndexChanging"
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
                    </asp:TableCell>
            <asp:TableCell>
                <asp:Table runat="server" Style="width: 250px;">
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="lblFiltros" runat="server" Text="Filtros:" Style="margin-left: 50px;" Font-Bold="true" Font-Size="12"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Tipos:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <br />
                            <asp:DropDownList ID="ddlTipos" runat="server" Width="100%"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                        <br />
                            <asp:Label ID="Label2" runat="server" Text="Diagnósticos: " Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                        <br />
                            <asp:DropDownList ID="ddlDiagnosticos" runat="server" Width="100%"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="4">
                        <br />
                            <asp:Label ID="Label3" runat="server" Text="Rango de edad:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label4" runat="server" Text="Desde:" Style="margin-left: 50px"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="4">
                        <br />
                            <asp:Label ID="Label6" runat="server" Text="Rango de fechas:" Style="margin-left: 50px" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="Desde:" Style="padding-left: 50px"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaInicial" runat="server" TextMode="Date"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="Label8" runat="server" Text="Hasta:" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaFinal" runat="server" TextMode="Date"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" >
                        <br />
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" OnClick="btnAplicarFiltros_Click" Style="margin-left: 50px" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
