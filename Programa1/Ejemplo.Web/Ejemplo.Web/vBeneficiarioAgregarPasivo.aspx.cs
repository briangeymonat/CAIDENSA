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
        private static List<cUsuario> LosTodosEspecialistasPS;
        private static List<cUsuario> LosEspecialistasAgregadosPS;
        private static List<cUsuario> LosTodosEspecialistasUS;
        private static List<cUsuario> LosEspecialistasAgregadosUS;

        private static List<cDiagnostico> LosTodosDiagnosticos;
        private static List<cDiagnostico> LosDiagnosticosAgregados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosTodosEspecialistasPS = dFachada.UsuarioTraerTodosEspecialistasActivos();
                LosEspecialistasAgregadosPS = new List<cUsuario>();

                LosTodosEspecialistasUS = dFachada.UsuarioTraerTodosEspecialistasActivos();
                LosEspecialistasAgregadosUS = new List<cUsuario>();

                LosTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();
                LosDiagnosticosAgregados = new List<cDiagnostico>();
                /*
                CargarCombos();
                CargarGrillas();
                
                this.btnPrimeraSesionMostrar.Visible = true;
                this.pnlPrimeraSesion.Visible = false;

                this.btnUltimaSesionMostrar.Visible = true;
                this.pnlUltimaSesion.Visible = false;

                this.btnAgregarPlanMostrar.Visible = true;
                this.pnlAgregarPlan.Visible = false;

                this.btnAgregarDiagnosticoMostrar.Visible = true;
                this.pnlAgregarDiagnostico.Visible = false;


                this.btnPrimeraSesionOcultar.Visible = false;

                this.btnUltimaSesionOcultar.Visible = false;

                this.btnAgregarPlanOcultar.Visible = false;

                this.btnAgregarDiagnosticoOcultar.Visible = false;
                this.rblLocalidadPS.SelectedIndex = 0;
                this.rblLocalidadUS.SelectedIndex = 0;
                
                CargarDdlHoras();
                */
            }
        }

        private void CargarDdlHoras()
        {
            DateTime dHora1 = DateTime.Parse("08:00");
            List<string> lstHorasDesde = new List<string>();
            lstHorasDesde.Add(dHora1.ToShortTimeString());
            do
            {
                dHora1 = dHora1.AddMinutes(15);
                lstHorasDesde.Add(dHora1.ToShortTimeString());
            }
            while (dHora1 != DateTime.Parse("19:45"));
            ddlDesdePS.DataSource = lstHorasDesde;
            ddlDesdePS.DataBind();
            ddlDesdeUS.DataSource = lstHorasDesde;
            ddlDesdeUS.DataBind();

            List<string> lstHorasHasta = new List<string>();
            DateTime dHora2 = DateTime.Parse("08:15");
            lstHorasHasta.Add(dHora2.ToShortTimeString());
            do
            {
                dHora2 = dHora2.AddMinutes(15);
                lstHorasHasta.Add(dHora2.ToShortTimeString());
            } while (dHora2 != DateTime.Parse("20:00"));
            ddlHastaPS.DataSource = lstHorasHasta;
            ddlHastaPS.DataBind();
            ddlHastaUS.DataSource = lstHorasHasta;
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
                if (!FaltanDatosAgregarDiagnostico())
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
                        cSesion unaPrimeraSesion = new cSesion();
                        string sTipo = ddlTipoSesionPS.SelectedValue.ToString();
                        if (sTipo == "Individual")
                            unaPrimeraSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (sTipo == "Grupo2")
                            unaPrimeraSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (sTipo == "Grupo3")
                            unaPrimeraSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (sTipo == "Taller")
                            unaPrimeraSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (sTipo == "PROES")
                            unaPrimeraSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        unaPrimeraSesion.Fecha = txtFechaPS.Text;
                        if (rblLocalidadPS.SelectedIndex == 0)
                            unaPrimeraSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        else
                            unaPrimeraSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        unaPrimeraSesion.HoraInicio = ddlDesdePS.SelectedValue;
                        unaPrimeraSesion.HoraFin = ddlHastaPS.SelectedValue;
                        cBeneficiarioSesion unBS = new cBeneficiarioSesion();
                        unBS.Beneficiario = unBeneficiario;
                        unBS.Plan = unBeneficiario.lstPlanes[0];
                        unBS.Estado = cUtilidades.EstadoSesion.Asistio;
                        unaPrimeraSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaPrimeraSesion.lstBeneficiarios.Add(unBS);
                        unaPrimeraSesion.lstUsuarios = LosEspecialistasAgregadosPS;
                        unaPrimeraSesion.Comentario = txtComentarioPS.Text;
                        #endregion

                        #region cargar ultima sesion
                        cSesion unaUltimaSesion = new cSesion();
                        string sTipo1 = ddlTipoSesionUS.SelectedValue.ToString();
                        if (sTipo1 == "Individual")
                            unaUltimaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (sTipo1 == "Grupo2")
                            unaUltimaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (sTipo1 == "Grupo3")
                            unaUltimaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (sTipo1 == "Taller")
                            unaUltimaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (sTipo1 == "PROES")
                            unaUltimaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        unaUltimaSesion.Fecha = txtFechaUS.Text;
                        if (rblLocalidadUS.SelectedIndex == 0)
                            unaUltimaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        else
                            unaUltimaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;
                        unaUltimaSesion.HoraInicio = ddlDesdeUS.SelectedValue;
                        unaUltimaSesion.HoraFin = ddlHastaUS.SelectedValue;
                        cBeneficiarioSesion unBS1 = new cBeneficiarioSesion();
                        unBS1.Beneficiario = unBeneficiario;
                        unBS1.Plan = unBeneficiario.lstPlanes[0];
                        unBS1.Estado = cUtilidades.EstadoSesion.Asistio;
                        unaUltimaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaUltimaSesion.lstBeneficiarios.Add(unBS1);
                        unaUltimaSesion.lstUsuarios = LosEspecialistasAgregadosUS;
                        unaUltimaSesion.Comentario = txtComentarioUS.Text;
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
                            this.btnPrimeraSesionMostrar.Visible = true;
                            this.pnlPrimeraSesion.Visible = false;
                            this.btnUltimaSesionMostrar.Visible = true;
                            this.pnlUltimaSesion.Visible = false;
                            this.btnAgregarPlanMostrar.Visible = true;
                            this.pnlAgregarPlan.Visible = false;
                            this.btnAgregarDiagnosticoMostrar.Visible = true;
                            this.pnlAgregarDiagnostico.Visible = false;
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
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha de la primera sesión es mayor a la fecha de la última sesión .')", true);

                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Falta ingresar el diagnóstico.')", true);

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
            if (txtFechaPS.Text == string.Empty ||
                LosEspecialistasAgregadosPS.Count <= 0)
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
            if (LosDiagnosticosAgregados.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
       
        #region Cargar combos y grillas
        protected void CargarComboTipoPlanes()
        {
            this.ddlTipoPlanes.DataSource = LosTiposPlanes;
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
            cEspecialidad unaEspecialidad = new cEspecialidad();
            unaEspecialidad.Codigo = int.Parse(ddlEspecialidadesPS.SelectedValue);
            List<cUsuario> lstMostrarPS = new List<cUsuario>();

            for (int i = 0; i < LosTodosEspecialistasPS.Count; i++)
            {
                if (LosTodosEspecialistasPS[i].Especialidad.Codigo == unaEspecialidad.Codigo)
                {
                    lstMostrarPS.Add(LosTodosEspecialistasPS[i]);
                }
            }

            grdTodosEspecialistasPS.DataSource = lstMostrarPS;
            grdTodosEspecialistasPS.DataBind();

            grdEspecialistasAgregadosPS.DataSource = LosEspecialistasAgregadosPS;
            grdEspecialistasAgregadosPS.DataBind();

        }
        protected void CargarGrillaEspecialistasUS()
        {
            cEspecialidad unaEspecialidad = new cEspecialidad();
            unaEspecialidad.Codigo = int.Parse(ddlEspecialidadesUS.SelectedValue);
            List<cUsuario> lstMostrarUS = new List<cUsuario>();

            for (int i = 0; i < LosTodosEspecialistasUS.Count; i++)
            {
                if (LosTodosEspecialistasUS[i].Especialidad.Codigo == unaEspecialidad.Codigo)
                {
                    lstMostrarUS.Add(LosTodosEspecialistasUS[i]);
                }
            }

            grdTodosEspecialistasUS.DataSource = lstMostrarUS;
            grdTodosEspecialistasUS.DataBind();

            grdEspecialistasAgregadosUS.DataSource = LosEspecialistasAgregadosUS;
            grdEspecialistasAgregadosUS.DataBind();

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
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosTodosEspecialistasPS.Count; i++)
            {
                if (LosTodosEspecialistasPS[i].Codigo == iCodigo)
                {
                    LosEspecialistasAgregadosPS.Add(LosTodosEspecialistasPS[i]);
                    LosTodosEspecialistasPS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasPS();
        }

        protected void grdEspecialistasAgregadosPS_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdEspecialistasAgregadosPS.Rows[e.RowIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosEspecialistasAgregadosPS.Count; i++)
            {
                if (LosEspecialistasAgregadosPS[i].Codigo == iCodigo)
                {
                    LosTodosEspecialistasPS.Add(LosEspecialistasAgregadosPS[i]);
                    LosEspecialistasAgregadosPS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasPS();
        }

        protected void grdEspecialistasAgregadosUS_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdEspecialistasAgregadosUS.Rows[e.RowIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosEspecialistasAgregadosUS.Count; i++)
            {
                if (LosEspecialistasAgregadosUS[i].Codigo == iCodigo)
                {
                    LosTodosEspecialistasUS.Add(LosEspecialistasAgregadosUS[i]);
                    LosEspecialistasAgregadosUS.RemoveAt(i);
                }
            }
            CargarGrillaEspecialistasUS();
        }

        protected void grdTodosEspecialistasUS_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosEspecialistasUS.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosTodosEspecialistasUS.Count; i++)
            {
                if (LosTodosEspecialistasUS[i].Codigo == iCodigo)
                {
                    LosEspecialistasAgregadosUS.Add(LosTodosEspecialistasUS[i]);
                    LosTodosEspecialistasUS.RemoveAt(i);
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