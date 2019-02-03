using Common.Clases;
using Dominio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vInformeDetalles : System.Web.UI.Page
    {
        static cInforme ElInforme;
        static string SVentanaAnterior;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SVentanaAnterior = Request.QueryString["Ventana"];
                ElInforme = new cInforme();
                ElInforme.Codigo = int.Parse(Request.QueryString["InformeId"]);
                ElInforme = dFachada.InformeTraerEspecifico(ElInforme);
                ElInforme.Beneficiario = dFachada.BeneficiarioTraerEspecifico(ElInforme.Beneficiario);
                ElInforme.lstSecciones = dFachada.SeccionTraerTodasPorInforme(ElInforme);
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    ElInforme.lstSecciones[i].lstUsuariosSeccion = dFachada.UsuarioSeccionTraerTodosPorSeccion(ElInforme.lstSecciones[i]);
                    for (int j = 0; j < ElInforme.lstSecciones[i].lstUsuariosSeccion.Count; j++)
                    {
                        ElInforme.lstSecciones[i].lstUsuariosSeccion[j].Usuario = dFachada.UsuarioTraerEspecifico(ElInforme.lstSecciones[i].lstUsuariosSeccion[j].Usuario);
                    }
                }
                CargarDatos();
            }
        }

        
        private void CargarDatos()
        {
            OcultarSecciones(false);
            for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
            {
                //TITULO
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                    lblTitulo.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                //ENCUADRE
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Encuadre)
                    lblEncuadre.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                //PRESENTACION
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    lblPresentacion.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                //EN SUMA
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    lblEnSuma.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                //ANTECEDENTES PATOLOGICOS
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                {
                    lblSubtituloAntecedentesPatologicos.Visible = true;
                    lblAntecedentesPatologicos.Visible = true;
                    lblAntecedentesPatologicos.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                //DESARROLLO
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                {
                    lblSubtituloDesarrollo.Visible = true;
                    lblDesarrollo.Visible = true;
                    lblDesarrollo.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                //ABORDAJE PSICOMOTRIZ
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz)
                {
                    lblSubtituloAbPsicomotriz.Visible = true;
                    lblAbordajePsicomotriz.Visible = true;
                    lblAbordajePsicomotriz.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                //ABORDAJE PEDAGOGICO
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico)
                {
                    lblSubtituloAbPedagogico.Visible = true;
                    lblAbordajePedagogico.Visible = true;
                    lblAbordajePedagogico.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                //ABORDAJE PSICOLOGICO
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico)
                {
                    lblSubtituloAbPsicologico.Visible = true;
                    lblAbordajePsicologico.Visible = true;
                    lblAbordajePsicologico.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }//ABORDAJE FONOAUDIOLOGICO
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico)
                {
                    lblSubtituloAbFonoaudiologico.Visible = true;
                    lblAbordajeFonoaudiologico.Visible = true;
                    lblAbordajeFonoaudiologico.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }//ABORDAJE FISIOTERAPEUTICO
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico)
                {
                    lblSubtituloAbFisioterapeutico.Visible = true;
                    lblAbordajeFisioterapeutico.Visible = true;
                    lblAbordajeFisioterapeutico.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }//SUGERENCIA
                else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                {
                    lblSubtituloSugerencia.Visible = true;
                    lblSugerencias.Visible = true;
                    lblSugerencias.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
            }
            lblTipo.Text = ElInforme.Tipo.ToString();
            lblNombreApellido.Text = ElInforme.Beneficiario.Nombres + " " + ElInforme.Beneficiario.Apellidos;
            lblCI.Text = ElInforme.Beneficiario.CI.ToString();
            lblFechaNacimiento.Text = ElInforme.Beneficiario.FechaNacimiento.ToString();

            #region Hallar la edad cronologica

            string[] aPartes = ElInforme.Beneficiario.FechaNacimiento.ToString().Split('/');
            int iAño = int.Parse(aPartes[2]);
            int iMes = int.Parse(aPartes[1]);
            int iDia = int.Parse(aPartes[0]);
            int iAñoActual = DateTime.Now.Year;
            int iMesActual = DateTime.Now.Month;
            int iDiaActual = DateTime.Now.Day;

            int iEdadAños = iAñoActual - iAño;
            int iEdadMeses;
            int iEdadDias;
            if (iMesActual >= iMes)
            {
                iEdadMeses = iMesActual - iMes;
            }
            else
            {
                iMesActual += 12;
                iEdadMeses = iMesActual - iMes;
                iEdadAños -= 1;
            }
            if (iDiaActual >= iDia)
            {
                iEdadDias = iDiaActual - iDia;
            }
            else
            {
                iDiaActual += 30;
                iEdadMeses -= 1;
                iEdadDias = iDiaActual - iDia;
            }
            #endregion

            lblEdadCronologica.Text = iEdadAños + " años y " + iEdadMeses + " meses";
            lblMotivoConsulta.Text = ElInforme.Beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = ElInforme.Beneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(ElInforme.Beneficiario);
            if(ElInforme.Fecha!= null)
            {
                lblFecha.Text = dFachada.BeneficiarioCentroPreferencia(ElInforme.Beneficiario) + ", " + DateTime.Parse(ElInforme.Fecha).ToString("dd 'de' MMMM 'de' yyyy");
            }
            else
            {
                btnExportarPDF.Visible = false;
            }
                
        }

        private void OcultarSecciones(bool parVisible)
        {
            lblSubtituloAntecedentesPatologicos.Visible = parVisible;
            lblAntecedentesPatologicos.Visible = parVisible;

            lblSubtituloDesarrollo.Visible = parVisible;
            lblDesarrollo.Visible = parVisible;

            lblSubtituloAbPsicomotriz.Visible = parVisible;
            lblAbordajePsicomotriz.Visible = parVisible;

            lblSubtituloAbFisioterapeutico.Visible = parVisible;
            lblAbordajeFisioterapeutico.Visible = parVisible;

            lblSubtituloAbFonoaudiologico.Visible = parVisible;
            lblAbordajeFonoaudiologico.Visible = parVisible;

            lblSubtituloAbPedagogico.Visible = parVisible;
            lblAbordajePedagogico.Visible = parVisible;

            lblSubtituloAbPsicologico.Visible = parVisible;
            lblAbordajePsicologico.Visible = parVisible;

            lblSubtituloSugerencia.Visible = parVisible;
            lblSugerencias.Visible = parVisible;
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            //Response.Redirect("vReportePDF.aspx?InformeId=" +Informe.Codigo);
            string sOpen = "window.open('vReportePDF.aspx?InformeId=" + ElInforme.Codigo+"', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), sOpen, true);
            
        }

        protected void btnAtras_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SVentanaAnterior);
        }
    }
}