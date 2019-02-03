using System;
using Common.Clases;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vSesionNueva : System.Web.UI.Page
    {
        private static List<string> LosTiposDeSesion = new List<string>() { "Individual", "Grupo 2", "Grupo 3", "Taller", "PROES" };
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };
        private static List<string> LosEspecialidades = new List<string>() { "Psicologia", "Pedadogia", "Fisioterapia", "Fonoaudiologia", "Psicomotricidad" };
        private static List<cBeneficiario> LosBenefiicarios;
        private static List<cBeneficiario> LosBeneficiariosAgregados;
        private static List<cUsuario> LosEspecialistas;
        private static List<cUsuario> LosEspecialistasAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int iCantidadBeneficiarios = int.Parse(Request.QueryString["CantidadBeneficiarios"]);
                int iCantidadEspecialistas = int.Parse(Request.QueryString["CantidadEspecialistas"]);
                LosBeneficiariosAgregados = new List<cBeneficiario>();
                LosEspecialistasAgregados = new List<cUsuario>();
                cBeneficiario unBen;
                for (int i = 0; i < iCantidadBeneficiarios; i++)
                {
                    unBen = new cBeneficiario();
                    unBen.Codigo = int.Parse(Request.QueryString["Beneficiario" + (i + 1).ToString()]);
                    LosBeneficiariosAgregados.Add(dFachada.BeneficiarioTraerEspecifico(unBen));
                }
                cUsuario unUsu;
                for (int i = 0; i < iCantidadEspecialistas; i++)
                {
                    unUsu = new cUsuario();
                    unUsu.Codigo = int.Parse(Request.QueryString["Usuario" + (i + 1).ToString()]);
                    LosEspecialistasAgregados.Add(dFachada.UsuarioTraerEspecifico(unUsu));
                }
                CargarDdlTiposDeSesion();
                CargarDdlEspecialidades();
                CargarBeneficiarios();
                CargarBeneficiariosAgregados();
                CargarEspecialistas();
                CargarEspecialistasAgregados();
                CargarDdlHoras();
                ddlHasta.SelectedIndex = 2;

                if (LosBeneficiariosAgregados.Count<=0)
                {
                    lblSeleccionarPlan.Visible = false;
                }


            }



        }

        private void CargarTodo()
        {
            LosBeneficiariosAgregados = new List<cBeneficiario>();
            LosEspecialistasAgregados = new List<cUsuario>();
            CargarDdlTiposDeSesion();
            CargarDdlEspecialidades();
            CargarBeneficiarios();
            CargarBeneficiariosAgregados();
            CargarEspecialistas();
            CargarEspecialistasAgregados();
            CargarDdlHoras();

        }
        private void CargarDdlTiposDeSesion()
        {
            ddlTipoSesion.DataSource = LosTiposDeSesion;
            ddlTipoSesion.DataBind();
        }
        private bool VerificarCantidadAgregados()
        {
            switch (ddlTipoSesion.SelectedValue.ToString())
            {
                case "Individual":
                    if (LosBeneficiariosAgregados.Count == 1) return true;
                    else return false;
                case "Grupo 2":
                    if (LosBeneficiariosAgregados.Count == 2) return true;
                    else return false;
                case "Grupo 3":
                    if (LosBeneficiariosAgregados.Count == 3) return true;
                    else return false;
                case "Taller":
                    if (LosBeneficiariosAgregados.Count == 4) return true;
                    else return false;
                case "PROES":
                    if (LosBeneficiariosAgregados.Count <= 8 && LosBeneficiariosAgregados.Count >= 5 && LosEspecialistasAgregados.Count == 2) return true;
                    else return false;
                default:
                    return false;
            }
        }
        private void CargarDdlEspecialidades()
        {
            ddlEspecialidades.DataSource = LosEspecialidades;
            ddlEspecialidades.DataBind();
        }

        private void CargarDdlHoras()
        {
            DateTime dHora1 = DateTime.Parse("08:00");
            List<string> lstHorasDesde = new List<string>();
            lstHorasDesde.Add(dHora1.ToShortTimeString());
            do
            {
                dHora1 = dHora1.AddMinutes(15);
                lstHorasDesde.Add(dHora1.ToShortTimeString());
            } while (dHora1 != DateTime.Parse("19:45"));
            ddlDesde.DataSource = lstHorasDesde;
            ddlDesde.DataBind();

            List<string> lstHorasHasta = new List<string>();
            DateTime dHora2 = DateTime.Parse("08:15");
            lstHorasHasta.Add(dHora2.ToShortTimeString());
            do
            {
                dHora2 = dHora2.AddMinutes(15);
                lstHorasHasta.Add(dHora2.ToShortTimeString());
            } while (dHora2 != DateTime.Parse("20:00"));
            ddlHasta.DataSource = lstHorasHasta;
            ddlHasta.DataBind();
        }

        private void CargarBeneficiarios()
        {
            string sConsulta = "SELECT DISTINCT B.* FROM Beneficiarios B JOIN Planes P ON B.BeneficiarioId = P.BeneficiarioId WHERE B.BeneficiarioEstado = 1 AND P.PlanActivo = 1";
            if (txtBuscarBeneficiarios.Text != string.Empty)
            {
                sConsulta += string.Format("AND (B.BeneficiarioNombres LIKE '{0}%' or B.BeneficiarioApellidos LIKE '{0}%' or CONVERT(varchar, B.BeneficiarioCI) LIKE '{0}%' )", txtBuscarBeneficiarios.Text);
            }
            if (LosBeneficiariosAgregados.Count > 0)
            {
                for (int i = 0; i < LosBeneficiariosAgregados.Count; i++)
                {
                    sConsulta += " AND B.BeneficiarioId != " + LosBeneficiariosAgregados[i].Codigo.ToString();
                }
            }
            LosBenefiicarios = dFachada.BeneficiarioTraerTodosConFiltros(sConsulta);
            grdBeneficiarios.DataSource = LosBenefiicarios;
            grdBeneficiarios.DataBind();
        }
        private void CargarBeneficiariosAgregados()
        {
            for (int i = 0; i < LosBeneficiariosAgregados.Count; i++)
            {
                LosBeneficiariosAgregados[i].lstPlanes = dFachada.PlanTraerActivosPorBeneficiario(LosBeneficiariosAgregados[i]);
            }
            grdBeneficiariosCargados.DataSource = LosBeneficiariosAgregados;
            grdBeneficiariosCargados.DataBind();



            #region Mostrar/Ocultar DdlPlanes
            List<List<string>> lstPlanes = new List<List<string>>();
            for (int i = 0; i < LosBeneficiariosAgregados.Count; i++)
            {
                lstPlanes.Add(new List<string>());
                foreach (cPlan unPlan in LosBeneficiariosAgregados[i].lstPlanes)
                {
                    lstPlanes[i].Add(unPlan.Tipo);
                }
            }

            switch (LosBeneficiariosAgregados.Count)
            {
                case 1:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = false;
                    ddlPlan3.Visible = false;
                    ddlPlan4.Visible = false;
                    ddlPlan5.Visible = false;
                    ddlPlan6.Visible = false;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = false;
                    lblNombre3.Visible = false;
                    lblNombre4.Visible = false;
                    lblNombre5.Visible = false;
                    lblNombre6.Visible = false;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan1.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    break;
                case 2:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = false;
                    ddlPlan4.Visible = false;
                    ddlPlan5.Visible = false;
                    ddlPlan6.Visible = false;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = false;
                    lblNombre4.Visible = false;
                    lblNombre5.Visible = false;
                    lblNombre6.Visible = false;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan2.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    break;
                case 3:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = true;
                    ddlPlan4.Visible = false;
                    ddlPlan5.Visible = false;
                    ddlPlan6.Visible = false;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = true;
                    lblNombre4.Visible = false;
                    lblNombre5.Visible = false;
                    lblNombre6.Visible = false;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan3.DataSource = lstPlanes[2];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = LosBeneficiariosAgregados[2].Nombres + " " + LosBeneficiariosAgregados[2].Apellidos;
                    break;
                case 4:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = true;
                    ddlPlan4.Visible = true;
                    ddlPlan5.Visible = false;
                    ddlPlan6.Visible = false;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = true;
                    lblNombre4.Visible = true;
                    lblNombre5.Visible = false;
                    lblNombre6.Visible = false;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan3.DataSource = lstPlanes[2];
                    ddlPlan4.DataSource = lstPlanes[3];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = LosBeneficiariosAgregados[2].Nombres + " " + LosBeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = LosBeneficiariosAgregados[3].Nombres + " " + LosBeneficiariosAgregados[3].Apellidos;
                    break;
                case 5:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = true;
                    ddlPlan4.Visible = true;
                    ddlPlan5.Visible = true;
                    ddlPlan6.Visible = false;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = true;
                    lblNombre4.Visible = true;
                    lblNombre5.Visible = true;
                    lblNombre6.Visible = false;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan3.DataSource = lstPlanes[2];
                    ddlPlan4.DataSource = lstPlanes[3];
                    ddlPlan5.DataSource = lstPlanes[4];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = LosBeneficiariosAgregados[2].Nombres + " " + LosBeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = LosBeneficiariosAgregados[3].Nombres + " " + LosBeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = LosBeneficiariosAgregados[4].Nombres + " " + LosBeneficiariosAgregados[4].Apellidos;
                    break;
                case 6:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = true;
                    ddlPlan4.Visible = true;
                    ddlPlan5.Visible = true;
                    ddlPlan6.Visible = true;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = true;
                    lblNombre4.Visible = true;
                    lblNombre5.Visible = true;
                    lblNombre6.Visible = true;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan3.DataSource = lstPlanes[2];
                    ddlPlan4.DataSource = lstPlanes[3];
                    ddlPlan5.DataSource = lstPlanes[4];
                    ddlPlan6.DataSource = lstPlanes[5];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    ddlPlan6.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = LosBeneficiariosAgregados[2].Nombres + " " + LosBeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = LosBeneficiariosAgregados[3].Nombres + " " + LosBeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = LosBeneficiariosAgregados[4].Nombres + " " + LosBeneficiariosAgregados[4].Apellidos;
                    lblNombre6.Text = LosBeneficiariosAgregados[5].Nombres + " " + LosBeneficiariosAgregados[5].Apellidos;
                    break;
                case 7:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = true;
                    ddlPlan4.Visible = true;
                    ddlPlan5.Visible = true;
                    ddlPlan6.Visible = true;
                    ddlPlan7.Visible = true;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = true;
                    lblNombre4.Visible = true;
                    lblNombre5.Visible = true;
                    lblNombre6.Visible = true;
                    lblNombre7.Visible = true;
                    lblNombre8.Visible = false;
                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan3.DataSource = lstPlanes[2];
                    ddlPlan4.DataSource = lstPlanes[3];
                    ddlPlan5.DataSource = lstPlanes[4];
                    ddlPlan6.DataSource = lstPlanes[5];
                    ddlPlan7.DataSource = lstPlanes[6];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    ddlPlan6.DataBind();
                    ddlPlan7.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = LosBeneficiariosAgregados[2].Nombres + " " + LosBeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = LosBeneficiariosAgregados[3].Nombres + " " + LosBeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = LosBeneficiariosAgregados[4].Nombres + " " + LosBeneficiariosAgregados[4].Apellidos;
                    lblNombre6.Text = LosBeneficiariosAgregados[5].Nombres + " " + LosBeneficiariosAgregados[5].Apellidos;
                    lblNombre7.Text = LosBeneficiariosAgregados[6].Nombres + " " + LosBeneficiariosAgregados[6].Apellidos;
                    break;
                case 8:
                    ddlPlan1.Visible = true;
                    ddlPlan2.Visible = true;
                    ddlPlan3.Visible = true;
                    ddlPlan4.Visible = true;
                    ddlPlan5.Visible = true;
                    ddlPlan6.Visible = true;
                    ddlPlan7.Visible = true;
                    ddlPlan8.Visible = true;
                    lblNombre1.Visible = true;
                    lblNombre2.Visible = true;
                    lblNombre3.Visible = true;
                    lblNombre4.Visible = true;
                    lblNombre5.Visible = true;
                    lblNombre6.Visible = true;
                    lblNombre7.Visible = true;
                    lblNombre8.Visible = true;

                    ddlPlan1.DataSource = lstPlanes[0];
                    ddlPlan2.DataSource = lstPlanes[1];
                    ddlPlan3.DataSource = lstPlanes[2];
                    ddlPlan4.DataSource = lstPlanes[3];
                    ddlPlan5.DataSource = lstPlanes[4];
                    ddlPlan6.DataSource = lstPlanes[5];
                    ddlPlan7.DataSource = lstPlanes[6];
                    ddlPlan8.DataSource = lstPlanes[7];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    ddlPlan6.DataBind();
                    ddlPlan7.DataBind();
                    ddlPlan8.DataBind();
                    lblNombre1.Text = LosBeneficiariosAgregados[0].Nombres + " " + LosBeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = LosBeneficiariosAgregados[1].Nombres + " " + LosBeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = LosBeneficiariosAgregados[2].Nombres + " " + LosBeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = LosBeneficiariosAgregados[3].Nombres + " " + LosBeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = LosBeneficiariosAgregados[4].Nombres + " " + LosBeneficiariosAgregados[4].Apellidos;
                    lblNombre6.Text = LosBeneficiariosAgregados[5].Nombres + " " + LosBeneficiariosAgregados[5].Apellidos;
                    lblNombre7.Text = LosBeneficiariosAgregados[6].Nombres + " " + LosBeneficiariosAgregados[6].Apellidos;
                    lblNombre8.Text = LosBeneficiariosAgregados[7].Nombres + " " + LosBeneficiariosAgregados[7].Apellidos;
                    break;
                default:
                    ddlPlan1.Visible = false;
                    ddlPlan2.Visible = false;
                    ddlPlan3.Visible = false;
                    ddlPlan4.Visible = false;
                    ddlPlan5.Visible = false;
                    ddlPlan6.Visible = false;
                    ddlPlan7.Visible = false;
                    ddlPlan8.Visible = false;
                    lblNombre1.Visible = false;
                    lblNombre2.Visible = false;
                    lblNombre3.Visible = false;
                    lblNombre4.Visible = false;
                    lblNombre5.Visible = false;
                    lblNombre6.Visible = false;
                    lblNombre7.Visible = false;
                    lblNombre8.Visible = false;
                    break;

            }

            #endregion


        }
        private void CargarEspecialistas()
        {
            cEspecialidad unaEspecialidad = new cEspecialidad();
            unaEspecialidad.Nombre = ddlEspecialidades.SelectedValue;//.ToString();
            unaEspecialidad = dFachada.EspecialidadTraerEspecificaPorNombre(unaEspecialidad);


            string sConsulta = "SELECT U.*, E.EspecialidadNombre FROM Usuarios U JOIN Especialidades E ON U.EspecialidadId=E.EspecialidadId" +
                " WHERE UsuarioEstado = 1 AND E.EspecialidadId = " + unaEspecialidad.Codigo.ToString();
            if (LosEspecialistasAgregados.Count > 0)
            {
                for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
                {
                    sConsulta += " AND UsuarioId != " + LosEspecialistasAgregados[i].Codigo.ToString();
                }
            }
            LosEspecialistas = dFachada.UsuarioTraerEspecialistasConFiltros(sConsulta);
            grdTodosEspecialistas.DataSource = LosEspecialistas;
            grdTodosEspecialistas.DataBind();
        }
        private void CargarEspecialistasAgregados()
        {
            grdEspecialistasAgregados.DataSource = LosEspecialistasAgregados;
            grdEspecialistasAgregados.DataBind();
        }

        protected void grdBeneficiarios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            LosBeneficiariosAgregados.Add(LosBenefiicarios[e.NewSelectedIndex]);
            CargarBeneficiarios();
            CargarBeneficiariosAgregados();
            lblSeleccionarPlan.Visible = true;

        }

        protected void grdBeneficiariosCargados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LosBeneficiariosAgregados.RemoveAt(e.RowIndex);
            CargarBeneficiariosAgregados();
            CargarBeneficiarios();
            if (LosBeneficiariosAgregados.Count <= 0)
            {
                lblSeleccionarPlan.Visible = false;
            }
        }

        protected void txtBuscarBeneficiarios_TextChanged(object sender, EventArgs e)
        {
            CargarBeneficiarios();
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEspecialistas();
        }

        protected void grdTodosEspecialistas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            LosEspecialistasAgregados.Add(LosEspecialistas[e.NewSelectedIndex]);
            CargarEspecialistasAgregados();
            CargarEspecialistas();
        }

        protected void grdEspecialistasAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LosEspecialistasAgregados.RemoveAt(e.RowIndex);
            CargarEspecialistasAgregados();
            CargarEspecialistas();
        }



        private bool FaltanDatos()
        {
            if (txtFecha.Text == string.Empty ||
                LosBeneficiariosAgregados.Count <= 0 ||
                LosEspecialistasAgregados.Count <= 0)
                return true;
            return false;
        }

        protected void grdBeneficiarios_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void grdBeneficiariosCargados_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void grdTodosEspecialistas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //Codigo
            e.Row.Cells[2].Visible = false; //NickName
            e.Row.Cells[3].Visible = false; //Contrasena
            e.Row.Cells[7].Visible = false; //TipoUsuario
            e.Row.Cells[8].Visible = false; //Domicilio
            e.Row.Cells[9].Visible = false; //FechaNacimiento
            e.Row.Cells[11].Visible = false; //Email
            e.Row.Cells[12].Visible = false; //Estado
            e.Row.Cells[13].Visible = false; //TipoContrato
        }

        protected void grdEspecialistasAgregados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //Codigo
            e.Row.Cells[2].Visible = false; //NickName
            e.Row.Cells[3].Visible = false; //Contrasena
            e.Row.Cells[7].Visible = false; //TipoUsuario
            e.Row.Cells[8].Visible = false; //Domicilio
            e.Row.Cells[9].Visible = false; //FechaNacimiento
            e.Row.Cells[11].Visible = false; //Email
            e.Row.Cells[12].Visible = false; //Estado
            e.Row.Cells[13].Visible = false; //TipoContrato
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!FaltanDatos())
            {
                if (!VerificarCantidadAgregados())
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El número de beneficiarios no coincide con el tipo de sesión.')", true);
                }
                else
                {
                    cSesion unaSesion = new cSesion();
                    switch (ddlTipoSesion.SelectedValue.ToString())
                    {
                        case "Individual":
                            unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                            break;
                        case "Grupo 2":
                            unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                            break;
                        case "Grupo 3":
                            unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                            break;
                        case "Taller":
                            unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                            break;
                        case "PROES":
                            unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                            break;
                    }
                    unaSesion.Fecha = txtFecha.Text;
                    if (RadioButtonList1.SelectedIndex == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                    else unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                    if (DateTime.Parse(ddlDesde.SelectedValue) >= DateTime.Parse(ddlHasta.SelectedValue))
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La hora de fin de la sesión debe ser mayor a la de inicio.')", true);
                    }
                    else
                    {
                        unaSesion.HoraInicio = ddlDesde.SelectedValue;
                        unaSesion.HoraFin = DateTime.Parse(ddlHasta.SelectedValue).AddMinutes(-1).ToShortTimeString();
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBen;
                        for (int i = 0; i < LosBeneficiariosAgregados.Count; i++)
                        {
                            unBen = new cBeneficiarioSesion();
                            unBen.Beneficiario = LosBeneficiariosAgregados[i]; switch (i)
                            {
                                case 0:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan1.SelectedIndex];
                                    break;
                                case 1:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan2.SelectedIndex];
                                    break;
                                case 2:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan3.SelectedIndex];
                                    break;
                                case 3:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan4.SelectedIndex];
                                    break;
                                case 4:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan5.SelectedIndex];
                                    break;
                                case 5:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan6.SelectedIndex];
                                    break;
                                case 6:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan7.SelectedIndex];
                                    break;
                                case 7:
                                    unBen.Plan = LosBeneficiariosAgregados[i].lstPlanes[ddlPlan8.SelectedIndex];
                                    break;
                                default:
                                    break;
                            }
                            unBen.Estado = cUtilidades.EstadoSesion.SinEstado;
                            unaSesion.lstBeneficiarios.Add(unBen);
                        }

                        unaSesion.lstUsuarios = LosEspecialistasAgregados;
                        List<cUsuario> lstEspecialistasNoDisponibles = dFachada.SesionVerificarFechaYHorarioUsuario(unaSesion);
                        List<cBeneficiario> lstBeneficiariosNoDisponibles = dFachada.SesionVerificarFechaYHorarioBeneficiario(unaSesion);
                        string sEspecialistas = "";
                        string sBeneficiarios = "";

                        // ESPECIALISTAS NO DISPONIBLES
                        if (lstEspecialistasNoDisponibles.Count > 0)
                        {
                            if (lstEspecialistasNoDisponibles.Count > 1)
                            {
                                sEspecialistas += "Lo especialistas ";
                            }
                            for (int i = 0; i < lstEspecialistasNoDisponibles.Count; i++)
                            {
                                if (i == lstEspecialistasNoDisponibles.Count - 1)
                                {
                                    sEspecialistas += lstEspecialistasNoDisponibles[i].Nombres + " " + lstEspecialistasNoDisponibles[i].Apellidos;
                                }
                                else if (i == 0)
                                {
                                    sEspecialistas += lstEspecialistasNoDisponibles[i].Nombres + " " + lstEspecialistasNoDisponibles[i].Apellidos + ", ";
                                }
                                else if (i == lstEspecialistasNoDisponibles.Count - 2)
                                {
                                    sEspecialistas += lstEspecialistasNoDisponibles[i].Nombres + " " + lstEspecialistasNoDisponibles[i].Apellidos + " y ";
                                }
                            }
                            if (lstEspecialistasNoDisponibles.Count > 1)
                            {
                                sEspecialistas += " no están disponibles para la sesión.";
                            }
                            else
                            {
                                sEspecialistas += " no está disponible para la sesión.";
                            }
                        }
                        if (lstBeneficiariosNoDisponibles.Count > 0)
                        {
                            if (lstEspecialistasNoDisponibles.Count > 1)
                            {
                                sBeneficiarios += "Los beneficiarios ";
                            }
                            for (int i = 0; i < lstBeneficiariosNoDisponibles.Count; i++)
                            {
                                if (i == lstBeneficiariosNoDisponibles.Count - 1)
                                {
                                    sBeneficiarios += lstBeneficiariosNoDisponibles[i].Nombres + " " + lstBeneficiariosNoDisponibles[i].Apellidos;
                                }
                                else if (i == 0)
                                {
                                    sBeneficiarios += lstBeneficiariosNoDisponibles[i].Nombres + " " + lstBeneficiariosNoDisponibles[i].Apellidos + ", ";
                                }
                                else if (i == lstBeneficiariosNoDisponibles.Count - 2)
                                {
                                    sBeneficiarios += lstBeneficiariosNoDisponibles[i].Nombres + " " + lstBeneficiariosNoDisponibles[i].Apellidos + " y ";
                                }
                            }
                            if (lstBeneficiariosNoDisponibles.Count > 1)
                            {
                                sBeneficiarios += " no están disponibles para la sesión.";
                            }
                            else
                            {
                                sBeneficiarios += " no está disponible para la sesión.";
                            }
                        }
                        if (lstEspecialistasNoDisponibles.Count > 0 || lstBeneficiariosNoDisponibles.Count > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", string.Format("alert('ERROR: {0}{1} Su horario coincide con el de otra sesión.')", sEspecialistas, sBeneficiarios), true);
                        }
                        else
                        {
                            if (dFachada.SesionAgregar(unaSesion)) { CargarTodo(); dFachada.SesionAgregarSesionesDelDia(); }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo agregar la sesión.')", true);
                            }
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Se requiere ingresar todos los datos de la sesión.')", true);
            }
        }

        protected void ddlDesde_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHasta.SelectedIndex < ddlDesde.SelectedIndex)
                ddlHasta.SelectedIndex = ddlDesde.SelectedIndex + 2;
        }
    }
}