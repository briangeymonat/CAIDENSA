<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioMostrar.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioMostrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Mostrar beneficiarios:
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
                            <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" PlaceHolder="Buscar por CI, Nombre o Apellido"
                                TextMode="Search"
                                OnTextChanged="txtBuscarBeneficiarios_TextChanged" AutoPostBack="true">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdBeneficiarios" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateSelectButton="true" OnSelectedIndexChanging="grdBeneficiarios_SelectedIndexChanging" OnRowCreated="grdBeneficiarios_RowCreated">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td colspan="3" class="auto-style1">
                            <h5>Filtros:</h5>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbActivos" runat="server" Text="Activos" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbPasivos" runat="server" Text="Pasivos" />
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
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Sexo:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="cblSexo" runat="server">
                                <asp:ListItem Text="Masculino"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label10" runat="server" Text="Plan:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="cblPlan" runat="server">
                                <asp:ListItem Text="ASSE"></asp:ListItem>
                                <asp:ListItem Text="AYEX"></asp:ListItem>
                                <asp:ListItem Text="CAMEC"></asp:ListItem>
                                <asp:ListItem Text="Círculo Católico"></asp:ListItem>
                                <asp:ListItem Text="MIDES"></asp:ListItem>
                                <asp:ListItem Text="Particular"></asp:ListItem>
                                <asp:ListItem Text="Policial"></asp:ListItem>
                            </asp:CheckBoxList>
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
                            <asp:CheckBoxList ID="cblEspecialidad" runat="server">
                                <asp:ListItem Text="Fisioterapia"></asp:ListItem>
                                <asp:ListItem Text="Fonoaudiologia"></asp:ListItem>
                                <asp:ListItem Text="Pedadogia"></asp:ListItem>
                                <asp:ListItem Text="Psicologia"></asp:ListItem>
                                <asp:ListItem Text="Psicomotricidad"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Diagnóstico:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlDiagnosticos" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" OnClick="btnAplicarFiltros_Click" />
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar filtros" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
