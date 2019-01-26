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
    public partial class DetallesSesion : System.Web.UI.Page
    {
        private static cSesion LaSesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LaSesion = new cSesion();
                LaSesion.Codigo = int.Parse(Request.QueryString["Id"]);
                LaSesion = dFachada.SesionTraerEspecifico(LaSesion);
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            lblFecha.Text = LaSesion.Fecha.ToString();
            lblHoraInicio.Text = LaSesion.HoraInicio.ToString();
            lblHoraFin.Text = LaSesion.HoraFin.ToString();
            lblComentario.Text = LaSesion.Comentario.ToString();
            switch (LaSesion.Centro)
            {
                case cUtilidades.Centro.JuanLacaze:
                    lblLocalidad.Text = "Juan Lacaze";
                    break;
                case cUtilidades.Centro.NuevaHelvecia:
                    lblLocalidad.Text = "Nueva Helvecia";
                    break;
                default:
                    lblLocalidad.Text = "";
                    break;
            }
            switch (LaSesion.TipoSesion)
            {
                case cUtilidades.TipoSesion.Individual:
                    lblTipoSesion.Text = "Individual";
                    break;
                case cUtilidades.TipoSesion.Grupo2:
                    lblTipoSesion.Text = "Grupo de 2";
                    break;
                case cUtilidades.TipoSesion.Grupo3:
                    lblTipoSesion.Text = "Grupo de 3";
                    break;
                case cUtilidades.TipoSesion.Taller:
                    lblTipoSesion.Text = "Taller";
                    break;
                case cUtilidades.TipoSesion.PROES:
                    lblTipoSesion.Text = "PROES";
                    break;
                default:
                    lblTipoSesion.Text = "";
                    break;
            }
            
            grdEspecialistas.DataSource = LaSesion.lstUsuarios;
            grdEspecialistas.DataBind();
            List<cBeneficiario> beneficiarios = new List<cBeneficiario>();
            for(int i=0; i<LaSesion.lstBeneficiarios.Count; i++)
            {
                beneficiarios.Add(LaSesion.lstBeneficiarios[i].Beneficiario);
            }
            grdBeneficiarios.DataSource = beneficiarios;
            grdBeneficiarios.DataBind();
        }
        

        protected void grdBeneficiarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false; 
            e.Row.Cells[5].Visible = false; 
            e.Row.Cells[6].Visible = false; 
            e.Row.Cells[7].Visible = false; 
            e.Row.Cells[8].Visible = false; 
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false; 
            e.Row.Cells[14].Visible = false; 
        }

        protected void grdEspecialistas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false; 
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false; 
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false; 
            e.Row.Cells[12].Visible = false; 
        }
    }
}