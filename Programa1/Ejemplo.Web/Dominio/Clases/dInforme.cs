using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dInforme
    {
        public static bool Agregar(cInforme parInforme)
        {
            return pInforme.Agregar(parInforme);
        }
        public static bool Redactar(cInforme parInforme)
        {
            return pInforme.Redactar(parInforme);
        }

        public static bool Finalizar(cInforme parInforme)
        {
            return pInforme.Finalizar(parInforme);
        }
        public static bool FinalizarSecciones(cInforme parInforme, cUsuario parUsuario)
        {
            return pInforme.FinalizarSecciones(parInforme, parUsuario);
        }

        public static int UltimoIngresado()
        {
            return pInforme.UltimoIngresado();
        }
        public static List<cInforme> TraerTodosPendientesPorEspecialista(cUsuario parUsuario)
        {
            return pInforme.TraerTodosPendientesPorEspecialista(parUsuario);
        }
        public static List<cInforme> TraerTodosEnProcesoPorEspecialista(cUsuario parUsuario)
        {
            return pInforme.TraerTodosEnProcesoPorEspecialista(parUsuario);
        }
        public static List<cInforme> TraerTodosTerminadosPorEspecialista(cUsuario parUsuario)
        {
            return pInforme.TraerTodosTerminadosPorEspecialista(parUsuario);
        }

        public static cInforme TraerEspecifico(cInforme parInforme)
        {
            return pInforme.TraerEspecifico(parInforme);
        }
        public static int VerificarSeccionesTerminadas(cInforme parInforme, cUsuario parUsuario)
        {
            return pInforme.VerificarSeccionesTerminadas(parInforme, parUsuario);
        }
        public static List<cInforme> TraerTodosConFiltros(string parConsulta)
        {
            return pInforme.TraerTodosConFiltros(parConsulta);
        }
        public static List<cInforme> TraerTodosTerminadosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return pInforme.TraerTodosTerminadosPorBeneficiario(parBeneficiario);
        }

    }
}
