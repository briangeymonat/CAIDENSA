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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                        TimeSpan d = listaBenConPlanes[a].lstPlanes[b].FechaFin - fechaActual;
                        Double td = d.TotalDays;
                        if (td < 185)
                        {
                            BeneficiariosConPlanesAVencerse.Add(listaBenConPlanes[a]);
                            //si tiene varios planes se lista solo una vez el beneficiario
                        }
                    }
                }
            }

            this.grdPlanesPorVencerse.DataSource = BeneficiariosConPlanesAVencerse;
            this.grdPlanesPorVencerse.DataBind();
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

            this.grdPlanesPorVencerse.DataSource = BeneficiariosConPlanesSinFechaVencimiento;
            this.grdPlanesPorVencerse.DataBind();
        }
        protected void CargarGrillaEspecialistasConInformesPendientes()
        {
            grdEspecialistasConInformesPendientes.DataSource = dFachada.UsuarioTraerTodosEspecialistasConInformesPendientes();
            grdEspecialistasConInformesPendientes.DataBind();
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
    }
}