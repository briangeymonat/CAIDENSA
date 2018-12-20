using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Common.Clases;
using Dominio;

namespace Ejemplo.Web
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<cNotificacion> notificacionesAdministrador = dFachada.NotifiacionTraerTodasNuevasAdministrador(vMiPerfil.U);
            List<cNotificacion> notificacionesEspecialista = dFachada.NotifiacionTraerTodasNuevasEspecialista(vMiPerfil.U);

            int a = notificacionesAdministrador.Count + notificacionesEspecialista.Count;
            if (vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Administrador)
            {
                //int a = notificacionesAdministrador.Count;
                if (a > 0)
                    MenuNavegacion.FindItem("Tareas").Text = "Tareas " + a; // y color rojo falta
            }
            else if (vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Administrativo)
            {
                //int a = notificacionesAdministrador.Count;
                if (a > 0)
                    MenuNavegacion.FindItem("Tareas").Text = "Tareas " + a; // y color rojo falta
            }
            else if (vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Usuario)
            {
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Estadísticas"));
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Usuarios"));
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Beneficiarios"));
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Itinerario"));
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Informes"));
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Diagnóstico"));
                MenuNavegacion.Items.Remove(MenuNavegacion.FindItem("Asistencias"));
                //int a = notificacionesEspecialista.Count;
                if (a > 0)
                    MenuNavegacion.FindItem("Tareas").Text = "Tareas " + a; // y color rojo falta
            }

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void MenuNavegacion_MenuItemClick(object sender, MenuEventArgs e)
        {
           
            String a = e.Item.Text;
            string[] parts = a.Split(' ');
            string part1;
            string part2;
            if (parts.Length==1)
            {
                part1 = parts[0];
            }
            else
            {
                part1 = parts[0];
                part2 = parts[1];
            }             
            if (part1=="Tareas")
            {
                cNotificacion notificacion = new cNotificacion();
                notificacion.Usuario = vMiPerfil.U;
                try
                {
                    bool resultado = dFachada.NotificacionCambiarEstadoVista(notificacion);
                    if (resultado)
                    {
                        Response.Redirect("vTareas.aspx");
                    }                    
                }
                catch(Exception ex)
                {
                    throw ex;
                }               
                
            }
            
        }
    }

}