<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioAgregarPasivo.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioAgregarPasivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Agregar beneficiario pasivo
    </h3>
    <br />
    <table>
        <tr>
            <td>

                <table class="table">
                    <tr>
                        <td>
                            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCi" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Sexo:"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Masculino"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAtributario" runat="server" Text="Atributario:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAtributario" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMotivoConsulta" runat="server" Text="Motivo de consulta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMotivoConsulta" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Diagnóstico:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelDiagnosticosInformeRedactar" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Todos los diagnósticos:"></asp:Label>
                                    <asp:GridView ID="grdTodosDiagnosticos" runat="server"></asp:GridView>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnQuitar" runat="server" Text="Quitar" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Diagnósticos agregados:"></asp:Label>
                                    <asp:GridView ID="grdDiagnosticosAgregados" runat="server"></asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                        </td>
                    </tr>
                    

                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAgregarBeneficiario" runat="server" Text="Agregar beneficiario" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <div id="divAgregarNuevoPlan" class="VentanaEmergente">
                    <h4>Agregar Plan
                    </h4>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlTipoPlanes" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbTratamiento" runat="server" Text="Tratamiento" />

                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbEvaluacion" runat="server" Text="Evaluación" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDesde" runat="server" Text="Desde:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHasta" runat="server" Text="Hasta:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnAgregarPlan" runat="server" Text="Agregar Plan" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>

    </table>
</asp:Content>
