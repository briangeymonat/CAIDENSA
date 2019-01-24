<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEstadisticasDiagnosticos.aspx.cs" Inherits="Ejemplo.Web.vEstadisticasDiagnosticos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:TAble runat="server">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell HorizontalAlign="left" Style="padding-left: 80px;">                
                <h2 >Duración promedio de tratamiento según diagnóstico
                </h2>
                <asp:TAble runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="grdDiagnosticoDuracion" runat="server">
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
                </asp:TAble>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell >
                <asp:TAble runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Style="padding-left: 80px;">
                            <br />
                            <h2>Cantidad de beneficiarios por diagnóstico según año
                            </h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow >
                        <asp:TableCell HorizontalAlign="Left"  Style="padding-left: 80px;">
                            <asp:DropDownList ID="ddlAños" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAños_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Chart ID="Chart2" runat="server"> 
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:TAble>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell >
                <asp:TAble runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Style="padding-left: 80px;">
                            <h2>Cantidad de beneficiarios por año según diagnóstico
                            </h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left"  Style="padding-left: 80px;">
                            <asp:DropDownList ID="ddlDiagnosticos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiagnosticos_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Chart ID="Chart1" runat="server">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:TAble>
            </asp:TableCell>
        </asp:TableRow>
    </asp:TAble>
</asp:Content>
