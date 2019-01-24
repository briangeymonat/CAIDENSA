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
        private static List<DateTime> LasHoras; 
        private static List<cItinerario> LosItinerarios;
        private static List<cUsuario> LosEspecialistas;
        private static List<List<cItinerario>> celdas;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime hora = DateTime.Parse("08:00");
                LasHoras = new List<DateTime>();
                LasHoras.Add(hora);
                do
                {
                    hora = hora.AddMinutes(15);
                    LasHoras.Add(hora);
                }
                while (hora != DateTime.Parse("20:00"));
                CargarDdlDias();
                CargarItinerarios();
                CargarDdlEspecialistas();
                CargarGrdItinerariosPorEspecialista();
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
            string Itinerarios = @"<table onscroll='screenTop'><tr ><td style='color:#000000; background-color:#80B7D8'>Hora</td>";
            for (int i = 0; i < LosEspecialistas.Count; i++)
            {
                Itinerarios += "<td style='color:#000000; background-color:#80B7D8'>" + LosEspecialistas[i].Nombres + " " + LosEspecialistas[i].Apellidos + "</td>";
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
                Itinerarios += "<tr><td style='height:20px; color:#000000; background-color:#80B7D8'>" + LasHoras[i].ToShortTimeString() + "</td>";
                for (int j = 0; j < LosEspecialistas.Count; j++)
                {
                    if (celdas[i][j].Comentario == null)
                    {
                        Itinerarios += "<td style='background-color:#FF8e8e; color:#5186A6' rowspan={0}></td>";
                    }
                    else
                    {
                        if (celdas[i][j].Comentario != "NO_LISTAR")
                        {
                            string nombres = "";
                            foreach (cBeneficiarioItinerario unBeneficiario in celdas[i][j].lstBeneficiarios)
                            {
                                string color = "";
                                switch (unBeneficiario.Plan.Tipo)
                                {
                                    case "ASSE":
                                        color = "#58FAF4";
                                        break;
                                    case "AYEX":
                                        color = "#8afa38";
                                        break;
                                    case "CAMEC":
                                        color = "#58FAF4";
                                        break;
                                    case "Círculo Católico":
                                        color = "#58FAF4";
                                        break;
                                    case "MIDES":
                                        color = "#F3F781";
                                        break;
                                    case "Particular":
                                        color = "#FE9A2E";
                                        break;
                                    case "Policial":
                                        color = "#58FAF4";
                                        break;
                                    default:
                                        color = "#ffffff";
                                        break;
                                }
                                nombres += "<p style='background-color:" + color + ";padding:5px 0px; margin:0px;'>" + unBeneficiario.Beneficiario.Nombres + " " + unBeneficiario.Beneficiario.Apellidos + "</p>";
                            }
                            int filas = 0;
                            for (int k = i; k < LasHoras.Count; k++)
                            {
                                if (DateTime.Parse(celdas[i][j].HoraFin) > LasHoras[k])
                                {
                                    filas++;
                                    //celdas[k][j] = new cItinerario();
                                    celdas[k][j].Comentario = "NO_LISTAR";
                                }

                            }
                            string elCentro = "";
                            if (celdas[i][j].Centro == cUtilidades.Centro.JuanLacaze) elCentro = " - JL"; else elCentro = " - NH";
                            Itinerarios += string.Format("<td style='background-color:#f5b041; color:#000000' rowspan={0}'>" +
                                    "<p style='padding:5px 0px; margin:0px;width:100px;'>" + celdas[i][j].TipoSesion + elCentro + "</p> " + nombres, filas, ((filas * 25) + filas));
                        }

                    }
                }
                Itinerarios += "</tr>";
            }
            Itinerarios += "</table>";


            //EMPIEZA ACA LA CREACION DE LA TABLA QUE SE MUESTRA
            DataTable dt = new DataTable();
            DataColumn Horas = dt.Columns.Add("Horas", typeof(string));
            List<DataColumn> ColumnasEspecialistas = new List<DataColumn>();
            foreach (cUsuario elEspecialista in LosEspecialistas)
            {

                ColumnasEspecialistas.Add(dt.Columns.Add(elEspecialista.Nombres + " " + elEspecialista.Apellidos, typeof(TemplateField)));
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
                        row[ColumnasEspecialistas[j]] = null;
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
                        row[ColumnasEspecialistas[j]] = button.TemplateControl;
                    }

                }
                dt.Rows.Add(row);
            }


            //dt.AcceptChanges();
            /*
            dtGrdItinerario.DataSource = dt;
            dtGrdItinerario.DataBind();
            */
            #endregion
            MostrarItinerarios(Itinerarios);


            // LosItinerarios = dFachada.ItinerarioTraerTodosPorDia(ddlDias.Text);
        }
        private void CargarDdlEspecialistas()
        {
            List<string> datos = new List<string>();
            foreach(cUsuario unUsuario in LosEspecialistas)
            {
                datos.Add(unUsuario.Nombres + " " + unUsuario.Apellidos);
            }
            ddlEspecialistas.DataSource = datos;
            ddlEspecialistas.DataBind();
        }
        private void CargarGrdItinerariosPorEspecialista()
        {
            DataTable dt = new DataTable();
            DataColumn TipoSesion = dt.Columns.Add("Tipo de Sesión", typeof(string));
            DataColumn HoraInicio = dt.Columns.Add("Hora de inicio", typeof(string));
            DataColumn HoraFin = dt.Columns.Add("Hora de fin", typeof(string));
            DataRow row;
            foreach (cItinerario unItinerario in LosItinerarios)
            {
                row = dt.NewRow();
                bool SeAgrega = false;
                foreach(cUsuario unUsuario in unItinerario.lstEspecialistas)
                {
                    if(unUsuario.Codigo == LosEspecialistas[ddlEspecialistas.SelectedIndex].Codigo)
                    {
                        SeAgrega = true;
                        switch(unItinerario.TipoSesion)
                        {
                            case cUtilidades.TipoSesion.Individual:
                                row[TipoSesion] = "Individual";
                                break;
                            case cUtilidades.TipoSesion.Grupo2:
                                row[TipoSesion] = "Grupo de 2";
                                break;
                            case cUtilidades.TipoSesion.Grupo3:
                                row[TipoSesion] = "Grupo de 3";
                                break;
                            case cUtilidades.TipoSesion.Taller:
                                row[TipoSesion] = "Taller";
                                break;
                            default:
                                row[TipoSesion] = "PROES";
                                break;
                        }
                        row[HoraInicio] = unItinerario.HoraInicio;
                        row[HoraFin] = unItinerario.HoraFin;
                        
                    }
                }
                if(SeAgrega)
                {
                    dt.Rows.Add(row);
                }
            }
            grdItinerarioPorEspecialista.DataSource = dt;
            grdItinerarioPorEspecialista.DataBind();
        }
        private void MostrarItinerarios(string parHtml)
        {
            frmItinerario.InnerHtml = parHtml;
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
       
        protected void ddlDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarItinerarios();
            CargarDdlEspecialistas();
            CargarGrdItinerariosPorEspecialista();
        }

        protected void rdblCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarItinerarios();
            CargarDdlEspecialistas();
            CargarGrdItinerariosPorEspecialista();
        }

        protected void grdItinerario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i] != null)
                    {
                        for (int j = 0; j < celdas[e.Row.DataItemIndex][i].lstBeneficiarios.Count; j++)
                        {
                            if (celdas[e.Row.DataItemIndex][i].lstBeneficiarios[j].Plan.Tipo == "ASSE")
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Aquamarine;
                            }
                        }
                    }
                }
            }
        }

        protected void ddlEspecialistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrdItinerariosPorEspecialista();
        }

        protected void grdItinerarioPorEspecialista_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            List<cItinerario> lstItinerarios = new List<cItinerario>();
            foreach (cItinerario unItinerario in LosItinerarios)
            {
                foreach (cUsuario unUsuario in unItinerario.lstEspecialistas)
                {
                    if (unUsuario.Codigo == LosEspecialistas[ddlEspecialistas.SelectedIndex].Codigo)
                    {
                        lstItinerarios.Add(unItinerario);
                    }
                }
            }
            Response.Redirect("vItinerarioModificarSesion.aspx?ItinerarioId=" + lstItinerarios[e.NewSelectedIndex].Codigo.ToString());
        }
    }
}