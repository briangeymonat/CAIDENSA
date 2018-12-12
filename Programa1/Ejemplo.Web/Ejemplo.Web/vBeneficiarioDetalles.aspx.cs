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
    public partial class vBeneficiarioDetalles : System.Web.UI.Page
    {
        private static List<string> Tipos = new List<string> { "ASSE", "AYEX", "CAMEC", "Círculo Católico", "MIDES", "Particular", "Policial" };
        private static cBeneficiario ElBeneficiario;
        private static List<cDiagnostico> lstUltimosDiagnosticos;
        private static List<cDiagnostico> lstHistorialDiagnosticos;
        private static List<cPlan> lstPlanesActivos;
        private static List<cPlan> lstPlanesInactivos;
        private static List<cInforme> lstInformes;
        private static bool Pensionista;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ElBeneficiario = new cBeneficiario();
                ElBeneficiario.Codigo = int.Parse(Request.QueryString["BeneficiarioId"]);
                habilitarCampos(false);
                this.PanelAgregarPlan.Visible = false;
                this.btnOcultar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnConfirmar.Visible = false;
                ActualizarTodo();
            }
        }
        private void ActualizarTodo()
        {
            ActualizarDatos();
            ActualizarGrids();
            ActualizarCampos();
        }

        private void ActualizarDatos()
        {
            ElBeneficiario = dFachada.BeneficiarioTraerEspecifico(ElBeneficiario);
            lstPlanesActivos = dFachada.PlanTraerActivosPorBeneficiario(ElBeneficiario);
            lstPlanesInactivos = dFachada.PlanTraerInactivosPorBeneficiario(ElBeneficiario);
            ddlTipos.DataSource = Tipos;
            ddlTipos.DataBind();
        }

        private void ActualizarGrids()
        {
            grdUltimosDiagnosticos.DataSource = lstUltimosDiagnosticos;
            grdUltimosDiagnosticos.DataBind();
            grdHistorialDiagnosticos.DataSource = lstHistorialDiagnosticos;
            grdHistorialDiagnosticos.DataBind();
            grdPlanesActivos.DataSource = lstPlanesActivos;
            grdPlanesActivos.DataBind();
            grdPlanesInactivos.DataSource = lstPlanesInactivos;
            grdPlanesInactivos.DataBind();
            grdInformes.DataSource = lstInformes;
            grdInformes.DataBind();
        }

        private void ActualizarCampos()
        {
            txtNombres.Text = ElBeneficiario.Nombres;
            txtApellidos.Text = ElBeneficiario.Apellidos;
            txtCi.Text = ElBeneficiario.CI.ToString();
            if (ElBeneficiario.Sexo == "M") rblSexo.Items[0].Selected = true; else rblSexo.Items[1].Selected = true;
            txtFechaNac.Text = ElBeneficiario.FechaNacimiento.ToString("yyyy-MM-dd");
            txtDomicilio.Text = ElBeneficiario.Domicilio;
            txtTelefono1.Text = ElBeneficiario.Telefono1;
            txtTelefono2.Text = ElBeneficiario.Telefono2;
            txtEmail.Text = ElBeneficiario.Email;
            if (ElBeneficiario.Atributario == "Pensionista")
            {
                cbPensionista.Checked = true;
                cbPensionista_CheckedChanged(new object(), new EventArgs());
                //txtAtributario.Text = string.Empty;
                //txtAtributario.Enabled = false;
                //cbPensionista.Checked = true;
            }
            else
            {
                cbPensionista.Checked = false;
                cbPensionista_CheckedChanged(new object(), new EventArgs());
                txtAtributario.Text = ElBeneficiario.Atributario;
            }

            txtEscolaridad.Text = ElBeneficiario.Escolaridad;
            txtDerivador.Text = ElBeneficiario.Derivador;
            txtMotivoConsulta.Text = ElBeneficiario.MotivoConsulta;
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
            cbPensionista_CheckedChanged(new object(), new EventArgs());
            this.btnCancelar.Visible = true;
            this.btnConfirmar.Visible = true;
            this.btnInhabilitar.Visible = false;
            this.btnHabilitar.Visible = false;
            this.btnModificar.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ActualizarCampos();
            habilitarCampos(false);
            cbPensionista_CheckedChanged(new object(), new EventArgs());
            this.btnCancelar.Visible = false;
            this.btnConfirmar.Visible = false;
            this.btnInhabilitar.Visible = true;
            this.btnHabilitar.Visible = true;
            this.btnModificar.Visible = true;
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
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosBeneficiario())
            {
                cBeneficiario unBeneficiario = new cBeneficiario();
                unBeneficiario.Codigo = ElBeneficiario.Codigo;
                unBeneficiario.Nombres = txtNombres.Text;
                unBeneficiario.Apellidos = txtApellidos.Text;
                unBeneficiario.CI = int.Parse(txtCi.Text);
                unBeneficiario.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                unBeneficiario.Domicilio = txtDomicilio.Text;
                unBeneficiario.Telefono1 = txtTelefono1.Text;
                unBeneficiario.Telefono2 = txtTelefono2.Text;
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
                if (dFachada.BeneficiarioTraerEspecificoVerificarModificar(unBeneficiario) != null)
                {
                    lblMensaje.Text = "Ya existe un beneficiario en el sistema con esa CI.";
                }
                else
                {
                    if (dFachada.BeneficiarioModificar(unBeneficiario))
                    {
                        lblMensaje.Text = "Beneficiario modificado correctamente.";
                        ActualizarTodo();
                        habilitarCampos(false);
                        this.btnCancelar.Visible = false;
                        this.btnConfirmar.Visible = false;
                        this.btnInhabilitar.Visible = true;
                        this.btnHabilitar.Visible = true;
                        this.btnModificar.Visible = true;
                    }
                    else
                    {
                        lblMensaje.Text = "No se pudo concretar la modificación del beneficiario.";
                        ActualizarTodo();
                    }
                }

            }
            else
            {
                lblMensaje.Text = "Faltaron completar datos para el registro.";
            }
        }


        protected void habilitarCampos(bool b)
        {
            this.txtNombres.Enabled = b;
            this.txtApellidos.Enabled = b;
            this.txtCi.Enabled = b;
            this.txtAtributario.Enabled = b;
            this.cbPensionista.Enabled = b;
            this.txtDerivador.Enabled = b;
            this.txtDomicilio.Enabled = b;
            this.txtEscolaridad.Enabled = b;
            this.txtFechaNac.Enabled = b;
            this.txtEmail.Enabled = b;
            this.txtMotivoConsulta.Enabled = b;
            this.txtTelefono1.Enabled = b;
            this.txtTelefono2.Enabled = b;
            this.rblSexo.Enabled = b;
        }

        private void LimpiarCamposPlan()
        {
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            cbTratamiento.Checked = false;
            cbEvaluacion.Checked = false;
            ddlTipos.SelectedIndex = -1;
        }

        private bool FaltanDatosPlan()
        {
            if (txtDesde.Text == string.Empty ||
                /*txtHasta.Text == string.Empty ||*/
                (cbTratamiento.Checked == false && cbEvaluacion.Checked == false))
            {
                return true;
            }
            return false;
        }
        protected void btnAgregarPlan_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosPlan())
            {
                cPlan unPlan = new cPlan();
                unPlan.Activo = true;
                unPlan.Evaluacion = cbEvaluacion.Checked;
                unPlan.Tratamiento = cbTratamiento.Checked;
                unPlan.Tipo = ddlTipos.SelectedItem.Text;
                if (txtHasta.Text != string.Empty)
                {
                    unPlan.FechaFin = DateTime.Parse(txtHasta.Text);
                }
                if (txtDesde.Text != string.Empty)
                {
                    unPlan.FechaInicio = DateTime.Parse(txtDesde.Text);
                }
                ElBeneficiario.lstPlanes = new List<cPlan>();
                ElBeneficiario.lstPlanes.Add(unPlan);
                if (dFachada.PlanAgregar(ElBeneficiario))
                {
                    lblMensajeAgregarPlan.Text = "Plan agregado correctamente al beneficiario";
                    LimpiarCamposPlan();
                    ActualizarTodo();
                }

            }
            else
            {
                lblMensajeAgregarPlan.Text = "Faltan ingresar datos para registrar el plan.";
            }
        }

        protected void grdPlanesActivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dFachada.PlanEliminar(lstPlanesActivos[e.RowIndex]);
        }

        protected void cbPensionista_CheckedChanged(object sender, EventArgs e)
        {
            Pensionista = cbPensionista.Checked;
            txtAtributario.Enabled = !Pensionista;
            if (Pensionista)
            {
                txtAtributario.Text = string.Empty;
            }
        }
    }
}