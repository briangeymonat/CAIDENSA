using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cUsuarioSesion
    {
        public cUsuario Usuario { get; set; }
        public cSesion Sesion { get; set; }
        public string Observacion { get; set; }
    }
}
