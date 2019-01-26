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
            cUsuario unUsuario = new cUsuario();
            unUsuario.NickName = txtNickName.Text;
            unUsuario.Contrasena = txtContrasena.Text;
            try
            {
                unUsuario = dFachada.UsuarioVerificarInicioSesion(unUsuario);
                if(unUsuario!=null)
                {
                    DateTime dFechaActual = DateTime.Now;

                    List<cBeneficiario> lstBeneficiarios = new List<cBeneficiario>();
                    List<cBeneficiario> lstBenConPlanes = new List<cBeneficiario>();
                    List<cBeneficiario> lstBeneficiariosConPlanesAVencerse = new List<cBeneficiario>();
                    lstBeneficiarios = dFachada.BeneficiarioTraerTodos();
                    cBeneficiario unBeneficiario;
                    for (int i = 0; i < lstBeneficiarios.Count; i++)
                    {
                        unBeneficiario = new cBeneficiario();
                        unBeneficiario = lstBeneficiarios[i];
                        unBeneficiario.lstPlanes = new List<cPlan>();
                        unBeneficiario.lstPlanes = dFachada.PlanTraerActivosPorBeneficiario(unBeneficiario);
                        lstBenConPlanes.Add(unBeneficiario);
                    }
                    for (int a = 0; a < lstBenConPlanes.Count; a++)
                    {
                        for (int b = 0; b < lstBenConPlanes[a].lstPlanes.Count; b++)
                        {
                            if (lstBenConPlanes[a].lstPlanes[b].FechaFin != null)
                            {
                                DateTime dFecha = new DateTime();
                                dFecha = DateTime.Parse(lstBenConPlanes[a].lstPlanes[b].FechaFin);
                                TimeSpan tsD = dFecha - dFechaActual;
                                Double douTd = tsD.TotalDays;
                                if (douTd < 185)
                                {
                                    lstBeneficiariosConPlanesAVencerse.Add(lstBenConPlanes[a]);
                                    break;
                                    //si tiene varios planes se lista solo una vez el beneficiario
                                }
                            }
                        }
                    }
                    cNotificacion unaNotificacion;
                    if (unUsuario.Tipo != cUtilidades.TipoDeUsuario.Usuario)
                    {
                        for (int i = 0; i < lstBeneficiariosConPlanesAVencerse.Count; i++)
                        {
                            for (int j = 0; j < lstBeneficiariosConPlanesAVencerse[i].lstPlanes.Count; j++)
                            {
                                unaNotificacion = new cNotificacion();
                                unaNotificacion.Usuario = unUsuario;
                                unaNotificacion.Plan = lstBeneficiariosConPlanesAVencerse[i].lstPlanes[j];
                                int a = dFachada.NotificacionVerificarIngresoParaAdministrador(unaNotificacion);
                                if (a == 0)
                                {
                                    dFachada.NotificacionAgregarDeAdministrador(unaNotificacion);
                                }
                            }
                        }
                    }
                    Response.Redirect("vMiPerfil.aspx?nick="+unUsuario.NickName);

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