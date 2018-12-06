using Common.Clases;
using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class dFachada
    {
        #region BENEFICIARIO

        public static bool BeneficiarioAgregar(cBeneficiario elBeneficiario)
        {
            cBeneficiario unBeneficiario = null;
            if (dBeneficiario.Agregar(elBeneficiario))
            {
                unBeneficiario = BeneficiarioTraerEspecificoCI(elBeneficiario);
            }
            if (unBeneficiario != null)
            {
                elBeneficiario.Codigo = unBeneficiario.Codigo;
                if (elBeneficiario.lstPlanes != null)
                {
                    PlanAgregar(elBeneficiario);
                }

                return true;
            }
            return false;
        }
        public static bool BeneficiarioHabilitar(cBeneficiario elBeneficiario)
        {
            return dBeneficiario.Habilitar(elBeneficiario);
        }
        public static bool BeneficiarioInhabilitar(cBeneficiario elBeneficiario)
        {
            return dBeneficiario.Inhabilitar(elBeneficiario);
        }
        public static bool BeneficiarioModificar(cBeneficiario elBeneficiario)
        {
            return dBeneficiario.Modificar(elBeneficiario);
        }
        public static cBeneficiario BeneficiarioTraerEspecifico(cBeneficiario elBeneficiario)
        {
            return dBeneficiario.TraerEspecifico(elBeneficiario);
        }
        public static cBeneficiario BeneficiarioTraerEspecificoCI(cBeneficiario elBeneficiario)
        {
            return dBeneficiario.TraerEspecificoCI(elBeneficiario);
        }
        public static List<cBeneficiario> BeneficiarioTraerTodos()
        {
            return dBeneficiario.TraerTodos();
        }
        public static List<cBeneficiario> BeneficiarioTraerTodosConFiltros(string parConsulta)
        {
            return dBeneficiario.TraerTodosConFiltros(parConsulta);
        }



        #endregion

        #region PLAN


        public static bool PlanAgregar(cBeneficiario elBeneficiario)
        {
            return dPlan.Agregar(elBeneficiario);
        }
        public static bool PlanEliminar(cPlan elPlan)
        {
            return dPlan.Eliminar(elPlan);
        }
        public static List<cPlan> PlanTraerActivosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            return dPlan.TraerActivosPorBeneficiario(elBeneficiario);
        }
        public static List<cPlan> PlanTraerInactivosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            return dPlan.TraerInactivosPorBeneficiario(elBeneficiario);
        }
        public static List<cPlan> PlanTraerTodosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            return dPlan.TraerTodosPorBeneficiario(elBeneficiario);
        }

        #endregion
    }
}
