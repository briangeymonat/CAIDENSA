<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEstadisticasCantidadSesionesPorTecnico.aspx.cs" Inherits="Ejemplo.Web.vEstadisticasCantidadSesionesPorTecnico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 80px;">Cantidad de sesiones de técnicos según tipo de sesión
    </h2>
    <asp:Table runat="server">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Width="100%" HorizontalAlign="Center" Style="padding-left: 80px;">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="Label1" runat="server" Text="Mes: " Style="padding-left: 25px;"></asp:Label>
                            <asp:DropDownList ID="ddlMeses" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" Text="Año: " Style="padding-left: 15px;"></asp:Label>
                            <asp:DropDownList ID="ddlAños" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAños_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:TextBox ID="txtBuscarEspecialista" runat="server" PlaceHolder="Buscar técnico" OnTextChanged="txtBuscarEspecialista_TextChanged" Width="50%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:GridView ID="grdTecnicosCantidadSesion" runat="server" ShowHeaderWhenEmpty="true">
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
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
