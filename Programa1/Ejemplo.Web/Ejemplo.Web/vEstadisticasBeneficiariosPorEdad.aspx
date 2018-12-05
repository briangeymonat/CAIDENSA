<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEstadisticasBeneficiariosPorEdad.aspx.cs" Inherits="Ejemplo.Web.vEstadisticasBeneficiariosPorEdad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Cantidad de beneficiarios por rango de edad 
    </h3>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Cantidad de beneficiarios que su edad esta dentro del rango definido: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCantidadBeneficiarios" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Beneficiarios:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdBeneficiarios" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Rango de edad:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Desde:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                        <td>
                             <asp:Button ID="btnAplicar" runat="server" Text="Aplicar" />
                        </td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
