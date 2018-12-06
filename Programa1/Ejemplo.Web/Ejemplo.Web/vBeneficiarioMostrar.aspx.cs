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
            ActualizarTodosLosBeneficiarios();
            ActualizarGrdBeneficiarios();
        }
        
        private void ActualizarGrdBeneficiarios()
        {
            grdBeneficiarios.DataSource = TodosLosBeneficiarios;
            grdBeneficiarios.DataBind();
            grdBeneficiarios.SelectedIndex = -1;
        }
        private void ActualizarTodosLosBeneficiarios()
        {

            string Consulta = "SELECT DISTINCT B.* FROM Beneficiarios B LEFT JOIN Planes P ON B.BeneficiarioId = P.BeneficiarioId";
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
            /*
            if (cbJuanLacaze.Checked) { if (condiciones.Count > 1) condiciones.Add("WHERE Centro=1"); else; condiciones.Add("BeneficiarioEstado=1"); }                 //Juan Lacaze
            if (cbNuevaHelvecia.Checked) { Localidades.Add("Nueva Helvecia"); }     //Nueva Helvecia
            */  
            //SEXO
                if (cblSexo.Items[0].Selected!=cblSexo.Items[1].Selected)
                {
                    if(cblSexo.Items[0].Selected)
                    {
                        if (condiciones.Count > 1) condiciones.Add(" and BeneficiarioSexo='M'"); else; condiciones.Add(" BeneficiarioSexo='M'");
                    }
                    else
                    {
                        if (condiciones.Count > 1) condiciones.Add(" and BeneficiarioSexo='F'"); else; condiciones.Add(" BeneficiarioSexo='F'");
                    }
                }
            //Plan
            for (int i = 0; i < cblPlan.Items.Count; i++)
            {
                if (cblPlan.Items[i].Selected)
                {
                    if (condiciones.Count > 1)
                        if (or) condiciones.Add(string.Format(" or P.PlanTipo='{0}'", cblPlan.Items[i].Text));
                        else condiciones.Add(string.Format(" and P.PlanTipo='{0}'", cblPlan.Items[i].Text));
                    else condiciones.Add(string.Format(" P.PlanTipo='{0}'", cblPlan.Items[i].Text));
                    or = true;
                }
            }

            or = false;

            //RANGO DE EDAD
            if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
            {
                if (condiciones.Count > 1) condiciones.Add(string.Format(" and (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int)" +
                    " -cast(convert(varchar(8), BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios)" +
                    " BETWEEN {0} and {1}",
                    txtDesde.Text, txtHasta.Text));
                else condiciones.Add(string.Format(" (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int)" +
                    " -cast(convert(varchar(8), BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios)" +
                    " BETWEEN {0} and {1}",
                    txtDesde.Text, txtHasta.Text));
            }
            //ESPECIALIDAD
            /*
            List<string> Especialidades = new List<string>();
            for (int i = 0; i < cblEspecialidad.Items.Count; i++)
            {
                if (cblEspecialidad.Items[i].Selected)
                {
                    Especialidades.Add(cblEspecialidad.Items[i].Text);
                }
            }
            */
            //DIAGNOSTICO
            /*
            string diagnostico = ddlDiagnosticos.SelectedItem.Text;
            */



            // FILTRADO DE BENEFICIARIOSif(condiciones.Count>=1)


            //BUSCADOR
            if (condiciones.Count > 1) condiciones.Add(string.Format(" and (BeneficiarioNombres LIKE '%{0}%' or BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, BeneficiarioCI) LIKE '%{0}%' )", txtBuscarBeneficiarios.Text));
            else condiciones.Add(string.Format(" (BeneficiarioNombres LIKE '{0}%' or BeneficiarioApellidos LIKE '{0}%' or CONVERT(varchar, BeneficiarioCI) LIKE '{0}%' )", txtBuscarBeneficiarios.Text));

            if (condiciones.Count>1)
            {
                for(int i=0; i<condiciones.Count;i++)
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
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId="+TodosLosBeneficiarios[e.NewSelectedIndex].Codigo.ToString());
        }
    }
}