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
        private static List<cItinerario> lstItinerarios;
        private static List<string> lstDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> lstHoras;
        private static List<List<cItinerario>> celdas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                DateTime hora = DateTime.Parse("08:00");
                lstHoras = new List<DateTime>();
                lstHoras.Add(hora);
                do
                {
                    hora = hora.AddMinutes(15);
                    lstHoras.Add(hora);
                } while (hora != DateTime.Parse("20:00"));
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
                if (lstItinerarios.Count <= 0)
                {
                    frmItinerario.Visible = false;
                    lblItinerario.Text = "No está registrado en el itinerario";
                }

            }
        }
        private void cargarUsuarioLogeado()
        {
            string nickname = Request.QueryString["nick"];
            cUsuario unUsuario = new cUsuario();
            unUsuario.NickName = nickname;
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
            DateTime fn = new DateTime();
            if (U.FechaNacimiento != null)
            {
                fn = DateTime.Parse(U.FechaNacimiento);
                txtFechaNac.Text = fn.ToString("yyyy-MM-dd");
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
                    string seleccionado = rbTipoDeEmpleado.SelectedValue;
                    if (seleccionado == "Socio")
                    {
                        unUsuario.TipoContrato = "S";
                    }
                    if (seleccionado == "Empleado")
                    {
                        unUsuario.TipoContrato = "E";
                    }
                    if (seleccionado == "Contratado")
                    {
                        unUsuario.TipoContrato = "C";
                    }
                    unUsuario.Especialidad = new cEspecialidad();
                    unUsuario.Especialidad.Codigo = int.Parse(this.ddlEspecialidad.SelectedValue);

                    try
                    {
                        bool resultado = dFachada.UsuarioModificar(unUsuario);
                        if (resultado)
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
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + codigo.ToString());
        }

        protected void grdInformesPendientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesPendientes.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + codigo.ToString());
        }

        private void CargarItinerarios()
        {
            pnlItinerario.Visible = true;

            lstItinerarios = dFachada.ItinerarioTraerTodosPorEspecialista(U);


            // ↓↓↓↓ ARMADO DEL HEADER ↓↓↓↓

            string Itinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < lstDias.Count; i++)
            {
                Itinerarios += "<td style='color:#000000; background-color:#80B7D8;width:100px;'>" + lstDias[i] + "</td>";
            }
            Itinerarios += "</tr>";

            // ↑↑↑↑ ARMADO DEL HEADER ↑↑↑↑

            #region ORDENAMIENTO DE SESIONES SEGÚN EL DÍA

            celdas = new List<List<cItinerario>>();

            for (int i = 0; i < lstHoras.Count; i++)
            {
                celdas.Add(new List<cItinerario>());

                for(int j=0;j<lstDias.Count;j++)
                {
                    bool HayAlgunaSesion = false;
                    for(int k=0; k<lstItinerarios.Count;k++)
                    {
                        if(!HayAlgunaSesion)
                        {
                            if (DateTime.Parse(lstItinerarios[k].HoraInicio) >= lstHoras[i] && DateTime.Parse(lstItinerarios[k].HoraInicio) < lstHoras[i + 1])
                            {
                                if(QueDiaEs(lstItinerarios[k])==lstDias[j])
                                {
                                    HayAlgunaSesion = true;
                                    celdas[i].Add(lstItinerarios[k]);
                                }
                            }
                        }
                    }
                    if (!HayAlgunaSesion) { celdas[i].Add(new cItinerario()); }
                }
            }

            for(int i=0; i<lstHoras.Count;i++)
            {
                Itinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + lstHoras[i].ToShortTimeString() + "</td>";
                for(int j=0; j<lstDias.Count;j++)
                {
                    if(celdas[i][j].Comentario==null)
                    {
                        Itinerarios += "<td style='background-color:#FF8e8e; color:#5186A6;width:100px;' rowspan={0}></td>";
                    }
                    else
                    {
                        if(celdas[i][j].Comentario != "NO_LISTAR")
                        {
                            string nombres = "";
                            foreach( cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                            {
                                string color = "";
                                switch(unBeneficiario.Plan.Tipo)
                                {
                                    case "ASSE":
                                        color = "#58FAF4";
                                        break;
                                    case "AYEX":
                                        color = "#8afa38";
                                        break;
                                    case "CAMEC":
                                        color = "#58FAF4";
                                        break;
                                    case "Círculo Católico":
                                        color = "#58FAF4";
                                        break;
                                    case "MIDES":
                                        color = "#F3F781";
                                        break;
                                    case "Particular":
                                        color = "#FE9A2E";
                                        break;
                                    case "Policial":
                                        color = "#58FAF4";
                                        break;
                                    default:
                                        color = "#ffffff";
                                        break;
                                }
                                nombres += "<p style='background-color:"+color+";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos+"</p>";
                            }
                            int filas = 0;
                            for(int k=i; k<lstHoras.Count;k++)
                            {
                                if(DateTime.Parse(celdas[i][j].HoraFin)>lstHoras[k])
                                {
                                    filas++;
                                    celdas[k][j].Comentario = "NO_LISTAR";
                                }
                            }
                            string centro = "";
                            if (celdas[i][j].Centro == cUtilidades.Centro.JuanLacaze) centro = " - JL"; else centro = " - NH";
                            Itinerarios += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                "<p style='padding:5px 0px; margin:0px;width:100px;'>" + celdas[i][j].TipoSesion +centro+ "</p> " + nombres , filas, ((filas * 25) + filas));
                        }
                    }
                }
                Itinerarios += "</tr>";
            }
            Itinerarios += "</table>";

            frmItinerario.InnerHtml = Itinerarios;

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