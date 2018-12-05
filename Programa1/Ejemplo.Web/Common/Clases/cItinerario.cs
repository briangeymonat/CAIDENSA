using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cItinerario
    {
        public int Codigo { get; set; }
        public cUtilidades.TipoSesion TipoSesion { get; set; }
        public string Dia { get; set; } //L,M,X,J,V,S
        public DateTime HoraInicio { get; set; }
        public DateTime HotaFin { get; set; }
        public string Comentario { get; set; }
        public cUtilidades.Centro Centro { get; set; }
        public Boolean Estado { get; set; }
        public List<cUsuario> lstEspecialistas { get; set; } //usuarios con especialidad
        public List<cBeneficiarioItinerario> lstBeneficiarios { get; set; }


    }
}
