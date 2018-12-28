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
        public static List<cItinerario> TraerTodosPorDia(char parDia, int parCentro)
        {
            return pItinerario.TraerTodosPorDia(parDia, parCentro);
        }
        public static bool ModificarEstadoDelDia(char parDia)
        {
            return pItinerario.ModificarEstadoDelDia(parDia);
        }
    }
}
