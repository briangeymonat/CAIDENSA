<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeRedactar.aspx.cs" Inherits="Ejemplo.Web.vInformeRedactar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left: 50px">Redactar Informe
    </h2>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label1" runat="server" Text="Tipo:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblTipo" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <br />
                <asp:Label ID="Label13" runat="server" Text="Título:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <br />
                <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" Style="padding-left: 50px">
                <br />
                <asp:Label ID="Label18" runat="server" Text="Beneficiario:" Font-Bold="true" Font-Size="12"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <br />
                <asp:Label ID="Label4" runat="server" Text="Nombres:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <br />
                <asp:Label ID="lblNombres" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label6" runat="server" Text="Apellidos:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblApellidos" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label2" runat="server" Text="Cédula de identidad:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblCI" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label8" runat="server" Text="Fecha de nacimiento:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblFechaNac" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label10" runat="server" Text="Edad cronológica:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblEdad" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label12" runat="server" Text="Motivo de consulta:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblMotivoConsulta" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label14" runat="server" Text="Escolaridad:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblEscolaridad" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label16" runat="server" Text="Encuadre:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblEncuadre" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label17" runat="server" Text="Ulitmo diagnóstico:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblUltimoDiagnostico" runat="server"></asp:Label><!-- Solo mostrar, no va en el informe-->
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <br />
                <asp:Label ID="Label15" runat="server" Text="Diagnóstico:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Panel ID="PanelDiagnosticosInformeRedactar" runat="server" ScrollBars="None" Height="250px">
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell Style="padding-left: 50px">
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Todos los diagnósticos:" Font-Bold="true"></asp:Label>
                    <asp:Panel ID="PanelTodosDiagnosticos" runat="server" ScrollBars="Vertical" Height="200px">
                        <asp:GridView ID="grdTodosDiagnosticos" runat="server" AutoGenerateSelectButton="True" OnRowCreated="grdTodosDiagnosticos_RowCreated" OnSelectedIndexChanging="grdTodosDiagnosticos_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Style="padding-left: 50px">
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Diagnósticos agregados:" Font-Bold="true"></asp:Label>
                    <asp:Panel ID="PanelDiagnosticosAgregados" runat="server" ScrollBars="Vertical" Height="200px">
                        <asp:GridView ID="grdDiagnosticosAgregados" runat="server" AutoGenerateDeleteButton="True" OnRowCreated="grdDiagnosticosAgregados_RowCreated" OnRowDeleting="grdDiagnosticosAgregados_RowDeleting" ShowHeaderWhenEmpty="True">
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
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <asp:Table runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell Width="50%">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px" Width="50%" ColumnSpan="2">
                            <asp:CheckBox ID="cbAntecedentesPatologicos" runat="server" Text="Antecedentes patológicos:" Font-Bold="true" AutoPostBack="True" OnCheckedChanged="cbAntecedentesPatologicos_CheckedChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:TextBox ID="txtAntecedentesPatologicos" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px" Width="100%">
                            <asp:CheckBox ID="cbDesarrollo" runat="server" Text="Desarrollo:" Font-Bold="true" AutoPostBack="True" OnCheckedChanged="cbDesarrollo_CheckedChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:TextBox ID="txtDesarrollo" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:Label ID="Label7" runat="server" Text="Presentación:" Font-Bold="true" Width="100%"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:TextBox ID="txtPresentacion" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:Label ID="lblAbordajePerfil" runat="server" Text="Abordaje" Font-Bold="true" Width="100%"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:TextBox ID="txtAbordaje" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:Label ID="Label11" runat="server" Text="En SUMA:" Font-Bold="true" Width="100%"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:TextBox ID="txtEnsuma" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:CheckBox ID="cbSugerencia" runat="server" Text="Sugerencias:" Font-Bold="true" AutoPostBack="True" OnCheckedChanged="cbSugerencia_CheckedChanged" Width="100%" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px">
                            <asp:TextBox ID="txtSugerencia" runat="server" TextMode="MultiLine" Height="130px" Width="100%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px" Width="50%">
                            <asp:Button ID="btnSalirSinGuardar" runat="server" Text="Salir sin guardar cambios de mis secciones" OnClick="btnSalirSinGuardar_Click" />
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios de mis secciones" OnClick="btnGuardar_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="padding-left: 50px" Width="50%">
                            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar mis secciones" OnClick="btnFinalizar_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell  Width="50%" HorizontalAlign="Left" Style="padding-left:50px" >
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="500px" Style="vertical-align:middle">
                <asp:Label ID="lblAbordajes" runat="server" Width="100%"></asp:Label>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
