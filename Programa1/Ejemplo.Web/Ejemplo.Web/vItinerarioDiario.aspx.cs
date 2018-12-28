using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace Ejemplo.Web
{

    public partial class vItinerarioDiario : System.Web.UI.Page
    {
        private static List<string> LosDias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        private static List<DateTime> LasHoras = new List<DateTime>();
        private static List<cItinerario> LosItinerarios;
        private static List<cUsuario> LosEspecialistas;
        private static List<List<cItinerario>> celdas;


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
                CargarDdlDias();
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

            char dia;
            int centro;
            switch (ddlDias.Text)
            {
                case "Lunes":
                    dia = 'L';
                    break;
                case "Martes":
                    dia = 'M';
                    break;
                case "Miércoles":
                    dia = 'X';
                    break;
                case "Jueves":
                    dia = 'J';
                    break;
                case "Viernes":
                    dia = 'V';
                    break;
                default:
                    dia = 'S';
                    break;
            }
            if (rdblCentro.SelectedValue.ToString() == "Juan Lacaze") { centro = 0; } else { centro = 1; }
            LosItinerarios = dFachada.ItinerarioTraerTodosPorDia(dia, centro);

            #region ARMADO DEL HEADER
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
                        if (unEspecialista.Codigo == LosEspecialistas[k].Codigo)
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
            Itinerarios += "</tr>";


            //-------------------FIN DE ARMADO DEL HEADER---------------------------
            #endregion


            #region ORDENAMIENTO DE SESIONES SEGÚN EL ESPECIALISTA

            //----------ORDENAMIENTO DE SESIONES SEGÚN EL ESPECIALISTA---------------



            //List<cItinerario> ItinerariosPorEspecialista = new List<cItinerario>();
            //for (int i = 0; i < LasHoras.Count; i++)
            //{
            //    Itinerarios += "<tr><td style='color:#000000; background-color:antiquewhite'>" + LasHoras[i].ToShortTimeString() + "</td>";
            //    for (int j = 0; j < LosEspecialistas.Count; j++)
            //    {
            //        for (int k = 0; k < LosItinerarios.Count; k++)
            //        {
            //            if (LosItinerarios[k].HoraInicio.TimeOfDay >= LasHoras[i].TimeOfDay &&
            //                LosItinerarios[k].HoraInicio.TimeOfDay < LasHoras[i + 1].TimeOfDay
            //                && LosItinerarios[k].lstEspecialistas.Contains(LosEspecialistas[j]))
            //            {
            //                /*
            //                int filas = 0;
            //                for (int l = i + 1; l < LasHoras.Count; l++)
            //                {
            //                    if (LosItinerarios[k].HoraFin >= LasHoras[l] && LosItinerarios[k].HoraFin < LasHoras[l + 1])
            //                    {
            //                        filas++;
            //                    }
            //                }
            //                */
            //                Itinerarios += string.Format("<td style='background-color:cadetblue; color:#ffd800' rowspan={0} onserverclick='btnSeleccionar_Click' ><button onclick='btnSeleccionar_Click(new object(), new EventArgs())'>" +
            //                    LosItinerarios[k].TipoSesion + " " + LosItinerarios[k].lstBeneficiarios[0].Beneficiario.Nombres + "</button></td>", (1).ToString());

            //            }
            //        }
            //    }
            //    Itinerarios += "</tr>";
            //}
            //Itinerarios += "</table>";


            #endregion


            #region SEGUNDO METODO ORDENAMIENTO DE SESIONES SEGÚN EL ESPECIALISTA

            celdas = new List<List<cItinerario>>();


            for (int i = 0; i < LasHoras.Count; i++)
            {

                celdas.Add(new List<cItinerario>());
                for (int j = 0; j < LosEspecialistas.Count; j++)
                {
                    bool HayAlgunaSesion = false;
                    for (int k = 0; k < LosItinerarios.Count; k++)
                    {
                        if (!HayAlgunaSesion)
                        {
                            if (DateTime.Parse(LosItinerarios[k].HoraInicio) >= LasHoras[i] && DateTime.Parse(LosItinerarios[k].HoraInicio) < LasHoras[i + 1])
                            {
                                for (int l = 0; l < LosItinerarios[k].lstEspecialistas.Count; l++)
                                {
                                    if (LosItinerarios[k].lstEspecialistas[l].Codigo == LosEspecialistas[j].Codigo)
                                    {
                                        HayAlgunaSesion = true;
                                        celdas[i].Add(LosItinerarios[k]);
                                    }
                                }
                            }
                        }
                    }
                    if (!HayAlgunaSesion) { celdas[i].Add(new cItinerario()); }
                }
            }




            for (int i = 0; i < LasHoras.Count; i++)
            {
                Itinerarios += "<tr><td style='color:#000000; background-color:antiquewhite'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for (int j = 0; j < LosEspecialistas.Count; j++)
                {
                    if (celdas[i][j].lstBeneficiarios == null)
                    {
                        Itinerarios += "<td style='background-color:#303cbf; color:#ffd800' rowspan={0}></td>";
                    }
                    else
                    {
                        string nombres = "";
                        foreach (cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                        {
                            nombres += " " + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos;
                        }
                        int filas = 0;
                        for (int k = i; k < LasHoras.Count; k++)
                        {
                            if (DateTime.Parse(celdas[i][j].HoraFin) > LasHoras[k])
                                filas++;
                        }
                        Itinerarios += string.Format("<td style='background-color:ffffff; color:#ffd800' rowspan={0} onserverclick='AccionOkay(" + celdas[i][j].Codigo + ")' >" +
                            "<button style='margin-top:0px; margin-left:0px; margin-right:0px; margin-bottom:0px;' " +
                            "class='btnMostrarSesion' id='" + celdas[i][j].Codigo + "'onclick='btnMostrarSesion()'>" +
                                celdas[i][j].TipoSesion + " " + nombres + "</button></td>", filas);
                    }
                }
                Itinerarios += "</tr>";
            }
            Itinerarios += "</table>";

            DataTable dt = new DataTable();
            DataColumn Horas = dt.Columns.Add("Horas", typeof(string));

            foreach (cUsuario elEspecialista in LosEspecialistas)
            {

                DataColumn nombre = dt.Columns.Add(elEspecialista.Nombres + " " + elEspecialista.Apellidos, typeof(Button));
            }
            DataRow row;
            //typeof(string)
            for (int i = 0; i < LasHoras.Count; i++)
            {
                row = dt.NewRow();
                row[Horas] = LasHoras[i].ToShortTimeString();
                for (int j = 0; j < LosEspecialistas.Count; j++)
                {
                    Button button;
                    if (celdas[i][j].lstBeneficiarios == null)
                    {
                        row[LosEspecialistas[j].Nombres + " " + LosEspecialistas[j].Apellidos] = null;
                    }
                    else
                    {
                        button = new Button();
                        string nombres = "";
                        foreach (cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                        {
                            nombres += " " + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos;
                        }
                        button.Text = celdas[i][j].TipoSesion.ToString() + " " + nombres;
                        button.Visible = true;
                        button.OnClientClick = "MostrarSesion(0)";
                        row[LosEspecialistas[j].Nombres + " " + LosEspecialistas[j].Apellidos] = button;
                    }

                }
                dt.Rows.Add(row);

            }


            //dt.AcceptChanges();
            dtGrdItinerario.DataSource = dt;
            dtGrdItinerario.DataBind();

            #endregion
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
        [WebMethod]
        public void MostrarSesion(int parCodigo)
        {
            btnSeleccionar_Click(new object(), new EventArgs());
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

        protected void ddlDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarItinerarios();
        }

        protected void rdblCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarItinerarios();
        }
    }
}