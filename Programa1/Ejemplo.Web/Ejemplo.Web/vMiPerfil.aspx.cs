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
        private static List<DateTime> LasHoras;
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
                LasHoras = new List<DateTime>();
                LasHoras.Add(hora);
                do
                {
                    hora = hora.AddMinutes(15);
                    LasHoras.Add(hora);
                } while (hora != DateTime.Parse("20:00"));
                if (U.Especialidad.Codigo != 6)
                {
                    CargarItinerarios();
                }
                
            }
        }
        private void cargarUsuarioLogeado()
        {
            string nickname = Request.QueryString["nick"];
            cUsuario usuario = new cUsuario();
            usuario.NickName = nickname;
            try
            {
                usuario = dFachada.UsuarioTraerEspecificoXNickName(usuario);
                U = usuario;
                if (usuario == null)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo iniciar sesion.')", true);
                    Response.Redirect("vLogin.aspx");
                }
                else
                {
                    i = 1;
                    Response.Redirect("vInicio.aspx?nick=" + usuario.NickName);
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
            if (U.FechaNacimiento != "")
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
            List<cInforme> ListaInformes = dFachada.InformeTraerTodosTerminadosPorEspecialista(U);
            cInforme informe;

            List<ListarInformes> ListaInformesParaListar = new List<ListarInformes>();
            ListarInformes informeAListar;

            for (int i = 0; i < ListaInformes.Count; i++)
            {
                informe = new cInforme();
                informe = ListaInformes[i];
                informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(informe.Beneficiario);
                informeAListar = new ListarInformes();
                informeAListar.Codigo = informe.Codigo;
                informeAListar.Fecha = informe.Fecha;
                informeAListar.Estado = informe.Estado;
                informeAListar.Tipo = informe.Tipo;
                informeAListar.CodigoBeneficiario = informe.Beneficiario.Codigo;
                informeAListar.Nombres = informe.Beneficiario.Nombres;
                informeAListar.Apellidos = informe.Beneficiario.Apellidos;
                ListaInformesParaListar.Add(informeAListar);
            }

            grdInformesRealizados.DataSource = ListaInformesParaListar;
            grdInformesRealizados.DataBind();
        }
        protected void CargarGrillaInformesPendientes()
        {
            List<cInforme> ListaInformes = dFachada.InformeTraerTodosPendientesPorEspecialista(vMiPerfil.U);
            cInforme informe;

            List<ListarInformes> ListaInformesParaListar = new List<ListarInformes>();
            ListarInformes informeAListar;

            for (int i = 0; i < ListaInformes.Count; i++)
            {
                informe = new cInforme();
                informe = ListaInformes[i];
                informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(informe.Beneficiario);
                informeAListar = new ListarInformes();
                informeAListar.Codigo = informe.Codigo;
                informeAListar.Fecha = informe.Fecha;
                informeAListar.Estado = informe.Estado;
                informeAListar.Tipo = informe.Tipo;
                informeAListar.CodigoBeneficiario = informe.Beneficiario.Codigo;
                informeAListar.Nombres = informe.Beneficiario.Nombres;
                informeAListar.Apellidos = informe.Beneficiario.Apellidos;
                ListaInformesParaListar.Add(informeAListar);
            }

            grdInformesPendientes.DataSource = ListaInformesParaListar;
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
            //ddlTipoUsuario.Enabled = pVisible;
            ddlEspecialidad.Enabled = pVisible;
            rbTipoDeEmpleado.Enabled = pVisible;


            btnModificar.Visible = !pVisible;

            btnConfirmar.Visible = pVisible;
            btnCancelar.Visible = pVisible;


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
                cUsuario usuario = new cUsuario();
                usuario.Codigo = U.Codigo;
                usuario.NickName = txtNickName.Text;
                usuario.Nombres = txtNombres.Text;
                usuario.Apellidos = txtApellidos.Text;
                usuario.CI = int.Parse(txtCi.Text);
                usuario.FechaNacimiento = txtFechaNac.Text;
                usuario.Domicilio = txtDomicilio.Text;
                usuario.Telefono = txtTelefono.Text;
                usuario.Email = txtEmail.Text;

                int i = dFachada.UsuarioVerificarNickNameYCi(usuario);
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El NickName o Cédula de identidad ya existe')", true);
                }
                else
                {
                    if (this.ddlTipoUsuario.SelectedValue == "Administrativo")
                    {
                        usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                    }
                    if (this.ddlTipoUsuario.SelectedValue == "Administrador")
                    {
                        usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                    }
                    if (this.ddlTipoUsuario.SelectedValue == "Usuario")
                    {
                        usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                    }
                    string seleccionado = rbTipoDeEmpleado.SelectedValue;
                    if (seleccionado == "Socio")
                    {
                        usuario.TipoContrato = "S";
                    }
                    if (seleccionado == "Empleado")
                    {
                        usuario.TipoContrato = "E";
                    }
                    if (seleccionado == "Contratado")
                    {
                        usuario.TipoContrato = "C";
                    }
                    usuario.Especialidad = new cEspecialidad();
                    usuario.Especialidad.Codigo = int.Parse(this.ddlEspecialidad.SelectedValue);

                    try
                    {
                        bool resultado = dFachada.UsuarioModificar(usuario);
                        if (resultado)
                        {
                            lblMensaje.Text = "Modificado correctamente";
                            U = usuario;
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

            LosItinerarios = dFachada.ItinerarioTraerTodosPorEspecialista(U);


            // ↓↓↓↓ ARMADO DEL HEADER ↓↓↓↓

            string Itinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < LosDias.Count; i++)
            {
                Itinerarios += "<td style='color:#000000; background-color:#80B7D8'>" + LosDias[i] + "</td>";
            }
            Itinerarios += "</tr>";

            // ↑↑↑↑ ARMADO DEL HEADER ↑↑↑↑

            #region ORDENAMIENTO DE SESIONES SEGÚN EL DÍA

            celdas = new List<List<cItinerario>>();

            for (int i = 0; i < LasHoras.Count; i++)
            {
                celdas.Add(new List<cItinerario>());

                for(int j=0;j<LosDias.Count;j++)
                {
                    bool HayAlgunaSesion = false;
                    for(int k=0; k<LosItinerarios.Count;k++)
                    {
                        if(!HayAlgunaSesion)
                        {
                            if (DateTime.Parse(LosItinerarios[k].HoraInicio) >= LasHoras[i] && DateTime.Parse(LosItinerarios[k].HoraInicio) < LasHoras[i + 1])
                            {
                                if(QueDiaEs(LosItinerarios[k])==LosDias[j])
                                {
                                    HayAlgunaSesion = true;
                                    celdas[i].Add(LosItinerarios[k]);
                                }
                            }
                        }
                    }
                    if (!HayAlgunaSesion) { celdas[i].Add(new cItinerario()); }
                }
            }

            for(int i=0; i<LasHoras.Count;i++)
            {
                Itinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for(int j=0; j<LosDias.Count;j++)
                {
                    if(celdas[i][j].Comentario==null)
                    {
                        Itinerarios += "<td style='background-color:#5186A6; color:#5186A6' rowspan={0}></td>";
                    }
                    else
                    {
                        if(celdas[i][j].Comentario != "NO_LISTAR")
                        {
                            string nombres = "";
                            foreach( cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                            {
                                nombres += "<br>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos;
                            }
                            int filas = 0;
                            for(int k=i; k<LasHoras.Count;k++)
                            {
                                if(DateTime.Parse(celdas[i][j].HoraFin)>LasHoras[k])
                                {
                                    filas++;
                                    celdas[k][j].Comentario = "NO_LISTAR";
                                }
                            }
                            Itinerarios += string.Format("<td style='background-color:#68D66C; color:#000000' rowspan={0}'>" +
                                "<b>" + celdas[i][j].TipoSesion + "</b> " + nombres , filas, ((filas * 25) + filas));
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