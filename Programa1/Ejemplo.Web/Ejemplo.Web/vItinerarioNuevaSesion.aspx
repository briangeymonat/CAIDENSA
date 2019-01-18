<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vItinerarioNuevaSesion.aspx.cs" Inherits="Ejemplo.Web.vItinerarioNuevaConsulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Nueva sesion de itinerario
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
                            <asp:DropDownList ID="ddlTipoSesion" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Día:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDias" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Localidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Juan Lacaze" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Nueva Helvecia"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Desde:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Time"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Time"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Beneficiarios"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" Width="100%" PlaceHolder="Buscar por CI, Nombre o Apellido" OnTextChanged="txtBuscarBeneficiarios_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Beneficiarios"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Beneficiarios agregados"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdBeneficiarios" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanging="grdBeneficiarios_SelectedIndexChanging" ShowHeaderWhenEmpty="true" OnRowCreated="grdBeneficiarios_RowCreated">
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
                            <asp:GridView ID="grdBeneficiariosAgregados" runat="server" AutoGenerateDeleteButton="true" OnRowDeleting="grdBeneficiariosAgregados_RowDeleting" ShowHeaderWhenEmpty="true" OnRowCreated="grdBeneficiariosAgregados_RowCreated">
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
                        <td>
                            <table>
                                <tr><td>Seleccionar plan</td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre1" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan1" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre2" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan2" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre3" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan3" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre4" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan4" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre5" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan5" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre6" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan6" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre7" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan7" runat="server"></asp:DropDownList></td></tr>
                                <tr><td>
                                    <asp:Label ID="lblNombre8" runat="server" Text="Label"></asp:Label></td><td>
                                    <asp:DropDownList ID="ddlPlan8" runat="server"></asp:DropDownList></td></tr>
                            </table>
                            
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
                            <asp:Label ID="Label7" runat="server" Text="Especialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEspecialidades" runat="server" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                            <asp:Label ID="Label8" runat="server" Text="Especialistas"></asp:Label>
                            <asp:GridView ID="grdTodosEspecialistas" runat="server" AutoGenerateSelectButton="true" ShowHeaderWhenEmpty="true"
                                OnSelectedIndexChanging="grdTodosEspecialistas_SelectedIndexChanging" OnRowCreated="grdTodosEspecialistas_RowCreated">

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
                            <asp:Label ID="Label9" runat="server" Text="Especialistas agregados"></asp:Label>
                            <asp:GridView ID="grdEspecialistasAgregados" runat="server" AutoGenerateDeleteButton="true" ShowHeaderWhenEmpty="true"
                                OnRowDeleting="grdEspecialistasAgregados_RowDeleting" OnRowCreated="grdEspecialistasAgregados_RowCreated">
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Comentario:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Width="100%" Height="130px"></asp:TextBox>
            </td>
        </tr>
    </table>

</asp:Content>
