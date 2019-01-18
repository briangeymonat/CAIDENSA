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
        List<cBeneficiario> BeneficiariosConPlanesPorVencerse;
        List<cBeneficiario> BeneficiariosConPlanesSinFechaDeVencimiento;
        private static List<cSesion> SesionesPasaronDelDia;
        public static bool enproceso;
        public static bool ventanaObservacion = true;
        public static bool ventanaReprogramar = true;
        public static bool ventanaObservacionVerDetalles = false;
        static List<cBeneficiario> lstBeneficiarios;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BeneficiariosConPlanesPorVencerse = new List<cBeneficiario>();
                BeneficiariosConPlanesSinFechaDeVencimiento = new List<cBeneficiario>();
                CargarComboBeneficiario();
                CargarGrillasAdministrativas();
                CargarGrillasEspecialistas();
                enproceso = false;
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
            BeneficiariosConPlanesPorVencerse = BeneficiariosConPlanesAVencerse;
            grdPlanesPorVencerse.DataSource = BeneficiariosConPlanesAVencerse;
            grdPlanesPorVencerse.DataBind();
        }
        protected void CargarGrillaPlanesSinFechaVencimiento()
        {
            List<cBeneficiario> listaBeneficiarios = new List<cBeneficiario>();
            List<cBeneficiario> listaBenConPlanes = new List<cBeneficiario>();
            List<cBeneficiario> BeneficiariosConPlanesSinFechaVencimiento = new List<cBeneficiario>();
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
                    if (listaBenConPlanes[a].lstPlanes[b].FechaFin == null)
                    {
                        BeneficiariosConPlanesSinFechaVencimiento.Add(listaBenConPlanes[a]);
                        break;
                        //si tiene varios planes se lista solo una vez el beneficiario
                    }
                }
            }
            BeneficiariosConPlanesSinFechaDeVencimiento = BeneficiariosConPlanesSinFechaVencimiento;
            this.grdBeneficiariosConPlanSinFechaVencimiento.DataSource = BeneficiariosConPlanesSinFechaVencimiento;
            this.grdBeneficiariosConPlanSinFechaVencimiento.DataBind();
        }
        protected void CargarGrillaEspecialistasConInformesPendientes()
        {
            grdEspecialistasConInformesPendientes.DataSource = dFachada.UsuarioTraerTodosEspecialistasConInformesPendientes();
            grdEspecialistasConInformesPendientes.DataBind();
        }
        protected void CargarGrillaSesionesPasaronDelDia()
        {
            SesionesPasaronDelDia = dFachada.SesionTraerPasaronDelDia();
            grdSesionesPasadasDelDia.DataSource = SesionesPasaronDelDia;
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
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + codigo.ToString());
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
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + codigo.ToString());
        }
        #endregion
        #region TAREAS ESPECIALISTAS

        protected void CargarGrillasEspecialistas()
        {//son 5 ahora 28/12
            CargarGrillaInformesPendientes();
            CargarGrillaInformesEnProceso();
            //CargarGrillaInformesTerminados();
            CargarGrillaSesionesDelDia();
            CargarGrillasSesionesObservaciones();
            CargarGrillaSesionesConObservacionesRealizadas();
        }

        protected void CargarGrillaInformesPendientes()
        {
            List<cInforme> ListaInformes = dFachada.InformeTraerTodosPendientesPorEspecialista(vMiPerfil.U);
            cInforme informe;

            List<ListarInformes> ListaInformesParaListar = new List<ListarInformes>();
            ListarInformes informeAListar;

            for (int i = 0; i < ListaInformes.Count; i++)
            {
                informe = new cInforme();
                informe = ListaInformes[i];
                informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(informe.Beneficiario);
                informeAListar = new ListarInformes();
                informeAListar.Codigo = informe.Codigo;
                informeAListar.Fecha = informe.Fecha;
                informeAListar.Estado = informe.Estado;
                informeAListar.Tipo = informe.Tipo;
                informeAListar.CodigoBeneficiario = informe.Beneficiario.Codigo;
                informeAListar.Nombres = informe.Beneficiario.Nombres;
                informeAListar.Apellidos = informe.Beneficiario.Apellidos;
                ListaInformesParaListar.Add(informeAListar);
            }

            grdInformesPendientes.DataSource = ListaInformesParaListar;
            grdInformesPendientes.DataBind();
        }
        protected void CargarGrillaInformesEnProceso()
        {
            List<cInforme> ListaInformes = dFachada.InformeTraerTodosEnProcesoPorEspecialista(vMiPerfil.U);
            cInforme informe;

            List<ListarInformes> ListaInformesParaListar = new List<ListarInformes>();
            ListarInformes informeAListar;

            for (int i = 0; i < ListaInformes.Count; i++)
            {
                informe = new cInforme();
                informe = ListaInformes[i];
                informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(informe.Beneficiario);
                informeAListar = new ListarInformes();
                informeAListar.Codigo = informe.Codigo;
                informeAListar.Fecha = informe.Fecha;
                informeAListar.Estado = informe.Estado;
                informeAListar.Tipo = informe.Tipo;
                informeAListar.CodigoBeneficiario = informe.Beneficiario.Codigo;
                informeAListar.Nombres = informe.Beneficiario.Nombres;
                informeAListar.Apellidos = informe.Beneficiario.Apellidos;
                ListaInformesParaListar.Add(informeAListar);
            }

            grdInformesEnProceso.DataSource = ListaInformesParaListar;
            grdInformesEnProceso.DataBind();
        }
        /*protected void CargarGrillaInformesTerminados()
        {
            List<cInforme> ListaInformes = dFachada.InformeTraerTodosTerminadosPorEspecialista(vMiPerfil.U);
            cInforme informe;

            List<ListarInformes> ListaInformesParaListar = new List<ListarInformes>();
            ListarInformes informeAListar;

            for (int i = 0; i < ListaInformes.Count; i++)
            {
                informe = new cInforme();
                informe = ListaInformes[i];
                informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(informe.Beneficiario);
                informeAListar = new ListarInformes();
                informeAListar.Codigo = informe.Codigo;
                informeAListar.Fecha = informe.Fecha;
                informeAListar.Estado = informe.Estado;
                informeAListar.Tipo = informe.Tipo;
                informeAListar.CodigoBeneficiario = informe.Beneficiario.Codigo;
                informeAListar.NombresBeneficiario = informe.Beneficiario.Nombres;
                informeAListar.ApellidosBeneficiario = informe.Beneficiario.Apellidos;
                ListaInformesParaListar.Add(informeAListar);
            }

            grdInformesTerminados.DataSource = ListaInformesParaListar;
            grdInformesTerminados.DataBind();
        }*/
        protected void CargarGrillaSesionesDelDia()
        {
            List<cSesion> sesiones = new List<cSesion>();
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
            lstBeneficiarios = new List<cBeneficiario>();
            lstBeneficiarios = dFachada.BeneficiarioTraerTodosPorEspecialista(vMiPerfil.U);
            List<string> lstNombreApellido = new List<string>() { "Todos" };
            string nomApe;
            for (int i = 0; i < lstBeneficiarios.Count; i++)
            {
                nomApe = "";
                nomApe = lstBeneficiarios[i].Nombres + " " + lstBeneficiarios[i].Apellidos;
                lstNombreApellido.Add(nomApe);
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
                string Consulta = "SELECT distinct S.* FROM UsuariosSesiones us " +
                    "join Sesiones S on us.SesionId = S.SesionId " +
                    "join BeneficiariosSesiones bs on bs.SesionId = S.SesionId";
                List<string> condiciones = new List<string>() { " WHERE" };
                condiciones.Add(string.Format(" us.UsuarioId={0} and us.UsuariosSesionesObservacion  is not NULL and us.UsuariosSesionesObservacion <> ''", vMiPerfil.U.Codigo));
                //Beneficiario
                if (ddlBeneficiario.SelectedIndex != 0)
                {
                    cBeneficiario beneficiario = lstBeneficiarios[ddlBeneficiario.SelectedIndex - 1];
                    int codigo = beneficiario.Codigo;
                    condiciones.Add(string.Format(" and bs.BeneficiarioId={0}", codigo));
                }
                //Fecha
                if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                {
                    condiciones.Add(string.Format(" and s.SesionFecha between '{0}' and '{1}'", txtDesde.Text, txtHasta.Text));
                }
                for (int i = 0; i < condiciones.Count; i++)
                {
                    Consulta += condiciones[i];
                }
                grdSesionesObservacionesRealizadas.DataSource = dFachada.SesionTraerTodasPorEspecialistaConFiltros(Consulta);
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
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + codigo.ToString());
        }
        protected void grdInformesEnProceso_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            enproceso = true;
            TableCell celdaCodigo = grdInformesEnProceso.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeRedactar.aspx?InformeId=" + codigo.ToString());
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
                int idSesion = int.Parse(celdaId.Text);
                string vtn = "window.open('vDetallesSesionParaAsistencia.aspx?Id=" + SesionesPasaronDelDia[e.NewSelectedIndex].Codigo + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
            }
            else
            {
                CargarGrillaSesionesPasaronDelDia();
                ventanaReprogramar = true;
            }

            //?id=<%# Eval('Id').ToString) %>
            //  + SesionesPasaronDelDia[e.NewSelectedIndex].Codigo
        }
        protected void grdObservacionesDeSesiones_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (ventanaObservacion)
            {
                ventanaObservacionVerDetalles = false;
                ventanaObservacion = false;
                TableCell celdaId = grdObservacionesDeSesiones.Rows[e.NewSelectedIndex].Cells[1];
                int idSesion = int.Parse(celdaId.Text);
                //Response.Redirect("vAgregarObservacionSesion.aspx?SesionId=" + idSesion);
                string vtn = "window.open('vAgregarObservacionSesion.aspx?SesionId=" + idSesion + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
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
                int idSesion = int.Parse(celdaId.Text);
                string vtn = "window.open('vAgregarObservacionSesion.aspx?SesionId=" + idSesion + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
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