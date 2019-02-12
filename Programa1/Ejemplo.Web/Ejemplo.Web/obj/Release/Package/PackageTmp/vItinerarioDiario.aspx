<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vItinerarioDiario.aspx.cs" Inherits="Ejemplo.Web.vItinerarioDiario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 50px">Itinerario diario
    </h2>
    <asp:Table runat="server" Width="100%">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell ColumnSpan="2" Width="50%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label1" runat="server" Text="Seleccionar día:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlDias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDias_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell >
                            <asp:Label ID="Label2" runat="server" Text="Localidad:" Font-Bold="true" Style="margin-left:15px"> </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:RadioButtonList ID="rdblCentro" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblCentro_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Juan Lacaze" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Nueva Helevecia"></asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <p style="margin-left:50px; font-size:16px;">Referencias:</p>
                        </asp:TableCell>
                        <asp:TableCell>
                                        <p style="background-color:#8afa38;" runat="server">AYEX</p>
                        </asp:TableCell>
                        <asp:TableCell>
                                        <p style="background-color:#58FAF4;" runat="server">FONASA</p>
                        </asp:TableCell>
                        <asp:TableCell>
                                        <p style="background-color:#F3F781;" runat="server">MIDES</p>
                        </asp:TableCell>
                        <asp:TableCell>
                                        <p style="background-color:#FE9A2E;" runat="server">Particular</p>
                        </asp:TableCell>
                        <asp:TableCell>
                                        <p style="background-color:#58FAF4;" runat="server">Policial</p>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell Width="20%">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblItinerariosXEspecialista" runat="server" Text="Itinerarios por especialista:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:DropDownList ID="ddlEspecialistas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialistas_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:GridView ID="grdItinerarioPorEspecialista" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdItinerarioPorEspecialista_SelectedIndexChanging">
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
            <asp:TableCell Width="80%">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="pnlItinerario" runat="server" ScrollBars="Vertical" Height="600px" Width="100%">
                            <formview id="frmItinerario" runat="server"></formview>

                            </asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>


</asp:Content>
