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
        static cSesion LaSesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int iIdSesion = int.Parse(Request.QueryString["SesionId"]);
                LaSesion = new cSesion();
                LaSesion.Codigo = iIdSesion;
                LaSesion = dFachada.SesionTraerEspecifico(LaSesion);
                CargarDatos();
            }

        }

        private void CargarDatos()
        {
            if (vTareas.ventanaObservacionVerDetalles)
            {
                lblFecha.Text = LaSesion.Fecha.ToString();
                lblHoraInicio.Text = LaSesion.HoraInicio.ToString();
                lblHoraFin.Text = LaSesion.HoraFin.ToString();

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
                List<cBeneficiario> lstBeneficiarios = new List<cBeneficiario>();
                for (int i = 0; i < LaSesion.lstBeneficiarios.Count; i++)
                {
                    lstBeneficiarios.Add(LaSesion.lstBeneficiarios[i].Beneficiario);
                }


                grdBeneficiarios.DataSource = lstBeneficiarios;
                grdBeneficiarios.DataBind();
                grdESpecialistas.DataSource = LaSesion.lstUsuarios;
                grdESpecialistas.DataBind();
                lblComentario.Text = LaSesion.Comentario.ToString();
                cUsuarioSesion unUS = new cUsuarioSesion();
                unUS.Sesion = LaSesion;
                unUS.Usuario = vMiPerfil.U;
                unUS = dFachada.SesionTraerObservacionPorUsuarioYSesion(unUS);
                txtObservacionSesion.Text = unUS.Observacion.ToString();
                txtObservacionSesion.Enabled = false;
                btnDescartar.Visible = false;
                btnRealizar.Visible = false;
                btnCerrar.Visible = true;
                lblAgregarObservacion.Text = "Observación realizada:";
            }
            else
            {
                lblFecha.Text = LaSesion.Fecha.ToString();
                lblHoraInicio.Text = LaSesion.HoraInicio.ToString();
                lblHoraFin.Text = LaSesion.HoraFin.ToString();
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
                List<cBeneficiario> lstBeneficiarios = new List<cBeneficiario>();
                for (int i = 0; i < LaSesion.lstBeneficiarios.Count; i++)
                {
                    lstBeneficiarios.Add(LaSesion.lstBeneficiarios[i].Beneficiario);
                }


                grdBeneficiarios.DataSource = lstBeneficiarios;
                grdBeneficiarios.DataBind();
                grdESpecialistas.DataSource = LaSesion.lstUsuarios;
                grdESpecialistas.DataBind();
                lblComentario.Text = LaSesion.Comentario.ToString();
                txtObservacionSesion.Enabled = true;
                btnDescartar.Visible = true;
                btnRealizar.Visible = true;
                btnCerrar.Visible = false;
                lblAgregarObservacion.Text = "Agregar observación sobre la sesión dada:";
            }

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
            cUsuarioSesion unUS = new cUsuarioSesion();
            unUS.Usuario = vMiPerfil.U;
            unUS.Sesion = LaSesion;
            unUS.Observacion = observacion;
            bool resultado = dFachada.SesionAgregarObservacion(unUS);
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
                cUsuarioSesion unUS = new cUsuarioSesion();
                unUS.Usuario = vMiPerfil.U;
                unUS.Sesion = LaSesion;
                unUS.Observacion = observacion;
                bool resultado = dFachada.SesionAgregarObservacion(unUS);
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

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            string script = "window.close();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, true);
        }
    }
}