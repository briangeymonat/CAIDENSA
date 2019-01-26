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
    public partial class vMiPerfil : System.Web.UI.Page
    {
        public static cUsuario U;
        public static int i = 0;
        public static int ii = 0;
        private static List<cItinerario> LosItinerarios;
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> LosHoras;
        private static List<List<cItinerario>> LasCeldas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosItinerarios = new List<cItinerario>();
                if (i == 0)
                {
                    cargarUsuarioLogeado();
                }
                CargarCombos();
                cargarDatos();
                CargarGrillaBeneficiariosQueAtiende();
                CargarGrillaInformesPendientes();
                CargarGrillaInformesRealizados();
                ModoEdicion(false);


                // CARGA DE HORAS
                DateTime dHora = DateTime.Parse("08:00");
                LosHoras = new List<DateTime>();
                LosHoras.Add(dHora);
                do
                {
                    dHora = dHora.AddMinutes(15);
                    LosHoras.Add(dHora);
                } while (dHora != DateTime.Parse("20:00"));
                if (U.Especialidad.Codigo != 6)
                {
                    CargarItinerarios();
                }
                else
                {
                    lblBeneficiariosQueAtiende.Visible = false;
                    lblInformesPendientes.Visible = false;
                    lblInformesRealizados.Visible = false;
                    grdBeneficiariosQueAtiende.Visible = false;
                    grdInformesPendientes.Visible = false;
                    grdInformesRealizados.Visible = false;
                    @ref.Visible = false;
                }
                if (grdBeneficiariosQueAtiende.PageCount <= 0)
                {
                    grdBeneficiariosQueAtiende.Visible = false;
                    lblBeneficiariosQueAtiende.Text = "No atiende ningun beneficiario";
                }
                if (grdInformesRealizados.PageCount <= 0)
                {
                    grdInformesRealizados.Visible = false;
                    lblInformesRealizados.Text = "No tiene informe realizados";
                }
                if (grdInformesPendientes.PageCount <= 0)
                {
                    grdInformesPendientes.Visible = false;
                    lblInformesPendientes.Text = "No tiene informe pendiente";
                }
                if (LosItinerarios.Count <= 0 || LosItinerarios==null)
                {
                    frmItinerario.Visible = false;
                    lblItinerario.Text = "No está registrado en el itinerario";
                    @ref.Visible = false;
                }

            }
        }
        private void cargarUsuarioLogeado()
        {
            string sNickname = Request.QueryString["nick"];
            cUsuario unUsuario = new cUsuario();
            unUsuario.NickName = sNickname;
            try
            {
                unUsuario = dFachada.UsuarioTraerEspecificoXNickName(unUsuario);
                U = unUsuario;
                if (unUsuario == null)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo iniciar sesion.')", true);
                    Response.Redirect("vLogin.aspx");
                }
                else
                {
                    i = 1;
                    Response.Redirect("vInicio.aspx?nick=" + unUsuario.NickName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarDatos()
        {
            txtNickName.Text = U.NickName;
            txtNombres.Text = U.Nombres;
            txtApellidos.Text = U.Apellidos;
            txtCi.Text = U.CI.ToString();
            DateTime dFN = new DateTime();
            if (U.FechaNacimiento != null)
            {
                dFN = DateTime.Parse(U.FechaNacimiento);
                txtFechaNac.Text = dFN.ToString("yyyy-MM-dd");
            }
            else
            {
                txtFechaNac.Text = string.Empty;
            }
            txtDomicilio.Text = U.Domicilio;
            txtTelefono.Text = U.Telefono;
            txtEmail.Text = U.Email;
            ddlTipoUsuario.SelectedValue = U.Tipo.ToString();
            ddlEspecialidad.SelectedIndex = (U.Especialidad.Codigo - 1);
            if (U.TipoContrato.ToString() == "Empleado")
            {
                rbTipoDeEmpleado.SelectedIndex = 0;
            }
            if (U.TipoContrato.ToString() == "Contratado")
            {
                rbTipoDeEmpleado.SelectedIndex = 1;
            }
            if (U.TipoContrato.ToString() == "Socio")
            {
                rbTipoDeEmpleado.SelectedIndex = 2;
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

        private void CargarGrillaBeneficiariosQueAtiende()
        {
            grdBeneficiariosQueAtiende.DataSource = dFachada.BeneficiarioTraerTodosPorEspecialista(U);
            grdBeneficiariosQueAtiende.DataBind();
        }
        private void CargarGrillaInformesRealizados()
        {
            List<cInforme> lstInformes = dFachada.InformeTraerTodosTerminadosPorEspecialista(U);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes lstinformeAListar;

            for (int i = 0; i < lstInformes.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes[i];
                unInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(unInforme.Beneficiario);
                lstinformeAListar = new ListarInformes();
                lstinformeAListar.Codigo = unInforme.Codigo;
                lstinformeAListar.Fecha = unInforme.Fecha;
                lstinformeAListar.Estado = unInforme.Estado;
                lstinformeAListar.Tipo = unInforme.Tipo;
                lstinformeAListar.CodigoBeneficiario = unInforme.Beneficiario.Codigo;
                lstinformeAListar.Nombres = unInforme.Beneficiario.Nombres;
                lstinformeAListar.Apellidos = unInforme.Beneficiario.Apellidos;
                lstInformesParaListar.Add(lstinformeAListar);
            }

            grdInformesRealizados.DataSource = lstInformesParaListar;
            grdInformesRealizados.DataBind();
        }
        protected void CargarGrillaInformesPendientes()
        {
            List<cInforme> lstInformes = dFachada.InformeTraerTodosPendientesPorEspecialista(vMiPerfil.U);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes informeAListar;

            for (int i = 0; i < lstInformes.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes[i];
                unInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(unInforme.Beneficiario);
                informeAListar = new ListarInformes();
                informeAListar.Codigo = unInforme.Codigo;
                informeAListar.Fecha = unInforme.Fecha;
                informeAListar.Estado = unInforme.Estado;
                informeAListar.Tipo = unInforme.Tipo;
                informeAListar.CodigoBeneficiario = unInforme.Beneficiario.Codigo;
                informeAListar.Nombres = unInforme.Beneficiario.Nombres;
                informeAListar.Apellidos = unInforme.Beneficiario.Apellidos;
                lstInformesParaListar.Add(informeAListar);
            }

            grdInformesPendientes.DataSource = lstInformesParaListar;
            grdInformesPendientes.DataBind();
        }

        private void ModoEdicion(bool pVisible)
        {
            txtNickName.Enabled = pVisible;
            txtNombres.Enabled = pVisible;
            txtApellidos.Enabled = pVisible;
            txtCi.Enabled = pVisible;
            txtFechaNac.Enabled = pVisible;
            txtDomicilio.Enabled = pVisible;
            txtTelefono.Enabled = pVisible;
            txtEmail.Enabled = pVisible;
            ddlEspecialidad.Enabled = pVisible;
            rbTipoDeEmpleado.Enabled = pVisible;


            btnModificar.Visible = !pVisible;

            btnConfirmar.Visible = pVisible;
            btnCancelar.Visible = pVisible;

            lblObligatorio1.Visible = pVisible;
            lblObligatorio2.Visible = pVisible;
            lblObligatorio3.Visible = pVisible;
            lblObligatorio4.Visible = pVisible;

        }
        private bool FaltanDatosObligatorios()
        {
            if (this.txtNickName.Text == string.Empty || this.txtNombres.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtCi.Text == string.Empty ||
                (this.rbTipoDeEmpleado.SelectedIndex != 0 && this.rbTipoDeEmpleado.SelectedIndex != 1 && this.rbTipoDeEmpleado.SelectedIndex != 2))
            {
                return true;
            }
            return false;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosObligatorios())
            {
                ModoEdicion(false);
                cUsuario unUsuario = new cUsuario();
                unUsuario.Codigo = U.Codigo;
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
                    string sSeleccionado = rbTipoDeEmpleado.SelectedValue;
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
                            U = unUsuario;
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ModoEdicion(false);
            cargarDatos();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            ModoEdicion(true);
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

        protected void grdInformesRealizados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[4].Visible = false; //codBeneficiario
        }

        protected void grdInformesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //fecha
            e.Row.Cells[5].Visible = false; //codBeneficiario
        }

        protected void grdBeneficiariosQueAtiende_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            ii = 1;
            TableCell celdaCodigo = grdBeneficiariosQueAtiende.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + iCodigo.ToString());
        }

        protected void grdInformesPendientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesPendientes.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + iCodigo.ToString());
        }

        private void CargarItinerarios()
        {
            pnlItinerario.Visible = true;

            LosItinerarios = dFachada.ItinerarioTraerTodosPorEspecialista(U);


            // ↓↓↓↓ ARMADO DEL HEADER ↓↓↓↓

            string sItinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < LosDias.Count; i++)
            {
                sItinerarios += "<td style='color:#000000; background-color:#80B7D8;width:100px;'>" + LosDias[i] + "</td>";
            }
            sItinerarios += "</tr>";

            // ↑↑↑↑ ARMADO DEL HEADER ↑↑↑↑

            #region ORDENAMIENTO DE SESIONES SEGÚN EL DÍA

            LasCeldas = new List<List<cItinerario>>();

            for (int i = 0; i < LosHoras.Count; i++)
            {
                LasCeldas.Add(new List<cItinerario>());

                for(int j=0;j<LosDias.Count;j++)
                {
                    bool bHayAlgunaSesion = false;
                    for(int k=0; k<LosItinerarios.Count;k++)
                    {
                        if(!bHayAlgunaSesion)
                        {
                            if (DateTime.Parse(LosItinerarios[k].HoraInicio) >= LosHoras[i] && DateTime.Parse(LosItinerarios[k].HoraInicio) < LosHoras[i + 1])
                            {
                                if(QueDiaEs(LosItinerarios[k])==LosDias[j])
                                {
                                    bHayAlgunaSesion = true;
                                    LasCeldas[i].Add(LosItinerarios[k]);
                                }
                            }
                        }
                    }
                    if (!bHayAlgunaSesion) { LasCeldas[i].Add(new cItinerario()); }
                }
            }

            for(int i=0; i<LosHoras.Count;i++)
            {
                sItinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LosHoras[i].ToShortTimeString() + "</td>";
                for(int j=0; j<LosDias.Count;j++)
                {
                    if(LasCeldas[i][j].Comentario==null)
                    {
                        sItinerarios += "<td style='background-color:#FF8e8e; color:#5186A6;width:100px;' rowspan={0}></td>";
                    }
                    else
                    {
                        if(LasCeldas[i][j].Comentario != "NO_LISTAR")
                        {
                            string sNombres = "";
                            foreach( cBeneficiarioItinerario unBeneficiario in LasCeldas[i][j].lstBeneficiarios)
                            {
                                string sColor = "";
                                switch(unBeneficiario.Plan.Tipo)
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
                                sNombres += "<p style='background-color:"+sColor+";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos+"</p>";
                            }
                            int iFilas = 0;
                            for(int k=i; k<LosHoras.Count;k++)
                            {
                                if(DateTime.Parse(LasCeldas[i][j].HoraFin)>LosHoras[k])
                                {
                                    iFilas++;
                                    LasCeldas[k][j].Comentario = "NO_LISTAR";
                                }
                            }
                            string sCentro = "";
                            if (LasCeldas[i][j].Centro == cUtilidades.Centro.JuanLacaze) sCentro = " - JL"; else sCentro = " - NH";
                            sItinerarios += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                "<p style='padding:5px 0px; margin:0px;width:100px;'>" + LasCeldas[i][j].TipoSesion +sCentro+ "</p> " + sNombres , iFilas, ((iFilas * 25) + iFilas));
                        }
                    }
                }
                sItinerarios += "</tr>";
            }
            sItinerarios += "</table>";

            frmItinerario.InnerHtml = sItinerarios;

            #endregion

        }
        private string QueDiaEs(cItinerario parItinerario)
        {
            switch(parItinerario.Dia)
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
    }
}