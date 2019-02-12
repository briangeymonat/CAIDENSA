<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioAgregarPasivo.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioAgregarPasivo1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 50px">Agregar beneficiario pasivo
    </h2>
    <br />
    <asp:Table runat="server" Width="100%">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Width="40%" Style="padding-left: 20px;">
                <%-- TABLA FORMULARIO DEL BENEFICIARIO --%>
                <asp:Table runat="server">
                    <%-- NOMBRE --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNombres" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio1" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- APELLIDO --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtApellidos" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- CEDULA DE IDENTIDAD --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtCi" runat="server" TextMode="Number" Width="160px" MaxLength="8"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:RangeValidator ID="rfvCi" ControlToValidate="txtCi" Type="Integer" MinimumValue="01000000" MaximumValue="99999999" ErrorMessage="Por favor ingrese una cédula correcta."
                                Display="Dynamic" runat="server"></asp:RangeValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- FECHA DE NACIMIENTO --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label24" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- SEXO --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" Text="Sexo:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2" Width="160px">
                            <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Masculino" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                    </asp:TableRow>
                    <%-- DOMICILIO --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDomicilio" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- TELEFONO 1 --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTel1" runat="server" Text="Teléfono 1:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTel1" runat="server" TextMode="Number" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- TELEFONO 2 --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTel2" runat="server" Text="Teléfono 2:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTel2" runat="server" TextMode="Number" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- ATRIBUTARIO --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblAtributario" runat="server" Text="Atributario:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtAtributario" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label25" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" OnCheckedChanged="cbPensionista_CheckedChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- MOTIVO DE CONSULTA --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblMotivoConsulta" runat="server" Text="Motivo de consulta:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtMotivoConsulta" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label26" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- ESCOLARIDAD --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEscolaridad" runat="server" Text="Escolaridad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEscolaridad" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- DERIVADOR --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblDerivador" runat="server" Text="Persona que lo deriva:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDerivador" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- EMAIL --%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEmail" runat="server" Text="Correo electronico:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- BOTON AGREGAR --%>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Button ID="btnAgregarBeneficiario" runat="server" Text="Agregar beneficiario" OnClick="btnAgregarBeneficiario_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <%-- LABEL MENSAJE BENEFICIARIO --%>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Label ID="lblMensajeBeneficiario" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell Width="50%">
                <div>
                    <asp:Button ID="btnPrimeraSesionMostrar" runat="server" Text="Mostrar primera sesión" OnClick="btnPrimeraSesionMostrar_Click" Width="200px" Style="text-align:left" />
                    <asp:Button ID="btnPrimeraSesionOcultar" runat="server" Text="Ocultar primera sesión" OnClick="btnPrimeraSesionOcultar_Click"  Width="200px" Style="text-align:left"/>
                    <%-- PANEL PRIMERA SESION --%>
                    <asp:Panel ID="pnlPrimeraSesion" runat="server" Style="border: solid 1px black; padding: 15px; " Width="90%">
                        <h2 style="padding-left: 50px">Primera sesión
                        </h2>

                        <asp:Table runat="server">
                            <asp:TableRow VerticalAlign="Top">
                                <asp:TableCell>
                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label1" runat="server" Text="Tipo de sesión:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlTipoSesionPS" runat="server" Enabled="False"></asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Label3" runat="server" Text="Fecha:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="4">
                                                <asp:TextBox ID="txtFechaPS" runat="server" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label4" runat="server" Text="Localidad:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RadioButtonList ID="rblLocalidadPS" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Juan Lacaze"></asp:ListItem>
                                                    <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Label5" runat="server" Text="Desde:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlDesdePS" runat="server"></asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Label6" runat="server" Text="Hasta:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlHastaPS" runat="server"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label8" runat="server" Text="Especialidad:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlEspecialidadesPS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialidadesPS_SelectedIndexChanged"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table runat="server" Width="100%">
                                        <asp:TableRow VerticalAlign="Top">
                                            <asp:TableCell Width="50%">
                                                <asp:Label ID="Label9" runat="server" Text="Especialistas" Font-Bold="true"></asp:Label>
                                                <asp:GridView ID="grdTodosEspecialistasPS" runat="server"
                                                    AutoGenerateSelectButton="True" OnRowCreated="grdTodosEspecialistasPS_RowCreated"
                                                    OnSelectedIndexChanging="grdTodosEspecialistasPS_SelectedIndexChanging"
                                                    ViewStateMode="Enabled" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None"
                                                    ShowHeaderWhenEmpty="true">
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
                                            <asp:TableCell Width="50%">
                                                <asp:Label ID="Label10" runat="server" Text="Especialistas agregados" Font-Bold="true"></asp:Label>
                                                <asp:GridView ID="grdEspecialistasAgregadosPS" runat="server"
                                                    AutoGenerateDeleteButton="True" OnRowCreated="grdEspecialistasAgregadosPS_RowCreated"
                                                    OnRowDeleting="grdEspecialistasAgregadosPS_RowDeleting"
                                                    ViewStateMode="Enabled" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None"
                                                    ShowHeaderWhenEmpty="true">
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
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label11" runat="server" Text="Comentario:" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtComentarioPS" runat="server" TextMode="MultiLine" Wrap="false" Width="100%" Height="130px"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                    </asp:Panel>
                </div>
                <div>
                    <asp:Button ID="btnUltimaSesionMostrar" runat="server" Text="Mostrar última sesión" OnClick="btnUltimaSesionMostrar_Click"  Width="200px" Style="text-align:left"/>
                    <asp:Button ID="btnUltimaSesionOcultar" runat="server" Text="Ocultar última sesión" OnClick="btnUltimaSesionOcultar_Click" Width="200px" Style="text-align:left" />

                    <asp:Panel ID="pnlUltimaSesion" runat="server" Style="border: solid 1px black; padding: 15px;" Width="90%">
                        <h2 style="padding-left: 50px">Última sesión
                        </h2>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label12" runat="server" Text="Tipo de sesión:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlTipoSesionUS" runat="server" Enabled="False"></asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Label13" runat="server" Text="Fecha:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="3">
                                                <asp:TextBox ID="txtFechaUS" runat="server" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label14" runat="server" Text="Localidad:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RadioButtonList ID="rblLocalidadUS" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Juan Lacaze"></asp:ListItem>
                                                    <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Label15" runat="server" Text="Desde:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlDesdeUS" runat="server"></asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="Label16" runat="server" Text="Hasta:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlHastaUS" runat="server"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label18" runat="server" Text="Especialidad:" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlEspecialidadesUS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialidadesUS_SelectedIndexChanged"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table runat="server" Width="100%">
                                        <asp:TableRow VerticalAlign="Top">
                                            <asp:TableCell Width="50%">
                                                <asp:Label ID="Label19" runat="server" Text="Especialistas" Font-Bold="true"></asp:Label>
                                                <asp:GridView ID="grdTodosEspecialistasUS" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdTodosEspecialistasUS_RowCreated" OnSelectedIndexChanging="grdTodosEspecialistasUS_SelectedIndexChanging"
                                                    ViewStateMode="Enabled" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None"
                                                    ShowHeaderWhenEmpty="true">
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

                                            <asp:TableCell Width="50%">
                                                <asp:Label ID="Label20" runat="server" Text="Especialistas agregados" Font-Bold="true"></asp:Label>
                                                <asp:GridView ID="grdEspecialistasAgregadosUS" runat="server" AutoGenerateDeleteButton="True" OnRowCreated="grdEspecialistasAgregadosUS_RowCreated" OnRowDeleting="grdEspecialistasAgregadosUS_RowDeleting"
                                                    ViewStateMode="Enabled" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None"
                                                    ShowHeaderWhenEmpty="true">
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
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label21" runat="server" Text="Comentario:" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtComentarioUS" runat="server" TextMode="MultiLine" Width="100%" Height="130px"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                </div>

                <asp:Button ID="btnAgregarPlanMostrar" runat="server" Text="Mostrar agregar plan" OnClick="btnAgregarPlanMostrar_Click" Width="200px" Style="text-align:left" />
                <asp:Button ID="btnAgregarPlanOcultar" runat="server" Text="Ocultar agregar plan" OnClick="btnAgregarPlanOcultar_Click" Width="200px" Style="text-align:left" />

                <div id="divAgregarNuevoPlan">
                    <asp:Panel ID="pnlAgregarPlan" runat="server" Style="border: solid 1px black; padding: 15px;" Width="90%">
                        <h2 style="padding-left: 50px">Agregar plan</h2>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <br />
                                    <asp:Label ID="lblTipo" runat="server" Text="Tipo:" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="3">
                                    <br />
                                    <asp:DropDownList ID="ddlTipoPlanes" runat="server"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:CheckBox ID="cbTratamiento" runat="server" Text="Tratamiento" />

                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:CheckBox ID="cbEvaluacion" runat="server" Text="Evaluación" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblDesde" runat="server" Text="Desde:" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblHasta" runat="server" Text="Hasta:" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                </div>

                <asp:Button ID="btnAgregarDiagnosticoMostrar" runat="server" Text="Mostrar agregar diagnóstico" OnClick="btnAgregarDiagnosticoMostrar_Click" Width="200px" Style="text-align:left" />
                <asp:Button ID="btnAgregarDiagnosticoOcultar" runat="server" Text="Ocultar agregar diagnóstico" OnClick="btnAgregarDiagnosticoOcultar_Click" Width="200px" Style="text-align:left" />
                <asp:Panel ID="pnlAgregarDiagnostico" runat="server" Style="border: solid 1px black; padding: 15px;" Width="90%">


                    <h2 style="padding-left: 50px">Agregar diagnóstico
                    </h2>
                    <asp:Table runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableRow VerticalAlign="Top">
                                        <asp:TableCell Width="50%">
                                            <asp:Label ID="Label22" runat="server" Text="Todos los diagnósticos:" Font-Bold="true"></asp:Label>
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="250px">
                                                <asp:GridView ID="grdTodosDiagnosticos" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdTodosDiagnosticos_RowCreated" OnSelectedIndexChanging="grdTodosDiagnosticos_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
                                        <asp:TableCell Width="50%">
                                            <asp:Label ID="Label23" runat="server" Text="Diagnósticos agregados:" Font-Bold="true"></asp:Label>
                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="250px">
                                                <asp:GridView ID="grdDiagnosticosAgregados" runat="server" AutoGenerateDeleteButton="True"
                                                    OnRowCreated="grdDiagnosticosAgregados_RowCreated" OnRowDeleting="grdDiagnosticosAgregados_RowDeleting"
                                                    ShowHeaderWhenEmpty="True">
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
                        </asp:TableRow>


                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
