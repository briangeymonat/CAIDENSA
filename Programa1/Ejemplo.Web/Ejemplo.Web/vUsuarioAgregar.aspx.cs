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
    public partial class AgregarUsuario : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombos();
            }
        }
        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (!FaltanDatosObligatorios())
            {
                cUsuario usuario = new cUsuario();
                usuario.Codigo = -1;//Para que cuando verifique tenga un codigo diferente a todos los que estan ingresados en bd
                usuario.NickName = this.txtNickName.Text;
                usuario.Nombres = this.txtNombres.Text;
                usuario.Apellidos = this.txtApellidos.Text;
                usuario.CI = int.Parse(this.txtCi.Text);

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
                    usuario.Domicilio = this.txtDomicilio.Text;
                    if (this.txtFechaNac.Text.ToString() != string.Empty)
                    {
                        usuario.FechaNacimiento = DateTime.Parse(this.txtFechaNac.Text);
                    }
                    usuario.Telefono = this.txtTelefono.Text;
                    usuario.Email = this.txtEmail.Text;
                    usuario.Estado = true;
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

                    usuario.Especialidad = dFachada.EspecialidadTraerEspecifica(usuario.Especialidad);
                    if (ddlTipoUsuario.SelectedValue == "Usuario" && usuario.Especialidad.Nombre == "Sin especialidad")
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se puede crear un usuario de tipo usuario que no tenga especialidad.')", true);
                    }
                    else
                    {
                        try
                        {
                            bool resultado = dFachada.UsuarioAgregar(usuario);
                            if (resultado)
                            {
                                lblMensaje.Text = "Agregado correctamente.";
                                LimpiarCampos();
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
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos obligatorios.')", true);
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
        private bool FaltanDatosObligatorios()
        {
            if (this.txtNickName.Text == string.Empty || this.txtNombres.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtCi.Text == string.Empty)
            {
                return true;
            }
            return false;
        }

        private void LimpiarCampos()
        {
            this.txtNickName.Text = string.Empty;
            this.txtNombres.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtCi.Text = string.Empty;
            this.txtDomicilio.Text = string.Empty;
            this.txtFechaNac.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.ddlTipoUsuario.SelectedIndex = 0;
            this.ddlEspecialidad.SelectedIndex = 0;
            this.rbTipoDeEmpleado.SelectedIndex = 0;
        }

        protected void btnAgregarUsuario_Click1(object sender, EventArgs e)
        {

        }
    }
}