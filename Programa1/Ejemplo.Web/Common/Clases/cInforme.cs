using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cInforme
    {
        public int Codigo { get; set; }
        public string Fecha { get; set; } 
        public cUtilidades.TipoInforme Tipo { get; set; }
        public cUtilidades.EstadoInforme Estado { get; set; }
        public cBeneficiario Beneficiario { get; set; }
        public List<cSeccion> lstSecciones { get; set; }

    }
}
