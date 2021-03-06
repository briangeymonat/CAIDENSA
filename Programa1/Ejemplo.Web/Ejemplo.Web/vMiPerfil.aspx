﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vMiPerfil.aspx.cs" Inherits="Ejemplo.Web.vMiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table runat="server" Width="100%">
        <asp:TableRow>
            
            <asp:TableCell HorizontalAlign="Left" Width="10%">
                <asp:ImageButton ID="btnAtras" runat="server" Text="Atrás" Style="margin-left:20px" ImageUrl="~/Img/atras.png" Width="50px" OnClick="btnAtras_Click"/>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Center" Width="80%">
    <h1>Mi perfil</h1>
            </asp:TableCell>
            <asp:TableCell Width="10%">

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table runat="server" Width="100%">
        <asp:TableRow VerticalAlign="Top" HorizontalAlign="Center">
            <asp:TableCell Width="25%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                            <asp:Label ID="Label1" runat="server" Text="Datos personales" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblNickname" runat="server" Text="Nickname:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNickName" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio1" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtNombres" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio2" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtApellidos" runat="server" Width="160px"></asp:TextBox>
                            <asp:Label ID="lblObligatorio3" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtCi" runat="server" Width="160px"></asp:TextBox><asp:Label ID="lblObligatorio4" runat="server" Text="*" CssClass="camposObligatorios"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDomicilio" runat="server" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTel" runat="server" Text="Telefono:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtTelefono" runat="server" TextMode="Phone" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="160px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlTipoUsuario" runat="server" Enabled="False" Width="160px"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" Width="160px"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblVinculo" runat="server" Text="Vínculo:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:RadioButtonList ID="rblTipoDeEmpleado" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Empleado</asp:ListItem>
                                <asp:ListItem>Contratado</asp:ListItem>
                                <asp:ListItem>Socio</asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="6">
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="button" OnClick="btnConfirmar_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell Width="50%">
                <div>
                        <asp:Label ID="lblItinerario" runat="server" Text="Itinerario semanal" Font-Bold="true" Font-Size="10"></asp:Label>
                    <asp:Panel ID="pnlItinerario" runat="server" Visible="false" ScrollBars="Vertical" Height="700px">
                        <formview id="frmItinerario" runat="server"></formview>
                    </asp:Panel>
                </div>
            </asp:TableCell>
            <asp:TableCell Width="25%">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell ID="ref">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:RadioButtonList ID="rblCalendario" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblCalendario_SelectedIndexChanged">
                                            <asp:ListItem Text="Itinerario semanal" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Sesiones reprogramdas"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" >
                                        <asp:TextBox ID="txtSemana" runat="server" TextMode="Week" Visible="false" AutoPostBack="true" OnTextChanged="txtSemana_TextChanged"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                            <p style="font-size:16px;">Referencias:</p>
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
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblBeneficiariosQueAtiende" runat="server" Text="Beneficiarios que atiende:" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="pnlBeneficiariosQueAtiende" runat="server" ScrollBars="Vertical" Height="150px">
                                <asp:GridView ID="grdBeneficiariosQueAtiende" runat="server" AutoGenerateSelectButton="True" Width="100%"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdBeneficiariosQueAtiende_RowCreated" OnSelectedIndexChanging="grdBeneficiariosQueAtiende_SelectedIndexChanging">
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

                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="lblInformesPendientes" runat="server" Text="Informes pendientes:" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="pnlInformesPendientes" runat="server" ScrollBars="Vertical" Height="150px">
                            <asp:GridView ID="grdInformesPendientes" runat="server" AutoGenerateSelectButton="True" Width="100%"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdInformesPendientes_RowCreated" OnSelectedIndexChanging="grdInformesPendientes_SelectedIndexChanging" ShowHeaderWhenEmpty="True">
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
                    <asp:TableRow>
                        <asp:TableCell>
                            <br />
                            <asp:Label ID="lblInformesRealizados" runat="server" Text="Informes realizados:" Font-Bold="true" Font-Size="10"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="pnlInformesRealizados" runat="server" ScrollBars="Vertical" Height="150px">
                            <asp:GridView ID="grdInformesRealizados" runat="server" Width="100%"
                                ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowCreated="grdInformesRealizados_RowCreated" ShowHeaderWhenEmpty="True"
                                OnSelectedIndexChanging="grdInformesRealizados_SelectedIndexChanging"
                                AutoGenerateSelectButton="true">
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
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
