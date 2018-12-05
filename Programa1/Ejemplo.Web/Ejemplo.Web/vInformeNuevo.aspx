<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeNuevo.aspx.cs" Inherits="Ejemplo.Web.vInformeNuevo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Nuevo informe
    </h3>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Tipo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <h4>
                    Beneficiario:
                </h4>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Nombres:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNombres" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Apellidos:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApellidos" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Fecha de nacimiento:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFechaNac" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Edad cronológica:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEdad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Motivo de consulta:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMotivoConsulta" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Escolaridad:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEscolaridad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Encuadre:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEncuadre" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Especialidad"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlEspecialidad" runat="server"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:GridView ID="grdTodosEspecialistas" runat="server"></asp:GridView>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAgregarEspecialista" runat="server" Text="Agregar" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnQuitarEspecialista" runat="server" Text="Quitar" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:GridView ID="grdEspecialistasAgregados" runat="server"></asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnRealizarInforme" runat="server" Text="Realizar Informe" />
            </td>
        </tr>
    </table>
</asp:Content>
