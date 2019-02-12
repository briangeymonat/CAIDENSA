<%@ Page Title="Informe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vReportePDF.aspx.cs" Inherits="Ejemplo.Web.vReportePDF" %>
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
</asp:Content>
