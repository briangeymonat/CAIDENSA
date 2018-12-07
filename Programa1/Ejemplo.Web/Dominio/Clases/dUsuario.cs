using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dUsuario
    {
        public static bool Agregar(cUsuario parUsuario)
        {
            return pUsuario.Agregar(parUsuario);
        }
        public static bool Eliminar(cUsuario parUsuario)
        {
            return pUsuario.Eliminar(parUsuario);
        }
        public static bool Habilitar(cUsuario parUsuario)
        {
            return pUsuario.Habilitar(parUsuario);
        }
        public static bool Modificar(cUsuario parUsuario)
        {
            return pUsuario.Modificar(parUsuario);
        }
        public static bool AgregarContrasena(cUsuario parUsuario)
        {
            return pUsuario.AgregarContrasena(parUsuario);
        }
        public static bool RestablecerContrasena(cUsuario parUsuario)
        {
            return pUsuario.RestablecerContrasena(parUsuario);
        }
        public static cUsuario TraerEspecifico(cUsuario parUsuario)
        {
            return pUsuario.TraerEspecifico(parUsuario);
        }
        public static cUsuario TraerEspecificoXNickName(cUsuario parUsuario)
        {
            return pUsuario.TraerEspecificoXNickName(parUsuario);
        }
        public static List<cUsuario> TraerTodosActivos()
        {
            return pUsuario.TraerTodosActivos();
        }
        public static List<cUsuario> TraerTodosInactivos()
        {
            return pUsuario.TraerTodosInactivos();
        }
        public static int VerificarNickNameYCi(cUsuario parUsuario)
        {
            return pUsuario.VerificarNickNameYCi(parUsuario);
        }
        public static int ExisteNickNameSinContrasena(cUsuario parUsuario)
        {
            return pUsuario.ExisteNickNameSinContrasena(parUsuario);
        }
        public static cUsuario VerificarInicioSesion(cUsuario parUsuario)
        {
            return pUsuario.VerificarInicioSesion(parUsuario);
        }
        public static List<cUsuario> TraerTodosActivosPorNombreApellido(string texto)
        {
            return pUsuario.TraerTodosActivosPorNombreApellido(texto);
        }
        public static List<cUsuario> TraerTodosInactivosPorNombreApellido(string texto)
        {
            return pUsuario.TraerTodosInactivosPorNombreApellido(texto);
        }
        public static List<cUsuario> TraerTodosActivosPorCI(string texto)
        {
            return pUsuario.TraerTodosActivosPorCI(texto);
        }
        public static List<cUsuario> TraerTodosInactivosPorCI(string texto)
        {
            return pUsuario.TraerTodosInactivosPorCI(texto);
        }
        public static int CantidadAdministradoresActivos()
        {
            return pUsuario.CantidadAdministradoresActivos();
        }
    }
}
