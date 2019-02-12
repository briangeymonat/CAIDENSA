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
                ddlMeses.SelectedIndex = DateTime.Today.Month - 1;
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
                string sConsulta = "Select distinct b.* from Beneficiarios b join " +
               "BeneficiariosSesiones bs on b.BeneficiarioId = bs.BeneficiarioId join " +
                "Sesiones S on S.SesionId = bs.SesionId join " +
                "Planes p on p.BeneficiarioId = b.BeneficiarioId join " +
                "UsuariosSesiones us on us.SesionId = s.SesionId join " +
                "Usuarios u on u.UsuarioId = us.UsuarioId join " +
                "Especialidades E on E.EspecialidadId = u.EspecialidadId";
                List<string> lstCondiciones = new List<string>();
                lstCondiciones.Add(" WHERE");
                lstCondiciones.Add(string.Format(" '{0}' = (select DATEPART(YEAR, s.SesionFecha)) and '{1}' = (select DATEPART(month, s.SesionFecha)) ", ddlAños.SelectedValue, ddlMeses.SelectedIndex + 1));
                bool bOr = false;

                //Localidad
                if (cbJuanLacaze.Checked)
                {
                    lstCondiciones.Add("  and s.SesionCentro = 0");
                }
                if (cbNuevaHelvecia.Checked)
                {
                    lstCondiciones.Add("  and s.SesionCentro = 1");
                }

                //Barra busqueda
                lstCondiciones.Add(string.Format(" and (b.BeneficiarioNombres LIKE '%{0}%' or b.BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, b.BeneficiarioCI) LIKE '%{0}%')", txtBuscarBeneficiarios.Text));

                //Tipo Plan
                #region Tipo Plan
                if (cbASSE.Checked)
                {
                    lstCondiciones.Add(" and (p.PlanTipo='ASSE'");
                    bOr = true;
                }
                if (cbAYEX.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or p.PlanTipo='AYEX'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (p.PlanTipo='AYEX'");
                        bOr = true;
                    }
                }
                if (cbCAMEC.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or p.PlanTipo='CAMEC'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (p.PlanTipo='CAMEC'");
                        bOr = true;
                    }
                }
                if (cbCirculocatolico.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or p.PlanTipo='Círculo Católico'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (p.PlanTipo='Círculo Católico'");
                        bOr = true;
                    }
                }
                if (cbMIDES.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or p.PlanTipo='MIDES'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (p.PlanTipo='MIDES'");
                        bOr = true;
                    }
                }
                if (cbParticular.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or p.PlanTipo='Particular'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (p.PlanTipo='Particular'");
                        bOr = true;
                    }
                }
                if (cbPolicial.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or p.PlanTipo='Policial'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (p.PlanTipo='Policial'");
                        bOr = true;
                    }
                }
                if (bOr) lstCondiciones.Add(")");
                bOr = false;
                #endregion

                //Edad
                if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                {
                    lstCondiciones.Add(string.Format(" and (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int) - cast(convert(varchar(8), B1.BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios B1 WHERE B1.BeneficiarioId = B.BeneficiarioId) BETWEEN {0} and {1}", txtDesde.Text, txtHasta.Text));
                }

                //Especialidad que asisten
                #region Especialidad que asisten
                if (cbFisioterapeuta.Checked)
                {
                    lstCondiciones.Add(" and (E.EspecialidadNombre='Fisioterapia'");
                    bOr = true;
                }
                if (cbFonoaudiologo.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or E.EspecialidadNombre='Fonoaudiologia'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (E.EspecialidadNombre='Fonoaudiologia'");
                        bOr = true;
                    }
                }
                if (cbPedagogo.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or E.EspecialidadNombre='Pedadogia'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (E.EspecialidadNombre='Pedadogia'");
                        bOr = true;
                    }
                }
                if (cbPsicologo.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or E.EspecialidadNombre='Psicologia'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (E.EspecialidadNombre='Psicologia'");
                        bOr = true;
                    }
                }
                if (cbPsicomotricista.Checked)
                {
                    if (bOr)
                    {
                        lstCondiciones.Add(" or E.EspecialidadNombre='Psicomotricidad'");
                    }
                    else
                    {
                        lstCondiciones.Add(" and (E.EspecialidadNombre='Psicomotricidad'");
                        bOr = true;
                    }
                }
                if (bOr)
                {
                    lstCondiciones.Add(")");
                }

                bOr = false;
                #endregion

                for (int i = 0; i < lstCondiciones.Count; i++)
                {
                    sConsulta += lstCondiciones[i];
                }
                List<cBeneficiario> lstBeneficiarios = dFachada.BeneficiarioTraerTodosConFiltros(sConsulta);
                List<List<string>> lstAsistencias = new List<List<string>>();
                for (int i = 0; i < lstBeneficiarios.Count; i++)
                {
                    lstAsistencias.Add(new List<string>());
                }
                for (int i = 0; i < lstBeneficiarios.Count; i++)
                {
                    lstAsistencias[i] = dFachada.SesionTraerAsistenciasDeBeneficiarioPorMes(lstBeneficiarios[i], ddlAños.SelectedValue, (ddlMeses.SelectedIndex + 1).ToString());
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombres", typeof(string));
                dt.Columns.Add("Apellidos", typeof(string));
                dt.Columns.Add("Cédula de identidad", typeof(string));
                int iMaximo = DateTime.DaysInMonth(int.Parse(ddlAños.SelectedValue), ddlMeses.SelectedIndex + 1);
                for (int i = 1; i <= iMaximo; i++)
                {
                    DateTime dDia = new DateTime();
                    dDia = dDia.AddYears(int.Parse(ddlAños.SelectedValue) - 1);
                    dDia = dDia.AddMonths(ddlMeses.SelectedIndex);
                    dDia = dDia.AddDays(i - 1);
                    string sElDia;
                    switch (dDia.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            sElDia = string.Format("Lun \n\r {0}", dDia.ToString("dd"));
                            break;
                        case DayOfWeek.Tuesday:
                            sElDia = string.Format("Mar {0}", dDia.ToString("dd"));
                            break;
                        case DayOfWeek.Wednesday:
                            sElDia = string.Format("Mie {0}", dDia.ToString("dd"));
                            break;
                        case DayOfWeek.Thursday:
                            sElDia = string.Format("Jue {0}", dDia.ToString("dd"));
                            break;
                        case DayOfWeek.Friday:
                            sElDia = string.Format("Vie {0}", dDia.ToString("dd"));
                            break;
                        case DayOfWeek.Saturday:
                            sElDia = string.Format("Sab {0}", dDia.ToString("dd"));
                            break;
                        default:
                            sElDia = string.Format("Dom {0}", dDia.ToString("dd"));
                            break;
                    }

                    dt.Columns.Add(sElDia /*+ i.ToString(), typeof(string)*/);
                }
                DataRow row;
                for (int i = 0; i < lstBeneficiarios.Count; i++)
                {
                    row = dt.NewRow();
                    row["Nombres"] = lstBeneficiarios[i].Nombres;
                    row["Apellidos"] = lstBeneficiarios[i].Apellidos;
                    row["Cédula de identidad"] = lstBeneficiarios[i].CI;
                    for (int j = 0; j < lstAsistencias[i].Count; j++)
                    {
                        DateTime dDia = new DateTime();
                        dDia = dDia.AddYears(int.Parse(ddlAños.SelectedValue) - 1);
                        dDia = dDia.AddMonths(ddlMeses.SelectedIndex);
                        dDia = dDia.AddDays(j);
                        string sElDia;
                        switch (dDia.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                sElDia = string.Format("Lun \n\r {0}", dDia.ToString("dd"));
                                break;
                            case DayOfWeek.Tuesday:
                                sElDia = string.Format("Mar {0}", dDia.ToString("dd"));
                                break;
                            case DayOfWeek.Wednesday:
                                sElDia = string.Format("Mie {0}", dDia.ToString("dd"));
                                break;
                            case DayOfWeek.Thursday:
                                sElDia = string.Format("Jue {0}", dDia.ToString("dd"));
                                break;
                            case DayOfWeek.Friday:
                                sElDia = string.Format("Vie {0}", dDia.ToString("dd"));
                                break;
                            case DayOfWeek.Saturday:
                                sElDia = string.Format("Sab {0}", dDia.ToString("dd"));
                                break;
                            default:
                                sElDia = string.Format("Dom {0}", dDia.ToString("dd"));
                                break;
                        }
                        row[sElDia] = lstAsistencias[i][j];
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