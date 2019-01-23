<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeDetalles.aspx.cs" Inherits="Ejemplo.Web.vInformeDetalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }

        .auto-style2 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Detalles del informe
    </h3>
    <table>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="lblCentro" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFecha" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Tipo:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblTipo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Título:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label3" runat="server" Text="Nombre:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3" class="auto-style2">
                <asp:Label ID="lblNombreApellido" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Cédula de identidad:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCI" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Fecha de nacimiento:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFechaNacimiento" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Edad cronológica:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEdadCronologica" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Motivo de consulta:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblMotivoConsulta" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Escolaridad:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblEscolaridad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Encuadre:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblEncuadre" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSubtituloAntecedentesPatologicos" runat="server" Text="Antecedentes patológicos:" Font-Bold="True"></asp:Label>
            </td>
            </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblAntecedentesPatologicos" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="lblSubtituloDesarrollo" runat="server" Text="Desarrollo:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="auto-style2">
                <asp:Label ID="lblDesarrollo" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Presentación:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblPresentacion" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSubtituloAbPsicomotriz" runat="server" Text="Abordaje Psicomotriz:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblAbordajePsicomotriz" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblSubtituloAbPedagogico" runat="server" Text="Abordaje Pedagógico:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblAbordajePedagogico" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblSubtituloAbPsicologico" runat="server" Text="Abordaje Psicológico:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblAbordajePsicologico" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblSubtituloAbFonoaudiologico" runat="server" Text="Abordaje Fonoaudiológico:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblAbordajeFonoaudiologico" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblSubtituloAbFisioterapeutico" runat="server" Text="Abordaje Fisioterapeútico:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblAbordajeFisioterapeutico" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="En suma:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblEnSuma" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="lblSubtituloSugerencia" runat="server" Text="Sugerencias:" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblSugerencias" runat="server" Width="530px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnExportarPDF" runat="server" Text="Exportar informe a pdf" OnClick="btnExportarPDF_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
