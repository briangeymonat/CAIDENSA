<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioAgregarPasivo.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioAgregarPasivo1" %>

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
                            <asp:TextBox ID="txtCi" runat="server" TextMode="Number"></asp:TextBox>
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
                            <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Masculino" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomicilio" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTel1" runat="server" Text="Teléfono 1:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel1" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTel2" runat="server" Text="Teléfono 2:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel2" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAtributario" runat="server" Text="Atributario:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAtributario" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" OnCheckedChanged="cbPensionista_CheckedChanged" />
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
                            <asp:Label ID="lblEscolaridad" runat="server" Text="Escolaridad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEscolaridad" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDerivador" runat="server" Text="Persona que lo deriva:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDerivador" runat="server"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Correo electronico:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAgregarBeneficiario" runat="server" Text="Agregar beneficiario" OnClick="btnAgregarBeneficiario_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMensajeBeneficiario" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2">
                <div>
                    <asp:Button ID="btnPrimeraSesionMostrar" runat="server" Text="Mostrar primera sesión" OnClick="btnPrimeraSesionMostrar_Click" />
                    <asp:Button ID="btnPrimeraSesionOcultar" runat="server" Text="Ocultar primera sesión" OnClick="btnPrimeraSesionOcultar_Click" />
                    <asp:Panel ID="PanelPrimeraSesion" runat="server">
                        <h3>Primera sesión
                        </h3>

                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Tipo de sesión:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoSesionPS" runat="server" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFechaPS" runat="server" TextMode="Date"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Localidad:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblLocalidadPS" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Juan Lacaze"></asp:ListItem>
                                                    <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Desde:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDesdePS" runat="server" TextMode="Time"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Hasta:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtHastaPS" runat="server" TextMode="Time"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Especialidad:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEspecialidadesPS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialidadesPS_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Especialistas"></asp:Label>
                                                <asp:GridView ID="grdTodosEspecialistasPS" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdTodosEspecialistasPS_RowCreated" OnSelectedIndexChanging="grdTodosEspecialistasPS_SelectedIndexChanging"></asp:GridView>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Especialistas agregados"></asp:Label>
                                                <asp:GridView ID="grdEspecialistasAgregadosPS" runat="server" AutoGenerateDeleteButton="True" OnRowCreated="grdEspecialistasAgregadosPS_RowCreated" OnRowDeleting="grdEspecialistasAgregadosPS_RowDeleting"></asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="Comentario:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtComentarioPS" runat="server" TextMode="MultiLine" Width="100%" Height="130px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                </div>
                </asp:Panel>
                <div>
                    <asp:Button ID="btnUltimaSesionMostrar" runat="server" Text="Mostrar última sesión" OnClick="btnUltimaSesionMostrar_Click" />
                    <asp:Button ID="btnUltimaSesionOcultar" runat="server" Text="Ocultar última sesión" OnClick="btnUltimaSesionOcultar_Click" />

                    <asp:Panel ID="PanelUltimaSesion" runat="server">
                        <h3>Última sesión
                        </h3>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="Tipo de sesión:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoSesionUS" runat="server" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Text="Fecha:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFechaUS" runat="server" TextMode="Date"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Text="Localidad:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblLocalidadUS" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Juan Lacaze"></asp:ListItem>
                                                    <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="Desde:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDesdeUS" runat="server" TextMode="Time"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Text="Hasta:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtHastaUS" runat="server" TextMode="Time"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" Text="Especialidad:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEspecialidadesUS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialidadesUS_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" Text="Especialistas"></asp:Label>
                                                <asp:GridView ID="grdTodosEspecialistasUS" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdTodosEspecialistasUS_RowCreated" OnSelectedIndexChanging="grdTodosEspecialistasUS_SelectedIndexChanging"></asp:GridView>
                                            </td>
                                            
                                            <td>
                                                <asp:Label ID="Label20" runat="server" Text="Especialistas agregados"></asp:Label>
                                                <asp:GridView ID="grdEspecialistasAgregadosUS" runat="server" AutoGenerateDeleteButton="True" OnRowCreated="grdEspecialistasAgregadosUS_RowCreated" OnRowDeleting="grdEspecialistasAgregadosUS_RowDeleting"></asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Text="Comentario:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtComentarioUS" runat="server" TextMode="MultiLine" Width="100%" Height="130px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>

                <div id="divAgregarNuevoPlan" class="VentanaEmergente">
                    <asp:Button ID="btnAgregarPlanMostrar" runat="server" Text="Mostrar agregar plan" OnClick="btnAgregarPlanMostrar_Click" />
                    <asp:Button ID="btnAgregarPlanOcultar" runat="server" Text="Ocultar agregar plan" OnClick="btnAgregarPlanOcultar_Click" />


                    <asp:Panel ID="PanelAgregarPlan" runat="server">
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

                        </table>
                    </asp:Panel>
                </div>

                <asp:Button ID="btnAgregarDiagnosticoMostrar" runat="server" Text="Mostrar agregar diagnóstico" OnClick="btnAgregarDiagnosticoMostrar_Click" />
                <asp:Button ID="btnAgregarDiagnosticoOcultar" runat="server" Text="Ocultar agregar diagnóstico" OnClick="btnAgregarDiagnosticoOcultar_Click" />
                <asp:Panel ID="PanelAgregarDiagnostico" runat="server">


                    <h4>Agregar diagnóstico
                    </h4>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label22" runat="server" Text="Todos los diagnósticos:"></asp:Label>
                                <asp:GridView ID="grdTodosDiagnosticos" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdTodosDiagnosticos_RowCreated" OnSelectedIndexChanging="grdTodosDiagnosticos_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Diagnósticos agregados:"></asp:Label>

                                <asp:GridView ID="grdDiagnosticosAgregados" runat="server" AutoGenerateDeleteButton="True" OnRowCreated="grdDiagnosticosAgregados_RowCreated" OnRowDeleting="grdDiagnosticosAgregados_RowDeleting" ShowHeaderWhenEmpty="True">
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

                    </table>
                </asp:Panel>
</asp:Content>
