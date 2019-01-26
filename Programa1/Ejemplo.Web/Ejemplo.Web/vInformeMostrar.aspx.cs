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
    public partial class vInformeMostrar : System.Web.UI.Page
    {
        List<string> LosEnums;
        List<cDiagnostico> LosDiagnosticos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDdlDiagnosticos();
                CargarDdlTipos();
                CargarInformes();
            }
        }
        private void CargarDdlTipos()
        {
            LosEnums = new List<string>() { "Todos" };
            foreach (var item in Enum.GetValues(typeof(cUtilidades.TipoInforme)))
            {
                LosEnums.Add(item.ToString());
            }
            for (int i = 0; i < LosEnums.Count; i++)
            {
                LosEnums[i] = LosEnums[i].Replace("_", " ");
            }

            ddlTipos.DataSource = LosEnums;
            ddlTipos.DataBind();
        }
        private void CargarDdlDiagnosticos()
        {
            LosDiagnosticos = dFachada.DiagnosticoTraerTodos();
            cDiagnostico unDiagnostico = new cDiagnostico();
            unDiagnostico.Codigo = 0;
            unDiagnostico.Tipo = "Todos";
            LosDiagnosticos.Insert(0, unDiagnostico);
            ddlDiagnosticos.DataSource = LosDiagnosticos;
            ddlDiagnosticos.DataValueField = "Tipo";
            ddlDiagnosticos.DataTextField = "Tipo";
            ddlDiagnosticos.DataBind();
        }

        private void CargarInformes()
        {
            if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty &&
                int.Parse(txtDesde.Text) > int.Parse(txtHasta.Text))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La edad máxima debe ser mayor a la mínima')", true);

            }
            else
            {
                if (txtFechaInicial.Text != string.Empty && txtFechaFinal.Text != string.Empty &&
                DateTime.Parse(txtFechaInicial.Text) > DateTime.Parse(txtFechaFinal.Text))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La fecha Desde: debe ser menor a la fecha Hasta:')", true);
                }
                else
                {

                    string sConsulta = "SELECT I.* FROM Informes I JOIN Beneficiarios B ON I.BeneficiarioId = B.BeneficiarioId";

                    List<string> lstCondiciones = new List<string>() { " WHERE" };
                    lstCondiciones.Add(" I.InformeFecha IS NOT NULL and I.InformeEstado = 2");
                    //Tipos
                    if (ddlTipos.SelectedIndex != 0)
                    {
                        lstCondiciones.Add(string.Format(" and I.InformeTipo = {0}", ddlTipos.SelectedIndex - 1));
                    }

                    //Diagnosticos
                    if (ddlDiagnosticos.SelectedIndex != 0)
                    {
                        lstCondiciones.Add(string.Format(" and I.InformeId in (SELECT S.InformeId FROM Secciones S WHERE S.SeccionNombre=2 and S.SeccionContenido LIKE '%{0}%')", ddlDiagnosticos.SelectedValue));
                    }


                    //Rango de edad
                    if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                    {
                            lstCondiciones.Add(string.Format(" and (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int)-cast(convert(varchar(8), B1.BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios B1 WHERE B1.BeneficiarioId = B.BeneficiarioId)" +
                                " BETWEEN {0} and {1}", txtDesde.Text, txtHasta.Text));
                    }

                    //Fecha
                    if (txtFechaInicial.Text != string.Empty && txtFechaFinal.Text != string.Empty)
                    {
                            lstCondiciones.Add(string.Format(" and I.InformeFecha BETWEEN '{0}' and '{1}'", txtFechaInicial.Text, txtFechaFinal.Text));
                    }


                    //BUSCADOR
                    lstCondiciones.Add(string.Format(" and (B.BeneficiarioNombres LIKE '%{0}%' or B.BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, B.BeneficiarioCI) LIKE '%{0}%' )", txtBuscarInforme.Text));

                    //ORDENAR POR FECHA
                    lstCondiciones.Add(" ORDER BY I.InformeFecha desc");

                    if (lstCondiciones.Count > 1)
                    {
                        for (int i = 0; i < lstCondiciones.Count; i++)
                        {
                            sConsulta += lstCondiciones[i];
                        }
                    }

                    List<cInforme> lstInformes = dFachada.InformeTraerTodosConFiltros(sConsulta);
                    cInforme unInforme;

                    List<ListarInformes> lstListaInformesParaListar = new List<ListarInformes>();
                    ListarInformes unInformeAListar;

                    for (int i = 0; i < lstInformes.Count; i++)
                    {
                        unInforme = new cInforme();
                        unInforme = lstInformes[i];
                        unInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(unInforme.Beneficiario);
                        unInformeAListar = new ListarInformes();
                        unInformeAListar.Codigo = unInforme.Codigo;
                        unInformeAListar.Fecha = unInforme.Fecha;
                        unInformeAListar.Estado = unInforme.Estado;
                        unInformeAListar.Tipo = unInforme.Tipo;
                        unInformeAListar.CodigoBeneficiario = unInforme.Beneficiario.Codigo;
                        unInformeAListar.Nombres = unInforme.Beneficiario.Nombres;
                        unInformeAListar.Apellidos = unInforme.Beneficiario.Apellidos;
                        lstListaInformesParaListar.Add(unInformeAListar);
                    }
                    grdInformes.DataSource = lstListaInformesParaListar;
                    grdInformes.DataBind();
                }


            }

        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            CargarInformes();
        }

        protected void grdInformes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo informe
            e.Row.Cells[5].Visible = false;//codigo beneficiario
        }

        protected void grdInformes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //PODRIA APARECER UNA VENTANITA PARA EXPORTAR A PDF EL INFORME
            TableCell celdaCodigo = grdInformes.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeDetalles.aspx?InformeId=" + iCodigo.ToString());

        }
    }
}