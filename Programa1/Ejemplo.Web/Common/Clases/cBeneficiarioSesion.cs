using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cBeneficiarioSesion
    {
        public cBeneficiario Beneficiario { get; set; }
        public cPlan Plan { get; set; }
        public cUtilidades.EstadoSesion Estado { get; set; }

    }
}
