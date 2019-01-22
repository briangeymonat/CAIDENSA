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
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" Text="Cantidad de beneficiarios que su edad esta dentro del rango definido: "></asp:Label>
                            <asp:Label ID="lblCantidadBeneficiarios" runat="server"></asp:Label>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Beneficiarios:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdBeneficiarios" runat="server" OnRowCreated="grdBeneficiarios_RowCreated">
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
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number" OnTextChanged="txtDesde_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Hasta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number" OnTextChanged="txtHasta_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>                           
                        <td>
                             <asp:Button ID="btnAplicar" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" />
                             <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
