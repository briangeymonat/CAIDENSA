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
    public partial class vTareas : System.Web.UI.Page
    {
        List<cBeneficiario> LosBeneficiariosConPlanesPorVencerse;
        List<cBeneficiario> LosBeneficiariosConPlanesSinFechaDeVencimiento;
        private static List<cSesion> LasSesionesPasaronDelDia;
        public static bool ventanaObservacion = true;
        public static bool ventanaReprogramar = true;
        public static bool ventanaObservacionVerDetalles = false;
        static List<cBeneficiario> LosBeneficiarios;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosBeneficiariosConPlanesPorVencerse = new List<cBeneficiario>();
                LosBeneficiariosConPlanesSinFechaDeVencimiento = new List<cBeneficiario>();
                CargarComboBeneficiario();
                CargarGrillasAdministrativas();
                CargarGrillasEspecialistas();
                if ((vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Administrador || vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Administrativo) && vMiPerfil.U.Especialidad.Nombre == "Sin especialidad")
                {
                    PanelTaerasAdministrativas.Visible = true;
                    PanelTaerasEspecialistas.Visible = false;
                }
                else if (vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Usuario && vMiPerfil.U.Especialidad.Nombre != "Sin especialidad")
                {
                    PanelTaerasAdministrativas.Visible = false;
                    PanelTaerasEspecialistas.Visible = true;
                }
                else if (vMiPerfil.U.Tipo == cUtilidades.TipoDeUsuario.Usuario && vMiPerfil.U.Especialidad.Nombre == "Sin especialidad")
                {
                    PanelTaerasAdministrativas.Visible = false;
                    PanelTaerasEspecialistas.Visible = false;
                }

            }
        }

        #region TAREAS ADMINISTRATIVAS


        protected void CargarGrillasAdministrativas()
        {
            CargarGrillaEspecialistasConInformesPendientes();
            CargarGrillaPlanesPorVencerse();
            CargarGrillaPlanesSinFechaVencimiento();
            CargarGrillaSesionesPasaronDelDia();
        }
        protected void CargarGrillaPlanesPorVencerse()
        {
            DateTime fechaActual = DateTime.Now;

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
                        TimeSpan tsD = dFecha - fechaActual;
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
            LosBeneficiariosConPlanesPorVencerse = lstBeneficiariosConPlanesAVencerse;
            grdPlanesPorVencerse.DataSource = lstBeneficiariosConPlanesAVencerse;
            grdPlanesPorVencerse.DataBind();
        }
        protected void CargarGrillaPlanesSinFechaVencimiento()
        {
            List<cBeneficiario> lstBeneficiarios = new List<cBeneficiario>();
            List<cBeneficiario> lstBenConPlanes = new List<cBeneficiario>();
            List<cBeneficiario> lstBeneficiariosConPlanesSinFechaVencimiento = new List<cBeneficiario>();
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
                    if (lstBenConPlanes[a].lstPlanes[b].FechaFin == null)
                    {
                        lstBeneficiariosConPlanesSinFechaVencimiento.Add(lstBenConPlanes[a]);
                        break;
                        //si tiene varios planes se lista solo una vez el beneficiario
                    }
                }
            }
            LosBeneficiariosConPlanesSinFechaDeVencimiento = lstBeneficiariosConPlanesSinFechaVencimiento;
            this.grdBeneficiariosConPlanSinFechaVencimiento.DataSource = lstBeneficiariosConPlanesSinFechaVencimiento;
            this.grdBeneficiariosConPlanSinFechaVencimiento.DataBind();
        }
        protected void CargarGrillaEspecialistasConInformesPendientes()
        {
            grdEspecialistasConInformesPendientes.DataSource = dFachada.UsuarioTraerTodosEspecialistasConInformesPendientes();
            grdEspecialistasConInformesPendientes.DataBind();
        }
        protected void CargarGrillaSesionesPasaronDelDia()
        {
            LasSesionesPasaronDelDia = dFachada.SesionTraerPasaronDelDia();
            grdSesionesPasadasDelDia.DataSource = LasSesionesPasaronDelDia;
            grdSesionesPasadasDelDia.DataBind();
        }


        protected void grdEspecialistasConInformesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //contraseña
            e.Row.Cells[6].Visible = false;//tipo
            e.Row.Cells[7].Visible = false;//domicilio
            e.Row.Cells[8].Visible = false;//fecha de nacimiento
            e.Row.Cells[9].Visible = false;//tel
            e.Row.Cells[10].Visible = false;//email
            e.Row.Cells[11].Visible = false;//estado
            e.Row.Cells[12].Visible = false;//tipo de contrato
        }
        protected void grdPlanesPorVencerse_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[5].Visible = false; //sexo
            e.Row.Cells[7].Visible = false; //tel2
            e.Row.Cells[8].Visible = false;//email
            e.Row.Cells[9].Visible = false;//domicilio
            e.Row.Cells[10].Visible = false;//fecha de nacimiento
            e.Row.Cells[11].Visible = false;//atributario
            e.Row.Cells[12].Visible = false;//motivo consulta
            e.Row.Cells[13].Visible = false;//escolaridad
            e.Row.Cells[14].Visible = false;//derivador
            e.Row.Cells[15].Visible = false;//estado
        }
        protected void grdPlanesPorVencerse_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdPlanesPorVencerse.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + iCodigo.ToString());
        }
        protected void grdBeneficiariosConPlanSinFechaVencimiento_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[5].Visible = false; //sexo
            e.Row.Cells[7].Visible = false; //tel2
            e.Row.Cells[8].Visible = false;//email
            e.Row.Cells[9].Visible = false;//domicilio
            e.Row.Cells[10].Visible = false;//fecha de nacimiento
            e.Row.Cells[11].Visible = false;//atributario
            e.Row.Cells[12].Visible = false;//motivo consulta
            e.Row.Cells[13].Visible = false;//escolaridad
            e.Row.Cells[14].Visible = false;//derivador
            e.Row.Cells[15].Visible = false;//estado
        }
        protected void grdBeneficiariosConPlanSinFechaVencimiento_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdBeneficiariosConPlanSinFechaVencimiento.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + iCodigo.ToString());
        }
        #endregion
        #region TAREAS ESPECIALISTAS

        protected void CargarGrillasEspecialistas()
        {
            CargarGrillaInformesPendientes();
            CargarGrillaInformesEnProceso();
            CargarGrillaSesionesDelDia();
            CargarGrillasSesionesObservaciones();
            CargarGrillaSesionesConObservacionesRealizadas();
        }

        protected void CargarGrillaInformesPendientes()
        {
            List<cInforme> lstInformes = dFachada.InformeTraerTodosPendientesPorEspecialista(vMiPerfil.U);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes unInformeAListar;

            for (int i = 0; i < lstInformes.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes[i];
                unInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(unInforme.Beneficiario);
                unInformeAListar = new ListarInformes();
                unInformeAListar.Codigo = unInforme.Codigo;
                unInformeAListar.Fecha = unInforme.Fecha;
                unInformeAListar.Estado = unInforme.Estado;
                unInformeAListar.Tipo = unInforme.Tipo;
                unInformeAListar.CodigoBeneficiario = unInforme.Beneficiario.Codigo;
                unInformeAListar.Nombres = unInforme.Beneficiario.Nombres;
                unInformeAListar.Apellidos = unInforme.Beneficiario.Apellidos;
                lstInformesParaListar.Add(unInformeAListar);
            }

            grdInformesPendientes.DataSource = lstInformesParaListar;
            grdInformesPendientes.DataBind();
        }
        protected void CargarGrillaInformesEnProceso()
        {
            List<cInforme> lstInformes = dFachada.InformeTraerTodosEnProcesoPorEspecialista(vMiPerfil.U);
            cInforme unInforme;

            List<ListarInformes> lstInformesParaListar = new List<ListarInformes>();
            ListarInformes unInformeAListar;

            for (int i = 0; i < lstInformes.Count; i++)
            {
                unInforme = new cInforme();
                unInforme = lstInformes[i];
                unInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(unInforme.Beneficiario);
                unInformeAListar = new ListarInformes();
                unInformeAListar.Codigo = unInforme.Codigo;
                unInformeAListar.Fecha = unInforme.Fecha;
                unInformeAListar.Estado = unInforme.Estado;
                unInformeAListar.Tipo = unInforme.Tipo;
                unInformeAListar.CodigoBeneficiario = unInforme.Beneficiario.Codigo;
                unInformeAListar.Nombres = unInforme.Beneficiario.Nombres;
                unInformeAListar.Apellidos = unInforme.Beneficiario.Apellidos;
                lstInformesParaListar.Add(unInformeAListar);
            }

            grdInformesEnProceso.DataSource = lstInformesParaListar;
            grdInformesEnProceso.DataBind();
        }
        protected void CargarGrillaSesionesDelDia()
        {
            List<cSesion> lstSesiones = new List<cSesion>();
            grdSesionesDelDia.DataSource = dFachada.SesionTraerProximasDelDiaPorEspecialista(vMiPerfil.U);
            grdSesionesDelDia.DataBind();
        }
        protected void CargarGrillasSesionesObservaciones()
        {
            grdObservacionesDeSesiones.DataSource = dFachada.SesionTraerPasaronDelDiaPorEspecialista(vMiPerfil.U);
            grdObservacionesDeSesiones.DataBind();
        }
        protected void CargarComboBeneficiario()
        {
            LosBeneficiarios = new List<cBeneficiario>();
            LosBeneficiarios = dFachada.BeneficiarioTraerTodosPorEspecialista(vMiPerfil.U);
            List<string> lstNombreApellido = new List<string>() { "Todos" };
            string sNomApe;
            for (int i = 0; i < LosBeneficiarios.Count; i++)
            {
                sNomApe = "";
                sNomApe = LosBeneficiarios[i].Nombres + " " + LosBeneficiarios[i].Apellidos;
                lstNombreApellido.Add(sNomApe);
            }
            ddlBeneficiario.DataSource = lstNombreApellido;
            ddlBeneficiario.DataBind();
        }
        protected void CargarGrillaSesionesConObservacionesRealizadas()
        {
            if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty &&
                DateTime.Parse(txtDesde.Text) > DateTime.Parse(txtHasta.Text))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha Desde: debe ser menor a la fecha Hasta:')", true);
            }
            else
            {
                string sConsulta = "SELECT distinct S.* FROM UsuariosSesiones us " +
                    "join Sesiones S on us.SesionId = S.SesionId " +
                    "join BeneficiariosSesiones bs on bs.SesionId = S.SesionId";
                List<string> lstCondiciones = new List<string>() { " WHERE" };
                lstCondiciones.Add(string.Format(" us.UsuarioId={0} and us.UsuariosSesionesObservacion  is not NULL and us.UsuariosSesionesObservacion <> ''", vMiPerfil.U.Codigo));
                //Beneficiario
                if (ddlBeneficiario.SelectedIndex != 0)
                {
                    cBeneficiario unBeneficiario = LosBeneficiarios[ddlBeneficiario.SelectedIndex - 1];
                    int iCodigo = unBeneficiario.Codigo;
                    lstCondiciones.Add(string.Format(" and bs.BeneficiarioId={0}", iCodigo));
                }
                //Fecha
                if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                {
                    lstCondiciones.Add(string.Format(" and s.SesionFecha between '{0}' and '{1}'", txtDesde.Text, txtHasta.Text));
                }
                for (int i = 0; i < lstCondiciones.Count; i++)
                {
                    sConsulta += lstCondiciones[i];
                }
                grdSesionesObservacionesRealizadas.DataSource = dFachada.SesionTraerTodasPorEspecialistaConFiltros(sConsulta);
                grdSesionesObservacionesRealizadas.DataBind();
            }
        }
        protected void grdInformesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //fecha
            e.Row.Cells[4].Visible = false; //estado
            e.Row.Cells[5].Visible = false; //codigo beneficiario
        }
        protected void grdInformesPendientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesPendientes.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + iCodigo.ToString());
        }
        protected void grdInformesEnProceso_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdInformesEnProceso.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + iCodigo.ToString());
        }
        protected void grdInformesEnProceso_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //fecha
            e.Row.Cells[4].Visible = false; //estado
            e.Row.Cells[5].Visible = false; //codigo beneficiario
        }
        #endregion
        
        protected void grdSesionesPasadasDelDia_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (ventanaReprogramar)
            {
                ventanaReprogramar = false;
                TableCell celdaId = grdSesionesPasadasDelDia.Rows[e.NewSelectedIndex].Cells[1];
                int iIdSesion = int.Parse(celdaId.Text);
                string sVtn = "window.open('vDetallesSesionParaAsistencia.aspx?Id=" + LasSesionesPasaronDelDia[e.NewSelectedIndex].Codigo + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);
            }
            else
            {
                CargarGrillaSesionesPasaronDelDia();
                ventanaReprogramar = true;
            }            
        }
        protected void grdObservacionesDeSesiones_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (ventanaObservacion)
            {
                ventanaObservacionVerDetalles = false;
                ventanaObservacion = false;
                TableCell celdaId = grdObservacionesDeSesiones.Rows[e.NewSelectedIndex].Cells[1];
                int iIdSesion = int.Parse(celdaId.Text);
                //Response.Redirect("vAgregarObservacionSesion.aspx?SesionId=" + idSesion);
                string sVtn = "window.open('vAgregarObservacionSesion.aspx?SesionId=" + iIdSesion + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);
            }
            else
            {
                ventanaObservacionVerDetalles = false;
                CargarGrillasEspecialistas();
                CargarGrillasAdministrativas();
                ventanaObservacion = true;
            }

        }
        protected void grdSesionesDelDia_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaId = grdSesionesDelDia.Rows[e.NewSelectedIndex].Cells[1];
            int iIdSesion = int.Parse(celdaId.Text);
            string sVtn = "window.open('vDetallesSesion.aspx?Id=" + iIdSesion + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=100', 'width=700')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);

        }
        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            CargarGrillaSesionesConObservacionesRealizadas();
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            ddlBeneficiario.SelectedIndex = 0;
            CargarGrillaSesionesConObservacionesRealizadas();
        }
        protected void grdSesionesObservacionesRealizadas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (ventanaObservacion)
            {
                ventanaObservacionVerDetalles = true;
                ventanaObservacion = false;
                TableCell celdaId = grdSesionesObservacionesRealizadas.Rows[e.NewSelectedIndex].Cells[1];
                int iIdSesion = int.Parse(celdaId.Text);
                string sVtn = "window.open('vAgregarObservacionSesion.aspx?SesionId=" + iIdSesion + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);
            }
            else
            {
                ventanaObservacionVerDetalles = false;
                CargarGrillasEspecialistas();
                CargarGrillasAdministrativas();
                ventanaObservacion = true;
            }
        }

        protected void grdSesionesPasadasDelDia_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //comentario
        }

        protected void grdObservacionesDeSesiones_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false;//comentario
        }

        protected void grdSesionesDelDia_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false;//comentario
        }

        protected void grdSesionesObservacionesRealizadas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[2].Visible = false;//comentario
        }
    }
}