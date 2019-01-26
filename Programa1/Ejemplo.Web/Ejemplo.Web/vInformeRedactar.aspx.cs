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
    public partial class vInformeRedactar : System.Web.UI.Page
    {
        static cInforme ElInforme;
        static List<cDiagnostico> LosDiagnosticos;
        static List<cDiagnostico> LosDiagnosticosAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                LosDiagnosticos = new List<cDiagnostico>();
                LosDiagnosticos = dFachada.DiagnosticoTraerTodos();
                LosDiagnosticosAgregados = new List<cDiagnostico>();
                CargarGrillasDiagnostico();
                txtAntecedentesPatologicos.Enabled = false;
                txtDesarrollo.Enabled = false;
                txtSugerencia.Enabled = false;
                CargarDatosEnProceso();
                CargarGrillasDiagnostico();
            }
        }

        protected void CargarDatosEnProceso()
        {
            lblTipo.Text = ElInforme.Tipo.ToString().Replace("_", " ");

            for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
            {
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                {
                    txtTitulo.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
            }

            lblNombres.Text = ElInforme.Beneficiario.Nombres.ToString();
            lblApellidos.Text = ElInforme.Beneficiario.Apellidos.ToString();
            lblFechaNac.Text = ElInforme.Beneficiario.FechaNacimiento.ToString();
            lblCI.Text = ElInforme.Beneficiario.CI.ToString();
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

            lblEdad.Text = iEdadAños + " años y " + iEdadMeses + " meses";
            lblMotivoConsulta.Text = ElInforme.Beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = ElInforme.Beneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(ElInforme.Beneficiario);
            #region TraerUltimos diagnosticos
            List<string> lstsDiagnosticos = dFachada.DiagnosticoTraerUltimosDiagnosticosPorBeneficiario(ElInforme.Beneficiario);
            string sDiag = "";
            if (lstsDiagnosticos.Count > 0)
            {
                for (int i = 0; i < lstsDiagnosticos.Count; i++)
                {
                    if (i == 0)
                        sDiag = lstsDiagnosticos[i];
                    else if (lstsDiagnosticos.Count - i == 1)
                        sDiag = sDiag + " y " + lstsDiagnosticos[i];
                    else
                        sDiag = sDiag + ", " + lstsDiagnosticos[i];
                }
                lblUltimoDiagnostico.Text = sDiag;//Funcion de traer ultimo diagnostico del beneficiario
            }
            else
            {
                lblUltimoDiagnostico.Text = "Este beneficiario aún no tiene diagnósticos";
            }
            #endregion

            string sDg = "";
            for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
            {
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Diagnóstico)
                {
                    sDg = ElInforme.lstSecciones[i].Contenido.ToString();
                }
            }
            List<cDiagnostico> lstDiagnosticos = new List<cDiagnostico>();
            cDiagnostico unDiagnostico;
            string[] aParts = sDg.Split(',');
            for (int i = 0; i < aParts.Length; i++)
            {
                string[] aParts2 = aParts[i].Split('y'); //por ahora no hay diagnosticos con y pero en caso de haberlo hay que buscar una alternativa
                for (int j = 0; j < aParts2.Length; j++)
                {
                    unDiagnostico = new cDiagnostico();
                    unDiagnostico.Tipo = aParts2[j].Trim();
                    lstDiagnosticos.Add(unDiagnostico);
                }
            }

            for (int i = 0; i < LosDiagnosticos.Count; i++)
            {
                for (int j = 0; j < lstDiagnosticos.Count; j++)
                {
                    if (LosDiagnosticos[i].Tipo == lstDiagnosticos[j].Tipo)
                    {
                        LosDiagnosticosAgregados.Add(LosDiagnosticos[i]);
                        LosDiagnosticos.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }


            for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
            {
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                {
                    txtPresentacion.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz && vMiPerfil.U.Especialidad.Nombre == "Psicomotricidad")
                {
                    txtAbordaje.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico && vMiPerfil.U.Especialidad.Nombre == "Pedadogia")
                {
                    txtAbordaje.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico && vMiPerfil.U.Especialidad.Nombre == "Psicologia")
                {
                    txtAbordaje.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico && vMiPerfil.U.Especialidad.Nombre == "Fonoaudiologia")
                {
                    txtAbordaje.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico && vMiPerfil.U.Especialidad.Nombre == "Fisioterapia")
                {
                    txtAbordaje.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                {
                    txtEnsuma.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                }


                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                {
                    txtAntecedentesPatologicos.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                    txtAntecedentesPatologicos.Enabled = true;
                    cbAntecedentesPatologicos.Checked = true;
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                {
                    txtDesarrollo.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                    txtDesarrollo.Enabled = true;
                    cbDesarrollo.Checked = true;
                }
                if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                {
                    txtSugerencia.Text = ElInforme.lstSecciones[i].Contenido.ToString();
                    txtSugerencia.Enabled = true;
                    cbSugerencia.Checked = true;
                }
            }






        }

        protected void CargarDatos()
        {

            lblTipo.Text = ElInforme.Tipo.ToString().Replace("_", " ");

            lblNombres.Text = ElInforme.Beneficiario.Nombres.ToString();
            lblApellidos.Text = ElInforme.Beneficiario.Apellidos.ToString();
            lblFechaNac.Text = ElInforme.Beneficiario.FechaNacimiento.ToString();
            lblCI.Text = ElInforme.Beneficiario.CI.ToString();

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

            lblEdad.Text = iEdadAños + " años y " + iEdadMeses + " meses";
            lblMotivoConsulta.Text = ElInforme.Beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = ElInforme.Beneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(ElInforme.Beneficiario);
            List<string> lstDiagnosticos = dFachada.DiagnosticoTraerUltimosDiagnosticosPorBeneficiario(ElInforme.Beneficiario);
            string sDiag = "";
            if (lstDiagnosticos.Count > 0)
            {
                for (int i = 0; i < lstDiagnosticos.Count; i++)
                {
                    if (i == 0)
                        sDiag = lstDiagnosticos[i];
                    else if (lstDiagnosticos.Count - i == 1)
                        sDiag = sDiag + " y " + lstDiagnosticos[i];
                    else
                        sDiag = sDiag + ", " + lstDiagnosticos[i];
                }
                lblUltimoDiagnostico.Text = sDiag;//Funcion de traer ultimo diagnostico del beneficiario
            }
            else
            {
                lblUltimoDiagnostico.Text = "Este beneficiario aún no tiene diagnósticos";
            }



        }
        protected void CargarGrillasDiagnostico()
        {
            grdTodosDiagnosticos.DataSource = LosDiagnosticos;
            grdTodosDiagnosticos.DataBind();
            grdTodosDiagnosticos.SelectedIndex = -1;
            grdDiagnosticosAgregados.DataSource = LosDiagnosticosAgregados;
            grdDiagnosticosAgregados.DataBind();
            grdDiagnosticosAgregados.SelectedIndex = -1;
        }

        protected void grdTodosDiagnosticos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosDiagnosticos.Rows[e.NewSelectedIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosDiagnosticos.Count; i++)
            {
                if (LosDiagnosticos[i].Codigo == iCodigo)
                {
                    LosDiagnosticosAgregados.Add(LosDiagnosticos[i]);
                    LosDiagnosticos.RemoveAt(i);
                }
            }
            CargarGrillasDiagnostico();
        }

        protected void grdTodosDiagnosticos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
        }

        protected void grdDiagnosticosAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaCodigo = grdDiagnosticosAgregados.Rows[e.RowIndex].Cells[1];
            int iCodigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < LosDiagnosticosAgregados.Count; i++)
            {
                if (LosDiagnosticosAgregados[i].Codigo == iCodigo)
                {
                    LosDiagnosticos.Add(LosDiagnosticosAgregados[i]);
                    LosDiagnosticosAgregados.RemoveAt(i);
                }
            }
            CargarGrillasDiagnostico();
        }

        protected void grdDiagnosticosAgregados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
        }

        protected void cbAntecedentesPatologicos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAntecedentesPatologicos.Checked)
                txtAntecedentesPatologicos.Enabled = true;
            else
                txtAntecedentesPatologicos.Enabled = false;
        }

        protected void cbDesarrollo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDesarrollo.Checked)
                txtDesarrollo.Enabled = true;
            else
                txtDesarrollo.Enabled = false;
        }

        protected void cbSugerencia_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSugerencia.Checked)
                txtSugerencia.Enabled = true;
            else
                txtSugerencia.Enabled = false;
        }

        protected void btnSalirSinGuardar_Click(object sender, EventArgs e)
        {
            Response.Redirect("vTareas.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bA = false;
            bool bD = false;
            bool bS = false;
            if (FaltanDatos())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos.')", true);
            }
            else
            {

                #region Cargando el contenido de las secciones obligatorias

                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtTitulo.Text;

                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Encuadre)
                    {
                        ElInforme.lstSecciones[i].Contenido = lblEncuadre.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Diagnóstico)
                    {
                        string sDiag = "";
                        if (LosDiagnosticosAgregados.Count > 0)
                        {
                            for (int j = 0; j < LosDiagnosticosAgregados.Count; j++)
                            {
                                if (j == 0)
                                    sDiag = LosDiagnosticosAgregados[j].Tipo;
                                else if (LosDiagnosticosAgregados.Count - j == 1)
                                    sDiag = sDiag + " y " + LosDiagnosticosAgregados[j].Tipo;
                                else
                                    sDiag = sDiag + ", " + LosDiagnosticosAgregados[j].Tipo;
                            }
                            ElInforme.lstSecciones[i].Contenido = sDiag;
                        }

                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtPresentacion.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz && vMiPerfil.U.Especialidad.Nombre == "Psicomotricidad")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico && vMiPerfil.U.Especialidad.Nombre == "Pedadogia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico && vMiPerfil.U.Especialidad.Nombre == "Psicologia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico && vMiPerfil.U.Especialidad.Nombre == "Fonoaudiologia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico && vMiPerfil.U.Especialidad.Nombre == "Fisioterapia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtEnsuma.Text;
                    }

                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAntecedentesPatologicos.Text;
                        bA = true;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtDesarrollo.Text;
                        bD = true;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtSugerencia.Text;
                        bS = true;
                    }



                }
                #endregion


                #region Agregar las secciones opcionales
                if (!bA)
                {
                    if (cbAntecedentesPatologicos.Checked)
                    {
                        cSeccion unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion unUS = new cUsuarioSeccion();
                            unUS.Usuario = new cUsuario();
                            unUS.Usuario = ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                            unaSeccion.lstUsuariosSeccion.Add(unUS);
                            unaSeccion.Contenido = txtAntecedentesPatologicos.Text;
                        }
                        ElInforme.lstSecciones.Add(unaSeccion);
                    }
                }
                if (!bD)
                {
                    if (cbDesarrollo.Checked)
                    {
                        cSeccion unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Desarrollo;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion unUS = new cUsuarioSeccion();
                            unUS.Usuario = new cUsuario();
                            unUS.Usuario = ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                            unaSeccion.lstUsuariosSeccion.Add(unUS);
                            unaSeccion.Contenido = txtDesarrollo.Text;
                        }
                        ElInforme.lstSecciones.Add(unaSeccion);
                    }
                }

                if (!bS)
                {
                    if (cbSugerencia.Checked)
                    {
                        cSeccion unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Sugerencias;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion unUS = new cUsuarioSeccion();
                            unUS.Usuario = new cUsuario();
                            unUS.Usuario = ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                            unaSeccion.lstUsuariosSeccion.Add(unUS);
                            unaSeccion.Contenido = txtSugerencia.Text;
                        }
                        ElInforme.lstSecciones.Add(unaSeccion);
                    }
                }

                #endregion

                try
                {
                    bool bResultado = dFachada.InformeRedactar(ElInforme);
                    if (bResultado)
                    {
                        Response.Redirect("vTareas.aspx");

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('No se pudo redactar el informe')", true);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        protected bool FaltanDatos()
        {
            bool bRetorno;
            if (txtTitulo.Text == "" || txtPresentacion.Text == "" || txtAbordaje.Text == "" || txtEnsuma.Text == "" || LosDiagnosticosAgregados.Count < 1)
            {
                bRetorno = true;
            }
            else
            {
                if (cbAntecedentesPatologicos.Checked && txtAntecedentesPatologicos.Text == "")
                    bRetorno = true;
                if (cbDesarrollo.Checked && txtDesarrollo.Text == "")
                    bRetorno = true;
                if (cbSugerencia.Checked && txtSugerencia.Text == "")
                    bRetorno = true;
                bRetorno = false;
            }
            return bRetorno;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            bool bA = false;
            bool bD = false;
            bool bS = false;
            if (FaltanDatos())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos.')", true);
            }
            else
            {

                #region Cargando el contenido de las secciones obligatorias

                for (int i = 0; i < ElInforme.lstSecciones.Count; i++)
                {
                    if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtTitulo.Text;

                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Encuadre)
                    {
                        ElInforme.lstSecciones[i].Contenido = lblEncuadre.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Diagnóstico)
                    {
                        string sDiag = "";
                        if (LosDiagnosticosAgregados.Count > 0)
                        {
                            for (int j = 0; j < LosDiagnosticosAgregados.Count; j++)
                            {
                                if (j == 0)
                                    sDiag = LosDiagnosticosAgregados[j].Tipo;
                                else if (LosDiagnosticosAgregados.Count - j == 1)
                                    sDiag = sDiag + " y " + LosDiagnosticosAgregados[j].Tipo;
                                else
                                    sDiag = sDiag + ", " + LosDiagnosticosAgregados[j].Tipo;
                            }
                            ElInforme.lstSecciones[i].Contenido = sDiag;
                        }

                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtPresentacion.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz && vMiPerfil.U.Especialidad.Nombre == "Psicomotricidad")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico && vMiPerfil.U.Especialidad.Nombre == "Pedadogia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico && vMiPerfil.U.Especialidad.Nombre == "Psicologia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico && vMiPerfil.U.Especialidad.Nombre == "Fonoaudiologia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico && vMiPerfil.U.Especialidad.Nombre == "Fisioterapia")
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtEnsuma.Text;
                    }

                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtAntecedentesPatologicos.Text;
                        bA = true;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtDesarrollo.Text;
                        bD = true;
                    }
                    else if (ElInforme.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                    {
                        ElInforme.lstSecciones[i].Contenido = txtSugerencia.Text;
                        bS = true;
                    }



                }
                #endregion


                #region Agregar las secciones opcionales
                if (!bA)
                {
                    if (cbAntecedentesPatologicos.Checked)
                    {
                        cSeccion unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion unUS = new cUsuarioSeccion();
                            unUS.Usuario = new cUsuario();
                            unUS.Usuario = ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                            unaSeccion.lstUsuariosSeccion.Add(unUS);
                            unaSeccion.Contenido = txtAntecedentesPatologicos.Text;
                        }
                        ElInforme.lstSecciones.Add(unaSeccion);
                    }
                }
                if (!bD)
                {
                    if (cbDesarrollo.Checked)
                    {
                        cSeccion unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Desarrollo;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion unUS = new cUsuarioSeccion();
                            unUS.Usuario = new cUsuario();
                            unUS.Usuario = ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                            unaSeccion.lstUsuariosSeccion.Add(unUS);
                            unaSeccion.Contenido = txtDesarrollo.Text;
                        }
                        ElInforme.lstSecciones.Add(unaSeccion);
                    }
                }

                if (!bS)
                {
                    if (cbSugerencia.Checked)
                    {
                        cSeccion unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Sugerencias;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < ElInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion unUS = new cUsuarioSeccion();
                            unUS.Usuario = new cUsuario();
                            unUS.Usuario = ElInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                            unaSeccion.lstUsuariosSeccion.Add(unUS);
                            unaSeccion.Contenido = txtSugerencia.Text;
                        }
                        ElInforme.lstSecciones.Add(unaSeccion);
                    }
                }

                #endregion

                try
                {
                    int i = dFachada.InformeVerificarSeccionesTerminadas(ElInforme, vMiPerfil.U);
                    if (i == 0)
                    {
                        bool bResultado = dFachada.InformeFinalizar(ElInforme);
                        if (bResultado)
                        {
                            ElInforme.Beneficiario.lstDiagnosticos = new List<cDiagnosticoBeneficiario>();
                            cDiagnosticoBeneficiario unDB;
                            for (int j = 0; j < LosDiagnosticosAgregados.Count; j++)
                            {
                                unDB = new cDiagnosticoBeneficiario();
                                unDB.Diagnostico = LosDiagnosticosAgregados[j];
                                unDB.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                                ElInforme.Beneficiario.lstDiagnosticos.Add(unDB);
                            }
                            bool bRes = dFachada.DiagnosticoAgregarDiagnosticoBeneficiario(ElInforme.Beneficiario);
                            if (bRes)
                            {
                                Response.Redirect("vTareas.aspx");
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('No se pudo agregar los diagnosticos al beneficiario')", true);
                            }

                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('No se pudo finalizar el informe')", true);

                        }
                    }
                    else
                    {
                        bool bRes = dFachada.InformeFinalizarSecciones(ElInforme, vMiPerfil.U);
                        if (bRes)
                        {
                            Response.Redirect("vTareas.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('No se pudo finalizar el informe')", true);

                        }

                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}