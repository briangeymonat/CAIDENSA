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
        static cBeneficiario ElBeneficiario;
        static List<cUsuario> LosTodosEspecialistas;
        static List<cUsuario> LosEspecialistasAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosTodosEspecialistas = new List<cUsuario>();
                LosEspecialistasAgregados = new List<cUsuario>();
                LosTodosEspecialistas = dFachada.UsuarioTraerTodosEspecialistasActivos();


                CargarCombos();
                CargarGrillas();
                cargarDatos();
            }
        }
        protected void cargarDatos()
        {
            ElBeneficiario = new cBeneficiario();
            ElBeneficiario.Codigo = int.Parse(Request.QueryString["idBeneficiario"]);
            ElBeneficiario = dFachada.BeneficiarioTraerEspecifico(ElBeneficiario);
            this.lblNombres.Text = ElBeneficiario.Nombres.ToString();
            this.lblApellidos.Text = ElBeneficiario.Apellidos.ToString();
            this.lblCI.Text = ElBeneficiario.CI.ToString();
            
            string[] aParts = ElBeneficiario.FechaNacimiento.Split(' ');

            this.lblFechaNac.Text = aParts[0];
            string sFecha = aParts[0];

            #region Hallar la edad cronológica edadAños, edadMeses, edadDias

            string[] aPartes = sFecha.Split('/');


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
            this.lblEdad.Text = iEdadAños + " años y " + iEdadMeses + " meses";
            lblMotivoConsulta.Text = ElBeneficiario.MotivoConsulta.ToString();
            lblEscolaridad.Text = ElBeneficiario.Escolaridad.ToString();
            lblEncuadre.Text = dFachada.ItinerarioTraerEncuadrePorBeneficiario(ElBeneficiario);
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


            List<cEspecialidad> lstEspecialidades = new List<cEspecialidad>();
            lstEspecialidades = dFachada.EspecialidadTraerTodas();
            for (int i = 0; i < lstEspecialidades.Count; i++)
            {
                if (lstEspecialidades[i].Nombre == "Sin especialidad")
                {
                    lstEspecialidades.RemoveAt(i);
                }
            }
            ddlEspecialidad.DataSource = lstEspecialidades;
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Codigo";
            ddlEspecialidad.DataBind();
        }

        protected void CargarGrillas()
        {
            cEspecialidad unaEspecialidad = new cEspecialidad();
            unaEspecialidad.Codigo = int.Parse(ddlEspecialidad.SelectedValue);
            List<cUsuario> lstMostrar = new List<cUsuario>();

            for (int i = 0; i < LosTodosEspecialistas.Count; i++)
            {
                if (LosTodosEspecialistas[i].Especialidad.Codigo == unaEspecialidad.Codigo)
                {
                    lstMostrar.Add(LosTodosEspecialistas[i]);
                }
            }
            this.grdTodosEspecialistas.DataSource = lstMostrar;
            this.grdTodosEspecialistas.DataBind();


            this.grdEspecialistasAgregados.DataSource = LosEspecialistasAgregados;
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
            cEspecialidad unaEspecialidad = new cEspecialidad();
            unaEspecialidad.Codigo = int.Parse(ddlEspecialidad.SelectedValue);
            List<cUsuario> lstMostrar = new List<cUsuario>();

            for (int i = 0; i < LosTodosEspecialistas.Count; i++)
            {
                if (LosTodosEspecialistas[i].Especialidad.Codigo == unaEspecialidad.Codigo)
                {
                    lstMostrar.Add(LosTodosEspecialistas[i]);
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
            string sNickName = string.Format(celdaNickName.Text);
            for (int i = 0; i < LosTodosEspecialistas.Count; i++)
            {
                if (LosTodosEspecialistas[i].NickName == sNickName)
                {
                    LosEspecialistasAgregados.Add(LosTodosEspecialistas[i]);
                    LosTodosEspecialistas.RemoveAt(i);
                }
            }
            CargarGrillas();
        }

        protected void grdEspecialistasAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell celdaNickName = grdEspecialistasAgregados.Rows[e.RowIndex].Cells[2];
            string sNickName = string.Format(celdaNickName.Text);
            for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
            {
                if (LosEspecialistasAgregados[i].NickName == sNickName)
                {
                    LosTodosEspecialistas.Add(LosEspecialistasAgregados[i]);
                    LosEspecialistasAgregados.RemoveAt(i);
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
            cInforme unInforme = new cInforme();
            if (ddlTipo.SelectedValue == "Evaluacion Psicomotriz")
                unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
            else if (ddlTipo.SelectedValue == "Evaluacion Psicopedagogica")
                unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
            else if (ddlTipo.SelectedValue == "Evaluacion Psicologica")
                unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
            else if (ddlTipo.SelectedValue == "Evaluacion Fonoaudiologa")
                unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
            else if (ddlTipo.SelectedValue == "Evolucion")
                unInforme.Tipo = cUtilidades.TipoInforme.Evolucion;
            else if (ddlTipo.SelectedValue == "Tolerancia Curricular")
                unInforme.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
            else if (ddlTipo.SelectedValue == "Juzgado")
                unInforme.Tipo = cUtilidades.TipoInforme.Juzgado;
            else if (ddlTipo.SelectedValue == "Interdiciplinario")
                unInforme.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
            else if (ddlTipo.SelectedValue == "Otro")
                unInforme.Tipo = cUtilidades.TipoInforme.Otro;

            unInforme.Beneficiario = new cBeneficiario();
            unInforme.Beneficiario = ElBeneficiario;

            unInforme.lstSecciones = new List<cSeccion>();
            if (LosEspecialistasAgregados.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: El informe debe contar con al menos un especialista que realice el mismo')", true);
            }
            else
            {

                #region Titulo
                cSeccion unaSeccion = new cSeccion();
                unaSeccion.Nombre = cUtilidades.NombreSeccion.Título;
                unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion unUS = new cUsuarioSeccion();
                    unUS.Usuario = new cUsuario();
                    unUS.Usuario = LosEspecialistasAgregados[i];
                    unUS.Estado = 0;
                    unaSeccion.lstUsuariosSeccion.Add(unUS);
                }
                unInforme.lstSecciones.Add(unaSeccion);
                #endregion


                #region Encuadre
                unaSeccion = new cSeccion();
                unaSeccion.Nombre = cUtilidades.NombreSeccion.Encuadre;
                //  seccion.Contenido = dFachada.ItinerarioTraerEncuadrePorBeneficiario(beneficiario); 
                unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                //CUANDO SE TENGA EL ITINERARIO EL CONTENIDO DE ESTA SECCION SE REALIZARÁ AQUI
                for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion unUS = new cUsuarioSeccion();
                    unUS.Usuario = new cUsuario();
                    unUS.Usuario = LosEspecialistasAgregados[i];
                    unUS.Estado = cUtilidades.EstadoInforme.Terminado;
                    unaSeccion.lstUsuariosSeccion.Add(unUS);
                }
                unInforme.lstSecciones.Add(unaSeccion);
                #endregion
                #region Diagnostico
                unaSeccion = new cSeccion();
                unaSeccion.Nombre = cUtilidades.NombreSeccion.Diagnóstico;
                unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion unUS = new cUsuarioSeccion();
                    unUS.Usuario = new cUsuario();
                    unUS.Usuario = LosEspecialistasAgregados[i];
                    unUS.Estado = 0;
                    unaSeccion.lstUsuariosSeccion.Add(unUS);
                }
                unInforme.lstSecciones.Add(unaSeccion);
                #endregion
                #region Presentacion
                unaSeccion = new cSeccion();
                unaSeccion.Nombre = cUtilidades.NombreSeccion.Presentación;
                unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion unUS = new cUsuarioSeccion();
                    unUS.Usuario = new cUsuario();
                    unUS.Usuario = LosEspecialistasAgregados[i];
                    unUS.Estado = 0;
                    unaSeccion.lstUsuariosSeccion.Add(unUS);
                }
                unInforme.lstSecciones.Add(unaSeccion);
                #endregion
                #region Abordajes
                for (int a = 0; a < LosEspecialistasAgregados.Count; a++)
                {
                    if (LosEspecialistasAgregados[a].Especialidad.Nombre == "Psicologia")
                    {
                        unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion unUS = new cUsuarioSeccion();
                        unUS.Usuario = new cUsuario();
                        unUS.Usuario = LosEspecialistasAgregados[a];
                        unUS.Estado = 0;
                        unaSeccion.lstUsuariosSeccion.Add(unUS);
                        unInforme.lstSecciones.Add(unaSeccion);
                    }
                    if (LosEspecialistasAgregados[a].Especialidad.Nombre == "Pedadogia")
                    {
                        unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion unUS = new cUsuarioSeccion();
                        unUS.Usuario = new cUsuario();
                        unUS.Usuario = LosEspecialistasAgregados[a];
                        unUS.Estado = 0;
                        unaSeccion.lstUsuariosSeccion.Add(unUS);
                        unInforme.lstSecciones.Add(unaSeccion);
                    }
                    if (LosEspecialistasAgregados[a].Especialidad.Nombre == "Fisioterapia")
                    {
                        unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion unUS = new cUsuarioSeccion();
                        unUS.Usuario = new cUsuario();
                        unUS.Usuario = LosEspecialistasAgregados[a];
                        unUS.Estado = 0;
                        unaSeccion.lstUsuariosSeccion.Add(unUS);
                        unInforme.lstSecciones.Add(unaSeccion);
                    }
                    if (LosEspecialistasAgregados[a].Especialidad.Nombre == "Fonoaudiologia")
                    {
                        unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion unUS = new cUsuarioSeccion();
                        unUS.Usuario = new cUsuario();
                        unUS.Usuario = LosEspecialistasAgregados[a];
                        unUS.Estado = 0;
                        unaSeccion.lstUsuariosSeccion.Add(unUS);
                        unInforme.lstSecciones.Add(unaSeccion);
                    }
                    if (LosEspecialistasAgregados[a].Especialidad.Nombre == "Psicomotricidad")
                    {
                        unaSeccion = new cSeccion();
                        unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                        cUsuarioSeccion unUS = new cUsuarioSeccion();
                        unUS.Usuario = new cUsuario();
                        unUS.Usuario = LosEspecialistasAgregados[a];
                        unUS.Estado = 0;
                        unaSeccion.lstUsuariosSeccion.Add(unUS);
                        unInforme.lstSecciones.Add(unaSeccion);
                    }
                }
                #endregion
                #region En Suma

                unaSeccion = new cSeccion();
                unaSeccion.Nombre = cUtilidades.NombreSeccion.En_Suma;
                unaSeccion.lstUsuariosSeccion = new List<cUsuarioSeccion>();
                for (int i = 0; i < LosEspecialistasAgregados.Count; i++)
                {
                    cUsuarioSeccion unUS = new cUsuarioSeccion();
                    unUS.Usuario = new cUsuario();
                    unUS.Usuario = LosEspecialistasAgregados[i];
                    unUS.Estado = 0;
                    unaSeccion.lstUsuariosSeccion.Add(unUS);
                }
                unInforme.lstSecciones.Add(unaSeccion);

                #endregion

                try
                {
                    bool bResultado = dFachada.InformeAgregar(unInforme);
                    if (bResultado)
                    {
                        int iIdUltimoInforme = dFachada.InformeUltimoIngresado();
                        cNotificacion unNotificacion;
                        for (int i = 0; i < unInforme.lstSecciones[0].lstUsuariosSeccion.Count; i++)
                        {
                            unNotificacion = new cNotificacion();
                            unNotificacion.Usuario = new cUsuario();
                            unNotificacion.Usuario = unInforme.lstSecciones[0].lstUsuariosSeccion[i].Usuario;
                            unNotificacion.Informe = new cInforme();
                            unNotificacion.Informe.Codigo = iIdUltimoInforme;
                            bool bRes = dFachada.NotificacionAgregarDeEspecialista(unNotificacion);
                        }


                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('Se realizó el nuevo informe correctamente')", true);
                        Response.Redirect("vBeneficiarioDetalles.aspx?BeneficiarioId=" + ElBeneficiario.Codigo);
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