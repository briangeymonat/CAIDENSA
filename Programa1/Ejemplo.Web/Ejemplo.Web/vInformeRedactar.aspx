<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeRedactar.aspx.cs" Inherits="Ejemplo.Web.vInformeRedactar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Redactar Informe
    </h3>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Tipo:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTipo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Título:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h4>Beneficiario</h4>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Nombres:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNombres" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Apellidos:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApellidos" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Fecha de nacimiento:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFechaNac" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Edad cronológica:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEdad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Motivo de consulta:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMotivoConsulta" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Escolaridad:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEscolaridad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="Encuadre:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEncuadre" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Antecedentes:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAntecedentes" runat="server"></asp:Label>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" Text="Ulitmo diagnóstico:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUltimoDiagnostico" runat="server"></asp:Label><!-- Solo mostrar, no va en el informe-->
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="Diagnóstico:"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelDiagnosticosInformeRedactar" runat="server" ScrollBars="None" Height="250px">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Todos los diagnósticos:"></asp:Label>
                <asp:Panel ID="PanelTodosDiagnosticos" runat="server" ScrollBars="Vertical" Height="200px">
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
                    </asp:Panel>
            </td>            
            <td>
                <asp:Label ID="Label5" runat="server" Text="Diagnósticos agregados:"></asp:Label>
                <asp:Panel ID="PanelDiagnosticosAgregados" runat="server" ScrollBars="Vertical" Height="200px">
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
                    </asp:Panel>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <table>
        <tr>
            <td>
                <asp:CheckBox ID="cbAntecedentesPatologicos" runat="server" Text="Antecedentes patológicos:" AutoPostBack="True" OnCheckedChanged="cbAntecedentesPatologicos_CheckedChanged" />
            </td>
            <td>
                <asp:TextBox ID="txtAntecedentesPatologicos" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbDesarrollo" runat="server" Text="Desarrollo:" AutoPostBack="True" OnCheckedChanged="cbDesarrollo_CheckedChanged" />
            </td>
            <td>
                <asp:TextBox ID="txtDesarrollo" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Presentación:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtPresentacion" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Abordaje"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtAbordaje" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="En SUMA:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtEnsuma" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbSugerencia" runat="server" Text="Sugerencias:" AutoPostBack="True" OnCheckedChanged="cbSugerencia_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtSugerencia" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSalirSinGuardar" runat="server" Text="Salir sin guardar cambios de mis secciones" OnClick="btnSalirSinGuardar_Click"  />
            </td>
            <td>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios de mis secciones" OnClick="btnGuardar_Click" />
            </td>
        </tr>
        <tr>
            <td>

            </td>
            <td>
                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar mis secciones" OnClick="btnFinalizar_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
