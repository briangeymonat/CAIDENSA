using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dNotificacion
    {
        public static List<cNotificacion> TraerTodasNuevasAdministrador(cUsuario parUsuario)
        {
            return pNotificacion.TraerTodasNuevasAdministrador(parUsuario);
        }
        public static List<cNotificacion> TraerTodasNuevasEspecialista(cUsuario parUsuario)
        {
            return pNotificacion.TraerTodasNuevasEspecialista(parUsuario);
        }
        public static bool AgregarDeEspecialista(cNotificacion parNotificacion)
        {
            return pNotificacion.AgregarDeEspecialista(parNotificacion);
        }
        public static bool AgregarDeAdministrador(cNotificacion parNotificacion)
        {
            return pNotificacion.AgregarDeAdministrador(parNotificacion);
        }

        public static int VerificarIngresoParaAdministrador(cNotificacion parNotificacion)
        {
            return pNotificacion.VerificarIngresoParaAdministrador(parNotificacion);
        }
        public static bool CambiarEstadoVista(cNotificacion parNotificacion)
        {
            return pNotificacion.CambiarEstadoVista(parNotificacion);
        }
    }
}
