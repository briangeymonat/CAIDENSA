using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cSeccion
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public List<cUsuarioSeccion> lstUsuarios { get; set; }

    }
}
