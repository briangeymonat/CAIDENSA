using Common.Clases;
using Dominio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web.UI;


namespace Ejemplo.Web
{
    public partial class vReportePDF : System.Web.UI.Page
    {
        static cInforme Informe;
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

                MemoryStream ms = new MemoryStream();
                Document documento = new Document(iTextSharp.text.PageSize.A4, 60f, 60f, 60f, 60f);
                PdfWriter pw = PdfWriter.GetInstance(documento, ms);
                documento.Open();

                #region Fuentes
                Font fontTitulo1 = FontFactory.GetFont("Times New Roman", 12, Font.UNDERLINE | Font.BOLD);
                fontTitulo1.SetColor(000, 000, 000);

                Font fontTitulo2 = FontFactory.GetFont("Times New Roman", 12, Font.BOLDITALIC);
                fontTitulo2.SetColor(000, 000, 000);

                Font fontTitulo3 = FontFactory.GetFont("Times New Roman", 12, Font.BOLD);
                fontTitulo3.SetColor(000, 000, 000);

                Font fontParrafo = FontFactory.GetFont("Times New Roman", 12);
                fontParrafo.SetColor(000, 000, 000);

                Font fontFecha = FontFactory.GetFont("Arial", 11);
                fontFecha.SetColor(000, 000, 000);
                #endregion

                //LOGO y HEADER
                string imageUrl = Server.MapPath("/Img") + "/logo.jpg";
                Image img = Image.GetInstance(imageUrl);
                img.ScalePercent(50);
                documento.Add(img);

                Paragraph parrafo = new Paragraph();
                //CENTRO Y FECHA
                parrafo = new Paragraph(dFachada.BeneficiarioCentroPreferencia(Informe.Beneficiario) + ", " + DateTime.Parse(Informe.Fecha).ToString("dd 'de' MMMM 'de' yyyy"), fontFecha);
                parrafo.Alignment = Element.ALIGN_RIGHT;
                documento.Add(parrafo);

                //Linea sepradora
                parrafo = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                documento.Add(parrafo);

                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //TITULO
                parrafo = new Paragraph();
                parrafo = new Paragraph(Informe.lstSecciones[0].Contenido.ToString(), fontTitulo1);
                parrafo.Alignment = Element.ALIGN_CENTER;
                documento.Add(parrafo);


                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //NOMBRE BENEFICIARIO
                Chunk txtNombre = new Chunk("Nombre: ", fontTitulo3);
                Chunk nombre = new Chunk(Informe.Beneficiario.Nombres + " " + Informe.Beneficiario.Apellidos, fontParrafo);
                parrafo = new Paragraph();
                parrafo.Add(txtNombre);
                parrafo.Add(nombre);
                parrafo.Alignment = Element.ALIGN_LEFT;
                documento.Add(parrafo);

                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //CEDULA DE IDENTIDAD
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk txtCI = new Chunk("Cédula de identidad: ", fontTitulo3);
                Chunk ci = new Chunk(Informe.Beneficiario.CI.ToString(), fontParrafo);
                parrafo.Add(txtCI);
                parrafo.Add(ci);
                documento.Add(parrafo);

                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //FECHA DE NACIMIENTO Y EDAD CRONOLOGICA
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
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

                Chunk txtFecha = new Chunk("Fecha de nacimiento: ", fontTitulo3);
                Chunk fecha = new Chunk(Informe.Beneficiario.FechaNacimiento.ToString(), fontParrafo);
                Chunk txtEdad = new Chunk("               Edad cronológica: ", fontTitulo3);
                Chunk edad = new Chunk(edadAños.ToString() + " años y " + edadMeses.ToString() + " meses", fontParrafo);
                parrafo.Add(txtFecha);
                parrafo.Add(fecha);
                parrafo.Add(txtEdad);
                parrafo.Add(edad);
                documento.Add(parrafo);

                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //MOTIVO DE CONSULTA
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk txtMC = new Chunk("Motivo de consulta: ", fontTitulo3);
                Chunk mc = new Chunk(Informe.Beneficiario.MotivoConsulta.ToString(), fontParrafo);
                parrafo.Add(txtMC);
                parrafo.Add(mc);
                documento.Add(parrafo);

                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //ESCOLARIDAD
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk txtEscolaridad = new Chunk("Escolaridad: ", fontTitulo3);
                Chunk escolaridad = new Chunk(Informe.Beneficiario.Escolaridad.ToString(), fontParrafo);
                parrafo.Add(txtEscolaridad);
                parrafo.Add(escolaridad);
                documento.Add(parrafo);

                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //ENCUADRE
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk txtEncuadre = new Chunk("Encuadre: ", fontTitulo3);
                Chunk Encuadre = new Chunk(Informe.lstSecciones[1].Contenido.ToString(), fontParrafo);
                parrafo.Add(txtEncuadre);
                parrafo.Add(Encuadre);
                documento.Add(parrafo);
                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                //Linea sepradora
                parrafo = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                documento.Add(parrafo);
                parrafo = new Paragraph("\n");
                documento.Add(parrafo);

                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //ANTECEDENTES PATOLOGICOS
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                    {
                        parrafo = new Paragraph("   Antecedentes patológicos: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //DESARROLLO
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                    {
                        parrafo = new Paragraph("   Desarrollo: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //PRESENTACION
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    {
                        parrafo = new Paragraph("   Presentación: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);

                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {

                    //ABORDAJE PSICOMOTRIZ
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz)
                    {
                        parrafo = new Paragraph("   Abordaje psicomotriz: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //ABORDAJE PEDAGOGICO
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico)
                    {
                        parrafo = new Paragraph("   Abordaje pedagógico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //ABORDAJE PSICOLOGICO
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico)
                    {
                        parrafo = new Paragraph("   Abordaje psicológico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //ABORDAJE FONOAUDIOLOGICO
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico)
                    {
                        parrafo = new Paragraph("   Abordaje fonoaudiológico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //ABORDAJE FISIOTERAPEUTICO
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico)
                    {
                        parrafo = new Paragraph("   Abordaje fisioterapéutico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //EN SUMA
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    {
                        parrafo = new Paragraph("   En suma: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }
                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    //SUGERENCIA
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                    {
                        parrafo = new Paragraph("   Sugerencias: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                        parrafo = new Paragraph(Informe.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        documento.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        documento.Add(parrafo);
                    }
                }

                documento.Close();

                byte[] bytesStream = ms.ToArray();

                Response.Clear();
                ms = new MemoryStream(bytesStream);
                Response.ContentType = "application/pdf";
                //ms = new MemoryStream();
                //ms.Write(bytesStream, 0, bytesStream.Length);
                //ms.Position = 0;
                Response.AddHeader("content-disposition", "attachemnt;filename=invoice.pdf");
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();
                //new FileStreamResult(ms, "Informe");

            }
        }
    } 
}