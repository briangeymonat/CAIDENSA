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
    public partial class vItinerarioNuevaConsulta : System.Web.UI.Page
    {
        private static List<string> TiposDeSesion = new List<string>() { "Individual", "Grupo 2", "Grupo 3", "Taller", "PROES" };
        private static List<string> Dias = new List<string>() { "Lunes", "Martas", "Miercoles", "Jueves", "Viernes", "Sabado" };
        private static List<string> Especialidades = new List<string>() { "Fonoaudiologia", "Fisioterapia", "Pedadogia", "Psicologia", "Psicomotricidad" };
        private static List<cBeneficiario> TodosLosBenefiicarios;
        private static List<cBeneficiario> BeneficiariosAgregados;
        private static List<cUsuario> LosEspecialistas;
        private static List<cUsuario> EspecialistasAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTodo();
            }

        }
        private void CargarTodo()
        {
            BeneficiariosAgregados = new List<cBeneficiario>();
            EspecialistasAgregados = new List<cUsuario>();
            CargarDdlTiposDeSesion();
            CargarDdlDias();
            CargarDdlEspecialidades();
            CargarBeneficiarios();
            CargarBeneficiariosAgregados();
            CargarEspecialistas();

        }
        private void CargarDdlTiposDeSesion()
        {
            ddlTipoSesion.DataSource = TiposDeSesion;
            ddlTipoSesion.DataBind();
        }
        protected void ddlTipoSesion_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarCantidadAgregados();
        }
        private bool VerificarCantidadAgregados()
        {
            string tipoSesion = ddlTipoSesion.SelectedValue.ToString();
            if ((tipoSesion == "Individual" && BeneficiariosAgregados.Count > 1) ||
                (tipoSesion == "Grupo 2" && BeneficiariosAgregados.Count > 2) ||
                (tipoSesion == "Grupo 3" && BeneficiariosAgregados.Count > 3) ||
                (tipoSesion == "Taller" && BeneficiariosAgregados.Count > 5) ||
                (tipoSesion == "PROES" && BeneficiariosAgregados.Count > 8))
                return false;
            else
                return true;
        }
        private void CargarDdlDias()
        {
            ddlDias.DataSource = Dias;
            ddlDias.DataBind();
        }
        private void CargarDdlEspecialidades()
        {
            ddlEspecialidades.DataSource = Especialidades;
            ddlEspecialidades.DataBind();
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
            grdBeneficiariosAgregados.DataSource = BeneficiariosAgregados;
            grdBeneficiariosAgregados.DataBind();
        }
        private void CargarEspecialistas()
        {
            cEspecialidad unaEspecialidad = new cEspecialidad();
            unaEspecialidad.Nombre = ddlEspecialidades.SelectedValue.ToString();
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
            CargarBeneficiariosAgregados();
            CargarBeneficiarios();

        }
        protected void grdBeneficiariosAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

        protected void ddlEspecialidades_TextChanged(object sender, EventArgs e)
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
            if (txtDesde.Text == string.Empty ||
                txtHasta.Text == string.Empty ||
                BeneficiariosAgregados.Count <= 0 ||
                EspecialistasAgregados.Count <= 0)
                return true;
            return false;
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
                    cItinerario unItinerario = new cItinerario();
                    switch (ddlTipoSesion.SelectedValue.ToString())
                    {
                        case "Individual":
                            unItinerario.TipoSesion = cUtilidades.TipoSesion.Individual;
                            break;
                        case "Grupo 2":
                            unItinerario.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                            break;
                        case "Grupo 3":
                            unItinerario.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                            break;
                        case "Taller":
                            unItinerario.TipoSesion = cUtilidades.TipoSesion.Taller;
                            break;
                        case "PROES":
                            unItinerario.TipoSesion = cUtilidades.TipoSesion.PROES;
                            break;
                    }
                    switch (ddlDias.SelectedValue.ToString())
                    {
                        case "Lunes":
                            unItinerario.Dia = "L";
                            break;
                        case "Martas":
                            unItinerario.Dia = "M";
                            break;
                        case "Miercoles":
                            unItinerario.Dia = "X";
                            break;
                        case "Jueves":
                            unItinerario.Dia = "J";
                            break;
                        case "Viernes":
                            unItinerario.Dia = "V";
                            break;
                        case "Sabado":
                            unItinerario.Dia = "S";
                            break;
                    }
                    if (RadioButtonList1.SelectedIndex == 0) unItinerario.Centro = cUtilidades.Centro.JuanLacaze;
                    else unItinerario.Centro = cUtilidades.Centro.NuevaHelvecia;
                    unItinerario.HoraInicio = DateTime.Parse(txtDesde.Text);
                    unItinerario.HoraFin = DateTime.Parse(txtHasta.Text);
                    unItinerario.lstBeneficiarios = new List<cBeneficiarioItinerario>();
                    cBeneficiarioItinerario unBen;
                    for (int i = 0; i < BeneficiariosAgregados.Count; i++)
                    {
                        unBen = new cBeneficiarioItinerario();
                        unBen.Beneficiario = BeneficiariosAgregados[i];
                        unBen.Plan = BeneficiariosAgregados[i].lstPlanes[0];
                        unItinerario.lstBeneficiarios.Add(unBen);
                    }
                    unItinerario.lstEspecialistas = EspecialistasAgregados;
                    unItinerario.Comentario = txtComentario.Text;
                    List<cUsuario> EspecialistasNoDisponibles = dFachada.ItinerarioVerificarHorarioUsuario(unItinerario);
                    List<cBeneficiario> BeneficiariosNoDisponibles = dFachada.ItinerarioVerificarHorarioBeneficiarios(unItinerario);
                    string especialistas = "";
                    string beneficiarios = "";


                    //ESPECIALISTAS NO DISPONIBLES
                    if (EspecialistasNoDisponibles.Count > 0)
                    {
                        if (EspecialistasNoDisponibles.Count > 1)
                        {
                            especialistas += "Los especialistas ";
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

                    //BENEFICIARIOS NO DISPONIBLES

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
                        if (dFachada.ItinerarioAgregar(unItinerario)) { CargarTodo(); }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo agregar la sesión al itinerario.')", true);
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Se requiere ingresar todos los datos de la sesión.')", true);
            }

        }

        protected void btnCargarEspecialidad_Click(object sender, EventArgs e)
        {
            CargarEspecialistas();
        }
    }
}