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
    public partial class vDetallesSesionParaAsistencia : System.Web.UI.Page
    {
        private static cSesion LaSesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LaSesion = new cSesion();
                LaSesion.Codigo = int.Parse(Request.QueryString["Id"]);
                LaSesion = dFachada.SesionTraerEspecifico(LaSesion);

                CargarTodo();



            }
        }
        private void CargarTodo()
        {
            lblFecha.Text = LaSesion.Fecha;
            lblHoraInicio.Text = LaSesion.HoraInicio;
            lblHoraFin.Text = LaSesion.HoraFin;
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
            MostrarBeneficiarios();
            CargarEspecialistas();
            CargarNombresBeneficiarios();

        }
        private void MostrarBeneficiarios()
        {
            switch (LaSesion.lstBeneficiarios.Count)
            {
                case 1:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = false;
                    rblBeneficiario2.Visible = false;
                    lblNombreBeneficiario3.Visible = false;
                    rblBeneficiario3.Visible = false;
                    lblNombreBeneficiario4.Visible = false;
                    rblBeneficiario4.Visible = false;
                    lblNombreBeneficiario5.Visible = false;
                    rblBeneficiario5.Visible = false;
                    lblNombreBeneficiario6.Visible = false;
                    rblBeneficiario6.Visible = false;
                    lblNombreBeneficiario7.Visible = false;
                    rblBeneficiario7.Visible = false;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                case 2:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = false;
                    rblBeneficiario3.Visible = false;
                    lblNombreBeneficiario4.Visible = false;
                    rblBeneficiario4.Visible = false;
                    lblNombreBeneficiario5.Visible = false;
                    rblBeneficiario5.Visible = false;
                    lblNombreBeneficiario6.Visible = false;
                    rblBeneficiario6.Visible = false;
                    lblNombreBeneficiario7.Visible = false;
                    rblBeneficiario7.Visible = false;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                case 3:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = true;
                    rblBeneficiario3.Visible = true;
                    lblNombreBeneficiario4.Visible = false;
                    rblBeneficiario4.Visible = false;
                    lblNombreBeneficiario5.Visible = false;
                    rblBeneficiario5.Visible = false;
                    lblNombreBeneficiario6.Visible = false;
                    rblBeneficiario6.Visible = false;
                    lblNombreBeneficiario7.Visible = false;
                    rblBeneficiario7.Visible = false;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                case 4:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = true;
                    rblBeneficiario3.Visible = true;
                    lblNombreBeneficiario4.Visible = true;
                    rblBeneficiario4.Visible = true;
                    lblNombreBeneficiario5.Visible = false;
                    rblBeneficiario5.Visible = false;
                    lblNombreBeneficiario6.Visible = false;
                    rblBeneficiario6.Visible = false;
                    lblNombreBeneficiario7.Visible = false;
                    rblBeneficiario7.Visible = false;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                case 5:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = true;
                    rblBeneficiario3.Visible = true;
                    lblNombreBeneficiario4.Visible = true;
                    rblBeneficiario4.Visible = true;
                    lblNombreBeneficiario5.Visible = true;
                    rblBeneficiario5.Visible = true;
                    lblNombreBeneficiario6.Visible = false;
                    rblBeneficiario6.Visible = false;
                    lblNombreBeneficiario7.Visible = false;
                    rblBeneficiario7.Visible = false;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                case 6:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = true;
                    rblBeneficiario3.Visible = true;
                    lblNombreBeneficiario4.Visible = true;
                    rblBeneficiario4.Visible = true;
                    lblNombreBeneficiario5.Visible = true;
                    rblBeneficiario5.Visible = true;
                    lblNombreBeneficiario6.Visible = true;
                    rblBeneficiario6.Visible = true;
                    lblNombreBeneficiario7.Visible = false;
                    rblBeneficiario7.Visible = false;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                case 7:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = true;
                    rblBeneficiario3.Visible = true;
                    lblNombreBeneficiario4.Visible = true;
                    rblBeneficiario4.Visible = true;
                    lblNombreBeneficiario5.Visible = true;
                    rblBeneficiario5.Visible = true;
                    lblNombreBeneficiario6.Visible = true;
                    rblBeneficiario6.Visible = true;
                    lblNombreBeneficiario7.Visible = true;
                    rblBeneficiario7.Visible = true;
                    lblNombreBeneficiario8.Visible = false;
                    rblBeneficiario8.Visible = false;
                    break;
                default:
                    lblNombreBeneficiario1.Visible = true;
                    rblBeneficiario1.Visible = true;
                    lblNombreBeneficiario2.Visible = true;
                    rblBeneficiario2.Visible = true;
                    lblNombreBeneficiario3.Visible = true;
                    rblBeneficiario3.Visible = true;
                    lblNombreBeneficiario4.Visible = true;
                    rblBeneficiario4.Visible = true;
                    lblNombreBeneficiario5.Visible = true;
                    rblBeneficiario5.Visible = true;
                    lblNombreBeneficiario6.Visible = true;
                    rblBeneficiario6.Visible = true;
                    lblNombreBeneficiario7.Visible = true;
                    rblBeneficiario7.Visible = true;
                    lblNombreBeneficiario8.Visible = true;
                    rblBeneficiario8.Visible = true;
                    break;
            }
        }

        private void CargarEspecialistas()
        {
            for (int i = 0; i < LaSesion.lstUsuarios.Count; i++)
            {
                // EN CASO DE SER UNO SOLO
                if (LaSesion.lstUsuarios.Count == 1) lblEspecialistas.Text = LaSesion.lstUsuarios[i].Nombres + " " + LaSesion.lstUsuarios[i].Apellidos + ".";
                // EN CASO DE SER EL PRIMERO
                else if (i == 0) lblEspecialistas.Text += LaSesion.lstUsuarios[i].Nombres + " " + LaSesion.lstUsuarios[i].Apellidos;
                // EN CASO DE SER UNO DEL MEDIO
                else if ((LaSesion.lstUsuarios.Count - 1) == i) lblEspecialistas.Text += "y " + LaSesion.lstUsuarios[i].Nombres + " " + LaSesion.lstUsuarios[i].Apellidos + ".";
                // EN CASO DE SER EL ULTIMO
                else lblEspecialistas.Text += ", " + LaSesion.lstUsuarios[i].Nombres + " " + LaSesion.lstUsuarios[i].Apellidos;

            }
        }

        private void CargarNombresBeneficiarios()
        {
            for (int i = 0; i < LaSesion.lstBeneficiarios.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        lblNombreBeneficiario1.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    case 1:
                        lblNombreBeneficiario2.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    case 2:
                        lblNombreBeneficiario3.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    case 3:
                        lblNombreBeneficiario4.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    case 4:
                        lblNombreBeneficiario5.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    case 5:
                        lblNombreBeneficiario6.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    case 6:
                        lblNombreBeneficiario7.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                    default:
                        lblNombreBeneficiario8.Text = LaSesion.lstBeneficiarios[i].Beneficiario.Nombres + " " + LaSesion.lstBeneficiarios[i].Beneficiario.Apellidos;
                        break;
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

            #region RECORRIDA POR LAS ASISTENCIAS

            List<cBeneficiarioSesion> reprogramadas = new List<cBeneficiarioSesion>();
            if (LaSesion.lstBeneficiarios.Count >= 1)
            {
                switch (rblBeneficiario1.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[0].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[0].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[0].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[0]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[0].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count >= 2)
            {
                switch (rblBeneficiario2.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[1].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[1].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[1].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[1]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[1].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count >= 3)
            {
                switch (rblBeneficiario3.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[2].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[2].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[2].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[2]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[2].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count >= 4)
            {
                switch (rblBeneficiario4.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[3].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[3].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[3].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[3]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[3].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count >= 5)
            {
                switch (rblBeneficiario5.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[4].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[4].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[4].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[4]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[4].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count >= 6)
            {
                switch (rblBeneficiario6.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[5].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[5].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[5].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[5]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[5].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count >= 7)
            {
                switch (rblBeneficiario7.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[6].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[6].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[6].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[6]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[6].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }
            if (LaSesion.lstBeneficiarios.Count == 8)
            {
                switch (rblBeneficiario8.SelectedValue)
                {
                    case "Asistió":
                        LaSesion.lstBeneficiarios[7].Estado = cUtilidades.EstadoSesion.Asistio;
                        break;
                    case "No asistió":
                        LaSesion.lstBeneficiarios[7].Estado = cUtilidades.EstadoSesion.NoAsistio;
                        break;
                    case "Reprogramada":
                        LaSesion.lstBeneficiarios[7].Estado = cUtilidades.EstadoSesion.Reprogramada;
                        reprogramadas.Add(LaSesion.lstBeneficiarios[7]);
                        break;
                    default:
                        LaSesion.lstBeneficiarios[7].Estado = cUtilidades.EstadoSesion.SinEstado;
                        break;
                }
            }

            #endregion

            LaSesion.Comentario = txtComentario.Text;
            if (dFachada.SesionMarcarAsitencias(LaSesion))
            {
                if (reprogramadas.Count > 0)
                {
                    string variables = "?CantidadBeneficiarios=" + reprogramadas.Count + "&CantidadEspecialistas=" + LaSesion.lstUsuarios.Count;
                    for (int i = 0; i < reprogramadas.Count; i++)
                    {
                        variables += "&Beneficiario" + (i + 1).ToString() + "=" + reprogramadas[i].Beneficiario.Codigo;
                    }
                    for (int i = 0; i < LaSesion.lstUsuarios.Count; i++)
                    {
                        variables += "&Usuario" + (i + 1).ToString() + "=" + LaSesion.lstUsuarios[i].Codigo;
                    }
                    Response.Redirect("vSesionReprogramar.aspx" + variables);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se han confirmado las asistencias correctamente')", true);
                    string script = "window.opener.location.reload(); window.close();";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, true);
                }
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string script = "window.close();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, true);
        }
    }
}