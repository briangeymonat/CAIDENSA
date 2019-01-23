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
        static cInforme Informe;
        static List<cDiagnostico> lstTodosDiagnosticos;
        static List<cDiagnostico> lstDiagnosticosAgregados;
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
                lstTodosDiagnosticos = new List<cDiagnostico>();
                lstTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();
                lstDiagnosticosAgregados = new List<cDiagnostico>();
                CargarGrillasDiagnostico();
                txtAntecedentesPatologicos.Enabled = false;
                txtDesarrollo.Enabled = false;
                txtSugerencia.Enabled = false;
                if (vTareas.enproceso)
                {
                    vTareas.enproceso = false;
                    //CargarDatos();
                    CargarDatosEnProceso(); // carga los datos con el contenido de las secciones
                    /*cbAntecedentesPatologicos.Enabled = false;
                    cbDesarrollo.Enabled = false;
                    cbSugerencia.Enabled = false;*/
                }
                else
                {
                    CargarDatosEnProceso();
                    //CargarDatos(); //carga los datos solo del beneficiario
                }
                CargarGrillasDiagnostico();



            }
        }

        protected void CargarDatosEnProceso()
        {
            lblTipo.Text = Informe.Tipo.ToString().Replace("_", " ");

            for (int i = 0; i < Informe.lstSecciones.Count; i++)
            {
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                {
                    txtTitulo.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
            }

            lblNombres.Text = Informe.Beneficiario.Nombres.ToString();
            lblApellidos.Text = Informe.Beneficiario.Apellidos.ToString();
            lblFechaNac.Text = Informe.Beneficiario.FechaNacimiento.ToString();

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

            lblEdad.Text = edadAños + " años y " + edadMeses + " meses";
            lblMotivoConsulta.Text = Informe.Beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = Informe.Beneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(Informe.Beneficiario);
            #region TraerUltimos diagnosticos
            List<string> diagnosticos = dFachada.DiagnosticoTraerUltimosDiagnosticosPorBeneficiario(Informe.Beneficiario);
            string diag = "";
            if (diagnosticos.Count > 0)
            {
                for (int i = 0; i < diagnosticos.Count; i++)
                {
                    if (i == 0)
                        diag = diagnosticos[i];
                    else if (diagnosticos.Count - i == 1)
                        diag = diag + " y " + diagnosticos[i];
                    else
                        diag = diag + ", " + diagnosticos[i];
                }
                lblUltimoDiagnostico.Text = diag;//Funcion de traer ultimo diagnostico del beneficiario
            }
            else
            {
                lblUltimoDiagnostico.Text = "Este beneficiario aún no tiene diagnósticos";
            }
            #endregion

            string dg = "";
            for (int i = 0; i < Informe.lstSecciones.Count; i++)
            {
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Diagnóstico)
                {
                    dg = Informe.lstSecciones[i].Contenido.ToString();
                }
            }
            List<cDiagnostico> lstDiagnosticos = new List<cDiagnostico>();
            cDiagnostico d;
            string[] parts = dg.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                string[] parts2 = parts[i].Split('y'); //por ahora no hay diagnosticos con y pero en caso de haberlo hay que buscar una alternativa
                for (int j = 0; j < parts2.Length; j++)
                {
                    d = new cDiagnostico();
                    d.Tipo = parts2[j].Trim();
                    lstDiagnosticos.Add(d);
                }
            }

            for (int i = 0; i < lstTodosDiagnosticos.Count; i++)
            {
                for (int j = 0; j < lstDiagnosticos.Count; j++)
                {
                    if (lstTodosDiagnosticos[i].Tipo == lstDiagnosticos[j].Tipo)
                    {
                        lstDiagnosticosAgregados.Add(lstTodosDiagnosticos[i]);
                        lstTodosDiagnosticos.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }


            for (int i = 0; i < Informe.lstSecciones.Count; i++)
            {
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                {
                    txtPresentacion.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz && vMiPerfil.U.Especialidad.Nombre == "Psicomotricidad")
                {
                    txtAbordaje.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico && vMiPerfil.U.Especialidad.Nombre == "Pedadogia")
                {
                    txtAbordaje.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico && vMiPerfil.U.Especialidad.Nombre == "Psicologia")
                {
                    txtAbordaje.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico && vMiPerfil.U.Especialidad.Nombre == "Fonoaudiologia")
                {
                    txtAbordaje.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico && vMiPerfil.U.Especialidad.Nombre == "Fisioterapia")
                {
                    txtAbordaje.Text = Informe.lstSecciones[i].Contenido.ToString();
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                {
                    txtEnsuma.Text = Informe.lstSecciones[i].Contenido.ToString();
                }


                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                {
                    txtAntecedentesPatologicos.Text = Informe.lstSecciones[i].Contenido.ToString();
                    txtAntecedentesPatologicos.Enabled = true;
                    cbAntecedentesPatologicos.Checked = true;
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                {
                    txtDesarrollo.Text = Informe.lstSecciones[i].Contenido.ToString();
                    txtDesarrollo.Enabled = true;
                    cbDesarrollo.Checked = true;
                }
                if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                {
                    txtSugerencia.Text = Informe.lstSecciones[i].Contenido.ToString();
                    txtSugerencia.Enabled = true;
                    cbSugerencia.Checked = true;
                }
            }






        }

        protected void CargarDatos()
        {

            lblTipo.Text = Informe.Tipo.ToString().Replace("_", " ");

            lblNombres.Text = Informe.Beneficiario.Nombres.ToString();
            lblApellidos.Text = Informe.Beneficiario.Apellidos.ToString();
            lblFechaNac.Text = Informe.Beneficiario.FechaNacimiento.ToString();

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

            lblEdad.Text = edadAños + " años y " + edadMeses + " meses";
            lblMotivoConsulta.Text = Informe.Beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = Informe.Beneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(Informe.Beneficiario);
            List<string> diagnosticos = dFachada.DiagnosticoTraerUltimosDiagnosticosPorBeneficiario(Informe.Beneficiario);
            string diag = "";
            if (diagnosticos.Count > 0)
            {
                for (int i = 0; i < diagnosticos.Count; i++)
                {
                    if (i == 0)
                        diag = diagnosticos[i];
                    else if (diagnosticos.Count - i == 1)
                        diag = diag + " y " + diagnosticos[i];
                    else
                        diag = diag + ", " + diagnosticos[i];
                }
                lblUltimoDiagnostico.Text = diag;//Funcion de traer ultimo diagnostico del beneficiario
            }
            else
            {
                lblUltimoDiagnostico.Text = "Este beneficiario aún no tiene diagnósticos";
            }



        }
        protected void CargarGrillasDiagnostico()
        {
            grdTodosDiagnosticos.DataSource = lstTodosDiagnosticos;
            grdTodosDiagnosticos.DataBind();
            grdTodosDiagnosticos.SelectedIndex = -1;
            grdDiagnosticosAgregados.DataSource = lstDiagnosticosAgregados;
            grdDiagnosticosAgregados.DataBind();
            grdDiagnosticosAgregados.SelectedIndex = -1;
        }

        protected void grdTodosDiagnosticos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaCodigo = grdTodosDiagnosticos.Rows[e.NewSelectedIndex].Cells[1];
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstTodosDiagnosticos.Count; i++)
            {
                if (lstTodosDiagnosticos[i].Codigo == codigo)
                {
                    lstDiagnosticosAgregados.Add(lstTodosDiagnosticos[i]);
                    lstTodosDiagnosticos.RemoveAt(i);
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
            int codigo = int.Parse(celdaCodigo.Text);
            for (int i = 0; i < lstDiagnosticosAgregados.Count; i++)
            {
                if (lstDiagnosticosAgregados[i].Codigo == codigo)
                {
                    lstTodosDiagnosticos.Add(lstDiagnosticosAgregados[i]);
                    lstDiagnosticosAgregados.RemoveAt(i);
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
            bool a = false;
            bool d = false;
            bool s = false;
            if (FaltanDatos())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos.')", true);
            }
            else
            {

                #region Cargando el contenido de las secciones obligatorias

                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                    {
                        Informe.lstSecciones[i].Contenido = txtTitulo.Text;

                    }
                    else if(Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Encuadre)
                    {
                        Informe.lstSecciones[i].Contenido = lblEncuadre.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Diagnóstico)
                    {
                        string diag = "";
                        if (lstDiagnosticosAgregados.Count > 0)
                        {
                            for (int j = 0; j < lstDiagnosticosAgregados.Count; j++)
                            {
                                if (j == 0)
                                    diag = lstDiagnosticosAgregados[j].Tipo;
                                else if (lstDiagnosticosAgregados.Count - j == 1)
                                    diag = diag + " y " + lstDiagnosticosAgregados[j].Tipo;
                                else
                                    diag = diag + ", " + lstDiagnosticosAgregados[j].Tipo;
                            }
                            Informe.lstSecciones[i].Contenido = diag;
                        }

                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    {
                        Informe.lstSecciones[i].Contenido = txtPresentacion.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz && vMiPerfil.U.Especialidad.Nombre == "Psicomotricidad")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico && vMiPerfil.U.Especialidad.Nombre == "Pedadogia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico && vMiPerfil.U.Especialidad.Nombre == "Psicologia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico && vMiPerfil.U.Especialidad.Nombre == "Fonoaudiologia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico && vMiPerfil.U.Especialidad.Nombre == "Fisioterapia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    {
                        Informe.lstSecciones[i].Contenido = txtEnsuma.Text;
                    }

                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                    {                        
                        Informe.lstSecciones[i].Contenido = txtAntecedentesPatologicos.Text;
                        a = true;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                    {
                        Informe.lstSecciones[i].Contenido = txtDesarrollo.Text;
                        d = true;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                    {
                        Informe.lstSecciones[i].Contenido = txtSugerencia.Text;
                        s = true;
                    }



                }
                #endregion


                #region Agregar las secciones opcionales
                if (!a)
                {
                    if (cbAntecedentesPatologicos.Checked)
                    {
                        cSeccion seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < Informe.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion us = new cUsuarioSeccion();
                            us.Usuario = new cUsuario();
                            us.Usuario = Informe.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            us.Estado = cUtilidades.EstadoInforme.EnProceso;
                            seccion.lstUsuariosSeccion.Add(us);
                            seccion.Contenido = txtAntecedentesPatologicos.Text;
                        }
                        Informe.lstSecciones.Add(seccion);
                    }
                }
                if (!d)
                {
                    if (cbDesarrollo.Checked)
                    {
                        cSeccion seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Desarrollo;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < Informe.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion us = new cUsuarioSeccion();
                            us.Usuario = new cUsuario();
                            us.Usuario = Informe.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            us.Estado = cUtilidades.EstadoInforme.EnProceso;
                            seccion.lstUsuariosSeccion.Add(us);
                            seccion.Contenido = txtDesarrollo.Text;
                        }
                        Informe.lstSecciones.Add(seccion);
                    }
                }

                if (!s)
                {
                    if (cbSugerencia.Checked)
                    {
                        cSeccion seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Sugerencias;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < Informe.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion us = new cUsuarioSeccion();
                            us.Usuario = new cUsuario();
                            us.Usuario = Informe.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            us.Estado = cUtilidades.EstadoInforme.EnProceso;
                            seccion.lstUsuariosSeccion.Add(us);
                            seccion.Contenido = txtSugerencia.Text;
                        }
                        Informe.lstSecciones.Add(seccion);
                    }
                }

                #endregion

                try
                {
                    bool resultado = dFachada.InformeRedactar(Informe);
                    if (resultado)
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
            bool retorno;
            if (txtTitulo.Text == "" || txtPresentacion.Text == "" || txtAbordaje.Text == "" || txtEnsuma.Text == "" || lstDiagnosticosAgregados.Count < 1)
            {
                retorno = true;
            }
            else
            {
                if (cbAntecedentesPatologicos.Checked && txtAntecedentesPatologicos.Text == "")
                    retorno = true;
                if (cbDesarrollo.Checked && txtDesarrollo.Text == "")
                    retorno = true;
                if (cbSugerencia.Checked && txtSugerencia.Text == "")
                    retorno = true;
                retorno = false;
            }
            return retorno;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            bool a = false;
            bool d = false;
            bool s = false;
            if (FaltanDatos())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Faltan datos.')", true);
            }
            else
            {

                #region Cargando el contenido de las secciones obligatorias

                for (int i = 0; i < Informe.lstSecciones.Count; i++)
                {
                    if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Título)
                    {
                        Informe.lstSecciones[i].Contenido = txtTitulo.Text;

                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Encuadre)
                    {
                        Informe.lstSecciones[i].Contenido = lblEncuadre.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Diagnóstico)
                    {
                        string diag = "";
                        if (lstDiagnosticosAgregados.Count > 0)
                        {
                            for (int j = 0; j < lstDiagnosticosAgregados.Count; j++)
                            {
                                if (j == 0)
                                    diag = lstDiagnosticosAgregados[j].Tipo;
                                else if (lstDiagnosticosAgregados.Count - j == 1)
                                    diag = diag + " y " + lstDiagnosticosAgregados[j].Tipo;
                                else
                                    diag = diag + ", " + lstDiagnosticosAgregados[j].Tipo;
                            }
                            Informe.lstSecciones[i].Contenido = diag;
                        }

                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Presentación)
                    {
                        Informe.lstSecciones[i].Contenido = txtPresentacion.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicomotriz && vMiPerfil.U.Especialidad.Nombre == "Psicomotricidad")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Pedagógico && vMiPerfil.U.Especialidad.Nombre == "Pedadogia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Psicológico && vMiPerfil.U.Especialidad.Nombre == "Psicologia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico && vMiPerfil.U.Especialidad.Nombre == "Fonoaudiologia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico && vMiPerfil.U.Especialidad.Nombre == "Fisioterapia")
                    {
                        Informe.lstSecciones[i].Contenido = txtAbordaje.Text;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.En_Suma)
                    {
                        Informe.lstSecciones[i].Contenido = txtEnsuma.Text;
                    }

                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Antecedentes_patológicos)
                    {
                        Informe.lstSecciones[i].Contenido = txtAntecedentesPatologicos.Text;
                        a = true;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Desarrollo)
                    {
                        Informe.lstSecciones[i].Contenido = txtDesarrollo.Text;
                        d = true;
                    }
                    else if (Informe.lstSecciones[i].Nombre == cUtilidades.NombreSeccion.Sugerencias)
                    {
                        Informe.lstSecciones[i].Contenido = txtSugerencia.Text;
                        s = true;
                    }



                }
                #endregion


                #region Agregar las secciones opcionales
                if (!a)
                {
                    if (cbAntecedentesPatologicos.Checked)
                    {
                        cSeccion seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < Informe.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion us = new cUsuarioSeccion();
                            us.Usuario = new cUsuario();
                            us.Usuario = Informe.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            us.Estado = cUtilidades.EstadoInforme.EnProceso;
                            seccion.lstUsuariosSeccion.Add(us);
                            seccion.Contenido = txtAntecedentesPatologicos.Text;
                        }
                        Informe.lstSecciones.Add(seccion);
                    }
                }
                if (!d)
                {
                    if (cbDesarrollo.Checked)
                    {
                        cSeccion seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Desarrollo;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < Informe.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion us = new cUsuarioSeccion();
                            us.Usuario = new cUsuario();
                            us.Usuario = Informe.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            us.Estado = cUtilidades.EstadoInforme.EnProceso;
                            seccion.lstUsuariosSeccion.Add(us);
                            seccion.Contenido = txtDesarrollo.Text;
                        }
                        Informe.lstSecciones.Add(seccion);
                    }
                }

                if (!s)
                {
                    if (cbSugerencia.Checked)
                    {
                        cSeccion seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Sugerencias;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        for (int i = 0; i < Informe.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            cUsuarioSeccion us = new cUsuarioSeccion();
                            us.Usuario = new cUsuario();
                            us.Usuario = Informe.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            us.Estado = cUtilidades.EstadoInforme.EnProceso;
                            seccion.lstUsuariosSeccion.Add(us);
                            seccion.Contenido = txtSugerencia.Text;
                        }
                        Informe.lstSecciones.Add(seccion);
                    }
                }

                #endregion

                try
                {
                    int i = dFachada.InformeVerificarSeccionesTerminadas(Informe, vMiPerfil.U);
                    if(i==0)
                    {
                        bool resultado = dFachada.InformeFinalizar(Informe);
                        if (resultado)
                        {
                            Informe.Beneficiario.lstDiagnosticos = new List<cDiagnosticoBeneficiario>();
                            cDiagnosticoBeneficiario db;
                            for(int j=0;j<lstDiagnosticosAgregados.Count;j++)
                            {
                                db = new cDiagnosticoBeneficiario();
                                db.Diagnostico = lstDiagnosticosAgregados[j];
                                db.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                                Informe.Beneficiario.lstDiagnosticos.Add(db);
                            }
                            bool res = dFachada.DiagnosticoAgregarDiagnosticoBeneficiario(Informe.Beneficiario);
                            if(res)
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
                        bool res = dFachada.InformeFinalizarSecciones(Informe, vMiPerfil.U);
                        if(res)
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