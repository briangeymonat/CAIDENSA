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
    public partial class vDiagnosticoAgregar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                CargarGrilla();
            }
        }      
        

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtDiagnostico.Text != "")
            {
                cDiagnostico diag = new cDiagnostico();
                diag.Tipo = txtDiagnostico.Text;
                if(!dFachada.DiagnosticoExiste(diag))
                {
                    bool resultado = dFachada.DiagnosticoAgregar(diag);
                    if(resultado)
                    {
                        this.btnAgregar.Visible = true;
                        this.lblMensaje.Text = "Agregado correctamente";
                        this.txtDiagnostico.Text = string.Empty;
                        CargarGrilla();
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Ya existe ese diagnóstico')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos')", true);
            }   
            
        }

        protected void CargarGrilla()
        {
            this.grdDiagnosticos.DataSource = dFachada.DiagnosticoTraerTodos();
            this.grdDiagnosticos.DataBind();
        }

        protected void grdDiagnosticos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdDiagnosticos.Rows[e.RowIndex].Cells[1];
            cDiagnostico diag = new cDiagnostico();
            diag.Codigo = int.Parse(celdaCodigo.Text);

            bool resultado = dFachada.DiagnosticoExisteDiagnosticoBeneficiario(diag);
            if(!resultado)
            {
                bool res = dFachada.DiagnosticoEliminar(diag);
                if(res)
                {
                    lblMensaje.Text = "Eliminado correctamente";
                    CargarGrilla();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se puede eliminar.')", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se puede eliminar un diagnóstico que tenga al menos un beneficiario.')", true);
            }
        }

        protected void grdDiagnosticos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[1].Visible = false;//codigo beneficiario
        }
    }
}