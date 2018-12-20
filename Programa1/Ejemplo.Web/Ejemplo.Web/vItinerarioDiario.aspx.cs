using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Ejemplo.Web
{

    public partial class vItinerarioDiario : System.Web.UI.Page
    {
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> LasHoras = new List<DateTime>();
        private static List<cItinerario> LosItinerarios;
        private static List<cUsuario> LosEspecialistas;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime hora = DateTime.Parse("08:00");
                LasHoras.Add(hora);
                do
                {
                    hora = hora.AddMinutes(15);
                    LasHoras.Add(hora);
                } while (hora != DateTime.Parse("20:00"));
                this.PanelDetallesSesion.Visible = false;
                CargarItinerarios();
            }
        }
        private void CargarDdlDias()
        {
            ddlDias.DataSource = LosDias;
            ddlDias.DataBind();
        }
        private void CargarItinerarios()
        {
            LosItinerarios = dFachada.ItinerarioTraerTodosPorDia('L', 0);


            //---------------------ARMADO DEL HEADER-------------------------------

            LosEspecialistas = new List<cUsuario>();
            cUsuario unEspecialista;
            for (int i = 0; i < LosItinerarios.Count; i++)
            {
                for (int j = 0; j < LosItinerarios[i].lstEspecialistas.Count; j++)
                {
                    unEspecialista = LosItinerarios[i].lstEspecialistas[j];
                    bool existe = false;
                    for (int k = 0; k < LosEspecialistas.Count; k++)
                    {
                        if (unEspecialista.Equals(LosEspecialistas[k]))
                        {
                            existe = true;
                        }
                    }
                    if (!existe) LosEspecialistas.Add(unEspecialista);
                }

            }
            string Itinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:antiquewhite'>Hora</td>";
            for (int i = 0; i < LosEspecialistas.Count; i++)
            {
                Itinerarios += "<td style='color:#000000; background-color:antiquewhite'>" + LosEspecialistas[i].Nombres + " " + LosEspecialistas[i].Apellidos + "</td>";
            }

            //-------------------FIN DE ARMADO DEL HEADER---------------------------



            //----------ORDENAMIENTO DE SESIONES SEGÚN EL ESPECIALISTA---------------

            Itinerarios += "</tr><tr>";

            List<cItinerario> ItinerariosPorEspecialista = new List<cItinerario>();
            for (int i = 0; i < LasHoras.Count; i++)
            {
                Itinerarios += "<td style='color:#000000; background-color:antiquewhite'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for (int j = 0; j < LosEspecialistas.Count; j++)
                {
                    for (int k = 0; k < LosItinerarios.Count; k++)
                    {
                        if (LosItinerarios[k].HoraInicio >= LasHoras[i] && 
                            LosItinerarios[k].HoraInicio < LasHoras[i + 1]
                            && LosItinerarios[j].lstEspecialistas.Contains(LosEspecialistas[j]))
                        {
                            Itinerarios += "<td style='background-color:cadetblue; color:#ffd800' rowspan='3' onserverclick='btnSeleccionar_Click' ><button onclick='btnSeleccionar_Click(new object(), new EventArgs())'>" +
                                LosItinerarios[k].TipoSesion +" "+LosItinerarios[k].lstBeneficiarios[0].Beneficiario.Nombres+ "</button></td>";
                        }
                    }
                }
                Itinerarios += "</tr>";
            }
            

            Itinerarios += "</table>";




            MostrarItinerarios(Itinerarios);


            // LosItinerarios = dFachada.ItinerarioTraerTodosPorDia(ddlDias.Text);
        }
        private void MostrarItinerarios(string parHtml)
        {
            frmItinerario.InnerHtml = parHtml;
        }
        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {

        }
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            string vtn = "window.open('vDetallesSesion.aspx','Detalles de sesion','scrollbars=yes,resizable=yes','height=200', 'width=300')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        protected void btnMostrarDetallesSesion_Click(object sender, EventArgs e)
        {
            this.PanelDetallesSesion.Visible = true;
        }


    }
}