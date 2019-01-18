using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dItinerario
    {
        public static bool Agregar(cItinerario parItinerario)
        {
            return pItinerario.Agregar(parItinerario);
        }
        public static List<cUsuario> VerificarHorarioUsuario(cItinerario parItinerario)
        {
            return pItinerario.VerificarHorarioUsuario(parItinerario);
        }
        public static List<cBeneficiario> VerificarHorarioBeneficiarios(cItinerario parItinerario)
        {
            return pItinerario.VerificarHorarioBeneficiarios(parItinerario);
        }
        public static List<cUsuario> VerificarHorarioUsuarioModificar(cItinerario parItinerario)
        {
            return pItinerario.VerificarHorarioUsuarioModificar(parItinerario);
        }
        public static List<cBeneficiario> VerificarHorarioBeneficiariosModificar(cItinerario parItinerario)
        {
            return pItinerario.VerificarHorarioBeneficiariosModificar(parItinerario);
        }
        public static List<cItinerario> TraerTodosPorDia(char parDia, int parCentro)
        {
            return pItinerario.TraerTodosPorDia(parDia, parCentro);
        }
        public static bool ModificarEstadoDelDia(char parDia)
        {
            return pItinerario.ModificarEstadoDelDia(parDia);
        }
        public static string TraerEncuadrePorBeneficiario(cBeneficiario parBeneficiario)
        {
            return pItinerario.TraerEncuadrePorBeneficiario(parBeneficiario);
        }
        public static cItinerario TraerEspecifico(cItinerario parItinerario)
        {
            return pItinerario.TraerEspecifico(parItinerario);
        }
        public static bool Modificar(cItinerario parItinerario)
        {
            return pItinerario.Modificar(parItinerario);
        }
        public static bool Eliminar(cItinerario parItinerario)
        {
            return pItinerario.Eliminar(parItinerario);
        }
    }
}
