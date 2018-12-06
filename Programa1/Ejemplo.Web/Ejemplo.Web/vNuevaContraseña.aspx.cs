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
    public partial class vNuevaContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnConfirmarNuevaContrasena_Click(object sender, EventArgs e)
        {
            cUsuario usuario = new cUsuario();
            usuario.NickName = txtNickName.Text;
            try
            {
                if(txtContrasena.Text==txtContrasenaRepetir.Text)
                {
                    int i = dFachada.UsuarioExisteNickNameSinContrasena(usuario);
                    if (i==0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El NickName no existe o ya tiene contraseña ingresada')", true);
                    }
                    else
                    {
                        usuario.Contrasena = txtContrasena.Text;
                        bool resultado = dFachada.UsuarioAgregarConstrasena(usuario);
                        if(resultado)
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se restableció la contraseña correctamente')", true);
                            Response.Redirect("vLogin.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo restablecer la contraseña')", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Las contraseñas no coinciden')", true);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}