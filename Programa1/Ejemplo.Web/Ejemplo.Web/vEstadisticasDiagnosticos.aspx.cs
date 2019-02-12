using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vEstadisticasDiagnosticos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombos();
                CargarGrilla();
                CargarGrafica1(GetData1());
                CargarGrafica2(GetData2());
            }
        }
        protected void CargarGrilla()
        {
            List<cDiagnostico> lstDiagnosticos = dFachada.DiagnosticoTraerTodos();
            List<cBeneficiario> lstBeneficiarios;
            List<List<int>> lstPromedios = new List<List<int>>();
            for (int i = 0; i < lstDiagnosticos.Count; i++)
            {
                lstPromedios.Add(new List<int>());
                lstBeneficiarios = dFachada.BeneficiarioTraerTodosPorDiagnostico(lstDiagnosticos[i]);
                for (int j = 0; j < lstBeneficiarios.Count; j++)
                {
                    string sDate1 = null;
                    string sDate2 = null;
                    for (int k = 0; k < lstBeneficiarios[j].lstDiagnosticos.Count; k++)
                    {
                        if (sDate1 == null)
                        {
                            if (lstBeneficiarios[j].lstDiagnosticos[k].Diagnostico.Codigo == lstDiagnosticos[i].Codigo)
                            {
                                sDate1 = lstBeneficiarios[j].lstDiagnosticos[k].Fecha;
                            }
                        }
                        else
                        {
                            if (lstBeneficiarios[j].lstDiagnosticos[k].Diagnostico.Codigo != lstDiagnosticos[i].Codigo &&
                                lstBeneficiarios[j].lstDiagnosticos[k - 1].Diagnostico.Codigo == lstDiagnosticos[i].Codigo)
                            {
                                if (sDate1 != lstBeneficiarios[j].lstDiagnosticos[k].Fecha)
                                {
                                    bool bExiste = false;
                                    for (int l = k + 1; l < lstBeneficiarios[j].lstDiagnosticos.Count; l++)
                                    {
                                        if (lstBeneficiarios[j].lstDiagnosticos[l].Diagnostico.Codigo == lstDiagnosticos[i].Codigo)
                                        {
                                            bExiste = true;
                                        }
                                    }
                                    if (!bExiste)
                                        sDate2 = lstBeneficiarios[j].lstDiagnosticos[k].Fecha;
                                }
                            }
                        }


                    }

                    if (sDate1 != null && sDate2 != null)
                    {
                        DateTime dFecha1 = DateTime.Parse(sDate1);
                        DateTime dFecha2 = DateTime.Parse(sDate2);
                        int iDiferencia = (dFecha2 - dFecha1).Days;
                        lstPromedios[i].Add(iDiferencia);

                    }
                }
            }

            int iPromedio = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("Diagnosticos", typeof(string));
            dt.Columns.Add("Duración promedio del tratamiento (días)", typeof(string));
            DataRow row;
            for (int t = 0; t < lstDiagnosticos.Count; t++)
            {
                iPromedio = 0;
                if (lstPromedios[t].Count > 0)
                {
                    for (int b = 0; b < lstPromedios[t].Count; b++)
                    {
                        iPromedio += lstPromedios[t][b];
                    }
                    iPromedio = (iPromedio / lstPromedios[t].Count);

                    row = dt.NewRow();
                    row["Diagnosticos"] = lstDiagnosticos[t].Tipo;
                    row["Duración promedio del tratamiento (días)"] = iPromedio.ToString();
                    dt.Rows.Add(row);
                }

            }
            grdDiagnosticoDuracion.DataSource = dt;
            grdDiagnosticoDuracion.DataBind();




        }

        protected void CargarCombos()
        {
            ddlDiagnosticos.DataSource = dFachada.DiagnosticoTraerTodos();
            ddlDiagnosticos.DataValueField = "Codigo";
            ddlDiagnosticos.DataTextField = "Tipo";
            ddlDiagnosticos.DataBind();

            ddlAños.DataSource = dFachada.DiagnosticoTraerTodosAñosQueHayDiagnosticos();
            ddlAños.DataBind();

        }
        protected DataTable GetData1()
        {
            DataTable dtReport = new DataTable();
            cDiagnostico unDiagnostico = new cDiagnostico();
            if (ddlDiagnosticos.SelectedValue != "")
            {
                unDiagnostico.Codigo = int.Parse(ddlDiagnosticos.SelectedValue);
                var vResultado = dFachada.EstadisticaTraerCantidadParaCadaAñoPorDiagnostico(unDiagnostico);
                List<string> lstAños = vResultado.Item1;
                List<int> lstCantidad = vResultado.Item2;
                dtReport.Columns.Add("Años", typeof(string));
                dtReport.Columns.Add("Cantidad", typeof(int));

                for (int i = 0; i < lstAños.Count; i++)
                {
                    dtReport.Rows.Add(lstAños[i], lstCantidad[i]);
                }
            }
            return dtReport;
        }
        public void CargarGrafica1(DataTable dtReport)
        {
            string[] aX = new string[dtReport.Rows.Count];
            double[] aY = new double[dtReport.Rows.Count];
            for (int i = 0; i < dtReport.Rows.Count; i++)
            {
                aX[i] = dtReport.Rows[i][0].ToString();
                aY[i] = Convert.ToDouble(dtReport.Rows[i][1]);
            }
            Chart1.Series[0].Points.DataBindXY(aX, aY);

            Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;

            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -50;
            Chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Cantidad de beneficiarios";
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Años";
            Chart1.Width = 1500;
            Chart1.Height = 400;
        }

        protected DataTable GetData2()
        {
            DataTable dtReport = new DataTable();
            if (ddlAños.SelectedValue != "")
            {
                int iAño = int.Parse(ddlAños.SelectedValue);
                var vResultado = dFachada.EstadisticaTraerCantidadParaCadaDiagnosticoPorAño(iAño);
                List<cDiagnostico> lstDiagnosticos = vResultado.Item1;
                List<int> lstCantidad = vResultado.Item2;
                List<cDiagnostico> lstTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();


                dtReport.Columns.Add("Diagnosticos", typeof(string));
                dtReport.Columns.Add("Cantidad", typeof(int));

                for (int i = 0; i < lstTodosDiagnosticos.Count; i++)
                {
                    for (int j = 0; j < lstDiagnosticos.Count; j++)
                    {
                        if (lstTodosDiagnosticos[i].Codigo == lstDiagnosticos[j].Codigo)
                        {
                            dtReport.Rows.Add(lstDiagnosticos[j].Tipo, lstCantidad[j]);
                            break;
                        }
                        else
                        {
                            if ((j + 1) == lstDiagnosticos.Count)
                                dtReport.Rows.Add(lstTodosDiagnosticos[i].Tipo, 0);
                        }
                    }
                }
            }
            return dtReport;
        }
        public void CargarGrafica2(DataTable dtReport)
        {
            string[] aX = new string[dtReport.Rows.Count];
            double[] aY = new double[dtReport.Rows.Count];
            for (int i = 0; i < dtReport.Rows.Count; i++)
            {
                aX[i] = dtReport.Rows[i][0].ToString();
                aY[i] = Convert.ToDouble(dtReport.Rows[i][1]);
            }
            Chart2.Series[0].Points.DataBindXY(aX, aY);

            Chart2.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;

            Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -50;
            Chart2.ChartAreas["ChartArea1"].AxisX.TitleFont = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            Chart2.ChartAreas["ChartArea1"].AxisY.TitleFont = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            Chart2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
            Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
            Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
            Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
            Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            Chart2.Series[0].IsValueShownAsLabel = true;
            Chart2.ChartAreas["ChartArea1"].AxisY.Title = "Cantidad de beneficiarios";
            Chart2.ChartAreas["ChartArea1"].AxisX.Title = "Diagnósticos";
            Chart2.Width = 1500;
            Chart2.Height = 400;
        }




        protected void ddlDiagnosticos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrafica1(GetData1());
            CargarGrafica2(GetData2());
        }

        protected void ddlAños_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrafica1(GetData1());
            CargarGrafica2(GetData2());
        }
    }
}