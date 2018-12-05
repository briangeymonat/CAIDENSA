using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cBeneficiario
    {
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int CI { get; set; }
        public string Sexo { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Email { get; set; }
        public string Domicilio { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Atributario { get; set; }
        public string MotivoConsulta { get; set; }
        public string Escolaridad { get; set; }
        public string Derivador { get; set; }
        public Boolean Estado { get; set; }
        public List<cDiagnosticoBeneficiario> lstDiagnosticos { get; set; }
        public List<cPlan> lstPlanes { get; set; }
    }
}
