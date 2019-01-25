<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioDetalles.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioDetalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 50px">Detalles del beneficiario
    </h2>
    <br />
    <asp:Table runat="server" Style="margin-left: 50px" Width="100%">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Width="30%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label1" runat="server" Text="Nombres:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNombres" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" Text="Apellidos:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtApellidos" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label3" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtCi" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label4" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label21" runat="server" Text="Sexo:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Masculino"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Domicilio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDomicilio" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label6" runat="server" Text="Telefono 1:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTelefono1" runat="server" TextMode="Phone" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="Telefono 2:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTelefono2" runat="server" TextMode="Phone" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label9" runat="server" Text="Atributario:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtAtributario" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" OnCheckedChanged="cbPensionista_CheckedChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label12" runat="server" Text="Motivo de consulta:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtMotivoConsulta" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label10" runat="server" Text="Escolaridad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEscolaridad" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label11" runat="server" Text="Persona que lo deriva:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDerivador" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label8" runat="server" Text="Correo electrónico:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEmail" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <br />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnHabilitar" runat="server" Text="Habilitar" OnClick="btnHabilitar_Click" />
                            <asp:Button ID="btnInhabilitar" runat="server" Text="Inhabilitar" OnClick="btnInhabilitar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Button ID="btnNuevoPlan" runat="server" Text="Nuevo plan" OnClick="btnNuevoPlan_Click" />
                            <asp:Button ID="btnOcultar" runat="server" Text="Ocultar ventana de agregar plan" OnClick="btnOcultar_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Panel ID="PanelAgregarPlan" runat="server">
                    <asp:Table runat="server" Style="border: solid 1px black;">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblAgregarPlan" runat="server" Text="Agregar plan"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <br />
                                <asp:Label ID="Label13" runat="server" Text="Tipo:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <br />
                                <asp:DropDownList ID="ddlTipos" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBox ID="cbEvaluacion" runat="server" Text="Evaluación" />
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:CheckBox ID="cbTratamiento" runat="server" Text="Tratamiento" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label14" runat="server" Text="Desde:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label15" runat="server" Text="Hasta:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblMensajeAgregarPlan" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <br />
                                <asp:Button ID="btnAgregarPlan" runat="server" Text="Agregar plan" OnClick="btnAgregarPlan_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel ID="PanelModificarPlan" runat="server">
                    <asp:Table runat="server" Style="margin-top: 40px; border: solid 1px black;">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="Label16" runat="server" Text="Modificar Fecha de vencimiento plan"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <br />
                                <asp:Label ID="Label19" runat="server" Text="Tipo:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <br />
                                <asp:DropDownList ID="ddlTipoPlanModificar" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBox ID="cboEvaluacionModificar" runat="server" Text="Evaluación" />
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:CheckBox ID="cboTratamientoModificar" runat="server" Text="Tratamiento" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label20" runat="server" Text="Desde:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtFechaInicioModificar" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label23" runat="server" Text="Hasta:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtFechaFinModificar" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <br />
                                <asp:Button ID="btnCancelarModificarPlan" runat="server" Text="Cancelar" OnClick="btnCancelarModificarPlan_Click" />
                                <asp:Button ID="btnModificarPlan" runat="server" Text="Modificar plan" OnClick="btnModificarPlan_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell Width="40%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnAgregarInforme" runat="server" Text="Agregar informe" OnClick="btnAgregarInforme_Click" Style="height: 26px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label22" runat="server" Text="Itinerario:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblItinerario" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="LAbel" runat="server" Text="Ultimos diagnósticos" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="PanelUltimosDiagnosticos" runat="server" ScrollBars="Vertical" Height="150px">
                                <asp:GridView ID="grdUltimosDiagnosticos" runat="server" ShowHeaderWhenEmpty="true" Width="100%">
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
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="lblPlanesActivos" runat="server" Text="Planes activos" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="grdPlanesActivos" runat="server" ShowHeaderWhenEmpty="true" Width="100%"
                                AutoGenerateDeleteButton="true" OnRowDeleting="grdPlanesActivos_RowDeleting"
                                AutoGenerateSelectButton="true" OnSelectedIndexChanging="grdPlanesActivos_SelectedIndexChanging"
                                OnRowCreated="grdPlanesActivos_RowCreated">

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
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Labelqq" runat="server" Text="Historial de informes" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="150px">
                                <asp:GridView ID="grdInformes" runat="server" ShowHeaderWhenEmpty="true" Width="100%" OnRowCreated="grdInformes_RowCreated">
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
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="lblHistorialPlanes" runat="server" Text="Historial de planes" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="150px">
                                <asp:GridView ID="grdPlanesInactivos" runat="server" ShowHeaderWhenEmpty="true" Width="100%"
                                    OnRowCreated="grdPlanesInactivos_RowCreated">
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
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label17" runat="server" Text="Historial de diagnósticos" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="150px">
                                <asp:GridView ID="grdHistorialDiagnosticos" runat="server" ShowHeaderWhenEmpty="true" Width="100%">
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
            </asp:TableCell>
            <asp:TableCell Width="30%">
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>


</asp:Content>
