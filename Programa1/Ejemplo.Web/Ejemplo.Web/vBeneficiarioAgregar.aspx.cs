
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
    public partial class vBeneficiarioAgregar : System.Web.UI.Page
    {
        private static List<cPlan> LosPlanes;
        private static List<string> LosTipos = new List<string> { "ASSE", "AYEX", "CAMEC", "Círculo Católico", "MIDES", "Particular", "Policial" };
        private static bool bPensionista;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ActualizarTodo();
                bPensionista = false;
            }
        }
        private void ActualizarTodo()
        {
            ActualizarDdlTipos();
            LimpiarCamposBeneficiario();
            LimpiarCamposPlan();
            LosPlanes = new List<cPlan>();
            ActualizarGrdPlanes();
            lblMensajePlan.Text = string.Empty;
        }


        private void ActualizarGrdPlanes()
        {
            grdPlanes.DataSource = LosPlanes;
            grdPlanes.DataBind();
            grdPlanes.SelectedIndex = -1;
        }
        private void ActualizarDdlTipos()
        {
            ddlTipoPlanes.DataSource = LosTipos;
            ddlTipoPlanes.DataBind();
        }


        private void LimpiarCamposBeneficiario()
        {
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtCi.Text = string.Empty;
            txtFechaNac.Text = string.Empty;
            txtDomicilio.Text = string.Empty;
            txtTel1.Text = string.Empty;
            txtTel2.Text = string.Empty;
            txtAtributario.Text = string.Empty;
            cbPensionista.Checked = false;
            txtMotivoConsulta.Text = string.Empty;
            txtEscolaridad.Text = string.Empty;
            txtDerivador.Text = string.Empty;
        }
        private void LimpiarCamposPlan()
        {
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            cbTratamiento.Checked = false;
            cbEvaluacion.Checked = false;
            ddlTipoPlanes.SelectedIndex = -1;
        }


        private bool FaltanDatosBeneficiario()
        {
            if (txtNombres.Text == string.Empty ||
                txtApellidos.Text == string.Empty ||
                txtCi.Text == string.Empty ||
                txtFechaNac.Text == string.Empty ||
                (txtAtributario.Text == string.Empty && !cbPensionista.Checked) ||
                txtMotivoConsulta.Text == string.Empty)
            {
                return true;
            }
            return false;
        }
        private bool FaltanDatosPlan()
        {
            if (txtDesde.Text == string.Empty ||
                (cbTratamiento.Checked == false && cbEvaluacion.Checked == false))
            {
                return true;
            }
            return false;
        }

        protected void btnAgregarBeneficiario_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosBeneficiario())
            {
                cBeneficiario unBeneficiario = new cBeneficiario();
                unBeneficiario.Nombres = txtNombres.Text;
                unBeneficiario.Apellidos = txtApellidos.Text;
                unBeneficiario.CI = int.Parse(txtCi.Text);
                unBeneficiario.FechaNacimiento = txtFechaNac.Text;
                unBeneficiario.Domicilio = txtDomicilio.Text;
                unBeneficiario.Telefono1 = txtTel1.Text;
                unBeneficiario.Telefono2 = txtTel2.Text;
                if (rblSexo.SelectedItem.Text == "Masculino")
                {
                    unBeneficiario.Sexo = "M";
                }
                else
                {
                    unBeneficiario.Sexo = "F";
                }
                if (cbPensionista.Checked)
                {
                    unBeneficiario.Atributario = "Pensionista";
                }
                else
                {
                    unBeneficiario.Atributario = txtAtributario.Text;
                }
                unBeneficiario.MotivoConsulta = txtMotivoConsulta.Text;
                unBeneficiario.Escolaridad = txtEscolaridad.Text;
                unBeneficiario.Derivador = txtDerivador.Text;
                unBeneficiario.Email = txtEmail.Text;
                unBeneficiario.Estado = true;

                unBeneficiario.lstPlanes = LosPlanes;
                if (dFachada.BeneficiarioTraerEspecificoCI(unBeneficiario) != null)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Ya existe un beneficiario en el sistema con esa CI.')", true);
                }
                else
                {
                    if (dFachada.BeneficiarioAgregar(unBeneficiario))
                    {
                        
                        lblMensajeBeneficiario.Text = "Beneficiario agregado correctamente.";
                        ActualizarTodo();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo concretar el registro del beneficiario.')", true);
                        ActualizarTodo();
                    }
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltaron completar datos para el registro.')", true);
            }
        }

        protected void btnAgregarPlan_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosPlan())
            {
                if (LosPlanes == null)
                {
                    LosPlanes = new List<cPlan>();
                }
                cPlan unPlan = new cPlan();
                unPlan.Activo = true;
                unPlan.Evaluacion = cbEvaluacion.Checked;
                unPlan.Tratamiento = cbTratamiento.Checked;
                unPlan.Tipo = ddlTipoPlanes.SelectedItem.Text;
                if (txtHasta.Text != string.Empty)
                {
                    unPlan.FechaFin = txtHasta.Text;
                }
                if (txtDesde.Text != string.Empty)
                {
                    unPlan.FechaInicio = txtDesde.Text;
                }
                LosPlanes.Add(unPlan);
                lblMensajePlan.Text = "Plan agregado correctamente al beneficiario";
                ActualizarGrdPlanes();
                LimpiarCamposPlan();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan registrar datos para agregar el plan.')", true);
            }

        }

        protected void cbPensionista_CheckedChanged(object sender, EventArgs e)
        {
            bPensionista = !bPensionista;
            txtAtributario.Enabled = !bPensionista;
            if (bPensionista)
            {
                txtAtributario.Text = string.Empty;
            }
        }

        protected void grdPlanes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[7].Visible = false; //activo
        }

        protected void grdPlanes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LosPlanes.RemoveAt(e.RowIndex);
            ActualizarGrdPlanes();
        }
    }
}