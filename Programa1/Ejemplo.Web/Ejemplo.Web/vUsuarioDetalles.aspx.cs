using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class DetallesUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.btnCancelar.Visible = false;
                this.btnConfirmar.Visible = false;
                this.btnHabilitar.Visible = false;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            this.btnModificar.Visible = false;
            this.btnInhabilitar.Visible = false;
            this.btnCancelar.Visible = true;
            this.btnConfirmar.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.btnModificar.Visible = true;
            this.btnInhabilitar.Visible = true;
            this.btnCancelar.Visible = false;
            this.btnConfirmar.Visible = false;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.btnModificar.Visible = true;
            this.btnInhabilitar.Visible = true;
            this.btnCancelar.Visible = false;
            this.btnConfirmar.Visible = false;
            this.lblMensaje.Text = "Se modificó correctamente";
        }
    }
}