<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vUsuarioDetalles.aspx.cs" Inherits="Ejemplo.Web.DetallesUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <h1>Detalles del usuario</h1></center>
    <br />
    <asp:Table runat="server">
        <asp:TableRow VerticalAlign="Top" HorizontalAlign="Center">
            <asp:TableCell Width="25%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblNickname" runat="server" Text="Nickname:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNickName" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio1" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNombres" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio2" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtApellidos" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio3" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtCi" runat="server" TextMode="Number" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio4" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date" Width="160px"></asp:TextBox>
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
                            <asp:Label ID="lblTel" runat="server" Text="Telefono:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTelefono" runat="server" TextMode="Number" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlTipoUsuario" runat="server" Width="160px"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" Width="160px"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblVinculo" runat="server" Text="Vínculo:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:RadioButtonList ID="rbTipoDeEmpleado" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Empleado</asp:ListItem>
                                <asp:ListItem>Contratado</asp:ListItem>
                                <asp:ListItem>Socio</asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="6">
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="button" OnClick="btnConfirmar_Click" />
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Button ID="btnInhabilitar" runat="server" Text="Inhabilitar" CssClass="button" OnClick="btnInhabilitar_Click" />
                            <asp:Button ID="btnHabilitar" runat="server" Text="Habilitar" CssClass="button" OnClick="btnHabilitar_Click" />

                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="6">
                            <asp:Button ID="btnRestablecerContrasena" runat="server" Text="Restablecer Contraseña" CssClass="button" OnClick="btnRestablecerContrasena_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell Width="50%">
                <div>
                    <asp:Panel ID="pnlItinerario" runat="server" Visible="false">
                        <asp:Label ID="lblItinerario" runat="server" Text="Itinerario semanal" Font-Bold="true" Font-Size="10"></asp:Label>
                        <formview id="frmItinerario" runat="server"></formview>
                    </asp:Panel>
                </div>
            </asp:TableCell>
            <asp:TableCell Width="25%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblBeneficiariosQueAtiende" runat="server" Text="Beneficiarios que atiende:" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="grdBeneficiariosQueAtiende" runat="server" AutoGenerateSelectButton="True"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdBeneficiariosQueAtiende_RowCreated" OnSelectedIndexChanging="grdBeneficiariosQueAtiende_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
                            <asp:Label ID="lblInformesPendientes" runat="server" Text="Informes pendientes:" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="grdInformesPendientes" runat="server" AutoGenerateSelectButton="True"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdInformesPendientes_RowCreated" OnSelectedIndexChanging="grdInformesPendientes_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
                            <asp:Label ID="lblInformesRealizados" runat="server" Text="Informes realizados:" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="grdInformesRealizados" runat="server" OnRowCreated="grdInformesRealizados_RowCreated"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True">
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
    </asp:Table>



</asp:Content>
