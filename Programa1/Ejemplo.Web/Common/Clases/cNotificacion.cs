using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cNotificacion
    {
        public int Codigo { get; set; }
        public Boolean Estado { get; set; }//vista o no vista
        public cUsuario Usuario { get; set; }
        public cPlan Plan { get; set; }
        public cInforme Informe { get; set; }
    }
}
