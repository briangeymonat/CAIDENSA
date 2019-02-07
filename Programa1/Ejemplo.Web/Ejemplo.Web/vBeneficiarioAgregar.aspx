<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioAgregar.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioAgregar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">

        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2  style="padding-left:50px">Agregar beneficiario
    </h2>
    <br />
    <asp:Table runat="server" CellPadding="20">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Width="600px" >

                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNombres" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio1" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtApellidos" runat="server" Width="160px"></asp:TextBox>

                            <asp:Label ID="Label3" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtCi" runat="server" Width="160px" TextMode="Number" MaxLength="8"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>                           
                        </asp:TableCell>
                        <asp:TableCell>
                             <asp:RangeValidator ID="rfvCi" ControlToValidate="txtCi" Type="Integer" MinimumValue="01000000" MaximumValue="99999999" ErrorMessage="Por favor ingrese una cédula correcta." 
                                Display="Dynamic" runat="server" ></asp:RangeValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" Text="Sexo:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Masculino" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Femenino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDomicilio" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTel1" runat="server" Text="Teléfono 1:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTel1" runat="server" TextMode="Phone" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTel2" runat="server" Text="Teléfono 2:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTel2" runat="server" TextMode="Phone" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblAtributario" runat="server" Text="Atributario:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtAtributario" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                            <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" OnCheckedChanged="cbPensionista_CheckedChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblMotivoConsulta" runat="server" Text="Motivo de consulta:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtMotivoConsulta" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEscolaridad" runat="server" Text="Escolaridad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEscolaridad" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblDerivador" runat="server" Text="Persona que lo deriva:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDerivador" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEmail" runat="server" Text="Correo electronico:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Button ID="btnAgregarBeneficiario" runat="server" Text="Agregar beneficiario" OnClick="btnAgregarBeneficiario_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Label ID="lblMensajeBeneficiario" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell>
                <div id="divAgregarNuevoPlan" style="border: solid 1px black; padding: 15px; ">
                    <asp:Label ID="lblBeneficiariosQueAtiende" runat="server" Text="Agregar plan:" Font-Bold="true" Font-Size="12"></asp:Label>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <br />
                                <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
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
                                <asp:Label ID="lblDesde" runat="server" Text="Desde:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblHasta" runat="server" Text="Hasta:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblMensajePlan" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnAgregarPlan" runat="server" Text="Agregar Plan" OnClick="btnAgregarPlan_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Planes agregados" Font-Bold="true" Font-Size="12" ></asp:Label>
                <br>
                <br>
                <asp:GridView ID="grdPlanes" runat="server"
                    AutoGenerateDeleteButton="true" OnRowCreated="grdPlanes_RowCreated"
                    OnRowDeleting="grdPlanes_RowDeleting" ShowHeaderWhenEmpty="true">
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

</asp:Content>
