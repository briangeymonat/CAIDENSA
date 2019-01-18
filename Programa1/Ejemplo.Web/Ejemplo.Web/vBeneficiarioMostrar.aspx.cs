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
    public partial class vBeneficiarioMostrar : System.Web.UI.Page
    {
        private static List<cBeneficiario> TodosLosBeneficiarios;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ActualizarTodo();
            }
        }

        private void ActualizarTodo()
        {
            CargarDiagnosticos();
            ActualizarTodosLosBeneficiarios();
            ActualizarGrdBeneficiarios();
        }
        private void CargarDiagnosticos()
        {
            List<cDiagnostico> LosDiagnosticos = dFachada.DiagnosticoTraerTodos();
            List<string> combo = new List<string>() { "Ninguno" };
            foreach (cDiagnostico unDiagnostico in LosDiagnosticos)
            {
                combo.Add(unDiagnostico.Tipo);
            }
            ddlDiagnosticos.DataSource = combo;
            ddlDiagnosticos.DataBind();
        }
        private void ActualizarGrdBeneficiarios()
        {
            grdBeneficiarios.DataSource = TodosLosBeneficiarios;
            grdBeneficiarios.DataBind();
            grdBeneficiarios.SelectedIndex = -1;
        }
        private void ActualizarTodosLosBeneficiarios()
        {

            string Consulta = "SELECT DISTINCT B.* FROM Beneficiarios B" +
                " JOIN Planes P ON B.BeneficiarioId = P.BeneficiarioId" +
                " JOIN BeneficiariosSesiones BS ON B.BeneficiarioId = BS.BeneficiarioId" +
                " JOIN Sesiones S ON BS.SesionId = S.SesionId" +
                " JOIN UsuariosSesiones US ON S.SesionId = US.SesionId" +
                " JOIN Usuarios U ON US.UsuarioId = U.UsuarioId" +
                " JOIN Especialidades E ON U.EspecialidadId = E.EspecialidadId";
            List<string> condiciones = new List<string>();
            condiciones.Add(" WHERE");

            bool or = false;

            // ACTIVOS O PASIVOS
            if ((cbActivos.Checked) != (cbPasivos.Checked))
            {
                // ACTIVO
                if (cbActivos.Checked) condiciones.Add(" BeneficiarioEstado=1");
                // PASIVO
                if (cbPasivos.Checked) condiciones.Add(" BeneficiarioEstado=0");
            }


            //LOCALIDAD
            if (cbJuanLacaze.Checked)                       //Juan Lacaze
            {
                if (condiciones.Count > 1) condiciones.Add(" and S.SesionCentro=0"); else condiciones.Add(" S.SesionCentro=0");
            }

            if (cbNuevaHelvecia.Checked)                    //Nueva Helvecia
            {
                if (condiciones.Count > 1) condiciones.Add(" and S.SesionCentro=1"); else condiciones.Add(" S.SesionCentro=1");
            }


            //SEXO
            if (cblSexo.Items[0].Selected != cblSexo.Items[1].Selected)
            {
                if (cblSexo.Items[0].Selected)
                {
                    if (condiciones.Count > 1) condiciones.Add(" and BeneficiarioSexo='M'"); else condiciones.Add(" BeneficiarioSexo='M'");
                }
                else
                {
                    if (condiciones.Count > 1) condiciones.Add(" and BeneficiarioSexo='F'"); else condiciones.Add(" BeneficiarioSexo='F'");
                }
            }


            //Plan
            for (int i = 0; i < cblPlan.Items.Count; i++)
            {
                if (cblPlan.Items[i].Selected)
                {
                    if (condiciones.Count > 1)
                        if (or) condiciones.Add(string.Format(" or P.PlanTipo='{0}'", cblPlan.Items[i].Text));
                        else condiciones.Add(string.Format(" and (P.PlanTipo='{0}'", cblPlan.Items[i].Text));
                    else condiciones.Add(string.Format(" (P.PlanTipo='{0}'", cblPlan.Items[i].Text));
                    or = true;
                }
            }
            if(or) condiciones.Add(")");


            or = false;
            

            //RANGO DE EDAD
            if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
            {
                if (condiciones.Count > 1) condiciones.Add(string.Format(" and (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int)" +
                    " -cast(convert(varchar(8), B1.BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios B1 WHERE B1.BeneficiarioId = B.BeneficiarioId)" +
                    " BETWEEN {0} and {1}",
                    txtDesde.Text, txtHasta.Text));
                else condiciones.Add(string.Format(" (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int)" +
                    " -cast(convert(varchar(8), B1.BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios B1 WHERE B1.BeneficiarioId = B.BeneficiarioId)" +
                    " BETWEEN {0} and {1}",
                    txtDesde.Text, txtHasta.Text));
            }
            //ESPECIALIDAD

            for (int i = 0; i < cblEspecialidad.Items.Count; i++)
            {
                if (cblEspecialidad.Items[i].Selected)
                {
                    if (condiciones.Count > 1)
                        if (or) condiciones.Add(string.Format(" or E.EspecialidadNombre = '{0}'", cblEspecialidad.Items[i].Text));
                        else condiciones.Add(string.Format(" and (E.EspecialidadNombre = '{0}'", cblEspecialidad.Items[i].Text));
                    else
                        condiciones.Add(string.Format(" (E.EspecialidadNombre = '{0}'", cblEspecialidad.Items[i].Text));
                    or = true;
                }
            }
            if (or) condiciones.Add(")");

            //DIAGNOSTICO

            if (ddlDiagnosticos.SelectedIndex != 0)
            {
                if (condiciones.Count > 1)
                    condiciones.Add(string.Format(" and B.BeneficiarioId in(SELECT DB.BeneficiarioId FROM DiagnosticosBeneficiarios DB  JOIN Diagnostico D ON DB.DiagnosticoId = D.DiagnosticoId WHERE D.DiagnosticoTipo = '{0}')", ddlDiagnosticos.SelectedValue));
                else
                    condiciones.Add(string.Format(" B.BeneficiarioId in(SELECT DB.BeneficiarioId FROM DiagnosticosBeneficiarios DB  JOIN Diagnostico D ON DB.DiagnosticoId = D.DiagnosticoId WHERE DB.DiagnosticosBeneficiariosFecha=(Select MAX(db1.DiagnosticosBeneficiariosFecha) from DiagnosticosBeneficiarios db1 where db1.BeneficiarioId=db.BeneficiarioId) and D.DiagnosticoTipo = '{0}')", ddlDiagnosticos.SelectedValue));
            }



            // FILTRADO DE BENEFICIARIOSif(condiciones.Count>=1)


            //BUSCADOR
            if (condiciones.Count > 1) condiciones.Add(string.Format(" and (BeneficiarioNombres LIKE '%{0}%' or BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, BeneficiarioCI) LIKE '%{0}%' )", txtBuscarBeneficiarios.Text));
            else condiciones.Add(string.Format(" (BeneficiarioNombres LIKE '%{0}%' or BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, BeneficiarioCI) LIKE '%{0}%' )", txtBuscarBeneficiarios.Text));

            if (condiciones.Count > 1)
            {
                for (int i = 0; i < condiciones.Count; i++)
                {
                    Consulta += condiciones[i];
                }
                TodosLosBeneficiarios = dFachada.BeneficiarioTraerTodosConFiltros(Consulta);
            }
            else
            {
                TodosLosBeneficiarios = dFachada.BeneficiarioTraerTodos();
            }
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            ActualizarTodosLosBeneficiarios();
            ActualizarGrdBeneficiarios();
        }

        protected void txtBuscarBeneficiarios_TextChanged(object sender, EventArgs e)
        {
            ActualizarTodosLosBeneficiarios();
            ActualizarGrdBeneficiarios();
        }

        protected void grdBeneficiarios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + TodosLosBeneficiarios[e.NewSelectedIndex].Codigo.ToString());
        }

        protected void grdBeneficiarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;//codigo
            e.Row.Cells[5].Visible = false;//sexo
            e.Row.Cells[6].Visible = false;//tel1
            e.Row.Cells[7].Visible = false;//tel2
            e.Row.Cells[8].Visible = false;//email
            e.Row.Cells[9].Visible = false;//domicilio
            e.Row.Cells[12].Visible = false;//Motivo consulta
            e.Row.Cells[13].Visible = false;//escolaridad
            e.Row.Cells[14].Visible = false;//derivador
            e.Row.Cells[15].Visible = false;//estado
        }
    }
}