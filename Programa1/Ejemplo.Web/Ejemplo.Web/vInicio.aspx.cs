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
    public partial class Inicio : System.Web.UI.Page
    {

        //static cUsuario U;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //cargarUsuarioLogeado();
            }
        }

       /* private void cargarUsuarioLogeado()
        {
            string nickname = Request.QueryString["nick"];
            cUsuario usuario = new cUsuario();
            usuario.NickName = nickname;
            try
            {
                usuario = dFachada.TraerEspecificoXNickNameUsuario(usuario);
                U = usuario;
                if(usuario==null)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo iniciar sesion.')", true);
                    Response.Redirect("vLogin.aspx");
                }                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }*/

    }
}