using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vSesionDiaria : System.Web.UI.Page
    {
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> LasHoras;
        private static List<cSesion> LasSesiones;
        private static List<cUsuario> LosEspecialistas;
        private static List<List<cSesion>> LasCeldas;
        public static bool ventanaObservacion = true;



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                DateTime dHora = DateTime.Parse("08:00");
                LasHoras = new List<DateTime>();
                LasHoras.Add(dHora);
                do
                {
                    dHora = dHora.AddMinutes(15);
                    LasHoras.Add(dHora);
                }
                while (dHora != DateTime.Parse("20:00"));
                this.txtFechaSesiones.Text = DateTime.Today.ToShortDateString();
                CargarSesiones();
                CargarDdlEspecialistas();
                CargarGrdSesionesPorEspecialista();
                if (LasSesiones.Count <= 0)
                {
                    frmSesiones.Visible = false;
                    lblSesionesXEspecialista.Visible = false;
                    ddlEspecialistas.Visible = false;
                }
                else
                {
                    frmSesiones.Visible = true;
                    lblSesionesXEspecialista.Visible = true;
                    ddlEspecialistas.Visible = true;
                }
            }
        }

        private void CargarSesiones()
        {
            if(txtFechaSesiones.Text != string.Empty)
            {
                LasSesiones = dFachada.SesionTraerTodasPorFecha(DateTime.Parse(txtFechaSesiones.Text), rdblCentro.SelectedIndex);



                #region ARMADO DEL HEADER
                //---------------------ARMADO DEL HEADER-------------------------------

                LosEspecialistas = new List<cUsuario>();
                cUsuario unEspecialista;
                for (int i = 0; i < LasSesiones.Count; i++)
                {
                    for (int j = 0; j < LasSesiones[i].lstUsuarios.Count; j++)
                    {
                        unEspecialista = LasSesiones[i].lstUsuarios[j];
                        bool bExiste = false;
                        for (int k = 0; k < LosEspecialistas.Count; k++)
                        {
                            if (unEspecialista.Codigo == LosEspecialistas[k].Codigo)
                            {
                                bExiste = true;
                            }
                        }
                        if (!bExiste) LosEspecialistas.Add(unEspecialista);
                    }

                }
                string sItinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
                for (int i = 0; i < LosEspecialistas.Count; i++)
                {
                    sItinerarios += "<td style='color:#000000; background-color:#80B7D8'>" + LosEspecialistas[i].Nombres + " " + LosEspecialistas[i].Apellidos + "</td>";
                }
                sItinerarios += "</tr>";


                //-------------------FIN DE ARMADO DEL HEADER---------------------------
                #endregion

                #region SEGUNDO METODO ORDENAMIENTO DE SESIONES SEGÚN EL ESPECIALISTA


                LasCeldas = new List<List<cSesion>>();


                for (int i = 0; i < LasHoras.Count; i++)
                {

                    LasCeldas.Add(new List<cSesion>());
                    for (int j = 0; j < LosEspecialistas.Count; j++)
                    {
                        bool bHayAlgunaSesion = false;
                        for (int k = 0; k < LasSesiones.Count; k++)
                        {
                            if (!bHayAlgunaSesion)
                            {
                                if (DateTime.Parse(LasSesiones[k].HoraInicio) >= LasHoras[i] && DateTime.Parse(LasSesiones[k].HoraInicio) < LasHoras[i + 1])
                                {
                                    for (int l = 0; l < LasSesiones[k].lstUsuarios.Count; l++)
                                    {
                                        if (LasSesiones[k].lstUsuarios[l].Codigo == LosEspecialistas[j].Codigo)
                                        {
                                            bHayAlgunaSesion = true;
                                            LasCeldas[i].Add(LasSesiones[k]);
                                        }
                                    }
                                }
                            }
                        }
                        if (!bHayAlgunaSesion) { LasCeldas[i].Add(new cSesion()); }
                    }
                }

                #endregion




                for (int i = 0; i < LasHoras.Count; i++)
                {
                    sItinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LasHoras[i].ToShortTimeString() + "</td>";
                    for (int j = 0; j < LosEspecialistas.Count; j++)
                    {
                        if (LasCeldas[i][j].Comentario == null)
                        {
                            sItinerarios += "<td style='background-color:#FF8e8e; color:#5186A6' rowspan={0}></td>";
                        }
                        else
                        {
                            if (LasCeldas[i][j].Comentario != "NO_LISTAR")
                            {
                                string sNombres = "";
                                foreach (cBeneficiarioSesion unBeneficiario in LasCeldas[i][j].lstBeneficiarios)
                                {
                                    string sColor = "";
                                    switch (unBeneficiario.Plan.Tipo)
                                    {
                                        case "ASSE":
                                            sColor = "#58FAF4";
                                            break;
                                        case "AYEX":
                                            sColor = "#8afa38";
                                            break;
                                        case "CAMEC":
                                            sColor = "#58FAF4";
                                            break;
                                        case "Círculo Católico":
                                            sColor = "#58FAF4";
                                            break;
                                        case "MIDES":
                                            sColor = "#F3F781";
                                            break;
                                        case "Particular":
                                            sColor = "#FE9A2E";
                                            break;
                                        case "Policial":
                                            sColor = "#58FAF4";
                                            break;
                                        default:
                                            sColor = "#ffffff";
                                            break;
                                    }
                                    sNombres += "<p style='background-color:" + sColor + ";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos + "</p>";
                                }
                                int iFilas = 0;
                                for (int k = i; k < LasHoras.Count; k++)
                                {
                                    if (DateTime.Parse(LasCeldas[i][j].HoraFin) > LasHoras[k])
                                    {
                                        iFilas++;
                                        //celdas[k][j] = new cItinerario();
                                        LasCeldas[k][j].Comentario = "NO_LISTAR";
                                    }

                                }
                                string sElCentro = "";
                                if (LasCeldas[i][j].Centro == cUtilidades.Centro.JuanLacaze) sElCentro = " - JL"; else sElCentro = " - NH";
                                sItinerarios += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                        "<p style='padding:5px 0px; margin:0px;width:100px;'>" + LasCeldas[i][j].TipoSesion + sElCentro + "</p> " + sNombres, iFilas, ((iFilas * 25) + iFilas));
                            }

                        }
                    }
                    sItinerarios += "</tr>";
                }
                sItinerarios += "</table>";

                frmSesiones.InnerHtml = sItinerarios;
            }
        }

        private void CargarDdlEspecialistas()
        {
            List<string> lstDatos = new List<string>();
            foreach (cUsuario unUsuario in LosEspecialistas)
            {
                lstDatos.Add(unUsuario.Nombres + " " + unUsuario.Apellidos);
            }
            ddlEspecialistas.DataSource = lstDatos;
            ddlEspecialistas.DataBind();
        }

        private void CargarGrdSesionesPorEspecialista()
        {
            DataTable dt = new DataTable();
            DataColumn dcTipoSesion = dt.Columns.Add("Tipo de Sesión", typeof(string));
            DataColumn dcHoraInicio = dt.Columns.Add("Hora de inicio", typeof(string));
            DataColumn dcHoraFin = dt.Columns.Add("Hora de fin", typeof(string));
            DataRow row;
            foreach (cSesion unaSesion in LasSesiones)
            {
                row = dt.NewRow();
                bool bSeAgrega = false;
                foreach (cUsuario unUsuario in unaSesion.lstUsuarios)
                {
                    if (unUsuario.Codigo == LosEspecialistas[ddlEspecialistas.SelectedIndex].Codigo)
                    {
                        bSeAgrega = true;
                        switch (unaSesion.TipoSesion)
                        {
                            case cUtilidades.TipoSesion.Individual:
                                row[dcTipoSesion] = "Individual";
                                break;
                            case cUtilidades.TipoSesion.Grupo2:
                                row[dcTipoSesion] = "Grupo de 2";
                                break;
                            case cUtilidades.TipoSesion.Grupo3:
                                row[dcTipoSesion] = "Grupo de 3";
                                break;
                            case cUtilidades.TipoSesion.Taller:
                                row[dcTipoSesion] = "Taller";
                                break;
                            default:
                                row[dcTipoSesion] = "PROES";
                                break;
                        }
                        row[dcHoraInicio] = unaSesion.HoraInicio;
                        row[dcHoraFin] = unaSesion.HoraFin;

                    }
                }
                if (bSeAgrega)
                {
                    dt.Rows.Add(row);
                }
            }
            grdSesionesPorEspecialista.DataSource = dt;
            grdSesionesPorEspecialista.DataBind();
        }
        protected void ddlEspecialistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrdSesionesPorEspecialista();
        }
        

        protected void grdSesionesPorEspecialista_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            List<cSesion> lstSesiones = new List<cSesion>();
            foreach (cSesion unaSesion in LasSesiones)
            {
                foreach (cUsuario unUsuario in unaSesion.lstUsuarios)
                {
                    if (unUsuario.Codigo == LosEspecialistas[ddlEspecialistas.SelectedIndex].Codigo)
                    {
                        lstSesiones.Add(unaSesion);
                    }
                }
            }
            string sVtn = "window.open('vDetallesSesion.aspx?Id=" + lstSesiones[e.NewSelectedIndex].Codigo + "','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", sVtn, true);
        }

        protected void txtFechaSesiones_TextChanged(object sender, EventArgs e)
        {
            CargarSesiones();
            CargarDdlEspecialistas();
            CargarGrdSesionesPorEspecialista();
            if (LasSesiones.Count <= 0)
            {
                frmSesiones.Visible = false;
                lblSesionesXEspecialista.Visible = false;
                ddlEspecialistas.Visible = false;
            }
            else
            {
                frmSesiones.Visible = true;
                lblSesionesXEspecialista.Visible = true;
                ddlEspecialistas.Visible = true;
            }
        }

        protected void rdblCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSesiones();
            CargarDdlEspecialistas();
            CargarGrdSesionesPorEspecialista();
            if (LasSesiones.Count <= 0)
            {
                frmSesiones.Visible = false;
                lblSesionesXEspecialista.Visible = false;
                ddlEspecialistas.Visible = false;
            }
            else
            {
                frmSesiones.Visible = true;
                lblSesionesXEspecialista.Visible = true;
                ddlEspecialistas.Visible = true;
            }
        }
    }
}