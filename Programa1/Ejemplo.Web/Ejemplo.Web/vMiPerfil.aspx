<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vMiPerfil.aspx.cs" Inherits="Ejemplo.Web.vMiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Mi perfil
    </h3>
    <br />
    <table>
        <tr>
            <td class="alinearArriba">
                <table class="alinearArriba">
                    <tr class="alinearArriba">
                        <td>
                            <asp:Label ID="lblNickname" runat="server" Text="Nickname:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label  ID="lblObligatorio1" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblObligatorio2" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
                        </td>
                        <td><asp:Label ID="lblObligatorio3" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCi" runat="server"></asp:TextBox>
                        </td>
                        <td><asp:Label ID="lblObligatorio4" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date"></asp:TextBox>
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
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTel" runat="server" Text="Telefono:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelefono" runat="server" TextMode="Phone"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoUsuario" runat="server" Enabled="False"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td>
                            <asp:RadioButtonList ID="rbTipoDeEmpleado" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Empleado</asp:ListItem>
                                <asp:ListItem>Contratado</asp:ListItem>
                                <asp:ListItem>Socio</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="button" OnClick="btnConfirmar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblBeneficiariosQueAtiende" runat="server" Text="Beneficiarios que atiende:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdBeneficiariosQueAtiende" runat="server" AutoGenerateSelectButton="True"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdBeneficiariosQueAtiende_RowCreated" OnSelectedIndexChanging="grdBeneficiariosQueAtiende_SelectedIndexChanging">
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblInformesRealizados" runat="server" Text="Informes realizados:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdInformesRealizados" runat="server"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdInformesRealizados_RowCreated" ShowHeaderWhenEmpty="True">
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblInformesPendientes" runat="server" Text="Informes pendientes:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div>
        <asp:Panel ID="pnlItinerario" runat="server" Visible="false">
            <h4>Itinerario semanal
            </h4>
            <formview id="frmItinerario" runat="server"></formview>
        </asp:Panel>
    </div>
</asp:Content>
