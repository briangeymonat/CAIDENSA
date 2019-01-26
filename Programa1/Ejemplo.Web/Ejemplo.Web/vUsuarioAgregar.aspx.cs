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
                cUsuario unUsuario = new cUsuario();
                unUsuario.Codigo = -1;//Para que cuando verifique tenga un codigo diferente a todos los que estan ingresados en bd
                unUsuario.NickName = this.txtNickName.Text;
                unUsuario.Nombres = this.txtNombres.Text;
                unUsuario.Apellidos = this.txtApellidos.Text;
                unUsuario.CI = int.Parse(this.txtCi.Text);

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
                    unUsuario.Domicilio = this.txtDomicilio.Text;
                    if (this.txtFechaNac.Text.ToString() != string.Empty)
                    {
                        unUsuario.FechaNacimiento = this.txtFechaNac.Text;
                    }
                    unUsuario.Telefono = this.txtTelefono.Text;
                    unUsuario.Email = this.txtEmail.Text;
                    unUsuario.Estado = true;
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

                    unUsuario.Especialidad = dFachada.EspecialidadTraerEspecifica(unUsuario.Especialidad);
                    if (ddlTipoUsuario.SelectedValue == "Usuario" && unUsuario.Especialidad.Nombre == "Sin especialidad")
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se puede crear un usuario de tipo usuario que no tenga especialidad.')", true);
                    }
                    else
                    {
                        try
                        {
                            bool bResultado = dFachada.UsuarioAgregar(unUsuario);
                            if (bResultado)
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

    }
}