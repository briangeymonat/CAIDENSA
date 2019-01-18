using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vEstadisticasBeneficiariosPorEdad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGrilla();
            }
        }
        protected void CargarGrilla()
        {
            if (txtDesde.Text !=string.Empty && txtHasta.Text != string.Empty && int.Parse(txtDesde.Text) > int.Parse(txtHasta.Text))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: La edad Desde debe ser menor a la edad Hasta')", true);
            }
            else
            {
                if (txtDesde.Text != string.Empty && txtHasta.Text != string.Empty)
                {
                    grdBeneficiarios.DataSource = dFachada.EstadisticaBeneficiarioTraerActivosPorEdad(int.Parse(txtDesde.Text), int.Parse(txtHasta.Text));
                    grdBeneficiarios.DataBind();
                    lblCantidadBeneficiarios.Text = " "+grdBeneficiarios.Rows.Count.ToString();
                }
                else if (txtDesde.Text == string.Empty && txtHasta.Text != string.Empty)
                {
                    grdBeneficiarios.DataSource = dFachada.EstadisticaBeneficiarioTraerActivosPorEdad(0, int.Parse(txtHasta.Text));
                    grdBeneficiarios.DataBind();
                    lblCantidadBeneficiarios.Text = " " + grdBeneficiarios.Rows.Count.ToString();
                }
                else if (txtDesde.Text != string.Empty && txtHasta.Text == string.Empty)
                {
                    grdBeneficiarios.DataSource = dFachada.EstadisticaBeneficiarioTraerActivosPorEdad(int.Parse(txtDesde.Text), 150);
                    grdBeneficiarios.DataBind();
                    lblCantidadBeneficiarios.Text = " " + grdBeneficiarios.Rows.Count.ToString();
                }
                else
                {
                    grdBeneficiarios.DataSource = dFachada.EstadisticaBeneficiarioTraerActivosPorEdad(0, 150);
                    grdBeneficiarios.DataBind();
                    lblCantidadBeneficiarios.Text = " " + grdBeneficiarios.Rows.Count.ToString();
                }
            }
        }

        protected void txtDesde_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void txtHasta_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            CargarGrilla();
        }

        protected void grdBeneficiarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;//codigo
            e.Row.Cells[4].Visible = false;//sexo
            e.Row.Cells[5].Visible = false;//tel1
            e.Row.Cells[6].Visible = false;//tel2
            e.Row.Cells[7].Visible = false;//email
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[11].Visible = false;//Motivo consulta
            e.Row.Cells[12].Visible = false;//escolaridad
            e.Row.Cells[13].Visible = false;//derivador
            e.Row.Cells[14].Visible = false;//estado
        }
    }
}