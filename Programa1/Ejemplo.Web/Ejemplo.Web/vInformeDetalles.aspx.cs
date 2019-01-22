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
    public partial class vInformeDetalles : System.Web.UI.Page
    {
        cInforme Informe;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Informe = new cInforme();
                Informe.Codigo = int.Parse(Request.QueryString["InformeId"]);
                Informe = dFachada.InformeTraerEspecifico(Informe);
                Informe.Beneficiario = dFachada.BeneficiarioTraerEspecifico(Informe.Beneficiario);
                Informe.lstSecciones = dFachada.SeccionTraerTodasPorInforme(Informe);
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    Informe.lstSecciones[i].lstUsuariosSeccion = dFachada.UsuarioSeccionTraerTodosPorSeccion(Informe.lstSecciones[i]);
                    for (int j = 0; j < Informe.lstSecciones[i].lstUsuariosSeccion.Count; j++)
                    {
                        Informe.lstSecciones[i].lstUsuariosSeccion[j].Usuario = dFachada.UsuarioTraerEspecifico(Informe.lstSecciones[i].lstUsuariosSeccion[j].Usuario);
                    }
                }
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            OcultarSecciones(false);
            for (int i = 0; i < Informe.lstSecciones.Count; i++)
            {
                //TITULO
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                    lblTitulo.Text = Informe.lstSecciones[i].Contenido.ToString();
                //ENCUADRE
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Encuadre)
                    lblEncuadre.Text = Informe.lstSecciones[i].Contenido.ToString();
                //PRESENTACION
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    lblPresentacion.Text = Informe.lstSecciones[i].Contenido.ToString();
                //EN SUMA
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    lblEnSuma.Text = Informe.lstSecciones[i].Contenido.ToString();
                //ANTECEDENTES PATOLOGICOS
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                {
                    lblSubtituloAntecedentesPatologicos.Visible = true;
                    lblAntecedentesPatologicos.Visible = true;
                    lblAntecedentesPatologicos.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                //DESARROLLO
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                {
                    lblSubtituloDesarrollo.Visible = true;
                    lblDesarrollo.Visible = true;
                    lblDesarrollo.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                //ABORDAJE PSICOMOTRIZ
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz)
                {
                    lblSubtituloAbPsicomotriz.Visible = true;
                    lblAbordajePsicomotriz.Visible = true;
                    lblAbordajePsicomotriz.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                //ABORDAJE PEDAGOGICO
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico)
                {
                    lblSubtituloAbPedagogico.Visible = true;
                    lblAbordajePedagogico.Visible = true;
                    lblAbordajePedagogico.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                //ABORDAJE PSICOLOGICO
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico)
                {
                    lblSubtituloAbPsicologico.Visible = true;
                    lblAbordajePsicologico.Visible = true;
                    lblAbordajePsicologico.Text = Informe.lstSecciones[i].Contenido.ToString();
                }//ABORDAJE FONOAUDIOLOGICO
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico)
                {
                    lblSubtituloAbFonoaudiologico.Visible = true;
                    lblAbordajeFonoaudiologico.Visible = true;
                    lblAbordajeFonoaudiologico.Text = Informe.lstSecciones[i].Contenido.ToString();
                }//ABORDAJE FISIOTERAPEUTICO
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico)
                {
                    lblSubtituloAbFisioterapeutico.Visible = true;
                    lblAbordajeFisioterapeutico.Visible = true;
                    lblAbordajeFisioterapeutico.Text = Informe.lstSecciones[i].Contenido.ToString();
                }//SUGERENCIA
                else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                {
                    lblSubtituloSugerencia.Visible = true;
                    lblSugerencias.Visible = true;
                    lblSugerencias.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
            }
            lblTipo.Text = Informe.Tipo.ToString();
            lblNombreApellido.Text = Informe.Beneficiario.Nombres + " " + Informe.Beneficiario.Apellidos;
            lblCI.Text = Informe.Beneficiario.CI.ToString();
            lblFechaNacimiento.Text = Informe.Beneficiario.FechaNacimiento.ToString();

            #region Hallar la edad cronologica
            string[] partes = Informe.Beneficiario.FechaNacimiento.ToString().Split('/');
            int año = int.Parse(partes[2]);
            int mes = int.Parse(partes[1]);
            int dia = int.Parse(partes[0]);
            int añoActual = DateTime.Now.Year;
            int mesActual = DateTime.Now.Month;
            int diaActual = DateTime.Now.Day;

            int edadAños = añoActual - año;
            int edadMeses;
            int edadDias;
            if (mesActual >= mes)
            {
                edadMeses = mesActual - mes;
            }
            else
            {
                mesActual += 12;
                edadMeses = mesActual - mes;
                edadAños -= 1;
            }
            if (diaActual >= dia)
            {
                edadDias = diaActual - dia;
            }
            else
            {
                diaActual += 30;
                edadMeses -= 1;
                edadDias = diaActual - dia;
            }
            #endregion

            lblEdadCronologica.Text = edadAños + " años y " + edadMeses + " meses";
            lblMotivoConsulta.Text = Informe.Beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = Informe.Beneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(Informe.Beneficiario);
            lblFecha.Text = dFachada.BeneficiarioCentroPreferencia(Informe.Beneficiario)+", "+DateTime.Parse(Informe.Fecha).ToString("dd 'de' MMMM 'de' yyyy");
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

        }
    }
}