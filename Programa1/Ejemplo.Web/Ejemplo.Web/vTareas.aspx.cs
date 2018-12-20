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
    public partial class vTareasEspecialistas : System.Web.UI.Page
    {
        List<cBeneficiario> BeneficiariosConPlanesPorVencerse;
        List<cBeneficiario> BeneficiariosConPlanesSinFechaDeVencimiento;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BeneficiariosConPlanesPorVencerse = new List<cBeneficiario>();
                BeneficiariosConPlanesSinFechaDeVencimiento = new List<cBeneficiario>();
                CargarGrillas();
            }
        }

        protected void btnDetallesSesion_Click(object sender, EventArgs e)
        {
            string vtn = "window.open('vDetallesSesionParaAsistencia.aspx','Detalles de sesion','scrollbars=yes,resizable=yes','height=300', 'width=300')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }
        protected void CargarGrillas()
        {
            CargarGrillaEspecialistasConInformesPendientes();
            CargarGrillaPlanesPorVencerse();
            CargarGrillaPlanesSinFechaVencimiento();
            CargarGrillaSesionesDelDia();
        }
        protected void CargarGrillaPlanesPorVencerse()
        {
            DateTime fechaActual = DateTime.Now;

            List<cBeneficiario> listaBeneficiarios = new List<cBeneficiario>();
            List<cBeneficiario> listaBenConPlanes = new List<cBeneficiario>();
            List<cBeneficiario> BeneficiariosConPlanesAVencerse = new List<cBeneficiario>();
            listaBeneficiarios = dFachada.BeneficiarioTraerTodos();
            cBeneficiario beneficiario;
            for (int i = 0; i < listaBeneficiarios.Count; i++)
            {
                beneficiario = new cBeneficiario();
                beneficiario = listaBeneficiarios[i];
                beneficiario.lstPlanes = new List<cPlan>();
                beneficiario.lstPlanes = dFachada.PlanTraerActivosPorBeneficiario(beneficiario);
                listaBenConPlanes.Add(beneficiario);
            }
            for (int a = 0; a < listaBenConPlanes.Count; a++)
            {
                for (int b = 0; b < listaBenConPlanes[a].lstPlanes.Count; b++)
                {
                    if (listaBenConPlanes[a].lstPlanes[b].FechaFin != null)
                    {
                        DateTime fecha = new DateTime();
                        fecha = DateTime.Parse(listaBenConPlanes[a].lstPlanes[b].FechaFin);
                        TimeSpan d = fecha - fechaActual;
                        Double td = d.TotalDays;
                        if (td < 185)
                        {
                            BeneficiariosConPlanesAVencerse.Add(listaBenConPlanes[a]);
                            break;
                            //si tiene varios planes se lista solo una vez el beneficiario
                        }
                    }
                }
            }
            BeneficiariosConPlanesPorVencerse = BeneficiariosConPlanesAVencerse;
            grdPlanesPorVencerse.DataSource = BeneficiariosConPlanesAVencerse;
            grdPlanesPorVencerse.DataBind();
        }
        protected void CargarGrillaPlanesSinFechaVencimiento()
        {
            List<cBeneficiario> listaBeneficiarios = new List<cBeneficiario>();
            List<cBeneficiario> listaBenConPlanes = new List<cBeneficiario>();
            List<cBeneficiario> BeneficiariosConPlanesSinFechaVencimiento = new List<cBeneficiario>();
            listaBeneficiarios = dFachada.BeneficiarioTraerTodos();
            cBeneficiario beneficiario;
            for (int i = 0; i < listaBeneficiarios.Count; i++)
            {
                beneficiario = new cBeneficiario();
                beneficiario = listaBeneficiarios[i];
                beneficiario.lstPlanes = new List<cPlan>();
                beneficiario.lstPlanes = dFachada.PlanTraerActivosPorBeneficiario(beneficiario);
                listaBenConPlanes.Add(beneficiario);
            }
            for (int a = 0; a < listaBenConPlanes.Count; a++)
            {
                for (int b = 0; b < listaBenConPlanes[a].lstPlanes.Count; b++)
                {
                    if (listaBenConPlanes[a].lstPlanes[b].FechaFin == null)
                    {
                        BeneficiariosConPlanesSinFechaVencimiento.Add(listaBenConPlanes[a]);
                        break;
                        //si tiene varios planes se lista solo una vez el beneficiario
                    }                    
                }
            }
            BeneficiariosConPlanesSinFechaDeVencimiento = BeneficiariosConPlanesSinFechaVencimiento;
            this.grdBeneficiariosConPlanSinFechaVencimiento.DataSource = BeneficiariosConPlanesSinFechaVencimiento;
            this.grdBeneficiariosConPlanSinFechaVencimiento.DataBind();
        }
        protected void CargarGrillaEspecialistasConInformesPendientes()
        {
            grdEspecialistasConInformesPendientes.DataSource = dFachada.UsuarioTraerTodosEspecialistasConInformesPendientes();
            grdEspecialistasConInformesPendientes.DataBind();
        }
        protected void CargarGrillaSesionesDelDia()
        {
            List<cSesion> sesiones = new List<cSesion>();
            grdSesionesDelDia.DataSource = sesiones;
            grdSesionesDelDia.DataBind();
        }

        protected void grdEspecialistasConInformesPendientes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[2].Visible = false; //contraseña
            e.Row.Cells[6].Visible = false;//tipo
            e.Row.Cells[7].Visible = false;//domicilio
            e.Row.Cells[8].Visible = false;//fecha de nacimiento
            e.Row.Cells[9].Visible = false;//tel
            e.Row.Cells[10].Visible = false;//email
            e.Row.Cells[11].Visible = false;//estado
            e.Row.Cells[12].Visible = false;//tipo de contrato
        }

        protected void grdPlanesPorVencerse_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[5].Visible = false; //sexo
            e.Row.Cells[7].Visible = false; //tel2
            e.Row.Cells[8].Visible = false;//email
            e.Row.Cells[9].Visible = false;//domicilio
            e.Row.Cells[10].Visible = false;//fecha de nacimiento
            e.Row.Cells[11].Visible = false;//atributario
            e.Row.Cells[12].Visible = false;//motivo consulta
            e.Row.Cells[13].Visible = false;//escolaridad
            e.Row.Cells[14].Visible = false;//derivador
            e.Row.Cells[15].Visible = false;//estado
        }

        protected void grdPlanesPorVencerse_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdPlanesPorVencerse.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId="+codigo.ToString());
        }

        protected void grdBeneficiariosConPlanSinFechaVencimiento_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[5].Visible = false; //sexo
            e.Row.Cells[7].Visible = false; //tel2
            e.Row.Cells[8].Visible = false;//email
            e.Row.Cells[9].Visible = false;//domicilio
            e.Row.Cells[10].Visible = false;//fecha de nacimiento
            e.Row.Cells[11].Visible = false;//atributario
            e.Row.Cells[12].Visible = false;//motivo consulta
            e.Row.Cells[13].Visible = false;//escolaridad
            e.Row.Cells[14].Visible = false;//derivador
            e.Row.Cells[15].Visible = false;//estado
        }

        protected void grdBeneficiariosConPlanSinFechaVencimiento_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdBeneficiariosConPlanSinFechaVencimiento.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + codigo.ToString());
        }

        
    }
}