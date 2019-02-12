<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vRestablecerItinerario.aspx.cs" Inherits="Ejemplo.Web.vRestablecerItinerario"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Style/Style1.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function Click()
        {
            document.getElementById("btnConfirm").click();
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Se eliminarán todos los itinerarios, ¿está seguro?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);


        }
</script>
</head>
<body onload="Click()"> 
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnConfirm" runat="server" OnClick = "OnConfirm" Text = " " OnClientClick = "Confirm()"/>
    </div>
    </form>
</body>
</html>
