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
        public static bool AgregarDiagnosticoBeneficiario(cBeneficiario parBeneficiario)
        {
            return pDiagnostico.AgregarDiagnosticoBeneficiario(parBeneficiario);
        }
        public static bool Agregar(cDiagnostico parDiagnostico)
        {
            return pDiagnostico.Agregar(parDiagnostico);
        }
        public static bool Existe(cDiagnostico parDiagnostico)
        {
            return pDiagnostico.Existe(parDiagnostico);
        }
        public static bool ExisteDiagnosticoBeneficiario(cDiagnostico parDiagnostico)
        {
            return pDiagnostico.ExisteDiagnosticoBeneficiario(parDiagnostico);
        }
        public static bool Eliminar(cDiagnostico parDiagnostico)
        {
            return pDiagnostico.Eliminar(parDiagnostico);
        }
    }
}
