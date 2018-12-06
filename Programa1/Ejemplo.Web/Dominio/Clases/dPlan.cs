using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dPlan
    {
        public static bool Agregar(cBeneficiario elBeneficiario)
        {
            return pPlan.Agregar(elBeneficiario);
        }
        public static bool Eliminar(cPlan elPlan)
        {
            return pPlan.Eliminar(elPlan);
        }
        public static List<cPlan> TraerActivosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            return pPlan.TraerActivosPorBeneficiario(elBeneficiario);
        }
        public static List<cPlan> TraerInactivosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            return pPlan.TraerInactivosPorBeneficiario(elBeneficiario);
        }
        public static List<cPlan> TraerTodosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            return pPlan.TraerTodosPorBeneficiario(elBeneficiario);
        }
    }
}
