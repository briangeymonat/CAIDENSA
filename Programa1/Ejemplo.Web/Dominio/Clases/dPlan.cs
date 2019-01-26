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
        public static bool Agregar(cBeneficiario parBeneficiario)
        {
            return pPlan.Agregar(parBeneficiario);
        }
        public static bool Eliminar(cPlan parPlan)
        {
            return pPlan.Eliminar(parPlan);
        }
        public static List<cPlan> TraerActivosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return pPlan.TraerActivosPorBeneficiario(parBeneficiario);
        }
        public static List<cPlan> TraerInactivosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return pPlan.TraerInactivosPorBeneficiario(parBeneficiario);
        }
        public static List<cPlan> TraerTodosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return pPlan.TraerTodosPorBeneficiario(parBeneficiario);
        }
        public static bool ModificarFechaVencimiento(cPlan parPlan)
        {
            return pPlan.ModificarFechaVencimiento(parPlan);
        }
    }
}
