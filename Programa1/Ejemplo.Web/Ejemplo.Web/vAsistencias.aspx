<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vAsistencias.aspx.cs" Inherits="Ejemplo.Web.vAsistencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Asistencias mensuales:
    </h3>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Beneficiarios"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" PlaceHolder="Buscar por CI, Nombre o Apellido"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMes" runat="server" TextMode="Month" OnTextChanged="txtMes_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h5>Filtros:</h5>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label2" runat="server" Text="Localidad:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbJuanLacaze" runat="server" Text="Juan Lacaze" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbNuevaHelvecia" runat="server" Text="Nueva Helvecia" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label3" runat="server" Text="Plan:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbASSE" runat="server" Text="ASSE" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbAYEX" runat="server" Text="AYEX" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbCAMEC" runat="server" Text="CAMEC" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbCirculocatolico" runat="server" Text="Círculo Católico" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbMIDES" runat="server" Text="MIDES" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbParticular" runat="server" Text="Particular" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbPolicial" runat="server" Text="Policial" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Rango de edad:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Desde:"></asp:Label>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Hasta:"></asp:Label>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label8" runat="server" Text="Especialidades a las que asiste:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbFisioterapeuta" runat="server" Text="Fisioterapeuta" />
                        </td>
                        </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbFonoaudiologo" runat="server" Text="Fonoaudiólogo" />
                        </td>
                        </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbPedagogo" runat="server" Text="Pedagogo" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbPsicologo" runat="server" Text="Psicólogo" />
                        </td>
                        </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbPsicomotricista" runat="server" Text="Psicomotricista" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
