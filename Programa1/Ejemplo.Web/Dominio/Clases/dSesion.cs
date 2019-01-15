using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dSesion
    {
        public static bool Agregar(cSesion parSesion)
        {
            return pSesion.Agregar(parSesion);
        }
        public static List<cSesion> TraerPasaronDelDia()
        {
            return pSesion.TraerPasaronDelDia();
        }
        public static List<cSesion> TraerProximasDelDiaPorEspecialista(cUsuario parUsuario)
        {
            return pSesion.TraerProximasDelDiaPorEspecialista(parUsuario);
        }
        public static List<cSesion> TraerPasaronDelDiaPorEspecialista(cUsuario parUsuario)
        {
            return pSesion.TraerPasaronDelDiaPorEspecialista(parUsuario);
        }
        public static cSesion TraerEspecifico(cSesion parSesion)
        {
            return pSesion.TraerEspecifico(parSesion);
        }
        public static bool MarcarAsitencias(cSesion parSesion)
        {
            return pSesion.MarcarAsitencias(parSesion);
        }
        public static List<cUsuario> VerificarFechaYHorarioUsuario(cSesion parSesion)
        {
            return pSesion.VerificarFechaYHorarioUsuario(parSesion);
        }
        public static List<cBeneficiario> VerificarFechaYHorarioBeneficiario(cSesion parSesion)
        {
            return pSesion.VerificarFechaYHorarioBeneficiario(parSesion);
        }
        public static bool AgregarObservacion(cUsuarioSesion parUS)
        {
            return pSesion.AgregarObservacion(parUS);
        }
        public static List<cSesion> TraerTodasPorEspecialistaConFiltros(string parConsulta)
        {
            return pSesion.TraerTodasPorEspecialistaConFiltros(parConsulta);
        }
        public static cUsuarioSesion TraerObservacionPorUsuarioYSesion(cUsuarioSesion parUsuarioSesion)
        {
            return pSesion.TraerObservacionPorUsuarioYSesion(parUsuarioSesion);
        }
        public static List<DateTime> TraerMaximaFechaYMinimaFecha()
        {
            return pSesion.TraerMaximaFechaYMinimaFecha();
        }
        public static List<string> TraerAsistenciasDeBeneficiarioPorMes(cBeneficiario parBeneficiario, string parAno, string parMes)
        {
            return pSesion.TraerAsistenciasDeBeneficiarioPorMes(parBeneficiario, parAno, parMes);
        }
    }
}