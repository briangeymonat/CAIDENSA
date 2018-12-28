<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioDetalles.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioDetalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Detalles del beneficiario
    </h3>
    <br />
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCi" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text="Sexo:"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Masculino"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Domicilio:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomicilio" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Telefono 1:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelefono1" runat="server" TextMode="Phone"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Telefono 2:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelefono2" runat="server" TextMode="Phone"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Correo electrónico:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Atributario:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAtributario" runat="server"></asp:TextBox>
                        </td>
                        <td>
                <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" OnCheckedChanged="cbPensionista_CheckedChanged"/>
            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Escolaridad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEscolaridad" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Persona que lo deriva:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDerivador" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Motivo de consulta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMotivoConsulta" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnHabilitar" runat="server" Text="Habilitar" OnClick="btnHabilitar_Click" />
                            <asp:Button ID="btnInhabilitar" runat="server" Text="Inhabilitar" OnClick="btnInhabilitar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnNuevoPlan" runat="server" Text="Nuevo plan" OnClick="btnNuevoPlan_Click" />
                            <asp:Button ID="btnOcultar" runat="server" Text="Ocultar ventana de agregar plan" OnClick="btnOcultar_Click" />
                        </td>
                    </tr>
                </table>

            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text="Itinerario:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblItinerario" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LAbel" runat="server" Text="Ultimos diagnósticos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdUltimosDiagnosticos" runat="server"
                                EmptyDataText="No se encuentran diagnósticos ingresados" ShowHeaderWhenEmpty="true">
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
                            <asp:Label ID="Label17" runat="server" Text="Historial de diagnósticos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdHistorialDiagnosticos" runat="server"
                                EmptyDataText="No se encuentran diagnósticos ingresados" ShowHeaderWhenEmpty="true">
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
                            <asp:Label ID="Label18" runat="server" Text="Planes activos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdPlanesActivos" runat="server"
                                EmptyDataText="No se encuentran planes activos ingresados" ShowHeaderWhenEmpty="true"
                                AutoGenerateDeleteButton="true" OnRowDeleting="grdPlanesActivos_RowDeleting"
                                AutoGenerateSelectButton="true" OnSelectedIndexChanging="grdPlanesActivos_SelectedIndexChanging">

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
                            <asp:Label ID="Labelq" runat="server" Text="Historial de planes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdPlanesInactivos" runat="server"
                                EmptyDataText="No se encuentran planes inactivos ingresados" ShowHeaderWhenEmpty="true">
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
                            <asp:Label ID="Labelqq" runat="server" Text="Historial de informes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdInformes" runat="server"
                                EmptyDataText="No se encuentran informes ingresados" ShowHeaderWhenEmpty="true">
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
                            <asp:GridView ID="grdPlanesSinFechaDeVencimiento" runat="server"
                                EmptyDataText="No se encuentran planes sin fecha de vencimiento" ShowHeaderWhenEmpty="true">
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
                            <asp:Button ID="btnAgregarInforme" runat="server" Text="Agregar informe" OnClick="btnAgregarInforme_Click" style="height: 26px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelAgregarPlan" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAgregarPlan" runat="server" Text="Agregar plan"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Tipo:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipos" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="cbEvaluacion" runat="server" Text="Evaluación" />
                </td>
                <td></td>
                <td colspan="2">
                    <asp:CheckBox ID="cbTratamiento" runat="server" Text="Tratamiento" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Desde:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Hasta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMensajeAgregarPlan" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnAgregarPlan" runat="server" Text="Agregar plan" OnClick="btnAgregarPlan_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelModificarPlan" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Modificar Fecha de vencimiento plan"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Tipo:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoPlanModificar" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="cboEvaluacionModificar" runat="server" Text="Evaluación" />
                </td>
                <td></td>
                <td>
                    <asp:CheckBox ID="cboTratamientoModificar" runat="server" Text="Tratamiento" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Desde:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaInicioModificar" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="Hasta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaFinModificar" runat="server" TextMode="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnCancelarModificarPlan" runat="server" Text="Cancelar" OnClick="btnCancelarModificarPlan_Click" />
                    <asp:Button ID="btnModificarPlan" runat="server" Text="Modificar plan" OnClick="btnModificarPlan_Click" />                    
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
