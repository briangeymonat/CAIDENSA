using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vItinerarioDiario : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelDetallesSesion.Visible = false;
            }
        }
          
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            string vtn = "window.open('vDetallesSesion.aspx','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        protected void btnMostrarDetallesSesion_Click(object sender, EventArgs e)
        {
            this.PanelDetallesSesion.Visible = true;
        }
    }
}