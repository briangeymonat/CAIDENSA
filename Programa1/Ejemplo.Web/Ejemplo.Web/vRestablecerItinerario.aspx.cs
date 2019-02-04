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
    public partial class vRestablecerItinerario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }
        private void RestablecerItinerario()
        {

            List<cItinerario> lstItinerarios = dFachada.ItinerarioTraerTodos();
            if (lstItinerarios.Count > 0)
            {
                bool bResultado = dFachada.ItinerarioRestablecer();
                if (bResultado)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se ha restablecido el itinerario')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('No hay itinerarios registrados')", true);
            }



        }

        public void OnConfirm(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                RestablecerItinerario();

            }
            string sScript = "window.close();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", sScript, true);
        }
    }
}