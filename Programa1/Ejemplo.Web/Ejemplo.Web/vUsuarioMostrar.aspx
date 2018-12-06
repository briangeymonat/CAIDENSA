<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vUsuarioMostrar.aspx.cs" Inherits="Ejemplo.Web.vUsuarioMostrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Usuarios
    </h3>
    <table class="table">
        <tr>
            <td style="width: 300px">
                <asp:TextBox ID="txtBuscarBeneficiario" runat="server" Width="100%" PlaceHolder="Buscar por CI, Nombre o Apellido" AutoPostBack="True" OnTextChanged="txtBuscarBeneficiario_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblFiltros" runat="server" Text="Filtros:" Style="margin-left: 50px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblUsuariosActivos" runat="server" Text="Usuarios activos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelUsuariosActivos" runat="server">
                <asp:GridView ID="grdUsuariosActivos" Width="100%" runat="server" AutoGenerateSelectButton="True"
                    ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnSelectedIndexChanging="grdUsuariosActivos_SelectedIndexChanging" EmptyDataText="No hay datos ingresados"
                    ShowHeaderWhenEmpty="True" OnRowCreated="grdUsuariosActivos_RowCreated">
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUsuariosInactivos" runat="server" Text="Usuarios inactivos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelUsuariosInactivos" runat="server">
                <asp:GridView ID="grdUsuariosInactivos"  Width="100%" runat="server" AutoGenerateSelectButton="True"
                    ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                     AutoGenerateColumns="true" EmptyDataText="No hay datos ingresados"
                    ShowHeaderWhenEmpty="true" OnSelectedIndexChanging="grdUsuariosInactivos_SelectedIndexChanging" OnRowCreated="grdUsuariosInactivos_RowCreated">
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
                        </td>
                    </tr>
                </table>          
            </td>
            <td>
                <table class="table" style="width: 200px">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEspecialidades" runat="server" Text="Especialidades:" Style="margin-left: 50px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbFisioterapeuta" runat="server" Text="Fisioterapeuta" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbFonoaudiologo" runat="server" Text="Fonoaudiólogo" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbPedagogo" runat="server" Text="Pedagogo" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbPsicologo" runat="server" Text="Psicólogo" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbPsicomotricista" runat="server" Text="Psicomotricista" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbSinEspecialidad" runat="server" Text="Sin especialidad" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:" Style="margin-left: 50px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbAdministrador" runat="server" Text="Administrador" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbAdministrativo" runat="server" Text="Administrativo" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbEspecialista" runat="server" Text="Técnico" Style="margin-left: 50px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" Style="margin-left: 50px" OnClick="btnAplicarFiltros_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar filtros" OnClick="btnLimpiarFiltros_Click" />
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
