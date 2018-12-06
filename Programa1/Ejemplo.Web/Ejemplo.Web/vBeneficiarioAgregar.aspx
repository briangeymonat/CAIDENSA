<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vBeneficiarioAgregar.aspx.cs" Inherits="Ejemplo.Web.vBeneficiarioAgregar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Agregar beneficiario
    </h3>
    <br />
    <table>
        <tr>
            <td>

    <table class="table">
        <tr>
            <td>
                <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCi" runat="server" Text="Cédula de identidad:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCi" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de nacimiento:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaNac" runat="server" TextMode="Date"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Sexo:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Masculino" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Femenino"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDomicilio" runat="server" Text="Domicilio:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDomicilio" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTel1" runat="server" Text="Teléfono 1:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTel1" runat="server" TextMode="Phone"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTel2" runat="server" Text="Teléfono 2:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTel2" runat="server" TextMode="Phone"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAtributario" runat="server" Text="Atributario:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAtributario" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBox ID="cbPensionista" runat="server" Text="Pensionista" AutoPostBack="True" OnCheckedChanged="cbPensionista_CheckedChanged"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMotivoConsulta" runat="server" Text="Motivo de consulta:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMotivoConsulta" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEscolaridad" runat="server" Text="Escolaridad:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEscolaridad" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDerivador" runat="server" Text="Persona que lo deriva:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDerivador" runat="server"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="Correo electronico:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
            </td>
            <td>
                *
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAgregarBeneficiario" runat="server" Text="Agregar beneficiario" OnClick="btnAgregarBeneficiario_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMensajeBeneficiario" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
            </td>
            <td>
                    <div id="divAgregarNuevoPlan" class="VentanaEmergente">
                        <h4>Agregar Plan
                        </h4>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlTipoPlanes" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbTratamiento" runat="server" Text="Tratamiento" />

                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbEvaluacion" runat="server" Text="Evaluación" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDesde" runat="server" Text="Desde:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesde" runat="server" TextMode="Date"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHasta" runat="server" Text="Hasta:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHasta" runat="server" TextMode="Date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMensajePlan" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button ID="btnAgregarPlan" runat="server" Text="Agregar Plan" OnClick="btnAgregarPlan_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Planes agregados"></asp:Label>
                <asp:GridView ID="grdPlanes" runat="server" 
                    AutoGenerateDeleteButton="true"
                    EmptyDataText="No se encuentran planes ingresados" ShowHeaderWhenEmpty="true"></asp:GridView>
            </td>
        </tr>
        
        </table>
    
</asp:Content>
