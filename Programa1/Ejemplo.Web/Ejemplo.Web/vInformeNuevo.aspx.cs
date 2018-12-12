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
    public partial class vInformeNuevo : System.Web.UI.Page
    {
        static cBeneficiario beneficiario;
        static List<cUsuario> lstTodosEspecialistas;
        static List<cUsuario> lstEspecialistasAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lstTodosEspecialistas = new List<cUsuario>();
                lstEspecialistasAgregados = new List<cUsuario>();
                lstTodosEspecialistas = dFachada.UsuarioTraerTodosEspecialistasActivos();


                CargarCombos();
                CargarGrillas();
                cargarDatos();
            }
        }
        protected void cargarDatos()
        {
            beneficiario = new cBeneficiario();
            beneficiario.Codigo = int.Parse(Request.QueryString["idBeneficiario"]);
            beneficiario = dFachada.BeneficiarioTraerEspecifico(beneficiario);
            this.lblNombres.Text = beneficiario.Nombres.ToString();
            this.lblApellidos.Text = beneficiario.Apellidos.ToString();
            this.lblFechaNac.Text = beneficiario.FechaNacimiento.ToString("dd-MM-yyyy");
            #region Hallar la edad cronológica edadAños, edadMeses, edadDias
            int año = beneficiario.FechaNacimiento.Year;
            int mes = beneficiario.FechaNacimiento.Month;
            int dia = beneficiario.FechaNacimiento.Day;
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
            this.lblEdad.Text = edadAños + " años y " + edadMeses + " meses";
            lblMotivoConsulta.Text = beneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = beneficiario.Escolaridad.ToString();
            //lblEncuadre.Text = cuando se haga el itinerario
            //falta ocultar algunas columnas de la grilla y los botones agregar y quitar, al igual que realizar informe.


        }


        protected void CargarCombos()
        {
            List<string> lstEnums = new List<string>();
            foreach (var item in Enum.GetValues(typeof(cUtilidades.TipoInforme)))
            {
                lstEnums.Add(item.ToString());
            }
            for (int i = 0; i < lstEnums.Count; i++)
            {
                lstEnums[i] = lstEnums[i].Replace("_", " ");
            }
            ddlTipo.DataSource = lstEnums;
            ddlTipo.DataBind();


            List<cEspecialidad> especialidades = new List<cEspecialidad>();
            especialidades = dFachada.EspecialidadTraerTodas();
            for (int i = 0; i < especialidades.Count; i++)
            {
                if (especialidades[i].Nombre == "Sin especialidad")
                {
                    especialidades.RemoveAt(i);
                }
            }
            ddlEspecialidad.DataSource = especialidades;
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Codigo";
            ddlEspecialidad.DataBind();
        }

        protected void CargarGrillas()
        {
            cEspecialidad especialidad = new cEspecialidad();
            especialidad.Codigo = int.Parse(ddlEspecialidad.SelectedValue);
            List<cUsuario> lstMostrar = new List<cUsuario>();

            for (int i = 0; i < lstTodosEspecialistas.Count; i++)
            {
                if (lstTodosEspecialistas[i].Especialidad.Codigo == especialidad.Codigo)
                {
                    lstMostrar.Add(lstTodosEspecialistas[i]);
                }
            }
            this.grdTodosEspecialistas.DataSource = lstMostrar;
            this.grdTodosEspecialistas.DataBind();


            this.grdEspecialistasAgregados.DataSource = lstEspecialistasAgregados;
            this.grdEspecialistasAgregados.DataBind();

        }

        protected void grdTodosEspecialistas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[7].Visible = false;//tipo
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//tel
            e.Row.Cells[11].Visible = false;//email
            e.Row.Cells[12].Visible = false;//estado
            e.Row.Cells[13].Visible = false;//tipo de contrato
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cEspecialidad especialidad = new cEspecialidad();
            especialidad.Codigo = int.Parse(ddlEspecialidad.SelectedValue);
            List<cUsuario> lstMostrar = new List<cUsuario>();

            for (int i = 0; i < lstTodosEspecialistas.Count; i++)
            {
                if (lstTodosEspecialistas[i].Especialidad.Codigo == especialidad.Codigo)
                {
                    lstMostrar.Add(lstTodosEspecialistas[i]);
                }
            }
            this.grdTodosEspecialistas.DataSource = lstMostrar;
            this.grdTodosEspecialistas.DataBind();

            this.grdEspecialistasAgregados.SelectedIndex = -1;
            this.grdTodosEspecialistas.SelectedIndex = -1;

        }

        protected void grdTodosEspecialistas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaNickName = grdTodosEspecialistas.Rows[e.NewSelectedIndex].Cells[2];
            string NickName = string.Format(celdaNickName.Text);
            for (int i = 0; i < lstTodosEspecialistas.Count; i++)
            {
                if (lstTodosEspecialistas[i].NickName == NickName)
                {
                    lstEspecialistasAgregados.Add(lstTodosEspecialistas[i]);
                    lstTodosEspecialistas.RemoveAt(i);
                }
            }
            CargarGrillas();
        }

        protected void grdEspecialistasAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaNickName = grdEspecialistasAgregados.Rows[e.RowIndex].Cells[2];
            string NickName = string.Format(celdaNickName.Text);
            for (int i = 0; i < lstEspecialistasAgregados.Count; i++)
            {
                if (lstEspecialistasAgregados[i].NickName == NickName)
                {
                    lstTodosEspecialistas.Add(lstEspecialistasAgregados[i]);
                    lstEspecialistasAgregados.RemoveAt(i);
                }
            }
            CargarGrillas();
        }

        protected void grdEspecialistasAgregados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[7].Visible = false;//tipo
            e.Row.Cells[8].Visible = false;//domicilio
            e.Row.Cells[9].Visible = false;//fecha de nacimiento
            e.Row.Cells[10].Visible = false;//tel
            e.Row.Cells[11].Visible = false;//email
            e.Row.Cells[12].Visible = false;//estado
            e.Row.Cells[13].Visible = false;//tipo de contrato            
        }

        protected void btnRealizarInforme_Click(object sender, EventArgs e)
        {
            cInforme informe = new cInforme();
            if (ddlTipo.SelectedValue == "Evaluacion Psicomotriz")
                informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
            else if (ddlTipo.SelectedValue == "Evaluacion Psicopedagogica")
                informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
            else if (ddlTipo.SelectedValue == "Evaluacion Psicologica")
                informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
            else if (ddlTipo.SelectedValue == "Evaluacion Fonoaudiologa")
                informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
            else if (ddlTipo.SelectedValue == "Evolucion")
                informe.Tipo = cUtilidades.TipoInforme.Evolucion;
            else if (ddlTipo.SelectedValue == "Tolerancia Curricular")
                informe.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
            else if (ddlTipo.SelectedValue == "Juzgado")
                informe.Tipo = cUtilidades.TipoInforme.Juzgado;
            else if (ddlTipo.SelectedValue == "Interdiciplinario")
                informe.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
            else if (ddlTipo.SelectedValue == "Otro")
                informe.Tipo = cUtilidades.TipoInforme.Otro;

            informe.Beneficiario = new cBeneficiario();
            informe.Beneficiario = beneficiario;

            informe.lstSecciones = new List<cSeccion>();
            if (lstEspecialistasAgregados.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El informe debe contar con al menos un especialista que realice el mismo')", true);
            }
            else
            {

                #region Titulo
                cSeccion seccion = new cSeccion();
                seccion.Nombre = cUtilidades.NombreSeccion.Título;
                seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < lstEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion us = new cUsuarioSeccion();
                    us.Usuario = new cUsuario();
                    us.Usuario = lstEspecialistasAgregados[i];
                    us.Estado = 0;
                    seccion.lstUsuariosSeccion.Add(us);
                }
                informe.lstSecciones.Add(seccion);
                #endregion
                #region Encuadre
                seccion = new cSeccion();
                seccion.Nombre = cUtilidades.NombreSeccion.Encuadre;
                seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                //CUANDO SE TENGA EL ITINERARIO EL CONTENIDO DE ESTA SECCION SE REALIZARÁ AQUI
                informe.lstSecciones.Add(seccion);
                #endregion
                #region Diagnostico
                seccion = new cSeccion();
                seccion.Nombre = cUtilidades.NombreSeccion.Diagnóstico;
                seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < lstEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion us = new cUsuarioSeccion();
                    us.Usuario = new cUsuario();
                    us.Usuario = lstEspecialistasAgregados[i];
                    us.Estado = 0;
                    seccion.lstUsuariosSeccion.Add(us);
                }
                informe.lstSecciones.Add(seccion);
                #endregion
                #region Abordajes
                for (int a = 0; a < lstEspecialistasAgregados.Count; a++)
                {
                    if (lstEspecialistasAgregados[a].Especialidad.Nombre == "Psicologia")
                    {
                        seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion us = new cUsuarioSeccion();
                        us.Usuario = new cUsuario();
                        us.Usuario = lstEspecialistasAgregados[a];
                        us.Estado = 0;
                        seccion.lstUsuariosSeccion.Add(us);
                        informe.lstSecciones.Add(seccion);
                    }
                    if (lstEspecialistasAgregados[a].Especialidad.Nombre == "Pedadogia")
                    {
                        seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion us = new cUsuarioSeccion();
                        us.Usuario = new cUsuario();
                        us.Usuario = lstEspecialistasAgregados[a];
                        us.Estado = 0;
                        seccion.lstUsuariosSeccion.Add(us);
                        informe.lstSecciones.Add(seccion);
                    }
                    if (lstEspecialistasAgregados[a].Especialidad.Nombre == "Fisioterapia")
                    {
                        seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion us = new cUsuarioSeccion();
                        us.Usuario = new cUsuario();
                        us.Usuario = lstEspecialistasAgregados[a];
                        us.Estado = 0;
                        seccion.lstUsuariosSeccion.Add(us);
                        informe.lstSecciones.Add(seccion);
                    }
                    if (lstEspecialistasAgregados[a].Especialidad.Nombre == "Fonoaudiologia")
                    {
                        seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion us = new cUsuarioSeccion();
                        us.Usuario = new cUsuario();
                        us.Usuario = lstEspecialistasAgregados[a];
                        us.Estado = 0;
                        seccion.lstUsuariosSeccion.Add(us);
                        informe.lstSecciones.Add(seccion);
                    }
                    if (lstEspecialistasAgregados[a].Especialidad.Nombre == "Psicomotricidad")
                    {
                        seccion = new cSeccion();
                        seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion us = new cUsuarioSeccion();
                        us.Usuario = new cUsuario();
                        us.Usuario = lstEspecialistasAgregados[a];
                        us.Estado = 0;
                        seccion.lstUsuariosSeccion.Add(us);
                        informe.lstSecciones.Add(seccion);
                    }
                }
                #endregion
                #region En Suma

                seccion = new cSeccion();
                seccion.Nombre = cUtilidades.NombreSeccion.En_Suma;
                seccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < lstEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion us = new cUsuarioSeccion();
                    us.Usuario = new cUsuario();
                    us.Usuario = lstEspecialistasAgregados[i];
                    us.Estado = 0;
                    seccion.lstUsuariosSeccion.Add(us);
                }
                informe.lstSecciones.Add(seccion);

                #endregion

                try
                {
                    bool resultado = dFachada.InformeAgregar(informe);
                    if(resultado)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se realizó el nuevo informe correctamente')", true);
                        Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + beneficiario.Codigo);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: No se pudo realizar el nuevo informe')", true);
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