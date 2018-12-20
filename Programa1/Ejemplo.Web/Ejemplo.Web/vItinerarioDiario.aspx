<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vItinerarioDiario.aspx.cs" Inherits="Ejemplo.Web.vItinerarioDiario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Itinerario diario
    </h3>
    <table>
        <tr>
            <td>
                <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Seleccionar día:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDias" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Localidad:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Juan Lacaze" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Nueva Helevecia"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr>
            <td>
                <formview ID="frmItinerario" runat="server"></formview>
                <%--<asp:GridView ID="grdItinerario" runat="server">
                </asp:GridView>--%>
    <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar un dato de la grilla y se muestran en una ventana flotante los detalles de la consulta" Height="51px" OnClick="btnSeleccionar_Click" Width="593px" />
                <asp:Button ID="btnMostrarDetallesSesion" runat="server" Text="Mostar detalles de la sesión en esta misma ventana" Height="51px" Width="593px" OnClick="btnMostrarDetallesSesion_Click" />
            
            </td>
            <td>
                <asp:Panel ID="PanelDetallesSesion" runat="server">
                    <h4>
                        Detalles de la sesión
                    </h4>
                     <table>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Beneficiarios"></asp:Label>
                    <asp:ListView ID="ListView1" runat="server"></asp:ListView>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Especialistas"></asp:Label>
                    <asp:ListView ID="ListView2" runat="server"></asp:ListView>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Comentario"></asp:Label>
                    <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Localidad:"></asp:Label>
                    <asp:Label ID="lblLocalidad" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnComentar" runat="server" Text="Comentar" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
                </td>
            </tr>
        </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
    
</asp:Content>
