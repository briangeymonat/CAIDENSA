﻿using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vBeneficiarioDetalles : System.Web.UI.Page
    {
        private static List<string> LosTipos = new List<string> { "ASSE", "AYEX", "CAMEC", "Círculo Católico", "MIDES", "Particular", "Policial" };
        private static cBeneficiario ElBeneficiario;
        private static List<cDiagnosticoBeneficiario> LosUltimosDiagnosticos;
        private static List<cDiagnosticoBeneficiario> LosHistorialDiagnosticos;
        private static List<cPlan> LosPlanesActivos;
        private static cPlan ElPlanAModificar;
        private static List<cPlan> LosPlanesInactivos;
        private static List<ListarInformes> LosInformes;
        private static bool BPensionista;
        private static string SVentanaAnterior;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosHistorialDiagnosticos = new List<cDiagnosticoBeneficiario>();
                LosUltimosDiagnosticos = new List<cDiagnosticoBeneficiario>();
                LosPlanesActivos = new List<cPlan>();
                LosPlanesInactivos = new List<cPlan>();
                SVentanaAnterior = Request.QueryString["ventana"];
                ElBeneficiario = new cBeneficiario();
                ElBeneficiario.Codigo = int.Parse(Request.QueryString["BeneficiarioId"]);
                habilitarCampos(false);
                txtAtributario.Enabled = false;
                this.pnlAgregarPlan.Visible = false;
                this.btnOcultar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnConfirmar.Visible = false;
                ActualizarTodo();
                pnlModificarPlan.Visible = false;
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
            ActualizarInformes();
            ActualizarDatos();
            ActualizarDiagnosticos();
            ActualizarGrids();
            ActualizarCampos();
            ActualizarItinerario();
        }

        private void ActualizarDatos()
        {
            ElBeneficiario = dFachada.BeneficiarioTraerEspecifico(ElBeneficiario);
            LosPlanesActivos = dFachada.PlanTraerActivosPorBeneficiario(ElBeneficiario);
            LosPlanesInactivos = dFachada.PlanTraerInactivosPorBeneficiario(ElBeneficiario);
            ddlTipos.DataSource = LosTipos;
            ddlTipos.DataBind();
        }

        private void ActualizarGrids()
        {
            grdPlanesActivos.DataSource = LosPlanesActivos;
            grdPlanesActivos.DataBind();
            grdPlanesInactivos.DataSource = LosPlanesInactivos;
            grdPlanesInactivos.DataBind();
            grdInformes.DataSource = LosInformes;
            grdInformes.DataBind();
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Tipo", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));

            DataRow row;
            for (int i = 0; i < LosHistorialDiagnosticos.Count; i++)
            {
                row = dt.NewRow();
                row["Tipo"] = LosHistorialDiagnosticos[i].Diagnostico.Tipo;
                row["Fecha"] = LosHistorialDiagnosticos[i].Fecha;

                dt.Rows.Add(row);
            }

            grdHistorialDiagnosticos.DataSource = dt;
            grdHistorialDiagnosticos.DataBind();

            DataTable dt1 = new DataTable();

            dt1.Columns.Add("Tipo", typeof(string));
            dt1.Columns.Add("Fecha", typeof(string));

            DataRow row1;
            for (int i = 0; i < LosUltimosDiagnosticos.Count; i++)
            {
                row1 = dt1.NewRow();
                row1["Tipo"] = LosUltimosDiagnosticos[i].Diagnostico.Tipo;
                row1["Fecha"] = LosUltimosDiagnosticos[i].Fecha;

                dt1.Rows.Add(row1);
            }

            grdUltimosDiagnosticos.DataSource = dt1;
            grdUltimosDiagnosticos.DataBind();



        }
        private void ActualizarInformes()
        {
            List<cInforme> lstInformes1 = dFachada.InformeTraerTodosTerminadosPorBeneficiario(ElBeneficiario);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes unInformeAListar;

            for (int i = 0; i < lstInformes1.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes1[i];
                unInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(unInforme.Beneficiario);
                unInformeAListar = new ListarInformes();
                unInformeAListar.Codigo = unInforme.Codigo;
                unInformeAListar.Fecha = unInforme.Fecha;
                unInformeAListar.Estado = unInforme.Estado;
                unInformeAListar.Tipo = unInforme.Tipo;
                unInformeAListar.CodigoBeneficiario = unInforme.Beneficiario.Codigo;
                unInformeAListar.Nombres = unInforme.Beneficiario.Nombres;
                unInformeAListar.Apellidos = unInforme.Beneficiario.Apellidos;
                lstInformesParaListar.Add(unInformeAListar);
            }
            LosInformes = lstInformesParaListar;
        }
        private void ActualizarCampos()
        {
            txtNombres.Text = ElBeneficiario.Nombres;
            txtApellidos.Text = ElBeneficiario.Apellidos;
            txtCi.Text = ElBeneficiario.CI.ToString();
            if (ElBeneficiario.Sexo == "M") rblSexo.Items[0].Selected = true; else rblSexo.Items[1].Selected = true;
            DateTime dFn = DateTime.Parse(ElBeneficiario.FechaNacimiento);
            txtFechaNac.Text = dFn.ToString("yyyy-MM-dd");
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

        private void ActualizarItinerario()
        {
            List<cItinerario> lstItinerarios = dFachada.ItinerarioTraerTodosPorBeneficiario(ElBeneficiario);
            if (lstItinerarios.Count > 0)
            {
                List<string> lstDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
                List<List<cItinerario>> lstItinerariosPorDia = new List<List<cItinerario>>();


                for (int i = 0; i < lstDias.Count; i++)
                {
                    lstItinerariosPorDia.Add(new List<cItinerario>());
                    foreach (cItinerario unItinerario in lstItinerarios)
                    {
                        if (QueDiaEs(unItinerario) == lstDias[i])
                        {
                            lstItinerariosPorDia[i].Add(unItinerario);
                        }
                    }
                }
                string sItinerario = "Asiste al centro ";
                for (int i = 0; i < lstItinerariosPorDia.Count; i++)
                {
                    if (lstItinerariosPorDia[i].Count > 0)
                    {
                        if (sItinerario.Length < 20)
                        {
                            sItinerario += "el día " + lstDias[i].ToLower();
                        }
                        else
                        {
                            sItinerario += " El día " + lstDias[i].ToLower();
                        }
                        bool bSeAgregoAlgunaSesion = false;
                        for (int j = 0; j < lstItinerariosPorDia[i].Count; j++)
                        {
                            string sEspecialistas = "";
                            if (lstItinerariosPorDia[i][j].lstEspecialistas.Count > 1)
                            {
                                sEspecialistas = "con los especialistas ";
                                for (int k = 0; k < lstItinerariosPorDia[i][j].lstEspecialistas.Count; k++)
                                {
                                    if (k == 0)
                                    {
                                        sEspecialistas += lstItinerariosPorDia[i][j].lstEspecialistas[k].Nombres + " " +
                                        lstItinerariosPorDia[i][j].lstEspecialistas[k].Apellidos;
                                    }
                                    else if (lstItinerariosPorDia[i][j].lstEspecialistas.Count - 1 != k && k != 0)
                                    {
                                        sEspecialistas += ", " + lstItinerariosPorDia[i][j].lstEspecialistas[k].Nombres + " " +
                                        lstItinerariosPorDia[i][j].lstEspecialistas[k].Apellidos;
                                    }
                                    else if (lstItinerariosPorDia[i][j].lstEspecialistas.Count - 1 == k)
                                    {
                                        sEspecialistas += " y " + lstItinerariosPorDia[i][j].lstEspecialistas[k].Nombres + " " +
                                        lstItinerariosPorDia[i][j].lstEspecialistas[k].Apellidos;
                                    }

                                }
                            }
                            else
                            {
                                sEspecialistas = "con el especialista " +
                                    lstItinerariosPorDia[i][j].lstEspecialistas[0].Nombres + " " +
                                    lstItinerariosPorDia[i][j].lstEspecialistas[0].Apellidos;
                            }
                            if (!bSeAgregoAlgunaSesion && lstItinerariosPorDia[i].Count - 1 != j)
                            {
                                sItinerario += string.Format(" desde las {0} hasta las {1} {2}", lstItinerariosPorDia[i][j].HoraInicio, lstItinerariosPorDia[i][j].HoraFin, sEspecialistas);
                                bSeAgregoAlgunaSesion = true;
                            }
                            else if (!bSeAgregoAlgunaSesion && lstItinerariosPorDia[i].Count - 1 == j)
                            {
                                sItinerario += string.Format(" desde las {0} hasta las {1} {2}.", lstItinerariosPorDia[i][j].HoraInicio, lstItinerariosPorDia[i][j].HoraFin, sEspecialistas);
                                bSeAgregoAlgunaSesion = true;
                            }
                            else if (bSeAgregoAlgunaSesion && lstItinerariosPorDia[i].Count - 1 != j)
                            {
                                sItinerario += string.Format(", desde las {0} hasta las {1} {2} ", lstItinerariosPorDia[i][j].HoraInicio, lstItinerariosPorDia[i][j].HoraFin, sEspecialistas);
                                bSeAgregoAlgunaSesion = true;
                            }
                            else if (bSeAgregoAlgunaSesion && lstItinerariosPorDia[i].Count - 1 == j)
                            {
                                sItinerario += string.Format(" y desde las {0} hasta las {1} {2}.", lstItinerariosPorDia[i][j].HoraInicio, lstItinerariosPorDia[i][j].HoraFin, sEspecialistas);
                                bSeAgregoAlgunaSesion = true;
                            }
                        }
                    }
                }
                this.lblItinerario.Text = sItinerario;

            }
            else
            {
                this.lblItinerario.Text = "Este beneficiario no se encuentra ingresado en el itinerario.";
            }

        }

        private void ActualizarDiagnosticos()
        {
            List<cDiagnosticoBeneficiario> lstDiagnosticos = dFachada.DiagnosticoTraerTodosDiagnosticosPorBeneficiario(ElBeneficiario);
            cDiagnosticoBeneficiario unDB;
            if (lstDiagnosticos.Count > 0)
            {
                LosUltimosDiagnosticos = new List<cDiagnosticoBeneficiario>();
                LosHistorialDiagnosticos = new List<cDiagnosticoBeneficiario>();
                string sUltimaFecha = lstDiagnosticos[0].Fecha;
                for (int i = 0; i < lstDiagnosticos.Count; i++)
                {
                    if (sUltimaFecha == lstDiagnosticos[i].Fecha)
                    {
                        unDB = new cDiagnosticoBeneficiario();
                        unDB.Diagnostico = new cDiagnostico();
                        unDB.Diagnostico = lstDiagnosticos[i].Diagnostico;
                        unDB.Fecha = lstDiagnosticos[i].Fecha;
                        LosUltimosDiagnosticos.Add(unDB);
                    }
                    else
                    {
                        LosHistorialDiagnosticos.Add(lstDiagnosticos[i]);
                    }
                }
            }
        }

        private string QueDiaEs(cItinerario parItinerario)
        {
            switch (parItinerario.Dia)
            {
                case "L":
                    return "Lunes";
                case "M":
                    return "Martes";
                case "X":
                    return "Miércoles";
                case "J":
                    return "Jueves";
                case "V":
                    return "Viernes";
                default:
                    return "Sábado";
            }
        }

        protected void btnNuevoPlan_Click(object sender, EventArgs e)
        {
            this.pnlAgregarPlan.Visible = true;
            this.btnNuevoPlan.Visible = false;
            this.btnOcultar.Visible = true;
        }

        protected void btnOcultar_Click(object sender, EventArgs e)
        {
            this.pnlAgregarPlan.Visible = false;
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
            dFachada.PlanEliminar(LosPlanesActivos[e.RowIndex]);
            ActualizarTodo();
        }

        protected void cbPensionista_CheckedChanged(object sender, EventArgs e)
        {
            BPensionista = cbPensionista.Checked;
            txtAtributario.Enabled = !BPensionista;
            if (BPensionista)
            {
                txtAtributario.Text = string.Empty;
            }
        }

        protected void btnAgregarInforme_Click(object sender, EventArgs e)
        {
            Response.Redirect("vInformeNuevo.aspx?idBeneficiario=" + ElBeneficiario.Codigo.ToString()+"&ventana="+SVentanaAnterior);
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
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha podido habilitar al beneficiario.')", true);
            }

        }

        protected void btnInhabilitar_Click(object sender, EventArgs e)
        {
            if (LosPlanesActivos.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha podido inhabilitar al beneficiario porque hay planes activos.')", true);
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
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha podido inhabilitar al beneficiario.')", true);
                }
            }



        }

        protected void grdPlanesActivos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (LosPlanesActivos[e.NewSelectedIndex].FechaFin == null)
            {
                ElPlanAModificar = LosPlanesActivos[e.NewSelectedIndex];
                pnlModificarPlan.Visible = true;
                List<string> lstTipo = new List<string>() { ElPlanAModificar.Tipo };
                ddlTipoPlanModificar.DataSource = lstTipo;
                ddlTipoPlanModificar.DataBind();
                ddlTipoPlanModificar.Enabled = false;
                cboEvaluacionModificar.Checked = ElPlanAModificar.Evaluacion;
                cboEvaluacionModificar.Enabled = false;
                cboTratamientoModificar.Checked = ElPlanAModificar.Tratamiento;
                cboTratamientoModificar.Enabled = false;
                DateTime dFN = DateTime.Parse(ElPlanAModificar.FechaInicio);
                txtFechaInicioModificar.Text = dFN.ToString("yyyy-MM-dd");
                txtFechaInicioModificar.Enabled = false;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El Plan al que solicitar ingresar la fecha de vencimiento ya tiene una.')", true);
            }
        }
        protected void btnCancelarModificarPlan_Click(object sender, EventArgs e)
        {
            pnlModificarPlan.Visible = false;
            ddlTipoPlanModificar.DataSource = ElPlanAModificar.Tipo;
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
                ElPlanAModificar.FechaFin = txtFechaFinModificar.Text;
                if (dFachada.PlanModificarFechaVencimiento(ElPlanAModificar))
                {
                    lblMensaje.Text = "La fecha del plan se ha modificado correctamente";
                    pnlModificarPlan.Visible = false;
                    ddlTipoPlanModificar.DataSource = ElPlanAModificar.Tipo;
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

        protected void grdInformes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[4].Visible = false; //cod beneficiario
        }

        protected void btnAtras_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SVentanaAnterior);
        }
    }
}