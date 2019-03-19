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
        static cInforme ElInforme;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Title = "Informe";
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

                MemoryStream ms = new MemoryStream();
                Document doc = new Document(iTextSharp.text.PageSize.A4, 72f, 72f, 72f, 72f);
                PdfWriter pw = PdfWriter.GetInstance(doc, ms);
                doc.Open();

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

                Font fontCursiva = FontFactory.GetFont("Times New Roman", 12, Font.ITALIC);
                fontCursiva.SetColor(000, 000, 000);
                #endregion

                //LOGO y HEADER
                string sImageUrl = Server.MapPath("/Img") + "/EncabezadoInforme.jpg";
                Image img = Image.GetInstance(sImageUrl);
                img.ScalePercent(38);
                doc.Add(img);

                Paragraph parrafo = new Paragraph();
                //CENTRO Y FECHA
                parrafo = new Paragraph(dFachada.BeneficiarioCentroPreferencia(ElInforme.Beneficiario) + ", " + DateTime.Parse(ElInforme.Fecha).ToString("dd 'de' MMMM 'de' yyyy"), fontFecha);
                parrafo.Alignment = Element.ALIGN_RIGHT;
                doc.Add(parrafo);

                //Linea sepradora
                parrafo = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //TITULO
                parrafo = new Paragraph();
                parrafo = new Paragraph(ElInforme.lstSecciones[0].Contenido.ToString(), fontTitulo1);
                parrafo.Alignment = Element.ALIGN_CENTER;
                doc.Add(parrafo);


                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //NOMBRE BENEFICIARIO
                Chunk chTxtNombre = new Chunk("Nombre: ", fontTitulo3);
                Chunk chNombre = new Chunk(ElInforme.Beneficiario.Nombres + " " + ElInforme.Beneficiario.Apellidos, fontParrafo);
                parrafo = new Paragraph();
                parrafo.Add(chTxtNombre);
                parrafo.Add(chNombre);
                parrafo.Alignment = Element.ALIGN_LEFT;
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //CEDULA DE IDENTIDAD
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk chTxtCI = new Chunk("Cédula de identidad: ", fontTitulo3);
                Chunk chCi = new Chunk(ElInforme.Beneficiario.CI.ToString(), fontParrafo);
                parrafo.Add(chTxtCI);
                parrafo.Add(chCi);
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //FECHA DE NACIMIENTO Y EDAD CRONOLOGICA
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
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

                Chunk chTxtFecha = new Chunk("Fecha de nacimiento: ", fontTitulo3);
                Chunk chFecha = new Chunk(ElInforme.Beneficiario.FechaNacimiento.ToString(), fontParrafo);
                Chunk chTxtEdad = new Chunk("               Edad cronológica: ", fontTitulo3);
                Chunk chEdad = new Chunk(iEdadAños.ToString() + " años y " + iEdadMeses.ToString() + " meses", fontParrafo);
                parrafo.Add(chTxtFecha);
                parrafo.Add(chFecha);
                parrafo.Add(chTxtEdad);
                parrafo.Add(chEdad);
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //MOTIVO DE CONSULTA
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk chTxtMC = new Chunk("Motivo de consulta: ", fontTitulo3);
                Chunk chMc = new Chunk(ElInforme.Beneficiario.MotivoConsulta.ToString(), fontParrafo);
                parrafo.Add(chTxtMC);
                parrafo.Add(chMc);
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //ESCOLARIDAD
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk chTxtEscolaridad = new Chunk("Escolaridad: ", fontTitulo3);
                Chunk chEscolaridad = new Chunk(ElInforme.Beneficiario.Escolaridad.ToString(), fontParrafo);
                parrafo.Add(chTxtEscolaridad);
                parrafo.Add(chEscolaridad);
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //ENCUADRE
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_LEFT;
                Chunk chTxtEncuadre = new Chunk("Encuadre: ", fontTitulo3);
                Chunk chEncuadre = new Chunk(ElInforme.lstSecciones[1].Contenido.ToString(), fontParrafo);
                parrafo.Add(chTxtEncuadre);
                parrafo.Add(chEncuadre);
                doc.Add(parrafo);
                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                //Linea sepradora
                parrafo = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                doc.Add(parrafo);
                parrafo = new Paragraph("\n");
                doc.Add(parrafo);

                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //ANTECEDENTES PATOLOGICOS
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                    {
                        parrafo = new Paragraph("   Antecedentes patológicos: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //DESARROLLO
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                    {
                        parrafo = new Paragraph("   Desarrollo: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //PRESENTACION
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    {
                        parrafo = new Paragraph("   Presentación: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);

                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {

                    //ABORDAJE PSICOMOTRIZ
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz)
                    {
                        parrafo = new Paragraph("   Abordaje psicomotriz: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //ABORDAJE PEDAGOGICO
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico)
                    {
                        parrafo = new Paragraph("   Abordaje pedagógico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //ABORDAJE PSICOPEDAGOGICO
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicopedagógico)
                    {
                        parrafo = new Paragraph("   Abordaje psicopedagógico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //ABORDAJE PSICOLOGICO
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico)
                    {
                        parrafo = new Paragraph("   Abordaje psicológico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //ABORDAJE FONOAUDIOLOGICO
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico)
                    {
                        parrafo = new Paragraph("   Abordaje fonoaudiológico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //ABORDAJE FISIOTERAPEUTICO
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico)
                    {
                        parrafo = new Paragraph("   Abordaje fisioterapéutico: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //EN SUMA
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    {
                        parrafo = new Paragraph("   En suma: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    //SUGERENCIA
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                    {
                        parrafo = new Paragraph("   Sugerencias: ", fontTitulo2);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                        parrafo = new Paragraph(ElInforme.lstSecciones[i].Contenido.ToString(), fontParrafo);
                        parrafo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parrafo);
                        parrafo = new Paragraph("\n");
                        doc.Add(parrafo);
                    }
                }
                parrafo = new Paragraph("\n");
                doc.Add(parrafo);
                parrafo = new Paragraph("Saluda atte: por CAIDEN,", fontParrafo);
                parrafo.Alignment = Element.ALIGN_LEFT;
                doc.Add(parrafo);
                parrafo = new Paragraph("\n \n \n \n");
                doc.Add(parrafo);



                Chunk chNom = new Chunk();
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_CENTER;
                for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                {
                    if (i > 0)
                    {
                        chNom = new Chunk("               " + ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Nombres + " " + ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Apellidos, fontTitulo2);
                    }
                    else
                    {
                        chNom = new Chunk(ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Nombres + " " + ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Apellidos, fontTitulo2);
                    }
                    parrafo.Add(chNom);
                }
                doc.Add(parrafo);

                Chunk chEspecialidad = new Chunk();
                parrafo = new Paragraph();
                parrafo.Alignment = Element.ALIGN_CENTER;
                bool bExiste = false;
                for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                {
                    if (i > 0)
                    {
                        if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Psicologia")
                        {
                            chEspecialidad = new Chunk("               Lic. en Psicología", fontParrafo);
                            bExiste = true;
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Pedadogia")
                        {
                            chEspecialidad = new Chunk("               Mtra. Pedagoga", fontParrafo);
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Fisioterapia")
                        {
                            chEspecialidad = new Chunk("               Lic. en Fisioterapia", fontParrafo);
                            bExiste = true;
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Fonoaudiologia")
                        {
                            chEspecialidad = new Chunk("               Lic. en Fonoaudiología", fontParrafo);
                            bExiste = true;
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Psicomotricidad")
                        {
                            chEspecialidad = new Chunk("               Lic. en Psicomotricidad", fontParrafo);
                            bExiste = true;
                        }
                    }
                    else
                    {
                        if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Psicologia")
                        {
                            chEspecialidad = new Chunk("Lic. en Psicología", fontParrafo);
                            bExiste = true;
                        }

                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Pedadogia")
                        {
                            chEspecialidad = new Chunk("Mtra. Pedagoga", fontParrafo);
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Fisioterapia")
                        {
                            chEspecialidad = new Chunk("Lic. en Fisioterapia", fontParrafo);
                            bExiste = true;
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Fonoaudiologia")
                        {
                            chEspecialidad = new Chunk("Lic. en Fonoaudiología", fontParrafo);
                            bExiste = true;
                        }
                        else if (ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario.Especialidad.Nombre == "Psicomotricidad")
                        {
                            chEspecialidad = new Chunk("Lic. en Psicomotricidad", fontParrafo);
                            bExiste = true;
                        }

                    }
                    parrafo.Add(chEspecialidad);

                }
                doc.Add(parrafo);

                parrafo = new Paragraph("\n");
                doc.Add(parrafo);
                if (bExiste)
                {
                    parrafo = new Paragraph("*Timbre profesional en poder de la empresa", fontCursiva);
                    parrafo.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(parrafo);
                }



                doc.Close();

                byte[] aBytesStream = ms.ToArray();

                Response.Clear();
                ms = new MemoryStream(aBytesStream);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachemnt;filename=Informe.pdf");
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();

            }
        }
    }
}