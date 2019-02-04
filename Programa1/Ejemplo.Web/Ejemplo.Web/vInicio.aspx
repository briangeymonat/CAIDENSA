<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vInicio.aspx.cs" Inherits="Ejemplo.Web.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
           function resolucion()
           {
               var width = screen.width;
               var height = screen.height;
               document.forms[0].appendChild(width);
               document.forms[0].appendChild(height);
           }


        function disableBackButton()
        {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >

</asp:Content>
