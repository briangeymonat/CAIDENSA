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
    public partial class vLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vMiPerfil.i = 0;
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            cUsuario usuario = new cUsuario();
            usuario.NickName = txtNickName.Text;
            usuario.Contrasena = txtContrasena.Text;
            try
            {
                usuario = dFachada.VerificarInicioSesionUsuario(usuario);
                if(usuario!=null)
                {
                    Response.Redirect("vMiPerfil.aspx?nick="+usuario.NickName);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Nickname o contraseña incorrecta.')", true);
                }
            }
          catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}