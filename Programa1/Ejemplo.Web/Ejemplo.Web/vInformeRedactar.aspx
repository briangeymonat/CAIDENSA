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
    <asp:Panel ID="PanelDiagnosticosInformeRedactar" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Todos los diagnósticos:"></asp:Label>
                <asp:GridView ID="grdTodosDiagnosticos" runat="server"></asp:GridView>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnQuitar" runat="server" Text="Quitar" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Diagnósticos agregados:"></asp:Label>
                <asp:GridView ID="grdDiagnosticosAgregados" runat="server"></asp:GridView>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <table>
        <tr>
            <td>
                <asp:CheckBox ID="cbAntecedentesPatologicos" runat="server" Text="Antecedentes patológicos:" AutoPostBack="True" />
            </td>
            <td>
                <asp:TextBox ID="txtAntecedentesPatologicos" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbDesarrollo" runat="server" Text="Desarrollo:" AutoPostBack="True" />
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
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
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
                <asp:CheckBox ID="cbSugerencia" runat="server" Text="Sugerencias:" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtSugerencia" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSalirSinGuardar" runat="server" Text="Salir sin guardar cambios de mis secciones"  />
            </td>
            <td>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios de mis secciones" />
            </td>
        </tr>
        <tr>
            <td>

            </td>
            <td>
                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar mis secciones" />
            </td>
        </tr>
    </table>
</asp:Content>
