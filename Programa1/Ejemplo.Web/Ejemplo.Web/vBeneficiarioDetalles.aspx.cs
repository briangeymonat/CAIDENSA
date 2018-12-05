using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vBeneficiarioDetalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                habilitarCampos(false);
                this.PanelAgregarPlan.Visible = false;
                this.btnOcultar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnConfirmar.Visible = false;
            }
        }

        protected void btnNuevoPlan_Click(object sender, EventArgs e)
        {
            this.PanelAgregarPlan.Visible = true;
            this.btnNuevoPlan.Visible = false;
            this.btnOcultar.Visible = true;
        }

        protected void btnOcultar_Click(object sender, EventArgs e)
        {
            this.PanelAgregarPlan.Visible = false;
            this.btnNuevoPlan.Visible = true;
            this.btnOcultar.Visible = false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            habilitarCampos(true);
            this.btnCancelar.Visible = true;
            this.btnConfirmar.Visible = true;
            this.btnInhabilitar.Visible = false;
            this.btnHabilitar.Visible = false;
            this.btnModificar.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            habilitarCampos(false);
            this.btnCancelar.Visible = false;
            this.btnConfirmar.Visible = false;
            this.btnInhabilitar.Visible = true;
            this.btnHabilitar.Visible = true;
            this.btnModificar.Visible = true;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            habilitarCampos(false);
            this.btnCancelar.Visible = false;
            this.btnConfirmar.Visible = false;
            this.btnInhabilitar.Visible = true;
            this.btnHabilitar.Visible = true;
            this.btnModificar.Visible = true;
            this.lblMensaje.Text = "Se modificó correctamente";
        }


        protected void habilitarCampos(bool b)
        {
            this.txtNombres.Enabled = b;
            this.txtApellidos.Enabled = b;
            this.txtCi.Enabled = b;
            this.txtAtributario.Enabled = b;
            this.txtDerivador.Enabled = b;
            this.txtDomicilio.Enabled = b;
            this.txtEscolaridad.Enabled = b;
            this.txtFechaNac.Enabled = b;
            this.txtMail.Enabled = b;
            this.txtMotivoConsulta.Enabled = b;
            this.txtTelefono1.Enabled = b;
            this.txtTelefono2.Enabled = b;
            this.rblSexo.Enabled = b;
        }
    }
}