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
        public static bool Agregar(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.Agregar(parBeneficiario);
        }
        public static bool Habilitar(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.Habilitar(parBeneficiario);
        }
        public static bool Inhabilitar(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.Inhabilitar(parBeneficiario);
        }
        public static bool Modificar(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.Modificar(parBeneficiario);
        }
        public static cBeneficiario TraerEspecifico(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.TraerEspecifico(parBeneficiario);
        }
        public static cBeneficiario TraerEspecificoCI(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.TraerEspecificoCI(parBeneficiario);
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
        public static string CentroPreferencia(cBeneficiario parBeneficiario)
        {
            return pBeneficiario.CentroPreferencia(parBeneficiario);
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
