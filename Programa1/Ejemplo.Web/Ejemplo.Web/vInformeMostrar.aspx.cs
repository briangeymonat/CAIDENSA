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
        List<string> lstEnums;
        List<cDiagnostico> lstDiagnosticos;
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
            lstEnums = new List<string>() { "Todos" };
            foreach (var item in Enum.GetValues(typeof(cUtilidades.TipoInforme)))
            {
                lstEnums.Add(item.ToString());
            }
            for (int i = 0; i < lstEnums.Count; i++)
            {
                lstEnums[i] = lstEnums[i].Replace("_", " ");
            }

            ddlTipos.DataSource = lstEnums;
            ddlTipos.DataBind();
        }
        private void CargarDdlDiagnosticos()
        {
            lstDiagnosticos = dFachada.DiagnosticoTraerTodos();
            cDiagnostico todos = new cDiagnostico();
            todos.Codigo = 0;
            todos.Tipo = "Todos";
            lstDiagnosticos.Insert(0, todos);
            ddlDiagnosticos.DataSource = lstDiagnosticos;
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

                    string Consulta = "SELECT I.* FROM Informes I JOIN Beneficiarios B ON I.BeneficiarioId = B.BeneficiarioId";

                    List<string> condiciones = new List<string>() { " WHERE" };
                    condiciones.Add(" I.InformeFecha IS NOT NULL and I.InformeEstado = 2");
                    //Tipos
                    if (ddlTipos.SelectedIndex != 0)
                    {
                        condiciones.Add(string.Format(" and I.InformeTipo = {0}", ddlTipos.SelectedIndex - 1));
                    }

                    //Diagnosticos
                    if (ddlDiagnosticos.SelectedIndex != 0)
                    {
                        condiciones.Add(string.Format(" and I.InformeId in (SELECT S.InformeId FROM Secciones S WHERE S.SeccionNombre=2 and S.SeccionContenido LIKE '%{0}%')", ddlDiagnosticos.SelectedValue));
                    }


                    //Rango de edad
                    if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                    {
                            condiciones.Add(string.Format(" and (Select floor((cast(convert(varchar(8), GETDATE(), 112) as int)-cast(convert(varchar(8), B1.BeneficiarioFechaNacimiento, 112) as int)) / 10000) from Beneficiarios B1 WHERE B1.BeneficiarioId = B.BeneficiarioId)" +
                                " BETWEEN {0} and {1}", txtDesde.Text, txtHasta.Text));
                    }

                    //Fecha
                    if (txtFechaInicial.Text != string.Empty && txtFechaFinal.Text != string.Empty)
                    {
                            condiciones.Add(string.Format(" and I.InformeFecha BETWEEN '{0}' and '{1}'", txtFechaInicial.Text, txtFechaFinal.Text));
                    }


                    //BUSCADOR
                    condiciones.Add(string.Format(" and (B.BeneficiarioNombres LIKE '%{0}%' or B.BeneficiarioApellidos LIKE '%{0}%' or CONVERT(varchar, B.BeneficiarioCI) LIKE '%{0}%' )", txtBuscarInforme.Text));

                    //ORDENAR POR FECHA
                    condiciones.Add(" ORDER BY I.InformeFecha desc");

                    if (condiciones.Count > 1)
                    {
                        for (int i = 0; i < condiciones.Count; i++)
                        {
                            Consulta += condiciones[i];
                        }
                    }

                    List<cInforme> lstInformes = dFachada.InformeTraerTodosConFiltros(Consulta);
                    cInforme informe;

                    List<ListarInformes> ListaInformesParaListar = new List<ListarInformes>();
                    ListarInformes informeAListar;

                    for (int i = 0; i < lstInformes.Count; i++)
                    {
                        informe = new cInforme();
                        informe = lstInformes[i];
                        informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(informe.Beneficiario);
                        informeAListar = new ListarInformes();
                        informeAListar.Codigo = informe.Codigo;
                        informeAListar.Fecha = informe.Fecha;
                        informeAListar.Estado = informe.Estado;
                        informeAListar.Tipo = informe.Tipo;
                        informeAListar.CodigoBeneficiario = informe.Beneficiario.Codigo;
                        informeAListar.Nombres = informe.Beneficiario.Nombres;
                        informeAListar.Apellidos = informe.Beneficiario.Apellidos;
                        ListaInformesParaListar.Add(informeAListar);
                    }
                    grdInformes.DataSource = ListaInformesParaListar;
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
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vInformeDetalles.aspx?InformeId=" + codigo.ToString());

        }
    }
}