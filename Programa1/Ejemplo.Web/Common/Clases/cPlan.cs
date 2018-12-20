using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cPlan
    {
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public Boolean Tratamiento { get; set; }
        public Boolean Evaluacion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public Boolean Activo { get; set; }

    }
}
