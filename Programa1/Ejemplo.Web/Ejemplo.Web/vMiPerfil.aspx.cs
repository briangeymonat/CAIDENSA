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
                ModoEdicion(false);
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
            txtFechaNac.Text = U.FechaNacimiento.ToString("yyyy-MM-dd");
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
            ddlTipoUsuario.Enabled = pVisible;
            ddlEspecialidad.Enabled = pVisible;
            rbTipoDeEmpleado.Enabled = pVisible;


            btnModificar.Visible = !pVisible;

            btnConfirmar.Visible = pVisible;
            btnCancelar.Visible = pVisible;


        }
        private bool FaltanDatosObligatorios()
        {
            if (this.txtNickName.Text == string.Empty || this.txtNombres.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtCi.Text == string.Empty)
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
                usuario.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);
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
    }
}