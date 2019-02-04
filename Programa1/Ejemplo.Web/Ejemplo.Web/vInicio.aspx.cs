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
                    /*string script = "resolucion();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

                    int iWidth = int.Parse(Request.Form["width"]);
                    int iHeight = int.Parse(Request.Form["height"]);
                    int iH = Request.Browser.ScreenPixelsHeight;
                    int iW = Request.Browser.ScreenPixelsWidth;*/
                    int iLeft = (1920 / 2) - (230);
                    int iTop = (1080 / 2) - (65);
                    string sVtn = "window.open('vRestablecerItinerario.aspx','Restablecer itinerario','scrollbars=yes,resizable=no,height=130, width=460,top=" + iTop + ", left=" + iLeft + "')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);
                }
            }
        }

    }
}