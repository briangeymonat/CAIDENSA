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
    public partial class vAgregarObservacionSesion : System.Web.UI.Page
    {
        static cSesion unaSesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                int idSesion = int.Parse(Request.QueryString["SesionId"]);
                unaSesion = new cSesion();
                unaSesion.Codigo = idSesion;
                unaSesion = dFachada.SesionTraerEspecifico(unaSesion);
                CargarDatos();
            }
            
        }

        private void CargarDatos()
        {
            lblFecha.Text = unaSesion.Fecha.ToString();
            lblHoraInicio.Text = unaSesion.HoraInicio.ToString();
            lblHoraFin.Text = unaSesion.HoraFin.ToString();
            lblLocalidad.Text = unaSesion.Centro.ToString();
            List<cBeneficiario> lstBeneficiarios = new List<cBeneficiario>();
            for (int i = 0; i < unaSesion.lstBeneficiarios.Count; i++)
            {
                lstBeneficiarios.Add(unaSesion.lstBeneficiarios[i].Beneficiario);
            }


            grdBeneficiarios.DataSource = lstBeneficiarios;
            grdBeneficiarios.DataBind();
            grdESpecialistas.DataSource = unaSesion.lstUsuarios;
            grdESpecialistas.DataBind();
            lblComentario.Text = unaSesion.Comentario.ToString();
        }

        protected void grdBeneficiarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //codigo
            e.Row.Cells[4].Visible = false; //sexo
            e.Row.Cells[6].Visible = false; //tel2
            e.Row.Cells[7].Visible = false;//email
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//atributario
            e.Row.Cells[11].Visible = false;//motivo consulta
            e.Row.Cells[12].Visible = false;//escolaridad
            e.Row.Cells[13].Visible = false;//derivador
            e.Row.Cells[14].Visible = false;//estado
        }
        protected void grdESpecialistas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //Codigo
            e.Row.Cells[1].Visible = false; //NickName
            e.Row.Cells[2].Visible = false; //Contrasena
            e.Row.Cells[6].Visible = false; //TipoUsuario
            e.Row.Cells[7].Visible = false; //Domicilio
            e.Row.Cells[8].Visible = false; //FechaNacimiento
            e.Row.Cells[10].Visible = false; //Email
            e.Row.Cells[11].Visible = false; //Estado
            e.Row.Cells[12].Visible = false; //TipoContrato
        }

        protected void btnDescartar_Click(object sender, EventArgs e)
        {
            string observacion = string.Empty;
            bool resultado = dFachada.SesionAgregarObservacion(vMiPerfil.U, unaSesion, observacion);
            if (resultado)
            {
                //vTareas.ventanaObservacion = false;
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se descartó la sesión para realizar la observación')", true);
                string script = "window.opener.location.reload(); window.close();";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, true);
                //Response.Redirect("vInicio.aspx");
            }
            else
            {
                lblMensaje.Text = "ERROR: No se pudo agregar la observación correctamente";
            }
        }

        protected void btnRealizar_Click(object sender, EventArgs e)
        {
            if (txtObservacionSesion.Text == string.Empty)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se ha ingresado la observación')", true);
            }
            else
            {
                string observacion = txtObservacionSesion.Text;
                bool resultado = dFachada.SesionAgregarObservacion(vMiPerfil.U, unaSesion, observacion);
                if (resultado)
                {
                    //vTareas.ventanaObservacion = false;
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se agregó la observación correctamente')", true);
                    string script = "window.opener.location.reload(); window.close();";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, true);
                }
                else
                {
                    lblMensaje.Text = "ERROR: No se pudo agregar la observación correctamente";
                }
            }

        }
    }
}