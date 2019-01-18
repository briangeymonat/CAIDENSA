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
        private static List<string> TiposDeSesion = new List<string>() { "Individual", "Grupo 2", "Grupo 3", "Taller", "PROES" };
        private static List<string> Dias = new List<string>() { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };
        private static List<string> Especialidades = new List<string>() { "Psicologia", "Pedadogia", "Fisioterapia", "Fonoaudiologia", "Psicomotricidad" };
        private static List<cBeneficiario> TodosLosBenefiicarios;
        private static List<cBeneficiario> BeneficiariosAgregados;
        private static List<cUsuario> LosEspecialistas;
        private static List<cUsuario> EspecialistasAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int CantidadBeneficiarios = int.Parse(Request.QueryString["CantidadBeneficiarios"]);
                int CantidadEspecialistas = int.Parse(Request.QueryString["CantidadEspecialistas"]);
                BeneficiariosAgregados = new List<cBeneficiario>();
                EspecialistasAgregados = new List<cUsuario>();
                cBeneficiario unBen;
                for (int i = 0; i < CantidadBeneficiarios; i++)
                {
                    unBen = new cBeneficiario();
                    unBen.Codigo = int.Parse(Request.QueryString["Beneficiario" + (i + 1).ToString()]);
                    BeneficiariosAgregados.Add(dFachada.BeneficiarioTraerEspecifico(unBen));
                }
                cUsuario unUsu;
                for (int i = 0; i < CantidadEspecialistas; i++)
                {
                    unUsu = new cUsuario();
                    unUsu.Codigo = int.Parse(Request.QueryString["Usuario" + (i + 1).ToString()]);
                    EspecialistasAgregados.Add(dFachada.UsuarioTraerEspecifico(unUsu));
                }
                CargarDdlTiposDeSesion();
                CargarDdlEspecialidades();
                CargarBeneficiarios();
                CargarBeneficiariosAgregados();
                CargarEspecialistas();
                CargarEspecialistasAgregados();
                CargarDdlHoras();
            }
            


        }

        private void CargarTodo()
        {
            BeneficiariosAgregados = new List<cBeneficiario>();
            EspecialistasAgregados = new List<cUsuario>();
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
            ddlTipoSesion.DataSource = TiposDeSesion;
            ddlTipoSesion.DataBind();
        }
        private bool VerificarCantidadAgregados()
        {
            switch (ddlTipoSesion.SelectedValue.ToString())
            {
                case "Individual":
                    if (BeneficiariosAgregados.Count == 1) return true;
                    else return false;
                case "Grupo 2":
                    if (BeneficiariosAgregados.Count == 2) return true;
                    else return false;
                case "Grupo 3":
                    if (BeneficiariosAgregados.Count == 3) return true;
                    else return false;
                case "Taller":
                    if (BeneficiariosAgregados.Count >= 4 && BeneficiariosAgregados.Count <= 5) return true;
                    else return false;
                case "PROES":
                    if (BeneficiariosAgregados.Count <= 8) return true;
                    else return false;
                default:
                    return false;
            }
        }
        private void CargarDdlEspecialidades()
        {
            ddlEspecialidades.DataSource = Especialidades;
            ddlEspecialidades.DataBind();
        }

        private void CargarDdlHoras()
        {
            DateTime hora1 = DateTime.Parse("08:00");
            List<string> LasHorasDesde = new List<string>();
            LasHorasDesde.Add(hora1.ToShortTimeString());
            do
            {
                hora1 = hora1.AddMinutes(15);
                LasHorasDesde.Add(hora1.ToShortTimeString());
            } while (hora1 != DateTime.Parse("19:45"));
            ddlDesde.DataSource = LasHorasDesde;
            ddlDesde.DataBind();

            List<string> LasHorasHasta = new List<string>();
            DateTime hora2 = DateTime.Parse("08:15");
            LasHorasHasta.Add(hora2.ToShortTimeString());
            do
            {
                hora2 = hora2.AddMinutes(15);
                LasHorasHasta.Add(hora2.ToShortTimeString());
            } while (hora2 != DateTime.Parse("20:00"));
            ddlHasta.DataSource = LasHorasHasta;
            ddlHasta.DataBind();
        }

        private void CargarBeneficiarios()
        {
            string Consulta = "SELECT DISTINCT B.* FROM Beneficiarios B JOIN Planes P ON B.BeneficiarioId = P.BeneficiarioId WHERE B.BeneficiarioEstado = 1 AND P.PlanActivo = 1";
            if (txtBuscarBeneficiarios.Text != string.Empty)
            {
                Consulta += string.Format("AND (B.BeneficiarioNombres LIKE '{0}%' or B.BeneficiarioApellidos LIKE '{0}%' or CONVERT(varchar, B.BeneficiarioCI) LIKE '{0}%' )", txtBuscarBeneficiarios.Text);
            }
            if (BeneficiariosAgregados.Count > 0)
            {
                for (int i = 0; i < BeneficiariosAgregados.Count; i++)
                {
                    Consulta += " AND B.BeneficiarioId != " + BeneficiariosAgregados[i].Codigo.ToString();
                }
            }
            TodosLosBenefiicarios = dFachada.BeneficiarioTraerTodosConFiltros(Consulta);
            grdBeneficiarios.DataSource = TodosLosBenefiicarios;
            grdBeneficiarios.DataBind();
        }
        private void CargarBeneficiariosAgregados()
        {
            for (int i = 0; i < BeneficiariosAgregados.Count; i++)
            {
                BeneficiariosAgregados[i].lstPlanes = dFachada.PlanTraerActivosPorBeneficiario(BeneficiariosAgregados[i]);
            }
            grdBeneficiariosCargados.DataSource = BeneficiariosAgregados;
            grdBeneficiariosCargados.DataBind();



            #region Mostrar/Ocultar DdlPlanes
            List<List<string>> losPlanes = new List<List<string>>();
            for (int i = 0; i < BeneficiariosAgregados.Count; i++)
            {
                losPlanes.Add(new List<string>());
                foreach (cPlan unPlan in BeneficiariosAgregados[i].lstPlanes)
                {
                    losPlanes[i].Add(unPlan.Tipo);
                }
            }

            switch (BeneficiariosAgregados.Count)
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan1.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan2.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan3.DataSource = losPlanes[2];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = BeneficiariosAgregados[2].Nombres + " " + BeneficiariosAgregados[2].Apellidos;
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan3.DataSource = losPlanes[2];
                    ddlPlan4.DataSource = losPlanes[3];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = BeneficiariosAgregados[2].Nombres + " " + BeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = BeneficiariosAgregados[3].Nombres + " " + BeneficiariosAgregados[3].Apellidos;
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan3.DataSource = losPlanes[2];
                    ddlPlan4.DataSource = losPlanes[3];
                    ddlPlan5.DataSource = losPlanes[4];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = BeneficiariosAgregados[2].Nombres + " " + BeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = BeneficiariosAgregados[3].Nombres + " " + BeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = BeneficiariosAgregados[4].Nombres + " " + BeneficiariosAgregados[4].Apellidos;
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan3.DataSource = losPlanes[2];
                    ddlPlan4.DataSource = losPlanes[3];
                    ddlPlan5.DataSource = losPlanes[4];
                    ddlPlan6.DataSource = losPlanes[5];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    ddlPlan6.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = BeneficiariosAgregados[2].Nombres + " " + BeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = BeneficiariosAgregados[3].Nombres + " " + BeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = BeneficiariosAgregados[4].Nombres + " " + BeneficiariosAgregados[4].Apellidos;
                    lblNombre6.Text = BeneficiariosAgregados[5].Nombres + " " + BeneficiariosAgregados[5].Apellidos;
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
                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan3.DataSource = losPlanes[2];
                    ddlPlan4.DataSource = losPlanes[3];
                    ddlPlan5.DataSource = losPlanes[4];
                    ddlPlan6.DataSource = losPlanes[5];
                    ddlPlan7.DataSource = losPlanes[6];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    ddlPlan6.DataBind();
                    ddlPlan7.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = BeneficiariosAgregados[2].Nombres + " " + BeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = BeneficiariosAgregados[3].Nombres + " " + BeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = BeneficiariosAgregados[4].Nombres + " " + BeneficiariosAgregados[4].Apellidos;
                    lblNombre6.Text = BeneficiariosAgregados[5].Nombres + " " + BeneficiariosAgregados[5].Apellidos;
                    lblNombre7.Text = BeneficiariosAgregados[6].Nombres + " " + BeneficiariosAgregados[6].Apellidos;
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

                    ddlPlan1.DataSource = losPlanes[0];
                    ddlPlan2.DataSource = losPlanes[1];
                    ddlPlan3.DataSource = losPlanes[2];
                    ddlPlan4.DataSource = losPlanes[3];
                    ddlPlan5.DataSource = losPlanes[4];
                    ddlPlan6.DataSource = losPlanes[5];
                    ddlPlan7.DataSource = losPlanes[6];
                    ddlPlan8.DataSource = losPlanes[7];
                    ddlPlan1.DataBind();
                    ddlPlan2.DataBind();
                    ddlPlan3.DataBind();
                    ddlPlan4.DataBind();
                    ddlPlan5.DataBind();
                    ddlPlan6.DataBind();
                    ddlPlan7.DataBind();
                    ddlPlan8.DataBind();
                    lblNombre1.Text = BeneficiariosAgregados[0].Nombres + " " + BeneficiariosAgregados[0].Apellidos;
                    lblNombre2.Text = BeneficiariosAgregados[1].Nombres + " " + BeneficiariosAgregados[1].Apellidos;
                    lblNombre3.Text = BeneficiariosAgregados[2].Nombres + " " + BeneficiariosAgregados[2].Apellidos;
                    lblNombre4.Text = BeneficiariosAgregados[3].Nombres + " " + BeneficiariosAgregados[3].Apellidos;
                    lblNombre5.Text = BeneficiariosAgregados[4].Nombres + " " + BeneficiariosAgregados[4].Apellidos;
                    lblNombre6.Text = BeneficiariosAgregados[5].Nombres + " " + BeneficiariosAgregados[5].Apellidos;
                    lblNombre7.Text = BeneficiariosAgregados[6].Nombres + " " + BeneficiariosAgregados[6].Apellidos;
                    lblNombre8.Text = BeneficiariosAgregados[7].Nombres + " " + BeneficiariosAgregados[7].Apellidos;
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


            string Consulta = "SELECT U.*, E.EspecialidadNombre FROM Usuarios U JOIN Especialidades E ON U.EspecialidadId=E.EspecialidadId" +
                " WHERE UsuarioEstado = 1 AND E.EspecialidadId = " + unaEspecialidad.Codigo.ToString();
            if (EspecialistasAgregados.Count > 0)
            {
                for (int i = 0; i < EspecialistasAgregados.Count; i++)
                {
                    Consulta += " AND UsuarioId != " + EspecialistasAgregados[i].Codigo.ToString();
                }
            }
            LosEspecialistas = dFachada.UsuarioTraerEspecialistasConFiltros(Consulta);
            grdTodosEspecialistas.DataSource = LosEspecialistas;
            grdTodosEspecialistas.DataBind();
        }
        private void CargarEspecialistasAgregados()
        {
            grdEspecialistasAgregados.DataSource = EspecialistasAgregados;
            grdEspecialistasAgregados.DataBind();
        }

        protected void grdBeneficiarios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            BeneficiariosAgregados.Add(TodosLosBenefiicarios[e.NewSelectedIndex]);
            CargarBeneficiarios();
            CargarBeneficiariosAgregados();

        }

        protected void grdBeneficiariosCargados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BeneficiariosAgregados.RemoveAt(e.RowIndex);
            CargarBeneficiariosAgregados();
            CargarBeneficiarios();
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
            EspecialistasAgregados.Add(LosEspecialistas[e.NewSelectedIndex]);
            CargarEspecialistasAgregados();
            CargarEspecialistas();
        }

        protected void grdEspecialistasAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            EspecialistasAgregados.RemoveAt(e.RowIndex);
            CargarEspecialistasAgregados();
            CargarEspecialistas();
        }



        private bool FaltanDatos()
        {
            if (txtFecha.Text == string.Empty ||
                BeneficiariosAgregados.Count <= 0 ||
                EspecialistasAgregados.Count <= 0)
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
                        for (int i = 0; i < BeneficiariosAgregados.Count; i++)
                        {
                            unBen = new cBeneficiarioSesion();
                            unBen.Beneficiario = BeneficiariosAgregados[i]; switch (i)
                            {
                                case 0:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan1.SelectedIndex];
                                    break;
                                case 1:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan2.SelectedIndex];
                                    break;
                                case 2:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan3.SelectedIndex];
                                    break;
                                case 3:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan4.SelectedIndex];
                                    break;
                                case 4:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan5.SelectedIndex];
                                    break;
                                case 5:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan6.SelectedIndex];
                                    break;
                                case 6:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan7.SelectedIndex];
                                    break;
                                case 7:
                                    unBen.Plan = BeneficiariosAgregados[i].lstPlanes[ddlPlan8.SelectedIndex];
                                    break;
                                default:
                                    break;
                            }
                            unBen.Estado = cUtilidades.EstadoSesion.SinEstado;
                            unaSesion.lstBeneficiarios.Add(unBen);
                        }

                        unaSesion.lstUsuarios = EspecialistasAgregados;
                        List<cUsuario> EspecialistasNoDisponibles = dFachada.SesionVerificarFechaYHorarioUsuario(unaSesion);
                        List<cBeneficiario> BeneficiariosNoDisponibles = dFachada.SesionVerificarFechaYHorarioBeneficiario(unaSesion);
                        string especialistas = "";
                        string beneficiarios = "";

                        // ESPECIALISTAS NO DISPONIBLES
                        if (EspecialistasNoDisponibles.Count > 0)
                        {
                            if (EspecialistasNoDisponibles.Count > 1)
                            {
                                especialistas += "Lo especialistas ";
                            }
                            for (int i = 0; i < EspecialistasNoDisponibles.Count; i++)
                            {
                                if (i == EspecialistasNoDisponibles.Count - 1)
                                {
                                    especialistas += EspecialistasNoDisponibles[i].Nombres + " " + EspecialistasNoDisponibles[i].Apellidos;
                                }
                                else if (i == 0)
                                {
                                    especialistas += EspecialistasNoDisponibles[i].Nombres + " " + EspecialistasNoDisponibles[i].Apellidos + ", ";
                                }
                                else if (i == EspecialistasNoDisponibles.Count - 2)
                                {
                                    especialistas += EspecialistasNoDisponibles[i].Nombres + " " + EspecialistasNoDisponibles[i].Apellidos + " y ";
                                }
                            }
                            if (EspecialistasNoDisponibles.Count > 1)
                            {
                                especialistas += " no están disponibles para la sesión.";
                            }
                            else
                            {
                                especialistas += " no está disponible para la sesión.";
                            }
                        }
                        if (BeneficiariosNoDisponibles.Count > 0)
                        {
                            if (EspecialistasNoDisponibles.Count > 1)
                            {
                                beneficiarios += "Los beneficiarios ";
                            }
                            for (int i = 0; i < BeneficiariosNoDisponibles.Count; i++)
                            {
                                if (i == BeneficiariosNoDisponibles.Count - 1)
                                {
                                    beneficiarios += BeneficiariosNoDisponibles[i].Nombres + " " + BeneficiariosNoDisponibles[i].Apellidos;
                                }
                                else if (i == 0)
                                {
                                    beneficiarios += BeneficiariosNoDisponibles[i].Nombres + " " + BeneficiariosNoDisponibles[i].Apellidos + ", ";
                                }
                                else if (i == BeneficiariosNoDisponibles.Count - 2)
                                {
                                    beneficiarios += BeneficiariosNoDisponibles[i].Nombres + " " + BeneficiariosNoDisponibles[i].Apellidos + " y ";
                                }
                            }
                            if (BeneficiariosNoDisponibles.Count > 1)
                            {
                                beneficiarios += " no están disponibles para la sesión.";
                            }
                            else
                            {
                                beneficiarios += " no está disponible para la sesión.";
                            }
                        }
                        if (EspecialistasNoDisponibles.Count > 0 || BeneficiariosNoDisponibles.Count > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", string.Format("alert('ERROR: {0}{1} Su horario coincide con el de otra sesión.')", especialistas, beneficiarios), true);
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
    }
}