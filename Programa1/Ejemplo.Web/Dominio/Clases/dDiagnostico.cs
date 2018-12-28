using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dDiagnostico
    {
        public static List<string> TraerUltimosDiagnosticosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return pDiagnostico.TraerUltimosDiagnosticosPorBeneficiario(parBeneficiario);
        }
        public static List<cDiagnostico> TraerTodos()
        {
            return pDiagnostico.TraerTodos();
        }
        public static bool AgregarDiagnosticoBeneficiario(cBeneficiario parBeneficiario, List<cDiagnostico> parLstDiagnosticos)
        {
            return pDiagnostico.AgregarDiagnosticoBeneficiario(parBeneficiario, parLstDiagnosticos);
        }
    }
}
