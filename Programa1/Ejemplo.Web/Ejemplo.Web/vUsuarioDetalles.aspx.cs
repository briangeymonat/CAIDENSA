﻿using Common.Clases;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombos();
                cargarDatos();
                ModoEdicion(false);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            ModoEdicion(true);
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
                usuario.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                usuario.Domicilio = txtDomicilio.Text;
                usuario.Telefono = txtTelefono.Text;
                usuario.Email = txtEmail.Text;

                int i = dFachada.VerificarNickNameYCiUsuario(usuario);
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
                        bool resultado = dFachada.ModificarUsuario(usuario);
                        if(resultado)
                        {
                            lblMensaje.Text = "Modificado correctamente";
                            ModoEdicion(false);
                        }
                        else
                        {
                            lblMensaje.Text = "ERROR: No se pudo agregar";
                        }
                    }
                    catch(Exception ex)
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
            
                usuario = dFachada.TraerEspecificoXNickNameUsuario(usuario);
                U = new cUsuario();
                U = usuario;
                txtNickName.Text = usuario.NickName;
                txtNombres.Text = usuario.Nombres;
                txtApellidos.Text = usuario.Apellidos;
                txtCi.Text = usuario.CI.ToString();
                txtFechaNac.Text = usuario.FechaNacimiento.ToString("yyyy-MM-dd");
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
            ddlEspecialidad.DataSource = dFachada.TraerTodasEspecialidades();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Codigo";
            ddlEspecialidad.DataBind();
        }
        private void ModoEdicion(bool pVisible)
        {
            cUsuario usuario = new cUsuario();
            usuario.NickName = txtNickName.Text;
            usuario = dFachada.TraerEspecificoXNickNameUsuario(usuario);

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

            if(usuario.Estado)//si esta activo
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

            try
            {
                bool resultado = dFachada.EliminarUsuario(usuario);
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
        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            cUsuario usuario = new cUsuario();
            usuario = U;

            try
            {
                bool resultado = dFachada.HabilitarUsuario(usuario);
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

        protected void btnRestablecerContrasena_Click(object sender, EventArgs e)
        {
            cUsuario usuario = new cUsuario();
            usuario = U;

            try
            {
                bool resultado = dFachada.RestablecerContrasenaUsuario(usuario);
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
}