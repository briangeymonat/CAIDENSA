using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vAsistencias : System.Web.UI.Page
    {
        List<string> años;
        List<string> meses = new List<string>() { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                años = new List<string>();
                List<DateTime> fechas = dFachada.SesionTraerMaximaFechaYMinimaFecha();
                DateTime fechaMaxima = fechas[0];
                DateTime fechaMinima = fechas[1];
                for (int i = fechaMaxima.Year; i >= fechaMinima.Year; i--)
                {
                    años.Add(i.ToString());
                }
                ddlAños.DataSource = años;
                ddlAños.DataBind();
                ddlMeses.DataSource = meses;
                ddlMeses.DataBind();
                CargarAsistencias();
            }
        }

        protected void CargarAsistencias()
        {
            if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty &&
                int.Parse(txtDesde.Text) > int.Parse(txtHasta.Text))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La edad máxima debe ser mayor a la mínima')", true);
            }
            else
            {
                string Consulta = "Select distinct b.* from Beneficiarios b join " +
               "BeneficiariosSesiones bs on b.BeneficiarioId = bs.BeneficiarioId join " +
                "Sesiones S on S.SesionId = bs.SesionId join " +
                "Planes p on p.BeneficiarioId = b.BeneficiarioId join " +
                "UsuariosSesiones us on us.SesionId = s.SesionId join " +
                "Usuarios u on u.UsuarioId = us.UsuarioId join " +
                "Especialidades E on E.EspecialidadId = u.EspecialidadId";
                List<string> condiciones = new List<string>();
                condiciones.Add(" WHERE");
                condiciones.Add(string.Format(" '{0}' = (select DATEPART(YEAR, s.SesionFecha)) and '{1}' = (select DATEPART(month, s.SesionFecha)) ", ddlAños.SelectedValue, ddlMeses.SelectedIndex + 1));
                bool or = false;

                //Localidad
                if (cbJuanLacaze.Checked)
                {
                    condiciones.Add("  and s.SesionCentro = 0");
                }
                if (cbNuevaHelvecia.Checked)
                {
                    condiciones.Add("  and s.SesionCentro = 1");
                }

                //Barra busqueda
                condiciones.Add(string.Format(" and (b.BeneficiarioNombres LIKE '%{0}%' or b.BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, b.BeneficiarioCI) LIKE '%{0}%')", txtBuscarBeneficiarios.Text));

                //Tipo Plan
                #region Tipo Plan
                if (cbASSE.Checked)
                {
                    condiciones.Add(" and (p.PlanTipo='ASSE'");
                    or = true;
                }
                if (cbAYEX.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or p.PlanTipo='AYEX'");
                    }
                    else
                    {
                        condiciones.Add(" and (p.PlanTipo='AYEX'");
                        or = true;
                    }
                }
                if (cbCAMEC.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or p.PlanTipo='CAMEC'");
                    }
                    else
                    {
                        condiciones.Add(" and (p.PlanTipo='CAMEC'");
                        or = true;
                    }
                }
                if (cbCirculocatolico.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or p.PlanTipo='Círculo Católico'");
                    }
                    else
                    {
                        condiciones.Add(" and (p.PlanTipo='Círculo Católico'");
                        or = true;
                    }
                }
                if (cbMIDES.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or p.PlanTipo='MIDES'");
                    }
                    else
                    {
                        condiciones.Add(" and (p.PlanTipo='MIDES'");
                        or = true;
                    }
                }
                if (cbParticular.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or p.PlanTipo='Particular'");
                    }
                    else
                    {
                        condiciones.Add(" and (p.PlanTipo='Particular'");
                        or = true;
                    }
                }
                if (cbPolicial.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or p.PlanTipo='Policial'");
                    }
                    else
                    {
                        condiciones.Add(" and (p.PlanTipo='Policial'");
                        or = true;
                    }
                }
                if (or) condiciones.Add(")");
                or = false;
                #endregion

                //Edad
                if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                {
                    condiciones.Add(string.Format(" and (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int) - cast(convert(varchar(8), B1.BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios B1 WHERE B1.BeneficiarioId = B.BeneficiarioId) BETWEEN {0} and {1}", txtDesde.Text, txtHasta.Text));
                }

                //Especialidad que asisten
                #region Especialidad que asisten
                if (cbFisioterapeuta.Checked)
                {
                    condiciones.Add(" and (E.EspecialidadNombre='Fisioterapia'");
                    or = true;
                }
                if (cbFonoaudiologo.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or E.EspecialidadNombre='Fonoaudiologia'");
                    }
                    else
                    {
                        condiciones.Add(" and (E.EspecialidadNombre='Fonoaudiologia'");
                        or = true;
                    }
                }
                if (cbPedagogo.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or E.EspecialidadNombre='Pedadogia'");
                    }
                    else
                    {
                        condiciones.Add(" and (E.EspecialidadNombre='Pedadogia'");
                        or = true;
                    }
                }
                if (cbPsicologo.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or E.EspecialidadNombre='Psicologia'");
                    }
                    else
                    {
                        condiciones.Add(" and (E.EspecialidadNombre='Psicologia'");
                        or = true;
                    }
                }
                if (cbPsicomotricista.Checked)
                {
                    if (or)
                    {
                        condiciones.Add(" or E.EspecialidadNombre='Psicomotricidad'");
                    }
                    else
                    {
                        condiciones.Add(" and (E.EspecialidadNombre='Psicomotricidad'");
                        or = true;
                    }
                }
                if (or)
                {
                    condiciones.Add(")");
                }

                or = false;
                #endregion

                for (int i = 0; i < condiciones.Count; i++)
                {
                    Consulta += condiciones[i];
                }
                List<cBeneficiario> LosBeneficiarios = dFachada.BeneficiarioTraerTodosConFiltros(Consulta);
                List<List<string>> Asistencias = new List<List<string>>();
                for (int i = 0; i < LosBeneficiarios.Count; i++)
                {
                    Asistencias.Add(new List<string>());
                }
                for (int i = 0; i < LosBeneficiarios.Count; i++)
                {
                    Asistencias[i] = dFachada.SesionTraerAsistenciasDeBeneficiarioPorMes(LosBeneficiarios[i], ddlAños.SelectedValue, (ddlMeses.SelectedIndex + 1).ToString());
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombres", typeof(string));
                dt.Columns.Add("Apellidos", typeof(string));
                dt.Columns.Add("Cédula de identidad", typeof(string));
                int maximo = DateTime.DaysInMonth(int.Parse(ddlAños.SelectedValue), ddlMeses.SelectedIndex + 1);
                for (int i = 1; i <= maximo; i++)
                {
                    DateTime dia = new DateTime();
                    dia = dia.AddYears(int.Parse(ddlAños.SelectedValue) - 1);
                    dia = dia.AddMonths(ddlMeses.SelectedIndex);
                    dia = dia.AddDays(i - 1);
                    string elDia;
                    switch (dia.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            elDia = string.Format("Lun \n\r {0}", dia.ToString("dd"));
                            break;
                        case DayOfWeek.Tuesday:
                            elDia = string.Format("Mar {0}", dia.ToString("dd"));
                            break;
                        case DayOfWeek.Wednesday:
                            elDia = string.Format("Mie {0}", dia.ToString("dd"));
                            break;
                        case DayOfWeek.Thursday:
                            elDia = string.Format("Jue {0}", dia.ToString("dd"));
                            break;
                        case DayOfWeek.Friday:
                            elDia = string.Format("Vie {0}", dia.ToString("dd"));
                            break;
                        case DayOfWeek.Saturday:
                            elDia = string.Format("Sab {0}", dia.ToString("dd"));
                            break;
                        default:
                            elDia = string.Format("Dom {0}", dia.ToString("dd"));
                            break;
                    }

                    dt.Columns.Add(elDia /*+ i.ToString(), typeof(string)*/);
                }
                DataRow row;
                for (int i = 0; i < LosBeneficiarios.Count; i++)
                {
                    row = dt.NewRow();
                    row["Nombres"] = LosBeneficiarios[i].Nombres;
                    row["Apellidos"] = LosBeneficiarios[i].Apellidos;
                    row["Cédula de identidad"] = LosBeneficiarios[i].CI;
                    for (int j = 0; j < Asistencias[i].Count; j++)
                    {
                        DateTime dia = new DateTime();
                        dia = dia.AddYears(int.Parse(ddlAños.SelectedValue) - 1);
                        dia = dia.AddMonths(ddlMeses.SelectedIndex);
                        dia = dia.AddDays(j);
                        string elDia;
                        switch (dia.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                elDia = string.Format("Lun \n\r {0}", dia.ToString("dd"));
                                break;
                            case DayOfWeek.Tuesday:
                                elDia = string.Format("Mar {0}", dia.ToString("dd"));
                                break;
                            case DayOfWeek.Wednesday:
                                elDia = string.Format("Mie {0}", dia.ToString("dd"));
                                break;
                            case DayOfWeek.Thursday:
                                elDia = string.Format("Jue {0}", dia.ToString("dd"));
                                break;
                            case DayOfWeek.Friday:
                                elDia = string.Format("Vie {0}", dia.ToString("dd"));
                                break;
                            case DayOfWeek.Saturday:
                                elDia = string.Format("Sab {0}", dia.ToString("dd"));
                                break;
                            default:
                                elDia = string.Format("Dom {0}", dia.ToString("dd"));
                                break;
                        }
                        row[elDia /*+ (j+1).ToString()*/] = Asistencias[i][j];
                    }
                    dt.Rows.Add(row);
                }

                grdAsistencias.DataSource = dt;
                grdAsistencias.DataBind();

            }





        }

        protected void ddlAños_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAsistencias();
        }

        protected void ddlMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAsistencias();
        }

        protected void txtBuscarBeneficiarios_TextChanged(object sender, EventArgs e)
        {
            CargarAsistencias();
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            CargarAsistencias();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cbJuanLacaze.Checked = false;
            cbNuevaHelvecia.Checked = false;
            cbASSE.Checked = false;
            cbAYEX.Checked = false;
            cbCAMEC.Checked = false;
            cbCirculocatolico.Checked = false;
            cbMIDES.Checked = false;
            cbParticular.Checked = false;
            cbPolicial.Checked = false;
            cbFonoaudiologo.Checked = false;
            cbPsicomotricista.Checked = false;
            cbFisioterapeuta.Checked = false;
            cbPedagogo.Checked = false;
            cbPsicologo.Checked = false;
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            txtBuscarBeneficiarios.Text = string.Empty;
            CargarAsistencias();
        }
    }
}