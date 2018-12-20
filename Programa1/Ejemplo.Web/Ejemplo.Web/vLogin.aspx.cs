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
                usuario = dFachada.UsuarioVerificarInicioSesion(usuario);
                if(usuario!=null)
                {
                    DateTime fechaActual = DateTime.Now;

                    List<cBeneficiario> listaBeneficiarios = new List<cBeneficiario>();
                    List<cBeneficiario> listaBenConPlanes = new List<cBeneficiario>();
                    List<cBeneficiario> BeneficiariosConPlanesAVencerse = new List<cBeneficiario>();
                    listaBeneficiarios = dFachada.BeneficiarioTraerTodos();
                    cBeneficiario beneficiario;
                    for (int i = 0; i < listaBeneficiarios.Count; i++)
                    {
                        beneficiario = new cBeneficiario();
                        beneficiario = listaBeneficiarios[i];
                        beneficiario.lstPlanes = new List<cPlan>();
                        beneficiario.lstPlanes = dFachada.PlanTraerActivosPorBeneficiario(beneficiario);
                        listaBenConPlanes.Add(beneficiario);
                    }
                    for (int a = 0; a < listaBenConPlanes.Count; a++)
                    {
                        for (int b = 0; b < listaBenConPlanes[a].lstPlanes.Count; b++)
                        {
                            if (listaBenConPlanes[a].lstPlanes[b].FechaFin != null)
                            {
                                DateTime fecha = new DateTime();
                                fecha = DateTime.Parse(listaBenConPlanes[a].lstPlanes[b].FechaFin);
                                TimeSpan d = fecha - fechaActual;
                                Double td = d.TotalDays;
                                if (td < 185)
                                {
                                    BeneficiariosConPlanesAVencerse.Add(listaBenConPlanes[a]);
                                    break;
                                    //si tiene varios planes se lista solo una vez el beneficiario
                                }
                            }
                        }
                    }
                    cUsuario user = new cUsuario();
                    cNotificacion notificacion;
                    user = dFachada.UsuarioTraerEspecificoXNickName(usuario);
                    if (user.Tipo != cUtilidades.TipoDeUsuario.Usuario)
                    {


                        for (int i = 0; i < BeneficiariosConPlanesAVencerse.Count; i++)
                        {
                            for (int j = 0; j < BeneficiariosConPlanesAVencerse[i].lstPlanes.Count; j++)
                            {
                                notificacion = new cNotificacion();
                                notificacion.Usuario = user;
                                notificacion.Plan = BeneficiariosConPlanesAVencerse[i].lstPlanes[j];
                                int a = dFachada.NotificacionVerificarIngresoParaAdministrador(notificacion);
                                if (a == 0)
                                {
                                    dFachada.NotificacionAgregarDeAdministrador(notificacion);
                                }
                            }
                        }
                    }
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