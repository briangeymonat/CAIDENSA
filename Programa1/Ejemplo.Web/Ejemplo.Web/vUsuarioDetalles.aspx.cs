using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class DetallesUsuario : System.Web.UI.Page
    {

        static cUsuario ElUsuario;
        private static List<cSesion> LasSesiones;
        private static List<cItinerario> LosItinerarios;
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> LasHoras;
        private static List<List<cItinerario>> celdas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                int iX = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                if (iX > 9)
                    txtSemana.Text = DateTime.Today.ToString("yyyy-W" + iX);
                else
                    txtSemana.Text = DateTime.Today.ToString("yyyy-W0" + iX);


                LosItinerarios = new List<cItinerario>();
                CargarCombos();
                cargarDatos();
                CargarGrillaBeneficiariosQueAtiende();
                CargarGrillaInformesPendientes();
                CargarGrillaInformesRealizados();
                ModoEdicion(false);


                // CARGA DE HORAS
                DateTime dHora = DateTime.Parse("08:00");
                LasHoras = new List<DateTime>();
                LasHoras.Add(dHora);
                do
                {
                    dHora = dHora.AddMinutes(15);
                    LasHoras.Add(dHora);
                } while (dHora != DateTime.Parse("20:00"));
                if (ElUsuario.Especialidad.Codigo != 6)
                {
                    CargarCalendario();
                }
                else
                {
                    lblBeneficiariosQueAtiende.Visible = false;
                    lblInformesPendientes.Visible = false;
                    lblInformesRealizados.Visible = false;
                    pnlBeneficiariosQueAtiende.Visible = false;
                    pnlInformesPendientes.Visible = false;
                    pnlInformesRealizados.Visible = false;
                    lblItinerario.Visible = false;
                    @ref.Visible = false;
                }

                if (vMiPerfil.U.TipoContrato.ToString() == "Empleado")
                {
                    rblTipoDeEmpleado.SelectedIndex = 0;
                    lblVinculo.Visible = false;
                    rblTipoDeEmpleado.Visible = false;
                }
                if (vMiPerfil.U.TipoContrato.ToString() == "Contratado")
                {
                    rblTipoDeEmpleado.SelectedIndex = 1;
                    lblVinculo.Visible = false;
                    rblTipoDeEmpleado.Visible = false;
                }
                if (vMiPerfil.U.TipoContrato.ToString() == "Socio")
                {
                    rblTipoDeEmpleado.SelectedIndex = 2;
                }



                if (grdBeneficiariosQueAtiende.PageCount<=0)
                {
                    pnlBeneficiariosQueAtiende.Visible = false;
                    lblBeneficiariosQueAtiende.Text = "No atiende ningun beneficiario";
                }
                if (grdInformesRealizados.PageCount <= 0)
                {
                    pnlInformesRealizados.Visible = false;
                    lblInformesRealizados.Text = "No tiene informe realizados";
                }
                if (grdInformesPendientes.PageCount <= 0)
                {
                    pnlInformesPendientes.Visible = false;
                    lblInformesPendientes.Text = "No tiene informe pendiente";
                }
                if(LosItinerarios.Count<= 0 || LosItinerarios == null)
                {
                    pnlItinerario.Visible = false;
                    lblItinerario.Text = "No está registrado en el itinerario";
                    @ref.Visible = false;
                }

            }
        }


        private void CargarGrillaBeneficiariosQueAtiende()
        {
            grdBeneficiariosQueAtiende.DataSource = dFachada.BeneficiarioTraerTodosPorEspecialista(ElUsuario);
            grdBeneficiariosQueAtiende.DataBind();
        }
        private void CargarGrillaInformesRealizados()
        {
            List<cInforme> lstInformes = dFachada.InformeTraerTodosTerminadosPorEspecialista(ElUsuario);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes unInformeAListar;

            for (int i = 0; i < lstInformes.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes[i];
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

            grdInformesRealizados.DataSource = lstInformesParaListar;
            grdInformesRealizados.DataBind();
        }
        protected void CargarGrillaInformesPendientes()
        {
            List<cInforme> lstInformes = dFachada.InformeTraerTodosPendientesPorEspecialista(ElUsuario);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes unInformeAListar;

            for (int i = 0; i < lstInformes.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes[i];
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

            grdInformesPendientes.DataSource = lstInformesParaListar;
            grdInformesPendientes.DataBind();
        }





        protected void btnModificar_Click(object sender, EventArgs e)
        {
            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            cUsuario unUser = new cUsuario();
            unUser.NickName = txtNickName.Text;
            unUser = dFachada.UsuarioTraerEspecificoXNickName(unUser);


            if (unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo && unUser.Tipo == cUtilidades.TipoDeUsuario.Administrador)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede modificar datos de un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                ModoEdicion(true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ModoEdicion(false);
            cargarDatos();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosObligatorios())
            {

                cUsuario unUsuario = new cUsuario();
                unUsuario.Codigo = ElUsuario.Codigo;
                unUsuario.NickName = txtNickName.Text;
                unUsuario.Nombres = txtNombres.Text;
                unUsuario.Apellidos = txtApellidos.Text;
                unUsuario.CI = int.Parse(txtCi.Text);
                unUsuario.FechaNacimiento = txtFechaNac.Text;
                unUsuario.Domicilio = txtDomicilio.Text;
                unUsuario.Telefono = txtTelefono.Text;
                unUsuario.Email = txtEmail.Text;

                int i = dFachada.UsuarioVerificarNickNameYCi(unUsuario);
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El NickName o Cédula de identidad ya existe')", true);
                }
                else
                {
                    if (this.ddlTipoUsuario.SelectedValue == "Administrativo")
                    {
                        unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                    }
                    if (this.ddlTipoUsuario.SelectedValue == "Administrador")
                    {
                        unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                    }
                    if (this.ddlTipoUsuario.SelectedValue == "Usuario")
                    {
                        unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                    }
                    string sSeleccionado = rblTipoDeEmpleado.SelectedValue;
                    if (sSeleccionado == "Socio")
                    {
                        unUsuario.TipoContrato = "S";
                    }
                    if (sSeleccionado == "Empleado")
                    {
                        unUsuario.TipoContrato = "E";
                    }
                    if (sSeleccionado == "Contratado")
                    {
                        unUsuario.TipoContrato = "C";
                    }
                    unUsuario.Especialidad = new cEspecialidad();
                    unUsuario.Especialidad.Codigo = int.Parse(this.ddlEspecialidad.SelectedValue);
                    try
                    {
                        bool bResultado = dFachada.UsuarioModificar(unUsuario);
                        if (bResultado)
                        {
                            lblMensaje.Text = "Modificado correctamente";
                            ModoEdicion(false);
                        }
                        else
                        {
                            lblMensaje.Text = "ERROR: No se pudo agregar";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos obligatorios.')", true);
            }
        }

        private void cargarDatos()
        {
            string sNickname = Request.QueryString["nickname"];
            cUsuario unUsuario = new cUsuario();
            unUsuario.NickName = sNickname;

            unUsuario = dFachada.UsuarioTraerEspecificoXNickName(unUsuario);
            ElUsuario = new cUsuario();
            ElUsuario = unUsuario;
            txtNickName.Text = unUsuario.NickName;
            txtNombres.Text = unUsuario.Nombres;
            txtApellidos.Text = unUsuario.Apellidos;
            txtCi.Text = unUsuario.CI.ToString();
            DateTime dFn = new DateTime();
            if (unUsuario.FechaNacimiento!=null)
            {
                dFn = DateTime.Parse(unUsuario.FechaNacimiento);
                txtFechaNac.Text = dFn.ToString("yyyy-MM-dd");
            }
            else
            {
                txtFechaNac.Text = string.Empty;
            }
            txtDomicilio.Text = unUsuario.Domicilio;
            txtTelefono.Text = unUsuario.Telefono;
            txtEmail.Text = unUsuario.Email;
            ddlTipoUsuario.SelectedValue = unUsuario.Tipo.ToString();
            ddlEspecialidad.SelectedIndex = (unUsuario.Especialidad.Codigo - 1);
            if (unUsuario.TipoContrato.ToString() == "Empleado")
            {
                rblTipoDeEmpleado.SelectedIndex = 0;
            }
            if (unUsuario.TipoContrato.ToString() == "Contratado")
            {
                rblTipoDeEmpleado.SelectedIndex = 1;
            }
            if (unUsuario.TipoContrato.ToString() == "Socio")
            {
                rblTipoDeEmpleado.SelectedIndex = 2;
            }
        }
        private void CargarCombos()
        {
            ddlTipoUsuario.DataSource = Enum.GetNames(typeof(cUtilidades.TipoDeUsuario));
            ddlTipoUsuario.DataBind();
            ddlEspecialidad.DataSource = dFachada.EspecialidadTraerTodas();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Codigo";
            ddlEspecialidad.DataBind();
        }
        private void ModoEdicion(bool pVisible)
        {
            cUsuario unUsuario = new cUsuario();
            unUsuario.NickName = txtNickName.Text;
            unUsuario = dFachada.UsuarioTraerEspecificoXNickName(unUsuario);

            txtNickName.Enabled = pVisible;
            txtNombres.Enabled = pVisible;
            txtApellidos.Enabled = pVisible;
            txtCi.Enabled = pVisible;
            txtFechaNac.Enabled = pVisible;
            txtDomicilio.Enabled = pVisible;
            txtTelefono.Enabled = pVisible;
            txtEmail.Enabled = pVisible;
            ddlTipoUsuario.Enabled = pVisible;
            ddlEspecialidad.Enabled = pVisible;
            rblTipoDeEmpleado.Enabled = pVisible;


            btnModificar.Visible = !pVisible;

            btnConfirmar.Visible = pVisible;
            btnCancelar.Visible = pVisible;

            lblObligatorio1.Visible = pVisible;
            lblObligatorio2.Visible = pVisible;
            lblObligatorio3.Visible = pVisible;
            lblObligatorio4.Visible = pVisible;


            if (unUsuario.Estado)//si esta activo
            {

                btnInhabilitar.Visible = true;
                btnHabilitar.Visible = false;
                btnRestablecerContrasena.Visible = true;
            }
            else //si esta inactivo
            {
                btnInhabilitar.Visible = false;
                btnHabilitar.Visible = true;
                btnModificar.Visible = false;
                btnRestablecerContrasena.Visible = false;
            }


        }
        private bool FaltanDatosObligatorios()
        {
            if (this.txtNickName.Text == string.Empty || this.txtNombres.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtCi.Text == string.Empty)
            {
                return true;
            }
            return false;
        }

        protected void btnInhabilitar_Click(object sender, EventArgs e)
        {
            cUsuario unUser = new cUsuario();
            unUser = ElUsuario;

            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            if (unUser.Tipo == cUtilidades.TipoDeUsuario.Administrador && unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede inhabilitar un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                int i = dFachada.UsuarioCantidadAdministradoresActivos();
                if (unUser.Tipo == cUtilidades.TipoDeUsuario.Administrador && i == 1)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Debe haber al menos un usuario administrador activo')", true);
                }
                else
                {
                    try
                    {
                        bool bResultado = dFachada.UsuarioEliminar(unUser);
                        if (bResultado)
                        {
                            lblMensaje.Text = "Inhabilitado correctamente";
                            ModoEdicion(false);
                        }
                        else
                        {
                            lblMensaje.Text = "ERROR: No se pudo inhabilitar";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            cUsuario unUser = new cUsuario();
            unUser = ElUsuario;

            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            if (ElUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrador && unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede habilitar un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                try
                {
                    bool bResultado = dFachada.UsuarioHabilitar(unUser);
                    if (bResultado)
                    {
                        lblMensaje.Text = "Habilitado correctamente";
                        ModoEdicion(false);
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR: No se pudo habilitar";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        protected void btnRestablecerContrasena_Click(object sender, EventArgs e)
        {
            cUsuario unUser = new cUsuario();
            unUser = ElUsuario;

            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            if (ElUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrador && unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede restablecer la contraseña de un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                try
                {
                    bool sResultado = dFachada.UsuarioRestablecerContrasena(unUser);
                    if (sResultado)
                    {
                        lblMensaje.Text = "Contraseña restablecida correctamente";
                        ModoEdicion(false);
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR: No se pudo restablecer la contraseña";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        protected void grdBeneficiariosQueAtiende_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            vMiPerfil.ii = 1;
            TableCell celdaCodigo = grdBeneficiariosQueAtiende.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + iCodigo.ToString()+"&ventana=vUsuarioDetalles.aspx?nickname="+ElUsuario.NickName);
        }

        protected void grdInformesPendientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesPendientes.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + iCodigo.ToString());
        }

        protected void grdBeneficiariosQueAtiende_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[5].Visible = false; //sexo
            e.Row.Cells[7].Visible = false; //tel2
            e.Row.Cells[8].Visible = false;//email
            e.Row.Cells[9].Visible = false;//domicilio
            e.Row.Cells[10].Visible = false;//fecha de nacimiento
            e.Row.Cells[11].Visible = false;//atributario
            e.Row.Cells[12].Visible = false;//motivo consulta
            e.Row.Cells[13].Visible = false;//escolaridad
            e.Row.Cells[14].Visible = false;//derivador
            e.Row.Cells[15].Visible = false;//estado
        }

        protected void grdInformesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //fecha
            e.Row.Cells[5].Visible = false; //codBeneficiario
        }

        protected void grdInformesRealizados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[5].Visible = false; //codBeneficiario
        }

        private void CargarItinerarios()
        {
            pnlItinerario.Visible = true;

            LosItinerarios = dFachada.ItinerarioTraerTodosPorEspecialista(ElUsuario);


            // ↓↓↓↓ ARMADO DEL HEADER ↓↓↓↓

            string sItinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < LosDias.Count; i++)
            {
                sItinerarios += "<td style='color:#000000; background-color:#80B7D8;width:100px;'>" + LosDias[i] + "</td>";
            }
            sItinerarios += "</tr>";

            // ↑↑↑↑ ARMADO DEL HEADER ↑↑↑↑

            #region ORDENAMIENTO DE SESIONES SEGÚN EL DÍA

            celdas = new List<List<cItinerario>>();

            for (int i = 0; i < LasHoras.Count; i++)
            {
                celdas.Add(new List<cItinerario>());

                for (int j = 0; j < LosDias.Count; j++)
                {
                    bool bHayAlgunaSesion = false;
                    for (int k = 0; k < LosItinerarios.Count; k++)
                    {
                        if (!bHayAlgunaSesion)
                        {
                            if (DateTime.Parse(LosItinerarios[k].HoraInicio) >= LasHoras[i] && DateTime.Parse(LosItinerarios[k].HoraInicio) < LasHoras[i + 1])
                            {
                                if (QueDiaEs(LosItinerarios[k]) == LosDias[j])
                                {
                                    bHayAlgunaSesion = true;
                                    celdas[i].Add(LosItinerarios[k]);
                                }
                            }
                        }
                    }
                    if (!bHayAlgunaSesion) { celdas[i].Add(new cItinerario()); }
                }
            }

            for (int i = 0; i < LasHoras.Count; i++)
            {
                sItinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for (int j = 0; j < LosDias.Count; j++)
                {
                    if (celdas[i][j].Comentario == null)
                    {
                        sItinerarios += "<td style='background-color:#FF8e8e; color:#5186A6;width:100px;' rowspan={0}></td>";
                    }
                    else
                    {
                        if (celdas[i][j].Comentario != "NO_LISTAR")
                        {
                            string sNombres = "";
                            foreach (cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                            {
                                string sColor = "";
                                switch (unBeneficiario.Plan.Tipo)
                                {
                                    case "ASSE":
                                        sColor = "#58FAF4";
                                        break;
                                    case "AYEX":
                                        sColor = "#8afa38";
                                        break;
                                    case "CAMEC":
                                        sColor = "#58FAF4";
                                        break;
                                    case "Círculo Católico":
                                        sColor = "#58FAF4";
                                        break;
                                    case "MIDES":
                                        sColor = "#F3F781";
                                        break;
                                    case "Particular":
                                        sColor = "#FE9A2E";
                                        break;
                                    case "Policial":
                                        sColor = "#58FAF4";
                                        break;
                                    default:
                                        sColor = "#ffffff";
                                        break;
                                }
                                sNombres += "<p style='background-color:" + sColor + ";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos + "</p>";
                            }
                            int iFilas = 0;
                            for (int k = i; k < LasHoras.Count; k++)
                            {
                                if (DateTime.Parse(celdas[i][j].HoraFin) > LasHoras[k])
                                {
                                    iFilas++;
                                    celdas[k][j].Comentario = "NO_LISTAR";
                                }
                            }
                            string sCentro = "";
                            if (celdas[i][j].Centro == cUtilidades.Centro.JuanLacaze) sCentro = " - JL"; else sCentro = " - NH";
                            sItinerarios += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                "<p style='padding:5px 0px; margin:0px;width:100px;'>" + celdas[i][j].TipoSesion + sCentro + "</p> " + sNombres, iFilas, ((iFilas * 25) + iFilas));
                        }
                    }
                }
                sItinerarios += "</tr>";
            }
            sItinerarios += "</table>";

            frmItinerario.InnerHtml = sItinerarios;

            #endregion

        }

        private void CargarSesionesReprogramadas()
        {
            string sAñoSemana = txtSemana.Text;

            string[] aPartes = sAñoSemana.Split('-');
            int iAño = int.Parse(aPartes[0].ToString());

            aPartes = sAñoSemana.Split('W');
            int iSemana = int.Parse(aPartes[1].ToString());

            DateTime dFechaInicial = DateTime.Parse("01/01/" + iAño);
            DateTime dHoy = DateTime.Today;
            dFechaInicial = dFechaInicial.AddDays(7 * (iSemana - 1));
            while (dFechaInicial.DayOfWeek != DayOfWeek.Monday)
            {
                dFechaInicial = dFechaInicial.AddDays(-1);
            }
            DateTime dFechaFinal = dFechaInicial.AddDays(6);
            LasSesiones = dFachada.SesionTraerPorRango(dFechaInicial, dFechaFinal, ElUsuario);

            pnlItinerario.Visible = true;

            // ↓↓↓↓ ARMADO DEL HEADER ↓↓↓↓

            string sSesiones = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < LosDias.Count; i++)
            {
                string sColor = "80B7D8";
                if (i == DateTime.Today.DayOfWeek.GetHashCode() - 1)
                    sColor = "FF86FD";

                sSesiones += "<td style='height:20px;color:#000000; background-color:#" + sColor + ";width:100px;'>" + LosDias[i] + "</td>";
            }
            sSesiones += "</tr>";

            // ↑↑↑↑ ARMADO DEL HEADER ↑↑↑↑

            #region ORDENAMIENTO DE SESIONES SEGÚN EL DÍA

            List<List<cSesion>> lstCeldas = new List<List<cSesion>>();

            for (int i = 0; i < LasHoras.Count; i++)
            {
                lstCeldas.Add(new List<cSesion>());

                for (int j = 0; j < LosDias.Count; j++)
                {
                    bool bHayAlgunaSesion = false;
                    for (int k = 0; k < LasSesiones.Count; k++)
                    {
                        if (!bHayAlgunaSesion)
                        {
                            if (DateTime.Parse(LasSesiones[k].HoraInicio) >= LasHoras[i] && DateTime.Parse(LasSesiones[k].HoraInicio) < LasHoras[i + 1])
                            {
                                if (DateTime.Parse(LasSesiones[k].Fecha).DayOfWeek.GetHashCode() - 1 == j)
                                {
                                    bHayAlgunaSesion = true;
                                    lstCeldas[i].Add(LasSesiones[k]);
                                }
                            }
                        }
                    }
                    if (!bHayAlgunaSesion) { lstCeldas[i].Add(new cSesion()); }
                }
            }

            for (int i = 0; i < LasHoras.Count; i++)
            {
                sSesiones += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for (int j = 0; j < LosDias.Count; j++)
                {
                    if (lstCeldas[i][j].Comentario == null)
                    {
                        sSesiones += "<td style='background-color:#FF8e8e; color:#5186A6;width:100px;' rowspan={0}></td>";
                    }
                    else
                    {
                        if (lstCeldas[i][j].Comentario != "NO_LISTAR")
                        {
                            string sNombres = "";
                            foreach (cBeneficiarioSesion unBeneficiario in lstCeldas[i][j].lstBeneficiarios)
                            {
                                string sColor = "";
                                switch (unBeneficiario.Plan.Tipo)
                                {
                                    case "ASSE":
                                        sColor = "#58FAF4";
                                        break;
                                    case "AYEX":
                                        sColor = "#8afa38";
                                        break;
                                    case "CAMEC":
                                        sColor = "#58FAF4";
                                        break;
                                    case "Círculo Católico":
                                        sColor = "#58FAF4";
                                        break;
                                    case "MIDES":
                                        sColor = "#F3F781";
                                        break;
                                    case "Particular":
                                        sColor = "#FE9A2E";
                                        break;
                                    case "Policial":
                                        sColor = "#58FAF4";
                                        break;
                                    default:
                                        sColor = "#ffffff";
                                        break;
                                }
                                sNombres += "<p style='background-color:" + sColor + ";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos + "</p>";
                            }
                            int iFilas = 0;
                            for (int k = i; k < LasHoras.Count; k++)
                            {
                                if (DateTime.Parse(lstCeldas[i][j].HoraFin) > LasHoras[k])
                                {
                                    iFilas++;
                                    lstCeldas[k][j].Comentario = "NO_LISTAR";
                                }
                            }
                            string sCentro = "";
                            if (lstCeldas[i][j].Centro == cUtilidades.Centro.JuanLacaze) sCentro = " - JL"; else sCentro = " - NH";
                            sSesiones += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                "<p style='padding:5px 0px; margin:0px;width:100px;'>" + lstCeldas[i][j].TipoSesion + sCentro + "</p> " + sNombres, iFilas, ((iFilas * 25) + iFilas));
                        }
                    }
                }
                sSesiones += "</tr>";
            }
            sSesiones += "</table>";

            frmItinerario.InnerHtml = sSesiones;

            #endregion


        }
        private void CargarCalendario()
        {
            if (rblCalendario.SelectedIndex == 0)
            {
                lblItinerario.Text = "Itinerario semanal";
                txtSemana.Visible = false;
                CargarItinerarios();
            }
            else
            {
                lblItinerario.Text = "Sesiones reprogramadas";
                txtSemana.Visible = true;
                CargarSesionesReprogramadas();
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

        protected void rblCalendario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCalendario();
        }

        protected void txtSemana_TextChanged(object sender, EventArgs e)
        {
            CargarSesionesReprogramadas();
        }

        protected void btnAtras_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vUsuarioMostrar.aspx");
        }

        protected void grdInformesRealizados_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesRealizados.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeDetalles.aspx?InformeId=" + iCodigo.ToString()+ "&Ventana=vUsuarioDetalles.aspx?nickname="+ElUsuario.NickName);
        }
    }
}