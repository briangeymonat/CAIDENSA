<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInformeNuevo.aspx.cs" Inherits="Ejemplo.Web.vInformeNuevo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="padding-left:50px" >
        Nuevo informe
    </h2>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label1" runat="server" Text="Tipo:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">  
                <br />              
                <asp:Label ID="Label12" runat="server" Text="Beneficiario:" Font-Bold="true" Font-Size="12"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <br />
                <asp:Label ID="Label3" runat="server" Text="Nombres:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <br />
                <asp:Label ID="lblNombres" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label4" runat="server" Text="Apellidos:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblApellidos" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Style="padding-left: 50px">
                <asp:Label ID="Label13" runat="server" Text="Cédula de identidad:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblCI" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label5" runat="server" Text="Fecha de nacimiento:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblFechaNac" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label6" runat="server" Text="Edad cronológica:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblEdad" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label7" runat="server" Text="Motivo de consulta:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblMotivoConsulta" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label8" runat="server" Text="Escolaridad:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblEscolaridad" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <asp:Label ID="Label9" runat="server" Text="Encuadre:" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblEncuadre" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px">
                <br />
                <asp:Label ID="Label10" runat="server" Text="Especialidad" Font-Bold="true"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <br />
                <asp:DropDownList ID="ddlEspecialidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table runat="server">
        <asp:TableRow VerticalAlign="Top">
            <asp:TableCell style="padding-left:50px">
                <br />
                <asp:Label ID="Label2" runat="server" Text="Todos los especialistas:" Font-Bold="true"></asp:Label>
                <asp:GridView ID="grdTodosEspecialistas" runat="server" OnRowCreated="grdTodosEspecialistas_RowCreated" AutoGenerateSelectButton="True" OnSelectedIndexChanging="grdTodosEspecialistas_SelectedIndexChanging" ShowHeaderWhenEmpty="True" Width="379px">
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
            <asp:TableCell style="padding-left:50px" >
                <br />
                <asp:Label ID="Label11" runat="server" Text="Especialistas agregados:" Font-Bold="true"></asp:Label>
                <asp:GridView ID="grdEspecialistasAgregados" runat="server" AutoGenerateDeleteButton="True" OnRowCreated="grdEspecialistasAgregados_RowCreated" OnRowDeleting="grdEspecialistasAgregados_RowDeleting" ShowHeaderWhenEmpty="True" Width="379px">
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
        <asp:TableRow>
            <asp:TableCell style="padding-left:50px" ColumnSpan="3">
                <br />
                <asp:Button ID="btnRealizarInforme" runat="server" Text="Realizar Informe" OnClick="btnRealizarInforme_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
