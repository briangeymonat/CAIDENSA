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
    public partial class DetallesUsuario : System.Web.UI.Page
    {

        static cUsuario U;
        private static List<cItinerario> LosItinerarios;
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> LasHoras;
        private static List<List<cItinerario>> celdas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                else
                {
                    lblBeneficiariosQueAtiende.Visible = false;
                    lblInformesPendientes.Visible = false;
                    lblInformesRealizados.Visible = false;
                    grdBeneficiariosQueAtiende.Visible = false;
                    grdInformesPendientes.Visible = false;
                    grdInformesRealizados.Visible = false;
                }

                if(grdBeneficiariosQueAtiende.PageCount<=0)
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
                if(LosItinerarios.Count<=0)
                {
                    frmItinerario.Visible = false;
                    lblItinerario.Text = "No está registrado en el itinerario";
                }

            }
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
            List<cInforme> ListaInformes = dFachada.InformeTraerTodosPendientesPorEspecialista(U);
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





        protected void btnModificar_Click(object sender, EventArgs e)
        {
            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            cUsuario usuario = new cUsuario();
            usuario.NickName = txtNickName.Text;
            usuario = dFachada.UsuarioTraerEspecificoXNickName(usuario);


            if (unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo && usuario.Tipo == cUtilidades.TipoDeUsuario.Administrador)
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
            string nickname = Request.QueryString["nickname"];
            cUsuario usuario = new cUsuario();
            usuario.NickName = nickname;

            usuario = dFachada.UsuarioTraerEspecificoXNickName(usuario);
            U = new cUsuario();
            U = usuario;
            txtNickName.Text = usuario.NickName;
            txtNombres.Text = usuario.Nombres;
            txtApellidos.Text = usuario.Apellidos;
            txtCi.Text = usuario.CI.ToString();
            DateTime fn = new DateTime();
            if (usuario.FechaNacimiento!=null)
            {
                fn = DateTime.Parse(usuario.FechaNacimiento);
                txtFechaNac.Text = fn.ToString("yyyy-MM-dd");
            }
            else
            {
                txtFechaNac.Text = string.Empty;
            }
            txtDomicilio.Text = usuario.Domicilio;
            txtTelefono.Text = usuario.Telefono;
            txtEmail.Text = usuario.Email;
            ddlTipoUsuario.SelectedValue = usuario.Tipo.ToString();
            ddlEspecialidad.SelectedIndex = (usuario.Especialidad.Codigo - 1);
            if (usuario.TipoContrato.ToString() == "Empleado")
            {
                rbTipoDeEmpleado.SelectedIndex = 0;
            }
            if (usuario.TipoContrato.ToString() == "Contratado")
            {
                rbTipoDeEmpleado.SelectedIndex = 1;
            }
            if (usuario.TipoContrato.ToString() == "Socio")
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
        private void ModoEdicion(bool pVisible)
        {
            cUsuario usuario = new cUsuario();
            usuario.NickName = txtNickName.Text;
            usuario = dFachada.UsuarioTraerEspecificoXNickName(usuario);

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
            rbTipoDeEmpleado.Enabled = pVisible;


            btnModificar.Visible = !pVisible;

            btnConfirmar.Visible = pVisible;
            btnCancelar.Visible = pVisible;

            lblObligatorio1.Visible = pVisible;
            lblObligatorio2.Visible = pVisible;
            lblObligatorio3.Visible = pVisible;
            lblObligatorio4.Visible = pVisible;


            if (usuario.Estado)//si esta activo
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
            cUsuario usuario = new cUsuario();
            usuario = U;

            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            if (usuario.Tipo == cUtilidades.TipoDeUsuario.Administrador && unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede inhabilitar un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                int i = dFachada.UsuarioCantidadAdministradoresActivos();
                if (usuario.Tipo == cUtilidades.TipoDeUsuario.Administrador && i == 1)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Debe haber al menos un usuario administrador activo')", true);
                }
                else
                {
                    try
                    {
                        bool resultado = dFachada.UsuarioEliminar(usuario);
                        if (resultado)
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
            cUsuario usuario = new cUsuario();
            usuario = U;

            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            if (U.Tipo == cUtilidades.TipoDeUsuario.Administrador && unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede habilitar un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                try
                {
                    bool resultado = dFachada.UsuarioHabilitar(usuario);
                    if (resultado)
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
            cUsuario usuario = new cUsuario();
            usuario = U;

            cUsuario unUsuario = new cUsuario();
            unUsuario = vMiPerfil.U;

            if (U.Tipo == cUtilidades.TipoDeUsuario.Administrador && unUsuario.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No puede restablecer la contraseña de un usuario administrador siendo adminsitrativo')", true);
            }
            else
            {
                try
                {
                    bool resultado = dFachada.UsuarioRestablecerContrasena(usuario);
                    if (resultado)
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
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + codigo.ToString());
        }

        protected void grdInformesPendientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesPendientes.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + codigo.ToString());
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
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[4].Visible = false; //codBeneficiario
        }

        private void CargarItinerarios()
        {
            pnlItinerario.Visible = true;

            LosItinerarios = dFachada.ItinerarioTraerTodosPorEspecialista(U);


            // ↓↓↓↓ ARMADO DEL HEADER ↓↓↓↓

            string Itinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < LosDias.Count; i++)
            {
                Itinerarios += "<td style='color:#000000; background-color:#80B7D8;width:100px;'>" + LosDias[i] + "</td>";
            }
            Itinerarios += "</tr>";

            // ↑↑↑↑ ARMADO DEL HEADER ↑↑↑↑

            #region ORDENAMIENTO DE SESIONES SEGÚN EL DÍA

            celdas = new List<List<cItinerario>>();

            for (int i = 0; i < LasHoras.Count; i++)
            {
                celdas.Add(new List<cItinerario>());

                for (int j = 0; j < LosDias.Count; j++)
                {
                    bool HayAlgunaSesion = false;
                    for (int k = 0; k < LosItinerarios.Count; k++)
                    {
                        if (!HayAlgunaSesion)
                        {
                            if (DateTime.Parse(LosItinerarios[k].HoraInicio) >= LasHoras[i] && DateTime.Parse(LosItinerarios[k].HoraInicio) < LasHoras[i + 1])
                            {
                                if (QueDiaEs(LosItinerarios[k]) == LosDias[j])
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

            for (int i = 0; i < LasHoras.Count; i++)
            {
                Itinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for (int j = 0; j < LosDias.Count; j++)
                {
                    if (celdas[i][j].Comentario == null)
                    {
                        Itinerarios += "<td style='background-color:#FF8e8e; color:#5186A6;width:100px;' rowspan={0}></td>";
                    }
                    else
                    {
                        if (celdas[i][j].Comentario != "NO_LISTAR")
                        {
                            string nombres = "";
                            foreach (cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                            {
                                string color = "";
                                switch (unBeneficiario.Plan.Tipo)
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
                                nombres += "<p style='background-color:" + color + ";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos + "</p>";
                            }
                            int filas = 0;
                            for (int k = i; k < LasHoras.Count; k++)
                            {
                                if (DateTime.Parse(celdas[i][j].HoraFin) > LasHoras[k])
                                {
                                    filas++;
                                    celdas[k][j].Comentario = "NO_LISTAR";
                                }
                            }
                            string centro = "";
                            if (celdas[i][j].Centro == cUtilidades.Centro.JuanLacaze) centro = " - JL"; else centro = " - NH";
                            Itinerarios += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                "<p style='padding:5px 0px; margin:0px;width:100px;'>" + celdas[i][j].TipoSesion + centro + "</p> " + nombres, filas, ((filas * 25) + filas));
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
    }
}