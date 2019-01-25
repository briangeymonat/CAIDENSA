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
        private static cPlan PlanAModificar;
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
                txtAtributario.Enabled = false;
                this.PanelAgregarPlan.Visible = false;
                this.btnOcultar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnConfirmar.Visible = false;
                ActualizarTodo();
                PanelModificarPlan.Visible = false;
                if (ElBeneficiario.Estado)
                {
                    btnHabilitar.Visible = false;
                    btnInhabilitar.Visible = true;
                }
                else
                {
                    btnHabilitar.Visible = true;
                    btnInhabilitar.Visible = false;
                    btnNuevoPlan.Visible = false;
                    btnAgregarInforme.Visible = false;
                    btnModificar.Visible = false;
                }
                if (vMiPerfil.ii == 1)
                {
                    vMiPerfil.ii = 0;
                    btnAgregarInforme.Visible = false;
                    btnModificar.Visible = false;
                    btnCancelar.Visible = false;
                    btnHabilitar.Visible = false;
                    btnInhabilitar.Visible = false;
                    btnConfirmar.Visible = false;
                    btnNuevoPlan.Visible = false;
                    btnOcultar.Visible = false;
                    lblPlanesActivos.Visible = false;
                    lblHistorialPlanes.Visible = false;
                    grdPlanesActivos.Visible = false;
                    grdPlanesInactivos.Visible = false;

                }
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
            DateTime fn = DateTime.Parse(ElBeneficiario.FechaNacimiento);
            txtFechaNac.Text = fn.ToString("yyyy-MM-dd");
            txtDomicilio.Text = ElBeneficiario.Domicilio;
            txtTelefono1.Text = ElBeneficiario.Telefono1;
            txtTelefono2.Text = ElBeneficiario.Telefono2;
            txtEmail.Text = ElBeneficiario.Email;
            if (ElBeneficiario.Atributario == "Pensionista")
            {
                cbPensionista.Checked = true;
                cbPensionista_CheckedChanged(new object(), new EventArgs());
            }
            else
            {
                cbPensionista.Checked = false;
                cbPensionista_CheckedChanged(new object(), new EventArgs());
                txtAtributario.Text = ElBeneficiario.Atributario;
                txtAtributario.Enabled = false;
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
            this.btnModificar.Visible = false;
            this.btnInhabilitar.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ActualizarCampos();
            habilitarCampos(false);
            cbPensionista_CheckedChanged(new object(), new EventArgs());
            txtAtributario.Enabled = false;
            this.btnCancelar.Visible = false;
            this.btnConfirmar.Visible = false;
            this.btnModificar.Visible = true;
            this.btnHabilitar.Visible = false;
            this.btnInhabilitar.Visible = true;
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
                unBeneficiario.FechaNacimiento = txtFechaNac.Text;
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
                        txtAtributario.Enabled = false;
                        this.btnCancelar.Visible = false;
                        this.btnConfirmar.Visible = false;
                        this.btnInhabilitar.Visible = true;
                        this.btnHabilitar.Visible = false;
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
                if ((txtHasta.Text != string.Empty && (DateTime.Parse(txtDesde.Text) <= DateTime.Parse(txtHasta.Text))) || txtHasta.Text == string.Empty)
                {
                    cPlan unPlan = new cPlan();
                    unPlan.Activo = true;
                    unPlan.Evaluacion = cbEvaluacion.Checked;
                    unPlan.Tratamiento = cbTratamiento.Checked;
                    unPlan.Tipo = ddlTipos.SelectedItem.Text;
                    unPlan.FechaInicio = txtDesde.Text;
                    if (txtHasta.Text != string.Empty)
                    {
                        unPlan.FechaFin = txtHasta.Text;
                    }
                    ElBeneficiario.lstPlanes = new List<cPlan>();
                    ElBeneficiario.lstPlanes.Add(unPlan);
                    if (dFachada.PlanAgregar(ElBeneficiario))
                    {
                        lblMensajeAgregarPlan.Text = "Plan agregado correctamente al beneficiario";
                        LimpiarCamposPlan();
                        ActualizarTodo();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se puedo agregar el plan.')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha de fin del plan debe ser mayor a la de inicio.')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan ingresar datos para registrar el plan.')", true);
            }
        }

        protected void grdPlanesActivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dFachada.PlanEliminar(lstPlanesActivos[e.RowIndex]);
            ActualizarTodo();
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

        protected void btnAgregarInforme_Click(object sender, EventArgs e)
        {
            Response.Redirect("vInformeNuevo.aspx?idBeneficiario=" + ElBeneficiario.Codigo.ToString());
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dFachada.BeneficiarioHabilitar(ElBeneficiario))
            {
                btnHabilitar.Visible = false;
                btnInhabilitar.Visible = true;
                btnModificar.Visible = true;
                btnNuevoPlan.Visible = true;
                btnAgregarInforme.Visible = true;
                ActualizarTodo();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha podido habilitar el beneficiario.')", true);
            }

        }

        protected void btnInhabilitar_Click(object sender, EventArgs e)
        {
            if (lstPlanesActivos.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha podido inhabilitar el beneficiario porque hay planes activos.')", true);
            }
            else
            {
                if (dFachada.BeneficiarioInhabilitar(ElBeneficiario))
                {
                    btnHabilitar.Visible = true;
                    btnInhabilitar.Visible = false;
                    btnModificar.Visible = false;
                    btnNuevoPlan.Visible = false;
                    btnAgregarInforme.Visible = false;
                    ActualizarTodo();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha podido inhabilitar el beneficiario.')", true);
                }
            }



        }

        protected void grdPlanesActivos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (lstPlanesActivos[e.NewSelectedIndex].FechaFin == null)
            {
                PlanAModificar = lstPlanesActivos[e.NewSelectedIndex];
                PanelModificarPlan.Visible = true;
                List<string> tipo = new List<string>() { PlanAModificar.Tipo };
                ddlTipoPlanModificar.DataSource = tipo;
                ddlTipoPlanModificar.DataBind();
                ddlTipoPlanModificar.Enabled = false;
                cboEvaluacionModificar.Checked = PlanAModificar.Evaluacion;
                cboEvaluacionModificar.Enabled = false;
                cboTratamientoModificar.Checked = PlanAModificar.Tratamiento;
                cboTratamientoModificar.Enabled = false;
                DateTime fn = DateTime.Parse(PlanAModificar.FechaInicio);
                txtFechaInicioModificar.Text = fn.ToString("yyyy-MM-dd");
                txtFechaInicioModificar.Enabled = false;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El Plan al que solicitar ingresar la fecha de vencimiento ya tiene una.')", true);
            }
        }
        protected void btnCancelarModificarPlan_Click(object sender, EventArgs e)
        {
            PanelModificarPlan.Visible = false;
            ddlTipoPlanModificar.DataSource = PlanAModificar.Tipo;
            ddlTipoPlanModificar.DataBind();
            ddlTipoPlanModificar.Enabled = false;
            cboEvaluacionModificar.Checked = false;
            cboEvaluacionModificar.Enabled = false;
            cboTratamientoModificar.Checked = false;
            cboTratamientoModificar.Enabled = false;
            txtFechaInicioModificar.Text = string.Empty;
            txtFechaInicioModificar.Enabled = false;
        }

        protected void btnModificarPlan_Click(object sender, EventArgs e)
        {
            if (txtFechaFinModificar.Text == string.Empty)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Para modificar debe ingresar la fecha de vencimiento del plan.')", true);
            }
            else
            {
                PlanAModificar.FechaFin = txtFechaFinModificar.Text;
                if (dFachada.PlanModificarFechaVencimiento(PlanAModificar))
                {
                    lblMensaje.Text = "La fecha del plan se ha modificado correctamente";
                    PanelModificarPlan.Visible = false;
                    ddlTipoPlanModificar.DataSource = PlanAModificar.Tipo;
                    ddlTipoPlanModificar.DataBind();
                    ddlTipoPlanModificar.Enabled = false;
                    cboEvaluacionModificar.Checked = false;
                    cboEvaluacionModificar.Enabled = false;
                    cboTratamientoModificar.Checked = false;
                    cboTratamientoModificar.Enabled = false;
                    txtFechaInicioModificar.Text = string.Empty;
                    txtFechaInicioModificar.Enabled = false;
                    ActualizarTodo();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Hubo un error al modificar la fecha de vencimiento del plan.')", true);
                }
            }

        }

        protected void grdPlanesActivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[7].Visible = false; //estado
        }

        protected void grdPlanesInactivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[6].Visible = false; //estado
        }
    }
}