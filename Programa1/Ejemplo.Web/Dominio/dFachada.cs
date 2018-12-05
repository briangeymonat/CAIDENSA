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
        #region Usuario

        public static bool AgregarUsuario(cUsuario parUsuario)
        {
            return dUsuario.Agregar(parUsuario);
        }
        public static bool EliminarUsuario(cUsuario parUsuario)
        {
            return dUsuario.Eliminar(parUsuario);
        }
        public static bool HabilitarUsuario(cUsuario parUsuario)
        {
            return dUsuario.Habilitar(parUsuario);
        }

        public static bool ModificarUsuario(cUsuario parUsuario)
        {
            return dUsuario.Modificar(parUsuario);
        }
        public static bool AgregarConstrasenaUsuario(cUsuario parUsuario)
        {
            return dUsuario.AgregarContrasena(parUsuario);
        }
        public static bool RestablecerContrasenaUsuario(cUsuario parUsuario)
        {
            return dUsuario.RestablecerContrasena(parUsuario);
        }
        public static cUsuario TraerEspecificoUsuario(cUsuario parUsuario)
        {
            return dUsuario.TraerEspecifico(parUsuario);
        }
        public static cUsuario TraerEspecificoXNickNameUsuario(cUsuario parUsuario)
        {
            return dUsuario.TraerEspecificoXNickName(parUsuario);
        }
        public static List<cUsuario> TraerTodosActivosUsuario()
        {
            return dUsuario.TraerTodosActivos();
        }
        public static List<cUsuario> TraerTodosInactivosUsuario()
        {
            return dUsuario.TraerTodosInactivos();
        }
        public static int VerificarNickNameYCiUsuario(cUsuario parUsuario)
        {
            return dUsuario.VerificarNickNameYCi(parUsuario);
        }
        public static int ExisteNickNameSinContrasenaUsuario(cUsuario parUsuario)
        {
            return dUsuario.ExisteNickNameSinContrasena(parUsuario);
        }
        public static cUsuario VerificarInicioSesionUsuario(cUsuario parUsuario)
        {
            return dUsuario.VerificarInicioSesion(parUsuario);
        }
        public static List<cUsuario> TraerTodosActivosPorNombreApellidoUsuario(string texto)
        {
            return dUsuario.TraerTodosActivosPorNombreApellido(texto);
        }
        public static List<cUsuario> TraerTodosInactivosPorNombreApellidoUsuario(string texto)
        {
            return dUsuario.TraerTodosInactivosPorNombreApellido(texto);
        }
        public static List<cUsuario> TraerTodosActivosPorCI(string texto)
        {
            return dUsuario.TraerTodosActivosPorCI(texto);
        }
        public static List<cUsuario> TraerTodosInactivosPorCI(string texto)
        {
            return dUsuario.TraerTodosInactivosPorCI(texto);
        }

        #endregion

        #region

        public static List<cEspecialidad> TraerTodasEspecialidades()
        {
            return dEspecialidad.TraerTodas();
        }
        public static cEspecialidad TraerEspecificaEspecialidad(cEspecialidad parEspecialidad)
        {
            return dEspecialidad.TraerEspecifica(parEspecialidad);
        }
        #endregion
    }
}
