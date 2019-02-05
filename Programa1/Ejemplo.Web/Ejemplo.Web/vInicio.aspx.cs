using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class Inicio : System.Web.UI.Page
    {

        //static cUsuario U;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dFachada.SesionAgregarSesionesDelDia();
                string sVentana = Request.QueryString["ventana"];
                if (sVentana == "si")
                {
                    string sVtn = "window.open('vRestablecerItinerario.aspx','Restablecer itinerario','scrollbars=yes,resizable=no,height=130, width=460')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);
                }
            }
        }

    }
}