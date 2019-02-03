<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vSesionNueva.aspx.cs" Inherits="Ejemplo.Web.vSesionNueva" %>

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
   
    <h2 style="padding-left:60px">Nueva sesión
    </h2>
    <asp:Table runat="server">
        <asp:TableRow  VerticalAlign="Top">
            <asp:TableCell style="padding-left:50px">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label1" runat="server" Text="Tipo de sesión:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlTipoSesion" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" Text="Fecha:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label3" runat="server" Text="Localidad:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Juan Lacaze" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="Label4" runat="server" Text="Desde:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlDesde" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDesde_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlHasta" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <br />
                            <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" Width="100%" PlaceHolder="Buscar beneficiario por CI, Nombre o Apellido" OnTextChanged="txtBuscarBeneficiarios_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell  style="padding-left:150px;">
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click"  />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell ColumnSpan="6" style="padding-left:50px">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow VerticalAlign="Top">
                        <asp:TableCell Width="35%">
                            <asp:Label ID="Label11" runat="server" Text="Beneficiarios" Font-Bold="true"></asp:Label>
                            <asp:Panel ID="pnlBeneficiario" runat="server" ScrollBars="Vertical" Height="150px">
                            <asp:GridView ID="grdBeneficiarios" runat="server" ShowHeaderWhenEmpty="true"
                                 OnRowCreated="grdBeneficiarios_RowCreated" OnSelectedIndexChanging="grdBeneficiarios_SelectedIndexChanging" AutoGenerateSelectButton="True">
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
                        <asp:TableCell Width="35%">
                            <asp:Label ID="lblBeneficiarioAgregados" runat="server" Text="Beneficiarios agregados" Font-Bold="true"></asp:Label>
                            <asp:GridView ID="grdBeneficiariosCargados" runat="server" ShowHeaderWhenEmpty="true"
                                 OnRowCreated="grdBeneficiariosCargados_RowCreated" OnRowDeleting="grdBeneficiariosCargados_RowDeleting" AutoGenerateDeleteButton="True">
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
                        <asp:TableCell  Width="30%">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblSeleccionarPlan" runat="server" Text="Seleccionar plan:" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre1" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan1" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre2" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan2" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre3" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan3" runat="server"></asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre4" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan4" runat="server"></asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre5" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan5" runat="server"></asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre6" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan6" runat="server"></asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre7" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell><asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan7" runat="server"></asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblNombre8" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlPlan8" runat="server"></asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell style="padding-left:50px">
                <asp:Table runat="server">
                    <asp:TableRow VerticalAlign="Top">
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="Label7" runat="server" Text="Especialidad:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell><br />
                            <asp:DropDownList ID="ddlEspecialidades" runat="server" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell ColumnSpan="6"  style="padding-left:50px">
                <asp:Table runat="server"  Width="100%">
                    <asp:TableRow VerticalAlign="Top">
                        <asp:TableCell Width="35%"><br>
                            <asp:Label ID="Label8" runat="server" Text="Especialistas" Font-Bold="true"></asp:Label>
                            <asp:GridView ID="grdTodosEspecialistas" runat="server" ShowHeaderWhenEmpty="true"
                                OnRowCreated="grdTodosEspecialistas_RowCreated" OnSelectedIndexChanging="grdTodosEspecialistas_SelectedIndexChanging" AutoGenerateSelectButton="True">
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
                        <asp:TableCell Width="35%">
                            <br>
                            <asp:Label ID="lblEspecialistasAgregados" runat="server" Text="Especialistas agregados" Font-Bold="true"></asp:Label>
                            <asp:GridView ID="grdEspecialistasAgregados" runat="server" ShowHeaderWhenEmpty="true"
                                 OnRowCreated="grdEspecialistasAgregados_RowCreated" OnRowDeleting="grdEspecialistasAgregados_RowDeleting" AutoGenerateDeleteButton="True">
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
                        <asp:TableCell Width="30%">

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
