<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEstadisticasBeneficiariosPorEdad.aspx.cs" Inherits="Ejemplo.Web.vEstadisticasBeneficiariosPorEdad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 80px;">Cantidad de beneficiarios por rango de edad 
    </h2>
    <asp:Table runat="server">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Style="padding-left: 80px;">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="Label2" runat="server" Text="Cantidad de beneficiarios que su edad esta dentro del rango definido: "></asp:Label>
                            <asp:Label ID="lblCantidadBeneficiarios" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Beneficiarios:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="pnlBeneficiario" runat="server" ScrollBars="Vertical" Height="500px">
                                <asp:GridView ID="grdBeneficiarios" runat="server" OnRowCreated="grdBeneficiarios_RowCreated">
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
                            </asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell Style="padding-left: 80px; padding-top: 40px">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="Label3" runat="server" Text="Rango de edad:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Desde:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number" Width="50px" OnTextChanged="txtDesde_TextChanged"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number" Width="50px" OnTextChanged="txtHasta_TextChanged"></asp:TextBox>
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
</asp:Content>
