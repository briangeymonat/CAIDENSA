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
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>

                        <asp:TableCell>
                            <asp:Label ID="Label28" runat="server" Text="Fecha de la primera sesión:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell >
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label29" runat="server" Text="Fecha de la última sesión:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label27" runat="server" Text="Localidad:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButtonList ID="rblLocalidad" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Juan Lacaze" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label30" runat="server" Text="Tipo:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlTipoPlanes" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:CheckBox ID="cbTratamiento" runat="server" Text="Tratamiento" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:CheckBox ID="cbEvaluacion" runat="server" Text="Evaluación" />
                                </asp:TableCell>
                            </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <h5 style="padding-left: 50px;text-align:left;">Agregar diagnóstico</h5>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top">
                        <asp:TableCell Width="50%">
                            <asp:Label ID="Label31" runat="server" Text="Todos los diagnósticos:" Font-Bold="true"></asp:Label>
                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="250px">
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
                            <asp:Label ID="Label32" runat="server" Text="Diagnósticos agregados:" Font-Bold="true"></asp:Label>
                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="250px">
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
                    <asp:TableRow>
                        <asp:TableCell VerticalAlign="Top">
                            <asp:Label runat="server" Text="Tipos de técnicos con los que asistió:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="cblEspecialidades" runat="server">
                                <asp:ListItem Text="Fisioterapeuta" Value="Fisioterapia"></asp:ListItem>
                                <asp:ListItem Text="Fonoaudiólogo" Value="Fonoaudiologia"></asp:ListItem>
                                <asp:ListItem Text="Psicomotricista" Value="Psicomotricidad"></asp:ListItem>
                                <asp:ListItem Text="Psicolólogo" Value="Psicologia"></asp:ListItem>
                                <asp:ListItem Text="Psicopedagogo" Value="Psicopedagogia"></asp:ListItem>
                                <asp:ListItem Text="Pedagogo" Value="Pedagogia"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
