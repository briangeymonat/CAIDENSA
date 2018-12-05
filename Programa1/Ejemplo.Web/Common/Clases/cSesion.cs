using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cSesion
    {
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public cUtilidades.TipoSesion TipoSesion { get; set; }
        public cUtilidades.Centro Centro { get; set; }
        public List<cUsuario> lstUsuarios { get; set; }
        public List<cBeneficiarioSesion> lstBeneficiarios { get; set; }
    }
}
