<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vUsuarioMostrar.aspx.cs" Inherits="Ejemplo.Web.vUsuarioMostrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Usuarios
    </h3>
    <table class="table">
        <tr>
            <td style="width:300px">
                <asp:TextBox ID="txtBuscarBeneficiario" runat="server" Width="100%" PlaceHolder="Buscar por CI, Nombre o Apellido"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblFiltros" runat="server" Text="Filtros:"  style="margin-left:50px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Usuarios activos"></asp:Label>
                <asp:GridView ID="grdUsuariosActivos" runat="server"></asp:GridView>
                <asp:Label ID="Label2" runat="server" Text="Usuarios inactivos"></asp:Label>
                <asp:GridView ID="grdUsuariosInactivos" runat="server"></asp:GridView>
            </td>
            <td>
                <table class="table" style="width:200px">
                    <tr>
                        <td>
                            <asp:Label ID="lblEspecialidades" runat="server" Text="Especialidades:"   style="margin-left:50px"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:CheckBox ID="cbFisioterapeuta" runat="server" Text="Fisioterapeuta"   style="margin-left:50px"/>
                        </td>
                    </tr>
                     <tr>
                        <td>
                             <asp:CheckBox ID="cbFonoaudiologo" runat="server" Text="Fonoaudiólogo"   style="margin-left:50px"/>
                        </td>
                    </tr>
                     <tr>
                        <td>
                             <asp:CheckBox ID="cbPedagogo" runat="server" Text="Pedagogo"  style="margin-left:50px"/>
                        </td>
                    </tr>
                     <tr>
                        <td>
                             <asp:CheckBox ID="cbPsicolgo" runat="server" Text="Psicólogo"  style="margin-left:50px"/>
                        </td>
                    </tr>
                     <tr>
                        <td>
                             <asp:CheckBox ID="cbPsicomotricista" runat="server" Text="Psicomotricista"  style="margin-left:50px"/>
                        </td>
                    </tr>
                     <tr>
                        <td>
                             <asp:CheckBox ID="cbSinEspecialidad" runat="server" Text="Sin especialidad"  style="margin-left:50px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:"  style="margin-left:50px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:CheckBox ID="CheckBox1" runat="server" Text="Administrador"  style="margin-left:50px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:CheckBox ID="CheckBox2" runat="server" Text="Administrativo"  style="margin-left:50px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:CheckBox ID="CheckBox3" runat="server" Text="Especialista" style="margin-left:50px"/>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar filtros"   style="margin-left:50px"/>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
