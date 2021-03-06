﻿using Common.Clases;
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
        public static List<cUsuario> TraerTodosActivosPorNombreApellido(string parTexto)
        {
            return pUsuario.TraerTodosActivosPorNombreApellido(parTexto);
        }
        public static List<cUsuario> TraerTodosInactivosPorNombreApellido(string parTexto)
        {
            return pUsuario.TraerTodosInactivosPorNombreApellido(parTexto);
        }
        public static List<cUsuario> TraerTodosActivosPorCI(string parTexto)
        {
            return pUsuario.TraerTodosActivosPorCI(parTexto);
        }
        public static List<cUsuario> TraerTodosInactivosPorCI(string parTexto)
        {
            return pUsuario.TraerTodosInactivosPorCI(parTexto);
        }
        public static int CantidadAdministradoresActivos()
        {
            return pUsuario.CantidadAdministradoresActivos();
        }
        public static List<cUsuario> TraerTodosEspecialistasActivos()
        {
            return pUsuario.TraerTodosEspecialistasActivos();
        }
        public static List<cUsuario> TraerTodosEspecialistasActivosPorEspecialidad(cEspecialidad parEspecialidad)
        {
            return pUsuario.TraerTodosEspecialistasActivosPorEspecialidad(parEspecialidad);
        }
        public static List<cUsuario> TraerEspecialistasConFiltros(string parConsulta)
        {
            return pUsuario.TraerEspecialistasConFiltros(parConsulta);
        }
        public static List<cUsuario> TraerTodosEspecialistasConInformesPendientes()
        {
            return pUsuario.TraerTodosEspecialistasConInformesPendientes();
        }
        public static List<cUsuario> TraerTodosPorItinerario(cItinerario parItinerario)
        {
            return pUsuario.TraerTodosPorItinerario(parItinerario);
        }

        public static List<cUsuarioSeccion> TraerTodosPorSeccion(cSeccion parSeccion)
        {
            return pUsuario.TraerTodosPorSeccion(parSeccion);
        }
        public static cUsuario TraerPrimeroPorEspecialidad(cEspecialidad parEspecialidad)
        {
            return pUsuario.TraerPrimeroPorEspecialidad(parEspecialidad);
        }

        #region Estadistica cantidad de sesion por tipo de sesion
        public static List<List<string>> TraerCantidadSesionPorTipoSesion(string parConsulta)
        {
            return pUsuario.TraerCantidadSesionPorTipoSesion(parConsulta);
        }
        #endregion

    }
}
