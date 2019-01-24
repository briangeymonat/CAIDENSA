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
            List<List<int>> promedios = new List<List<int>>();
            for (int i = 0; i < lstDiagnosticos.Count; i++)
            {
                promedios.Add(new List<int>());
                lstBeneficiarios = dFachada.BeneficiarioTraerTodosPorDiagnostico(lstDiagnosticos[i]);
                for (int j = 0; j < lstBeneficiarios.Count; j++)
                {
                    string date1 = null;
                    string date2 = null;
                    for (int k = 0; k < lstBeneficiarios[j].lstDiagnosticos.Count; k++)
                    {
                        if (date1 == null)
                        {
                            if (lstBeneficiarios[j].lstDiagnosticos[k].Diagnostico.Codigo == lstDiagnosticos[i].Codigo)
                            {
                                date1 = lstBeneficiarios[j].lstDiagnosticos[k].Fecha;
                            }
                        }
                        else
                        {
                            if (lstBeneficiarios[j].lstDiagnosticos[k].Diagnostico.Codigo != lstDiagnosticos[i].Codigo &&
                                lstBeneficiarios[j].lstDiagnosticos[k - 1].Diagnostico.Codigo == lstDiagnosticos[i].Codigo)
                            {
                                if (date1 != lstBeneficiarios[j].lstDiagnosticos[k].Fecha)
                                {
                                    bool existe = false;
                                    for (int l = k + 1; l < lstBeneficiarios[j].lstDiagnosticos.Count; l++)
                                    {
                                        if (lstBeneficiarios[j].lstDiagnosticos[l].Diagnostico.Codigo == lstDiagnosticos[i].Codigo)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                        date2 = lstBeneficiarios[j].lstDiagnosticos[k].Fecha;
                                }
                            }
                        }


                    }

                    if (date1 != null && date2 != null)
                    {
                        DateTime fecha1 = DateTime.Parse(date1);
                        DateTime fecha2 = DateTime.Parse(date2);
                        int a = (fecha2 - fecha1).Days;
                        promedios[i].Add(a);
                        //promedios[i][j] = a;

                    }
                }
            }

            int promedio = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("Diagnosticos", typeof(string));
            dt.Columns.Add("Duración promedio del tratamiento (días)", typeof(string));
            DataRow row;
            for (int t = 0; t < lstDiagnosticos.Count; t++)
            {
                promedio = 0;
                if (promedios[t].Count > 0)
                {
                    for (int b = 0; b < promedios[t].Count; b++)
                    {
                        promedio += promedios[t][b];
                    }
                    promedio = (promedio / promedios[t].Count);

                    row = dt.NewRow();
                    row["Diagnosticos"] = lstDiagnosticos[t].Tipo;
                    row["Duración promedio del tratamiento (días)"] = promedio.ToString();
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
            cDiagnostico diagnostico = new cDiagnostico();
            diagnostico.Codigo = int.Parse(ddlDiagnosticos.SelectedValue);
            var resultado = dFachada.EstadisticaTraerCantidadParaCadaAñoPorDiagnostico(diagnostico);
            List<string> años = resultado.Item1;
            List<int> cantidad = resultado.Item2;
            DataTable dtReport = new DataTable();
            dtReport.Columns.Add("Años", typeof(string));
            dtReport.Columns.Add("Cantidad", typeof(int));

            for (int i=0; i<años.Count; i++)
            {
                dtReport.Rows.Add(años[i], cantidad[i]);
            }    
            return dtReport;
        }
        public void CargarGrafica1(DataTable dtReport)
        {
            string[] x = new string[dtReport.Rows.Count];
            double[] y = new double[dtReport.Rows.Count];
            for (int i = 0; i < dtReport.Rows.Count; i++)
            {
                x[i] = dtReport.Rows[i][0].ToString();
                y[i] = Convert.ToDouble(dtReport.Rows[i][1]);
            }
            Chart1.Series[0].Points.DataBindXY(x, y);

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
            int año = int.Parse(ddlAños.SelectedValue);
            var resultado = dFachada.EstadisticaTraerCantidadParaCadaDiagnosticoPorAño(año);
            List<cDiagnostico> diagnosticos = resultado.Item1;
            List<int> cantidad = resultado.Item2;
            List<cDiagnostico> lstTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();

            DataTable dtReport = new DataTable();
            dtReport.Columns.Add("Diagnosticos", typeof(string));
            dtReport.Columns.Add("Cantidad", typeof(int));

            for(int i=0; i<lstTodosDiagnosticos.Count; i++)
            {
                for (int j = 0; j < diagnosticos.Count; j++)
                {
                    if(lstTodosDiagnosticos[i].Codigo == diagnosticos[j].Codigo)
                    {
                        dtReport.Rows.Add(diagnosticos[j].Tipo, cantidad[j]);
                        break;
                    }
                    else
                    {
                        if ((j+1)==diagnosticos.Count)
                        dtReport.Rows.Add(lstTodosDiagnosticos[i].Tipo, 0);
                    }
                } 
            }
            return dtReport;
        }
        public void CargarGrafica2(DataTable dtReport)
        {
            string[] x = new string[dtReport.Rows.Count];
            double[] y = new double[dtReport.Rows.Count];
            for (int i = 0; i < dtReport.Rows.Count; i++)
            {
                x[i] = dtReport.Rows[i][0].ToString();
                y[i] = Convert.ToDouble(dtReport.Rows[i][1]);
            }
            Chart2.Series[0].Points.DataBindXY(x, y);

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