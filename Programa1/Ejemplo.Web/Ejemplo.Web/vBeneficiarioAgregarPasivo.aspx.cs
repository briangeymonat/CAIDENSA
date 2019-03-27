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
    public partial class vBeneficiarioAgregarPasivo1 : System.Web.UI.Page
    {
        private static List<string> LosTiposPlanes = new List<string> { "ASSE", "AYEX", "CAMEC", "Círculo Católico", "MIDES", "Particular", "Policial" };


        private static List<cDiagnostico> LosTodosDiagnosticos;
        private static List<cDiagnostico> LosDiagnosticosAgregados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosTodosDiagnosticos = dFachada.DiagnosticoTraerTodos();
                LosDiagnosticosAgregados = new List<cDiagnostico>();
                CargarComboTipoPlanes();
                CargarGrillaDiagnosticos();
            }
        }
    }
}