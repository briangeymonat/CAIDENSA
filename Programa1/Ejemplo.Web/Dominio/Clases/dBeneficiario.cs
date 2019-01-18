using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dBeneficiario
    {
        public static bool Agregar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Agregar(elBeneficiario);
        }
        public static bool Habilitar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Habilitar(elBeneficiario);
        }
        public static bool Inhabilitar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Inhabilitar(elBeneficiario);
        }
        public static bool Modificar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Modificar(elBeneficiario);
        }
        public static cBeneficiario TraerEspecifico(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.TraerEspecifico(elBeneficiario);
        }
        public static cBeneficiario TraerEspecificoCI(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.TraerEspecificoCI(elBeneficiario);
        }
        public static cBeneficiario TraerEspecificoVerificarModificar(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.TraerEspecificoVerificarModificar(parBeneficiario);
        }
        public static List<cBeneficiario> TraerTodos()
        {
            return pBeneficiario.TraerTodos();
        }
        public static List<cBeneficiario> TraerTodosConFiltros(string parConsulta)
        {
            return pBeneficiario.TraerTodosConFiltros(parConsulta);
        }
        public static List<cBeneficiarioItinerario> TraerTodosPorItinerario(cItinerario parItinerario)
        {
            return pBeneficiario.TraerTodosPorItinerario(parItinerario);
        }
        public static List<cBeneficiario> TraerTodosPorEspecialista(cUsuario parUsuario)
        {
            return pBeneficiario.TraerTodosPorEspecialista(parUsuario);
        }
        public static List<cBeneficiario> TraerTodosPorDiagnostico(cDiagnostico parDiagnostico)
        {
            return pBeneficiario.TraerTodosPorDiagnostico(parDiagnostico);
        }

        #region ESTADISTICA Beneficiarios activos por rango de edad
        public static List<cBeneficiario> TraerActivosPorEdad(int parDesde, int parHasta)
        {
            return pBeneficiario.TraerActivosPorEdad(parDesde, parHasta);
        }
        public static Tuple<List<string>, List<int>> TraerCantidadParaCadaAñoPorDiagnostico(cDiagnostico parDiagnostico)
        {
            return pBeneficiario.TraerCantidadParaCadaAñoPorDiagnostico(parDiagnostico);
        }
        public static Tuple<List<cDiagnostico>, List<int>> TraerCantidadParaCadaDiagnosticoPorAño(int parAño)
        {
            return pBeneficiario.TraerCantidadParaCadaDiagnosticoPorAño(parAño);
        }


            #endregion
        }
}
