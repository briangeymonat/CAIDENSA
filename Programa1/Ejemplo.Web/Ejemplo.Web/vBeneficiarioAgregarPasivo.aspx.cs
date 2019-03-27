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
    public partial class vBeneficiarioAgregarPasivo1 : System.Web.UI.Page
    {
        private static List<string> LosTiposPlanes = new List<string> { "ASSE", "AYEX", "CAMEC", "Círculo Católico", "MIDES", "Particular", "Policial" };


        private static List<cDiagnostico> LosTodosDiagnosticos;
        private static List<cDiagnostico> LosDiagnosticosAgregados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();
                LosDiagnosticosAgregados = new List<cDiagnostico>();
                CargarComboTipoPlanes();
                CargarGrillaDiagnosticos();
            }
        }


        protected void cbPensionista_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPensionista.Checked)
            {
                txtAtributario.Enabled = false;
                txtAtributario.Text = string.Empty;
            }
            else
            {
                txtAtributario.Enabled = true;
            }
        }

        protected void btnAgregarBeneficiario_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosBeneficiario())
            {
                #region cargar beneficiario
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
                unBeneficiario.Estado = false;
                #endregion
                if (dFachada.BeneficiarioTraerEspecificoCI(unBeneficiario) == null)
                {
                    #region cargar plan
                    cPlan unPlan = new cPlan();
                    unPlan.Activo = false;
                    unPlan.Evaluacion = cbEvaluacion.Checked;
                    unPlan.Tratamiento = cbTratamiento.Checked;
                    unPlan.Tipo = ddlTipoPlanes.SelectedItem.Text;
                    unPlan.FechaInicio = txtDesde.Text;
                    unPlan.FechaFin = txtHasta.Text;
                    unBeneficiario.lstPlanes = new List<cPlan>();
                    unBeneficiario.lstPlanes.Add(unPlan);
                    #endregion
                    bool b = dFachada.BeneficiarioAgregar(unBeneficiario);
                    if (b)
                    {
                        unBeneficiario.lstPlanes = dFachada.PlanTraerTodosPorBeneficiario(unBeneficiario);
                    }
                    if (txtDesde.Text != string.Empty || txtHasta.Text != string.Empty)
                    {

                        if (DateTime.Parse(txtDesde.Text) < DateTime.Parse(txtHasta.Text))
                        {
                            if (cbTratamiento.Checked == true || cbEvaluacion.Checked == true)
                            {

                                bool bSeleccionada = false;
                                for (int i = 0; i < cblEspecialidades.Items.Count; i++)
                                {
                                    if (cblEspecialidades.Items[i].Selected)
                                    {
                                        bSeleccionada = true;
                                    }
                                }

                                if (bSeleccionada)
                                {

                                    #region cargar lista de usuarios para las sesiones
                                    // AGREGAR ESPECIALISTAS DE LAS ESPECIALIDADES SELECCIONADAS
                                    List<cUsuario> lstUsuariosSesiones = new List<cUsuario>();
                                    cEspecialidad unaEspecialidad;
                                    for (int i = 0; i < cblEspecialidades.Items.Count; i++)
                                    {
                                        if (cblEspecialidades.Items[i].Selected)
                                        {
                                            bSeleccionada = true;
                                            unaEspecialidad = new cEspecialidad();
                                            unaEspecialidad.Nombre = cblEspecialidades.Items[i].Value;
                                            lstUsuariosSesiones.Add(dFachada.UsuarioTraerPrimeroPorEspecialidad(unaEspecialidad));
                                        }
                                    }
                                    #endregion

                                    #region cargar primera sesion
                                    cSesion unaPrimeraSesion = new cSesion();
                                    unaPrimeraSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                                    unaPrimeraSesion.Fecha = txtHasta.Text;
                                    if (rblLocalidad.SelectedIndex == 0)
                                        unaPrimeraSesion.Centro = cUtilidades.Centro.JuanLacaze;
                                    else
                                        unaPrimeraSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                                    unaPrimeraSesion.HoraInicio = "08:00";
                                    unaPrimeraSesion.HoraFin = "08:45";
                                    cBeneficiarioSesion unBS = new cBeneficiarioSesion();
                                    unBS.Beneficiario = unBeneficiario;
                                    unBS.Plan = unBeneficiario.lstPlanes[0];
                                    unBS.Estado = cUtilidades.EstadoSesion.Asistio;
                                    unaPrimeraSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                                    unaPrimeraSesion.lstBeneficiarios.Add(unBS);
                                    unaPrimeraSesion.Comentario = string.Empty;
                                    unaPrimeraSesion.lstUsuarios = new List<cUsuario>();
                                    unaPrimeraSesion.lstUsuarios = lstUsuariosSesiones;
                                    #endregion

                                    #region cargar ultima sesion
                                    cSesion unaUltimaSesion = new cSesion();
                                    unaUltimaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                                    unaUltimaSesion.Fecha = txtHasta.Text;
                                    if (rblLocalidad.SelectedIndex == 0)
                                        unaUltimaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                                    else
                                        unaUltimaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;
                                    unaUltimaSesion.HoraInicio = "08:00";
                                    unaUltimaSesion.HoraFin = "08:45";
                                    cBeneficiarioSesion unBS1 = new cBeneficiarioSesion();
                                    unBS1.Beneficiario = unBeneficiario;
                                    unBS1.Plan = unBeneficiario.lstPlanes[0];
                                    unBS1.Estado = cUtilidades.EstadoSesion.Asistio;
                                    unaUltimaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                                    unaUltimaSesion.lstBeneficiarios.Add(unBS1);
                                    unaUltimaSesion.Comentario = string.Empty;
                                    unaUltimaSesion.lstUsuarios = new List<cUsuario>();
                                    unaUltimaSesion.lstUsuarios = lstUsuariosSesiones;
                                    #endregion

                                    #region cargar diagnostico
                                    unBeneficiario.lstDiagnosticos = new List<cDiagnosticoBeneficiario>();
                                    cDiagnosticoBeneficiario db;
                                    for (int i = 0; i < LosDiagnosticosAgregados.Count; i++)
                                    {
                                        db = new cDiagnosticoBeneficiario();
                                        db.Diagnostico = LosDiagnosticosAgregados[i];
                                        db.Fecha = unaUltimaSesion.Fecha;
                                        unBeneficiario.lstDiagnosticos.Add(db);
                                    }
                                    #endregion
                                    if (b && dFachada.SesionAgregar(unaPrimeraSesion) &&
                                        dFachada.SesionAgregar(unaUltimaSesion) &&
                                        dFachada.DiagnosticoAgregarDiagnosticoBeneficiario(unBeneficiario))
                                    {
                                        lblMensajeBeneficiario.Text = "Beneficiario pasivo agregado correctamente.";
                                        LimpiarCampos();
                                    }
                                    else
                                    {
                                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo concretar el registro del beneficiario.')", true);
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Debe seleccionar las si el beneficiario asistía a tratamiento y/o evaluación.')", true);
                                }
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Debe seleccionar las especialidades a las que asistía el beneficiario.')", true);
                            }

                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha de la primera sesión es mayor a la fecha de la última sesión .')", true);
                        }

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Debe seleccionar las fecha de la primera y última sesión del beneficiario.')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Ya existe un beneficiario en el sistema con esa CI.')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos del beneficiario.')", true);

            }
        }

        private void LimpiarCampos()
        {
            //CAMPOS DE DATOS PERSONALES
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtCi.Text = string.Empty;
            txtFechaNac.Text = string.Empty;
            rblSexo.SelectedIndex = 0;
            txtDomicilio.Text = string.Empty;
            txtTel1.Text = string.Empty;
            txtTel2.Text = string.Empty;
            txtAtributario.Text = string.Empty;
            cbPensionista.Checked = false;
            txtMotivoConsulta.Text = string.Empty;
            txtEscolaridad.Text = string.Empty;
            txtDerivador.Text = string.Empty;

            //CAMPOS DE LAS SESIONES
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            cbTratamiento.Checked = false;
            cbEvaluacion.Checked = false;
            rblLocalidad.SelectedIndex = 0;
            ddlTipoPlanes.SelectedIndex = 0;
            cblEspecialidades.ClearSelection();

            CargarGrillaDiagnosticos();
        }

        #region Faltan datos
        private bool FaltanDatosBeneficiario()
        {
            if (txtNombres.Text == string.Empty || txtApellidos.Text == string.Empty ||
                txtCi.Text == string.Empty || txtMotivoConsulta.Text == string.Empty ||
                (txtAtributario.Text == string.Empty && !cbPensionista.Checked))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool FaltanDatosDeSesiones()
        {
            if (txtDesde.Text == string.Empty ||
                txtHasta.Text == string.Empty ||
                !HayEspecialidadesSeleccionadas())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool HayEspecialidadesSeleccionadas()
        {
            bool bHaySeleccionadas = false;
            foreach (ListItem unItem in cblEspecialidades.Items)
            {
                if (unItem.Selected)
                {
                    bHaySeleccionadas = true;
                }
            }
            return bHaySeleccionadas;
        }
        #endregion

        #region Cargar combos y grillas
        protected void CargarComboTipoPlanes()
        {
            this.ddlTipoPlanes.DataSource = LosTiposPlanes;
            this.ddlTipoPlanes.DataBind();
        }


        protected void CargarGrillaDiagnosticos()
        {
            grdTodosDiagnosticos.DataSource = LosTodosDiagnosticos;
            grdTodosDiagnosticos.DataBind();

            grdDiagnosticosAgregados.DataSource = LosDiagnosticosAgregados;
            grdDiagnosticosAgregados.DataBind();
        }
        #endregion

        #region Seleccionar, eliminar y ocultar columnas de grillas
        protected void grdTodosDiagnosticos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosDiagnosticos.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosTodosDiagnosticos.Count; i++)
            {
                if (LosTodosDiagnosticos[i].Codigo == iCodigo)
                {
                    LosDiagnosticosAgregados.Add(LosTodosDiagnosticos[i]);
                    LosTodosDiagnosticos.RemoveAt(i);
                }
            }
            CargarGrillaDiagnosticos();
        }

        protected void grdDiagnosticosAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdDiagnosticosAgregados.Rows[e.RowIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosDiagnosticosAgregados.Count; i++)
            {
                if (LosDiagnosticosAgregados[i].Codigo == iCodigo)
                {
                    LosTodosDiagnosticos.Add(LosDiagnosticosAgregados[i]);
                    LosDiagnosticosAgregados.RemoveAt(i);
                }
            }
            CargarGrillaDiagnosticos();
        }

        #endregion

        protected void grdDiagnosticosAgregados_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdTodosDiagnosticos_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
    }
}