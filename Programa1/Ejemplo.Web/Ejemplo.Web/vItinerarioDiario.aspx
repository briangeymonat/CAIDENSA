<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vItinerarioDiario.aspx.cs" Inherits="Ejemplo.Web.vItinerarioDiario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
            function btnMostrarSesion(id) {
                    $.ajax({
                            type: "POST",
                    url: "vItinerarioDiario.aspx/MostrarSesion",
                    data: '{parCodigo: ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: resultado,
                    error: errores
                });
            };
        function resultado(msg) {
            //msg.d tiene el resultado devuelto por el método
            $('#num3').val(msg.d);
        }
        function errores(msg) {
            //msg.responseText tiene el mensaje de error enviado por el servidor
            alert('Error: ' + msg.responseText);
        }
                    </script>
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
                <asp:DropDownList ID="ddlDias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDias_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Localidad:"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rdblCentro" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblCentro_SelectedIndexChanged" AutoPostBack="true">
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
                <asp:GridView ID="grdItinerario" runat="server" OnRowDataBound="grdItinerario_RowDataBound"></asp:GridView>
                <asp:DataGrid ID="dtGrdItinerario" runat="server"></asp:DataGrid>                
            
            </td>
            <td>
                <asp:DropDownList ID="ddlEspecialistas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialistas_SelectedIndexChanged"></asp:DropDownList>
                <asp:GridView ID="grdItinerarioPorEspecialista" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdItinerarioPorEspecialista_SelectedIndexChanging"></asp:GridView>
            </td>            
        </tr>
    </table>
    
    
</asp:Content>
