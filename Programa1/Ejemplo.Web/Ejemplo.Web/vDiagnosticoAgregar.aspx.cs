using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vDiagnosticoAgregar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.btnCancelar.Visible = false;
                this.btnEliminar.Visible = false;
                this.btnModificar.Visible = false;
                this.btnAgregar.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.btnCancelar.Visible = false;
            this.btnEliminar.Visible = false;
            this.btnModificar.Visible = false;
            this.btnAgregar.Visible = true;
            this.lblMensaje.Text = string.Empty;
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            this.btnCancelar.Visible = true;
            this.btnEliminar.Visible = true;
            this.btnModificar.Visible = true;
            this.btnAgregar.Visible = false;
            this.lblMensaje.Text = string.Empty;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            this.btnCancelar.Visible = false;
            this.btnEliminar.Visible = false;
            this.btnModificar.Visible = false;
            this.btnAgregar.Visible = true;
            this.lblMensaje.Text = "Modificado correctamente";
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            this.btnCancelar.Visible = false;
            this.btnEliminar.Visible = false;
            this.btnModificar.Visible = false;
            this.btnAgregar.Visible = true;
            this.lblMensaje.Text = "Eliminar correctamente";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.btnCancelar.Visible = false;
            this.btnEliminar.Visible = false;
            this.btnModificar.Visible = false;
            this.btnAgregar.Visible = true;
            this.lblMensaje.Text = "Agregado correctamente";
        }
    }
}