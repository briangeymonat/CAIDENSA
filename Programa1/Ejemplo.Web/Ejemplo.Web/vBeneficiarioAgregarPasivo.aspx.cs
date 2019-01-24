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
        private static List<string> TiposPlanes = new List<string> { "ASSE", "AYEX", "CAMEC", "Círculo Católico", "MIDES", "Particular", "Policial" };
        private static List<cUsuario> lstTodosEspecialistasPS;
        private static List<cUsuario> lstEspecialistasAgregadosPS;
        private static List<cUsuario> lstTodosEspecialistasUS;
        private static List<cUsuario> lstEspecialistasAgregadosUS;

        private static List<cDiagnostico> lstTodosDiagnosticos;
        private static List<cDiagnostico> lstDiagnosticosAgregados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lstTodosEspecialistasPS = dFachada.UsuarioTraerTodosEspecialistasActivos();
                lstEspecialistasAgregadosPS = new List<cUsuario>();

                lstTodosEspecialistasUS = dFachada.UsuarioTraerTodosEspecialistasActivos();
                lstEspecialistasAgregadosUS = new List<cUsuario>();

                lstTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();
                lstDiagnosticosAgregados = new List<cDiagnostico>();

                CargarCombos();
                CargarGrillas();
                this.btnPrimeraSesionMostrar.Visible = true;
                this.PanelPrimeraSesion.Visible = false;

                this.btnUltimaSesionMostrar.Visible = true;
                this.PanelUltimaSesion.Visible = false;

                this.btnAgregarPlanMostrar.Visible = true;
                this.PanelAgregarPlan.Visible = false;

                this.btnAgregarDiagnosticoMostrar.Visible = true;
                this.PanelAgregarDiagnostico.Visible = false;


                this.btnPrimeraSesionOcultar.Visible = false;

                this.btnUltimaSesionOcultar.Visible = false;

                this.btnAgregarPlanOcultar.Visible = false;

                this.btnAgregarDiagnosticoOcultar.Visible = false;
                this.rblLocalidadPS.SelectedIndex = 0;
                this.rblLocalidadUS.SelectedIndex = 0;

                CargarDdlHoras();

            }
        }

        private void CargarDdlHoras()
        {
            DateTime hora1 = DateTime.Parse("08:00");
            List<string> LasHorasDesde = new List<string>();
            LasHorasDesde.Add(hora1.ToShortTimeString());
            do
            {
                hora1 = hora1.AddMinutes(15);
                LasHorasDesde.Add(hora1.ToShortTimeString());
            } while (hora1 != DateTime.Parse("19:45"));
            ddlDesdePS.DataSource = LasHorasDesde;
            ddlDesdePS.DataBind();
            ddlDesdeUS.DataSource = LasHorasDesde;
            ddlDesdeUS.DataBind();

            List<string> LasHorasHasta = new List<string>();
            DateTime hora2 = DateTime.Parse("08:15");
            LasHorasHasta.Add(hora2.ToShortTimeString());
            do
            {
                hora2 = hora2.AddMinutes(15);
                LasHorasHasta.Add(hora2.ToShortTimeString());
            } while (hora2 != DateTime.Parse("20:00"));
            ddlHastaPS.DataSource = LasHorasHasta;
            ddlHastaPS.DataBind();
            ddlHastaUS.DataSource = LasHorasHasta;
            ddlHastaUS.DataBind();
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
                if (!FaltanDatosPrimeraSesion())
                {
                    if (!FaltanDatosUltimaSesion())
                    {
                        if (!FaltanDatosAgregarPlan())
                        {
                            if (!FaltanDatosAgregarDiagnostico())
                            {
                                if (DateTime.Parse(ddlDesdePS.SelectedValue) < DateTime.Parse(ddlHastaPS.SelectedValue)                                    )
                                {
                                    if (DateTime.Parse(ddlDesdeUS.SelectedValue) < DateTime.Parse(ddlHastaUS.SelectedValue))
                                    {
                                        if (DateTime.Parse(txtFechaPS.Text) <= DateTime.Parse(txtFechaUS.Text))
                                        {
                                            if (DateTime.Parse(txtDesde.Text) <= DateTime.Parse(txtHasta.Text))
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
                                                    #region cargar primera sesion
                                                    cSesion primeraSesion = new cSesion();
                                                    string tipo = ddlTipoSesionPS.SelectedValue.ToString();
                                                    if (tipo == "Individual")
                                                        primeraSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                                                    if (tipo == "Grupo2")
                                                        primeraSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                                                    if (tipo == "Grupo3")
                                                        primeraSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                                                    if (tipo == "Taller")
                                                        primeraSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                                                    if (tipo == "PROES")
                                                        primeraSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                                                    primeraSesion.Fecha = txtFechaPS.Text;
                                                    if (rblLocalidadPS.SelectedIndex == 0)
                                                        primeraSesion.Centro = cUtilidades.Centro.JuanLacaze;
                                                    else
                                                        primeraSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                                                    primeraSesion.HoraInicio = ddlDesdePS.SelectedValue;
                                                    primeraSesion.HoraFin = ddlHastaPS.SelectedValue;
                                                    cBeneficiarioSesion bs = new cBeneficiarioSesion();
                                                    bs.Beneficiario = unBeneficiario;
                                                    bs.Plan = unBeneficiario.lstPlanes[0];
                                                    bs.Estado = cUtilidades.EstadoSesion.Asistio;
                                                    primeraSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                                                    primeraSesion.lstBeneficiarios.Add(bs);
                                                    primeraSesion.lstUsuarios = lstEspecialistasAgregadosPS;
                                                    primeraSesion.Comentario = txtComentarioPS.Text;
                                                    #endregion

                                                    #region cargar ultima sesion
                                                    cSesion ultimaSesion = new cSesion();
                                                    string tipo1 = ddlTipoSesionUS.SelectedValue.ToString();
                                                    if (tipo == "Individual")
                                                        ultimaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                                                    if (tipo == "Grupo2")
                                                        ultimaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                                                    if (tipo == "Grupo3")
                                                        ultimaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                                                    if (tipo == "Taller")
                                                        ultimaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                                                    if (tipo == "PROES")
                                                        ultimaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                                                    ultimaSesion.Fecha = txtFechaUS.Text;
                                                    if (rblLocalidadUS.SelectedIndex == 0)
                                                        ultimaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                                                    else
                                                        ultimaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;
                                                    ultimaSesion.HoraInicio = ddlDesdeUS.SelectedValue;
                                                    ultimaSesion.HoraFin = ddlHastaUS.SelectedValue;
                                                    cBeneficiarioSesion bs1 = new cBeneficiarioSesion();
                                                    bs1.Beneficiario = unBeneficiario;
                                                    bs1.Plan = unBeneficiario.lstPlanes[0];
                                                    bs1.Estado = cUtilidades.EstadoSesion.Asistio;
                                                    ultimaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                                                    ultimaSesion.lstBeneficiarios.Add(bs1);
                                                    ultimaSesion.lstUsuarios = lstEspecialistasAgregadosUS;
                                                    ultimaSesion.Comentario = txtComentarioUS.Text;
                                                    #endregion

                                                    #region cargar diagnostico
                                                    unBeneficiario.lstDiagnosticos = new List<cDiagnosticoBeneficiario>();
                                                    cDiagnosticoBeneficiario db;
                                                    for (int i = 0; i < lstDiagnosticosAgregados.Count; i++)
                                                    {
                                                        db = new cDiagnosticoBeneficiario();
                                                        db.Diagnostico = lstDiagnosticosAgregados[i];
                                                        db.Fecha = ultimaSesion.Fecha;
                                                        unBeneficiario.lstDiagnosticos.Add(db);
                                                    }
                                                    #endregion
                                                    if (b && dFachada.SesionAgregar(primeraSesion) &&
                                                        dFachada.SesionAgregar(ultimaSesion) &&
                                                        dFachada.DiagnosticoAgregarDiagnosticoBeneficiario(unBeneficiario))
                                                    {
                                                        lblMensajeBeneficiario.Text = "Beneficiario pasivo agregado correctamente.";
                                                        LimpiarCampos();
                                                        this.btnPrimeraSesionMostrar.Visible = true;
                                                        this.PanelPrimeraSesion.Visible = false;
                                                        this.btnUltimaSesionMostrar.Visible = true;
                                                        this.PanelUltimaSesion.Visible = false;
                                                        this.btnAgregarPlanMostrar.Visible = true;
                                                        this.PanelAgregarPlan.Visible = false;
                                                        this.btnAgregarDiagnosticoMostrar.Visible = true;
                                                        this.PanelAgregarDiagnostico.Visible = false;
                                                        this.btnPrimeraSesionOcultar.Visible = false;
                                                        this.btnUltimaSesionOcultar.Visible = false;
                                                        this.btnAgregarPlanOcultar.Visible = false;
                                                        this.btnAgregarDiagnosticoOcultar.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo concretar el registro del beneficiario.')", true);
                                                    }

                                                }
                                                else
                                                {
                                                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Ya existe un beneficiario en el sistema con esa CI.')", true);
                                                }
                                            }
                                            else
                                            {
                                                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha de inicio del plan es mayor a la fecha de fin .')", true);

                                            }
                                        }
                                        else
                                        {
                                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha de la primera sesión es mayor a la fecha de la última sesión .')", true);

                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La hora de inicio de la ultima sesion es mayor a la hora de fin .')", true);

                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La hora de inicio de la primera sesion es mayor a la hora de fin .')", true);

                                }
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan ingresar el diagnóstico.')", true);

                            }
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan ingresar datos del plan.')", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan ingresar datos de la última sesión.')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan ingresar datos de la primera sesión.')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan ingresar datos personales del beneficiario.')", true);
            }



        }

        private void LimpiarCampos()
        {
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


            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            cbTratamiento.Checked = false;
            cbEvaluacion.Checked = false;
            ddlTipoPlanes.SelectedIndex = 0;


            ddlTipoSesionPS.SelectedIndex = 0;
            txtFechaPS.Text = string.Empty;
            ddlDesdePS.SelectedIndex = 0;
            ddlHastaPS.SelectedIndex = 0;
            rblLocalidadPS.SelectedIndex = 0;
            ddlEspecialidadesPS.SelectedIndex = 0;
            txtComentarioPS.Text = string.Empty;


            ddlTipoSesionUS.SelectedIndex = 0;
            txtFechaUS.Text = string.Empty;
            ddlDesdeUS.SelectedIndex = 0;
            ddlHastaUS.SelectedIndex = 0;
            rblLocalidadUS.SelectedIndex = 0;
            ddlEspecialidadesUS.SelectedIndex = 0;
            txtComentarioUS.Text = string.Empty;


            CargarGrillas();
            CargarCombos();
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
        private bool FaltanDatosPrimeraSesion()
        {
            if (txtFechaPS.Text == string.Empty  ||
                lstEspecialistasAgregadosPS.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool FaltanDatosUltimaSesion()
        {
            if (txtFechaUS.Text == string.Empty ||
                lstEspecialistasAgregadosUS.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool FaltanDatosAgregarPlan()
        {
            if (txtDesde.Text == string.Empty || txtHasta.Text == string.Empty || (!cbTratamiento.Checked && !cbEvaluacion.Checked))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool FaltanDatosAgregarDiagnostico()
        {
            if (lstDiagnosticosAgregados.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Botones de mostrar y ocultar
        protected void btnPrimeraSesionMostrar_Click(object sender, EventArgs e)
        {
            this.PanelPrimeraSesion.Visible = true;
            this.btnPrimeraSesionMostrar.Visible = false;
            this.btnPrimeraSesionOcultar.Visible = true;
        }

        protected void btnPrimeraSesionOcultar_Click(object sender, EventArgs e)
        {
            this.PanelPrimeraSesion.Visible = false;
            this.btnPrimeraSesionMostrar.Visible = true;
            this.btnPrimeraSesionOcultar.Visible = false;
        }

        protected void btnUltimaSesionMostrar_Click(object sender, EventArgs e)
        {
            this.PanelUltimaSesion.Visible = true;
            this.btnUltimaSesionMostrar.Visible = false;
            this.btnUltimaSesionOcultar.Visible = true;
        }

        protected void btnUltimaSesionOcultar_Click(object sender, EventArgs e)
        {
            this.PanelUltimaSesion.Visible = false;
            this.btnUltimaSesionMostrar.Visible = true;
            this.btnUltimaSesionOcultar.Visible = false;
        }

        protected void btnAgregarPlanMostrar_Click(object sender, EventArgs e)
        {
            this.PanelAgregarPlan.Visible = true;
            this.btnAgregarPlanMostrar.Visible = false;
            this.btnAgregarPlanOcultar.Visible = true;
        }

        protected void btnAgregarPlanOcultar_Click(object sender, EventArgs e)
        {
            this.PanelAgregarPlan.Visible = false;
            this.btnAgregarPlanMostrar.Visible = true;
            this.btnAgregarPlanOcultar.Visible = false;
        }

        protected void btnAgregarDiagnosticoMostrar_Click(object sender, EventArgs e)
        {
            this.PanelAgregarDiagnostico.Visible = true;
            this.btnAgregarDiagnosticoMostrar.Visible = false;
            this.btnAgregarDiagnosticoOcultar.Visible = true;
        }

        protected void btnAgregarDiagnosticoOcultar_Click(object sender, EventArgs e)
        {
            this.PanelAgregarDiagnostico.Visible = false;
            this.btnAgregarDiagnosticoMostrar.Visible = true;
            this.btnAgregarDiagnosticoOcultar.Visible = false;
        }
        #endregion

        #region Cargar combos y grillas
        protected void CargarCombos()
        {
            CargarComboEspecialidades();
            CargarComboTipoSesion();
            CargarComboTipoPlanes();
        }

        protected void CargarComboTipoSesion()
        {
            List<string> lstEnums = new List<string>();
            foreach (var item in Enum.GetValues(typeof(cUtilidades.TipoSesion)))
            {
                lstEnums.Add(item.ToString());
            }
            this.ddlTipoSesionPS.DataSource = lstEnums;
            this.ddlTipoSesionPS.DataBind();
            this.ddlTipoSesionUS.DataSource = lstEnums;
            this.ddlTipoSesionUS.DataBind();

        }
        protected void CargarComboEspecialidades()
        {
            this.ddlEspecialidadesPS.DataSource = dFachada.EspecialidadTraerTodas();//sacar la especialidad sin especialidad
            this.ddlEspecialidadesPS.DataTextField = "Nombre";
            this.ddlEspecialidadesPS.DataValueField = "Codigo";
            this.ddlEspecialidadesPS.DataBind();
            this.ddlEspecialidadesUS.DataSource = dFachada.EspecialidadTraerTodas();
            this.ddlEspecialidadesUS.DataTextField = "Nombre";
            this.ddlEspecialidadesUS.DataValueField = "Codigo";
            this.ddlEspecialidadesUS.DataBind();
        }
        protected void CargarComboTipoPlanes()
        {
            this.ddlTipoPlanes.DataSource = TiposPlanes;
            this.ddlTipoPlanes.DataBind();
        }

        protected void CargarGrillas()
        {
            CargarGrillaEspecialistasPS();
            CargarGrillaEspecialistasUS();
            CargarGrillaDiagnosticos();
        }

        protected void CargarGrillaEspecialistasPS()
        {
            cEspecialidad especialidad = new cEspecialidad();
            especialidad.Codigo = int.Parse(ddlEspecialidadesPS.SelectedValue);
            List<cUsuario> lstMostrarPS = new List<cUsuario>();

            for (int i = 0; i < lstTodosEspecialistasPS.Count; i++)
            {
                if (lstTodosEspecialistasPS[i].Especialidad.Codigo == especialidad.Codigo)
                {
                    lstMostrarPS.Add(lstTodosEspecialistasPS[i]);
                }
            }

            grdTodosEspecialistasPS.DataSource = lstMostrarPS;
            grdTodosEspecialistasPS.DataBind();

            grdEspecialistasAgregadosPS.DataSource = lstEspecialistasAgregadosPS;
            grdEspecialistasAgregadosPS.DataBind();

        }
        protected void CargarGrillaEspecialistasUS()
        {
            cEspecialidad especialidad = new cEspecialidad();
            especialidad.Codigo = int.Parse(ddlEspecialidadesUS.SelectedValue);
            List<cUsuario> lstMostrarUS = new List<cUsuario>();

            for (int i = 0; i < lstTodosEspecialistasUS.Count; i++)
            {
                if (lstTodosEspecialistasUS[i].Especialidad.Codigo == especialidad.Codigo)
                {
                    lstMostrarUS.Add(lstTodosEspecialistasUS[i]);
                }
            }

            grdTodosEspecialistasUS.DataSource = lstMostrarUS;
            grdTodosEspecialistasUS.DataBind();

            grdEspecialistasAgregadosUS.DataSource = lstEspecialistasAgregadosUS;
            grdEspecialistasAgregadosUS.DataBind();

        }

        protected void CargarGrillaDiagnosticos()
        {
            grdTodosDiagnosticos.DataSource = lstTodosDiagnosticos;
            grdTodosDiagnosticos.DataBind();

            grdDiagnosticosAgregados.DataSource = lstDiagnosticosAgregados;
            grdDiagnosticosAgregados.DataBind();
        }
        #endregion

        #region Seleccionar, eliminar y ocultar columnas de grillas
        protected void grdTodosDiagnosticos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosDiagnosticos.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstTodosDiagnosticos.Count; i++)
            {
                if (lstTodosDiagnosticos[i].Codigo == codigo)
                {
                    lstDiagnosticosAgregados.Add(lstTodosDiagnosticos[i]);
                    lstTodosDiagnosticos.RemoveAt(i);
                }
            }
            CargarGrillaDiagnosticos();
        }

        protected void grdDiagnosticosAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdDiagnosticosAgregados.Rows[e.RowIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstDiagnosticosAgregados.Count; i++)
            {
                if (lstDiagnosticosAgregados[i].Codigo == codigo)
                {
                    lstTodosDiagnosticos.Add(lstDiagnosticosAgregados[i]);
                    lstDiagnosticosAgregados.RemoveAt(i);
                }
            }
            CargarGrillaDiagnosticos();
        }

        protected void grdDiagnosticosAgregados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
        }

        protected void grdTodosDiagnosticos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
        }
        protected void ddlEspecialidadesPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaEspecialistasPS();
        }

        protected void ddlEspecialidadesUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaEspecialistasUS();
        }

        protected void grdTodosEspecialistasPS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[7].Visible = false;//tipo
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//tel
            e.Row.Cells[11].Visible = false;//email
            e.Row.Cells[12].Visible = false;//estado
            e.Row.Cells[13].Visible = false;//tipo de contrato
        }

        protected void grdTodosEspecialistasPS_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosEspecialistasPS.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstTodosEspecialistasPS.Count; i++)
            {
                if (lstTodosEspecialistasPS[i].Codigo == codigo)
                {
                    lstEspecialistasAgregadosPS.Add(lstTodosEspecialistasPS[i]);
                    lstTodosEspecialistasPS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasPS();
        }

        protected void grdEspecialistasAgregadosPS_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdEspecialistasAgregadosPS.Rows[e.RowIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstEspecialistasAgregadosPS.Count; i++)
            {
                if (lstEspecialistasAgregadosPS[i].Codigo == codigo)
                {
                    lstTodosEspecialistasPS.Add(lstEspecialistasAgregadosPS[i]);
                    lstEspecialistasAgregadosPS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasPS();
        }

        protected void grdEspecialistasAgregadosUS_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdEspecialistasAgregadosUS.Rows[e.RowIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstEspecialistasAgregadosUS.Count; i++)
            {
                if (lstEspecialistasAgregadosUS[i].Codigo == codigo)
                {
                    lstTodosEspecialistasUS.Add(lstEspecialistasAgregadosUS[i]);
                    lstEspecialistasAgregadosUS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasUS();
        }

        protected void grdTodosEspecialistasUS_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosEspecialistasUS.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstTodosEspecialistasUS.Count; i++)
            {
                if (lstTodosEspecialistasUS[i].Codigo == codigo)
                {
                    lstEspecialistasAgregadosUS.Add(lstTodosEspecialistasUS[i]);
                    lstTodosEspecialistasUS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasUS();
        }

        protected void grdEspecialistasAgregadosPS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[7].Visible = false;//tipo
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//tel
            e.Row.Cells[11].Visible = false;//email
            e.Row.Cells[12].Visible = false;//estado
            e.Row.Cells[13].Visible = false;//tipo de contrato
        }

        protected void grdTodosEspecialistasUS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[7].Visible = false;//tipo
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//tel
            e.Row.Cells[11].Visible = false;//email
            e.Row.Cells[12].Visible = false;//estado
            e.Row.Cells[13].Visible = false;//tipo de contrato
        }

        protected void grdEspecialistasAgregadosUS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[7].Visible = false;//tipo
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//tel
            e.Row.Cells[11].Visible = false;//email
            e.Row.Cells[12].Visible = false;//estado
            e.Row.Cells[13].Visible = false;//tipo de contrato
        }
        #endregion
    }
}