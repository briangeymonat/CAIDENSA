using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vEstadisticasCantidadSesionesPorTecnico : System.Web.UI.Page
    {

        List<string> LosAños;
        List<string> LosMeses = new List<string>() { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosAños = new List<string>();
                List<DateTime> lstFechas = dFachada.SesionTraerMaximaFechaYMinimaFecha();
                DateTime dFechaMaxima = lstFechas[0];
                DateTime dFechaMinima = lstFechas[1];
                for (int i = dFechaMaxima.Year; i >= dFechaMinima.Year; i--)
                {
                    LosAños.Add(i.ToString());
                }
                ddlAños.DataSource = LosAños;
                ddlAños.DataBind();
                ddlMeses.DataSource = LosMeses;
                ddlMeses.DataBind();
                ddlMeses.SelectedIndex=DateTime.Today.Month-1;
                CargarGrilla();
            }
        }
        protected void CargarGrilla()
        {
            string sConsulta = string.Format("Select distinct U.UsuarioNombres, U.UsuarioApellidos, " +
                "(Select Count(*) from Sesiones s1 join UsuariosSesiones us1 on us1.SesionId = s1.SesionId where us1.UsuarioId = u.UsuarioId and s1.SesionTipo = 0 and '{0}' = (select DATEPART(YEAR, s1.SesionFecha)) and '{1}' = (select DATEPART(month, s1.SesionFecha))) Individual, " +
                "(Select Count(*) from Sesiones s1 join UsuariosSesiones us1 on us1.SesionId = s1.SesionId where us1.UsuarioId = u.UsuarioId and s1.SesionTipo = 1 and '{0}' = (select DATEPART(YEAR, s1.SesionFecha)) and '{1}' = (select DATEPART(month, s1.SesionFecha))) Grupo2, " +
                "(Select Count(*) from Sesiones s1 join UsuariosSesiones us1 on us1.SesionId = s1.SesionId where us1.UsuarioId = u.UsuarioId and s1.SesionTipo = 2 and '{0}' = (select DATEPART(YEAR, s1.SesionFecha)) and '{1}' = (select DATEPART(month, s1.SesionFecha))) Grupo3, " +
                "(Select Count(*) from Sesiones s1 join UsuariosSesiones us1 on us1.SesionId = s1.SesionId where us1.UsuarioId = u.UsuarioId and s1.SesionTipo = 3 and '{0}' = (select DATEPART(YEAR, s1.SesionFecha)) and '{1}' = (select DATEPART(month, s1.SesionFecha))) Taller, " +
                "(Select Count(*) from Sesiones s1 join UsuariosSesiones us1 on us1.SesionId = s1.SesionId where us1.UsuarioId = u.UsuarioId and s1.SesionTipo = 4 and '{0}' = (select DATEPART(YEAR, s1.SesionFecha)) and '{1}' = (select DATEPART(month, s1.SesionFecha))) PROES " +
                "from Usuarios U join UsuariosSesiones US on u.UsuarioId = us.UsuarioId join Sesiones S on S.SesionId = us.SesionId " +
                " Where '{0}' = (select DATEPART(YEAR, s.SesionFecha)) and '{1}' = (select DATEPART(month, s.SesionFecha)) " +
                "and (u.UsuarioNombres LIKE '%{2}%' or u.UsuarioApellidos LIKE '%{2}%')",
                this.ddlAños.SelectedValue, this.ddlMeses.SelectedIndex + 1, this.txtBuscarEspecialista.Text);
            List<List<string>> lstUsuarios = dFachada.EstadisticaTraerCantidadSesionPorTipoSesion(sConsulta);

            DataTable dt = new DataTable();
            dt.Columns.Add("Especialista", typeof(string));
            dt.Columns.Add("Sesión Individual", typeof(string));
            dt.Columns.Add("Sesión Grupo de 2", typeof(string));
            dt.Columns.Add("Sesión Grupo de 3", typeof(string));
            dt.Columns.Add("Sesión Taller", typeof(string));
            dt.Columns.Add("Sesión PROES", typeof(string));

            DataRow row;
            for (int i = 0; i < lstUsuarios.Count; i++)
            {
                row = dt.NewRow();
                row["Especialista"] = lstUsuarios[i][0] + " " + lstUsuarios[i][1];
                row["Sesión Individual"] = lstUsuarios[i][2];
                row["Sesión Grupo de 2"] = lstUsuarios[i][3];
                row["Sesión Grupo de 3"] = lstUsuarios[i][4];
                row["Sesión Taller"] = lstUsuarios[i][5];
                row["Sesión PROES"] = lstUsuarios[i][6];
                dt.Rows.Add(row);
            }
            grdTecnicosCantidadSesion.DataSource = dt;
            grdTecnicosCantidadSesion.DataBind();

        }

        protected void txtBuscarEspecialista_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlAños_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}