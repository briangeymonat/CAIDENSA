﻿using Common.Clases;
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

        public static bool UsuarioAgregar(cUsuario parUsuario)
        {
            return dUsuario.Agregar(parUsuario);
        }
        public static bool UsuarioEliminar(cUsuario parUsuario)
        {
            return dUsuario.Eliminar(parUsuario);
        }
        public static bool UsuarioHabilitar(cUsuario parUsuario)
        {
            return dUsuario.Habilitar(parUsuario);
        }

        public static bool UsuarioModificar(cUsuario parUsuario)
        {
            return dUsuario.Modificar(parUsuario);
        }
        public static bool UsuarioAgregarConstrasena(cUsuario parUsuario)
        {
            return dUsuario.AgregarContrasena(parUsuario);
        }
        public static bool UsuarioRestablecerContrasena(cUsuario parUsuario)
        {
            return dUsuario.RestablecerContrasena(parUsuario);
        }
        public static cUsuario UsuarioTraerEspecifico(cUsuario parUsuario)
        {
            return dUsuario.TraerEspecifico(parUsuario);
        }
        public static cUsuario UsuarioTraerEspecificoXNickName(cUsuario parUsuario)
        {
            return dUsuario.TraerEspecificoXNickName(parUsuario);
        }
        public static List<cUsuario> UsuarioTraerTodosActivos()
        {
            return dUsuario.TraerTodosActivos();
        }
        public static List<cUsuario> UsuarioTraerTodosInactivos()
        {
            return dUsuario.TraerTodosInactivos();
        }
        public static int UsuarioVerificarNickNameYCi(cUsuario parUsuario)
        {
            return dUsuario.VerificarNickNameYCi(parUsuario);
        }
        public static int UsuarioExisteNickNameSinContrasena(cUsuario parUsuario)
        {
            return dUsuario.ExisteNickNameSinContrasena(parUsuario);
        }
        public static cUsuario UsuarioVerificarInicioSesion(cUsuario parUsuario)
        {
            return dUsuario.VerificarInicioSesion(parUsuario);
        }
        public static List<cUsuario> UsuarioTraerTodosActivosPorNombreApellido(string parTexto)
        {
            return dUsuario.TraerTodosActivosPorNombreApellido(parTexto);
        }
        public static List<cUsuario> UsuarioTraerTodosInactivosPorNombreApellido(string parTexto)
        {
            return dUsuario.TraerTodosInactivosPorNombreApellido(parTexto);
        }
        public static List<cUsuario> UsuarioTraerTodosActivosPorCI(string parTexto)
        {
            return dUsuario.TraerTodosActivosPorCI(parTexto);
        }
        public static List<cUsuario> UsuarioTraerTodosInactivosPorCI(string parTexto)
        {
            return dUsuario.TraerTodosInactivosPorCI(parTexto);
        }
        public static int UsuarioCantidadAdministradoresActivos()
        {
            return dUsuario.CantidadAdministradoresActivos();
        }
        public static List<cUsuario> UsuarioTraerTodosEspecialistasActivos()
        {
            return dUsuario.TraerTodosEspecialistasActivos();
        }
        public static List<cUsuario> UsuarioTraerTodosEspecialistasActivosPorEspecialidad(cEspecialidad parEspecialidad)
        {
            return dUsuario.TraerTodosEspecialistasActivosPorEspecialidad(parEspecialidad);
        }
        public static List<cUsuario> UsuarioTraerEspecialistasConFiltros(string parConsulta)
        {
            return dUsuario.TraerEspecialistasConFiltros(parConsulta);
        }
        public static List<cUsuario> UsuarioTraerTodosEspecialistasConInformesPendientes()
        {
            return dUsuario.TraerTodosEspecialistasConInformesPendientes();
        }
        public static List<cUsuario> UsuarioTraerTodosPorItinerario(cItinerario parItinerario)
        {
            return dUsuario.TraerTodosPorItinerario(parItinerario);
        }

        public static List<cUsuarioSeccion> UsuarioSeccionTraerTodosPorSeccion(cSeccion parSeccion)
        {
            return dUsuario.TraerTodosPorSeccion(parSeccion);
        }
        public static cUsuario UsuarioTraerPrimeroPorEspecialidad(cEspecialidad parEspecialidad)
        {
            return dUsuario.TraerPrimeroPorEspecialidad(parEspecialidad);
        }


        #endregion

        #region Especialidad

        public static List<cEspecialidad> EspecialidadTraerTodas()
        {
            return dEspecialidad.TraerTodas();
        }
        public static cEspecialidad EspecialidadTraerEspecifica(cEspecialidad parEspecialidad)
        {
            return dEspecialidad.TraerEspecifica(parEspecialidad);
        }
        public static cEspecialidad EspecialidadTraerEspecificaPorNombre(cEspecialidad parEspecialidad)
        {
            return dEspecialidad.TraerEspecificaPorNombre(parEspecialidad);
        }
        #endregion


        #region BENEFICIARIO

        public static bool BeneficiarioAgregar(cBeneficiario parBeneficiario)
        {
            cBeneficiario unBeneficiario = null;
            if (dBeneficiario.Agregar(parBeneficiario))
            {
                unBeneficiario = BeneficiarioTraerEspecificoCI(parBeneficiario);
            }
            if (unBeneficiario != null)
            {
                parBeneficiario.Codigo = unBeneficiario.Codigo;
                if (parBeneficiario.lstPlanes != null)
                {
                    PlanAgregar(parBeneficiario);
                }

                return true;
            }
            return false;
        }
        public static bool BeneficiarioHabilitar(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.Habilitar(parBeneficiario);
        }
        public static bool BeneficiarioInhabilitar(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.Inhabilitar(parBeneficiario);
        }
        public static bool BeneficiarioModificar(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.Modificar(parBeneficiario);
        }
        public static cBeneficiario BeneficiarioTraerEspecifico(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.TraerEspecifico(parBeneficiario);
        }
        public static cBeneficiario BeneficiarioTraerEspecificoCI(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.TraerEspecificoCI(parBeneficiario);
        }
        public static cBeneficiario BeneficiarioTraerEspecificoVerificarModificar(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.TraerEspecificoVerificarModificar(parBeneficiario);
        }
        public static List<cBeneficiario> BeneficiarioTraerTodos()
        {
            return dBeneficiario.TraerTodos();
        }
        public static List<cBeneficiario> BeneficiarioTraerTodosConFiltros(string parConsulta)
        {
            List<cBeneficiario> lstBeneficiarios = dBeneficiario.TraerTodosConFiltros(parConsulta);
            foreach(cBeneficiario unBen in lstBeneficiarios)
            {
                unBen.lstPlanes = PlanTraerActivosPorBeneficiario(unBen);
            }
            return lstBeneficiarios;
        }
        public static List<cBeneficiarioItinerario> BeneficiarioTraerTodosPorItinerario(cItinerario parItinerario)
        {
            return dBeneficiario.TraerTodosPorItinerario(parItinerario);
        }
        public static List<cBeneficiario> BeneficiarioTraerTodosPorEspecialista(cUsuario parUsuario)
        {
            return dBeneficiario.TraerTodosPorEspecialista(parUsuario);
        }
        public static List<cBeneficiario> BeneficiarioTraerTodosPorDiagnostico(cDiagnostico parDiagnostico)
        {
            return dBeneficiario.TraerTodosPorDiagnostico(parDiagnostico);
        }
        public static string BeneficiarioCentroPreferencia(cBeneficiario parBeneficiario)
        {
            return dBeneficiario.CentroPreferencia(parBeneficiario);
        }

        #endregion


        #region PLAN


        public static bool PlanAgregar(cBeneficiario parBeneficiario)
        {
            return dPlan.Agregar(parBeneficiario);
        }
        public static bool PlanEliminar(cPlan parPlan)
        {
            return dPlan.Eliminar(parPlan);
        }
        public static List<cPlan> PlanTraerActivosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dPlan.TraerActivosPorBeneficiario(parBeneficiario);
        }
        public static List<cPlan> PlanTraerInactivosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dPlan.TraerInactivosPorBeneficiario(parBeneficiario);
        }
        public static List<cPlan> PlanTraerTodosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dPlan.TraerTodosPorBeneficiario(parBeneficiario);
        }
        public static bool PlanModificarFechaVencimiento(cPlan parPlan)
        {
            return dPlan.ModificarFechaVencimiento(parPlan);
        }

        #endregion

        #region Informe

        public static bool InformeAgregar(cInforme parInforme)
        {
            return dInforme.Agregar(parInforme);
        }
        public static bool InformeRedactar(cInforme parInforme)
        {
            return dInforme.Redactar(parInforme);
        }
        public static bool InformeFinalizar(cInforme parInforme)
        {
            return dInforme.Finalizar(parInforme);
        }
        public static bool InformeFinalizarSecciones(cInforme parInforme, cUsuario parUsuario)
        {
            return dInforme.FinalizarSecciones(parInforme, parUsuario);
        }
        public static int InformeUltimoIngresado()
        {
            return dInforme.UltimoIngresado();
        }
        public static List<cInforme> InformeTraerTodosPendientesPorEspecialista(cUsuario parUsuario)
        {
            return dInforme.TraerTodosPendientesPorEspecialista(parUsuario);
        }
        public static List<cInforme> InformeTraerTodosEnProcesoPorEspecialista(cUsuario parUsuario)
        {
            return dInforme.TraerTodosEnProcesoPorEspecialista(parUsuario);
        }
        public static List<cInforme> InformeTraerTodosTerminadosPorEspecialista(cUsuario parUsuario)
        {
            return dInforme.TraerTodosTerminadosPorEspecialista(parUsuario);
        }
        public static cInforme InformeTraerEspecifico(cInforme parInforme)
        {
            return dInforme.TraerEspecifico(parInforme);
        }
        public static int InformeVerificarSeccionesTerminadas(cInforme parInforme, cUsuario parUsuario)
        {
            return dInforme.VerificarSeccionesTerminadas(parInforme, parUsuario);
        }
        public static List<cInforme> InformeTraerTodosConFiltros(string parConsulta)
        {
            return dInforme.TraerTodosConFiltros(parConsulta);
        }
        public static List<cInforme> InformeTraerTodosTerminadosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dInforme.TraerTodosTerminadosPorBeneficiario(parBeneficiario);
        }

        #endregion

        #region Itinerario

        public static bool ItinerarioAgregar(cItinerario parItinerario)
        {
            return dItinerario.Agregar(parItinerario);
        }
        public static List<cUsuario> ItinerarioVerificarHorarioUsuario(cItinerario parItinerario)
        {
            return dItinerario.VerificarHorarioUsuario(parItinerario);
        }
        public static List<cBeneficiario> ItinerarioVerificarHorarioBeneficiarios(cItinerario parItinerario)
        {
            return dItinerario.VerificarHorarioBeneficiarios(parItinerario);
        }
        public static List<cUsuario> ItinerarioVerificarHorarioUsuarioModificar(cItinerario parItinerario)
        {
            return dItinerario.VerificarHorarioUsuarioModificar(parItinerario);
        }
        public static List<cBeneficiario> ItinerarioVerificarHorarioBeneficiariosModificar(cItinerario parItinerario)
        {
            return dItinerario.VerificarHorarioBeneficiariosModificar(parItinerario);
        }

        public static List<cItinerario> ItinerarioTraerTodosPorDia(char parDia, int parCentro)
        {
            List<cItinerario> lstItinerarios = dItinerario.TraerTodosPorDia(parDia, parCentro);
            for (int i = 0; i < lstItinerarios.Count; i++)
            {
                lstItinerarios[i].lstEspecialistas = UsuarioTraerTodosPorItinerario(lstItinerarios[i]);
            }
            for (int i = 0; i < lstItinerarios.Count; i++)
            {
                lstItinerarios[i].lstBeneficiarios = BeneficiarioTraerTodosPorItinerario(lstItinerarios[i]);
            }
            return lstItinerarios;
        }
        public static List<cItinerario> ItinerarioTraerTodosPorEspecialista(cUsuario parUsuario)
        {
            List<cItinerario> lstItinerarios = dItinerario.TraerTodosPorEspecialista(parUsuario);
            for (int i = 0; i < lstItinerarios.Count; i++)
            {
                lstItinerarios[i].lstEspecialistas = UsuarioTraerTodosPorItinerario(lstItinerarios[i]);
            }
            for (int i = 0; i < lstItinerarios.Count; i++)
            {
                lstItinerarios[i].lstBeneficiarios = BeneficiarioTraerTodosPorItinerario(lstItinerarios[i]);
            }
            return lstItinerarios;
        }
        public static List<cItinerario> ItinerarioTraerTodosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cItinerario> lstItinerarios = dItinerario.TraerTodosPorBeneficiario(parBeneficiario);
            for (int i = 0; i < lstItinerarios.Count; i++)
            {
                lstItinerarios[i].lstEspecialistas = UsuarioTraerTodosPorItinerario(lstItinerarios[i]);
            }
            for (int i = 0; i < lstItinerarios.Count; i++)
            {
                lstItinerarios[i].lstBeneficiarios = BeneficiarioTraerTodosPorItinerario(lstItinerarios[i]);
            }
            return lstItinerarios;
        }
        public static cItinerario ItinerarioTraerEspecifico(cItinerario parItinerario)
        {
            cItinerario unItinerario = dItinerario.TraerEspecifico(parItinerario);
            unItinerario.lstEspecialistas = UsuarioTraerTodosPorItinerario(unItinerario);
            unItinerario.lstBeneficiarios = BeneficiarioTraerTodosPorItinerario(unItinerario);
            return unItinerario;
        }

        public static bool ItinerarioModificarEstadoDelDia(char parDia)
        {
            return dItinerario.ModificarEstadoDelDia(parDia);
        }
        public static string ItinerarioTraerEncuadrePorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dItinerario.TraerEncuadrePorBeneficiario(parBeneficiario);
        }

        public static bool ItinerarioModificar(cItinerario parItinerario)
        {
            return dItinerario.Modificar(parItinerario);
        }
        public static bool ItinerarioEliminar(cItinerario parItinerario)
        {
            return dItinerario.Eliminar(parItinerario);
        }
        public static bool ItinerarioRestablecer()
        {
            return dItinerario.Restablecer();
        }
        public static List<cItinerario> ItinerarioTraerTodos()
        {
            return dItinerario.TraerTodos();
        }
        #endregion

        #region Notificacion

        public static List<cNotificacion> NotifiacionTraerTodasNuevasAdministrador(cUsuario parUsuario)
        {
            return dNotificacion.TraerTodasNuevasAdministrador(parUsuario);
        }
        public static List<cNotificacion> NotifiacionTraerTodasNuevasEspecialista(cUsuario parUsuario)
        {
            return dNotificacion.TraerTodasNuevasEspecialista(parUsuario);
        }
        public static bool NotificacionAgregarDeEspecialista(cNotificacion parNotificacion)
        {
            return dNotificacion.AgregarDeEspecialista(parNotificacion);
        }
        public static bool NotificacionAgregarDeAdministrador(cNotificacion parNotificacion)
        {
            return dNotificacion.AgregarDeAdministrador(parNotificacion);
        }
        public static int NotificacionVerificarIngresoParaAdministrador(cNotificacion parNotificacion)
        {
            return dNotificacion.VerificarIngresoParaAdministrador(parNotificacion);
        }
        public static bool NotificacionCambiarEstadoVista(cNotificacion parNotificacion)
        {
            return dNotificacion.CambiarEstadoVista(parNotificacion);
        }
        #endregion


        #region Sesion

        public static bool SesionAgregar(cSesion parSesion)
        {
            return dSesion.Agregar(parSesion);
        }

        public static bool SesionAgregarSesionesDelDia()
        {
            char dia;
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dia = 'L';
                    break;
                case DayOfWeek.Tuesday:
                    dia = 'M';
                    break;
                case DayOfWeek.Wednesday:
                    dia = 'X';
                    break;
                case DayOfWeek.Thursday:
                    dia = 'J';
                    break;
                case DayOfWeek.Friday:
                    dia = 'V';
                    break;
                default:
                    dia = 'S';
                    break;
            }
            List<cItinerario> lstJL = ItinerarioTraerTodosPorDia(dia, 0);
            List<cItinerario> lstNH = ItinerarioTraerTodosPorDia(dia, 1);
            cSesion unaSesion;
            bool bRetorno = true;
            foreach(cItinerario unItinerario in lstJL)
            {
                unaSesion = new cSesion();
                if (!unItinerario.Estado)
                {
                    unaSesion.Centro = unItinerario.Centro;
                    unaSesion.Codigo = unItinerario.Codigo;
                    unaSesion.Fecha = DateTime.Today.ToShortDateString();
                    unaSesion.HoraFin = unItinerario.HoraFin;
                    unaSesion.HoraInicio = unItinerario.HoraInicio;
                    unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                    for (int i=0; i<unItinerario.lstBeneficiarios.Count;i++)
                    {
                        unaSesion.lstBeneficiarios.Add(new cBeneficiarioSesion());
                        unaSesion.lstBeneficiarios[i].Beneficiario = unItinerario.lstBeneficiarios[i].Beneficiario;
                        unaSesion.lstBeneficiarios[i].Plan = unItinerario.lstBeneficiarios[i].Plan;
                        unaSesion.lstBeneficiarios[i].Estado = cUtilidades.EstadoSesion.SinEstado;
                    }
                    unaSesion.lstUsuarios = unItinerario.lstEspecialistas;
                    unaSesion.TipoSesion = unItinerario.TipoSesion;
                    bRetorno = bRetorno == SesionAgregar(unaSesion);
                }
            }
            foreach (cItinerario unItinerario in lstNH)
            {
                unaSesion = new cSesion();
                if (!unItinerario.Estado)
                {
                    unaSesion.Centro = unItinerario.Centro;
                    unaSesion.Codigo = unItinerario.Codigo;
                    unaSesion.Fecha = DateTime.Today.ToShortDateString();
                    unaSesion.HoraFin = unItinerario.HoraFin;
                    unaSesion.HoraInicio = unItinerario.HoraInicio;
                    unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                    for (int i = 0; i < unItinerario.lstBeneficiarios.Count; i++)
                    {
                        unaSesion.lstBeneficiarios.Add(new cBeneficiarioSesion());
                        unaSesion.lstBeneficiarios[i].Beneficiario = unItinerario.lstBeneficiarios[i].Beneficiario;
                        unaSesion.lstBeneficiarios[i].Plan = unItinerario.lstBeneficiarios[i].Plan;
                        unaSesion.lstBeneficiarios[i].Estado = cUtilidades.EstadoSesion.SinEstado;
                    }
                    unaSesion.lstUsuarios = unItinerario.lstEspecialistas;
                    unaSesion.TipoSesion = unItinerario.TipoSesion;
                    bRetorno = bRetorno == SesionAgregar(unaSesion);
                }
            }
            if(bRetorno)
            {

                bRetorno = bRetorno == ItinerarioModificarEstadoDelDia(dia);
            }
            
            return bRetorno;
            
        }

        public static List<cSesion> SesionTraerPasaronDelDia()
        {
            return dSesion.TraerPasaronDelDia();
        }
        public static List<cSesion> SesionTraerProximasDelDiaPorEspecialista(cUsuario parUsuario)
        {
            return dSesion.TraerProximasDelDiaPorEspecialista(parUsuario);
        }
        public static List<cSesion> SesionTraerPasaronDelDiaPorEspecialista(cUsuario parUsuario)
        {
            return dSesion.TraerPasaronDelDiaPorEspecialista(parUsuario);
        }
        public static cSesion SesionTraerEspecifico(cSesion parSesion)
        {
            return dSesion.TraerEspecifico(parSesion);
        }
        public static bool SesionMarcarAsitencias(cSesion parSesion)
        {
            return dSesion.MarcarAsitencias(parSesion);
        }
        public static List<cUsuario> SesionVerificarFechaYHorarioUsuario(cSesion parSesion)
        {
            return dSesion.VerificarFechaYHorarioUsuario(parSesion);
        }
        public static List<cBeneficiario> SesionVerificarFechaYHorarioBeneficiario(cSesion parSesion)
        {
            return dSesion.VerificarFechaYHorarioBeneficiario(parSesion);
        }
        public static bool SesionAgregarObservacion(cUsuarioSesion parUS)
        {
            return dSesion.AgregarObservacion(parUS);
        }
        public static List<cSesion> SesionTraerTodasPorEspecialistaConFiltros(string parConsulta)
        {
            return dSesion.TraerTodasPorEspecialistaConFiltros(parConsulta);
        }
        public static cUsuarioSesion SesionTraerObservacionPorUsuarioYSesion(cUsuarioSesion parUsuarioSesion)
        {
            return dSesion.TraerObservacionPorUsuarioYSesion(parUsuarioSesion);
        }
        public static List<DateTime> SesionTraerMaximaFechaYMinimaFecha()
        {
            return dSesion.TraerMaximaFechaYMinimaFecha();
        }
        public static List<string> SesionTraerAsistenciasDeBeneficiarioPorMes(cBeneficiario parBeneficiario, string parAno, string parMes)
        {
            return dSesion.TraerAsistenciasDeBeneficiarioPorMes(parBeneficiario, parAno, parMes);
        }
        public static List<cSesion> SesionTraerPorRango(DateTime parFechaInicial, DateTime parFechaFinal, cUsuario parUsuario)
        {
            return dSesion.TraerPorRango(parFechaInicial, parFechaFinal, parUsuario);
        }
        public static List<cSesion> SesionTraerTodasPorFecha(DateTime parFecha, int parCentro)
        {
            return dSesion.TraerTodasPorFecha(parFecha, parCentro);
        }
        #endregion

        #region Seccion

        public static List<cSeccion> SeccionTraerTodasPorInforme(cInforme parInforme)
        {
            return dSeccion.TraerTodasPorInforme(parInforme);
        }


        #endregion

        #region Diagnostico

        public static List<string> DiagnosticoTraerUltimosDiagnosticosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dDiagnostico.TraerUltimosDiagnosticosPorBeneficiario(parBeneficiario);
        }
        public static List<cDiagnosticoBeneficiario> DiagnosticoTraerTodosDiagnosticosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            return dDiagnostico.TraerTodosDiagnosticosPorBeneficiario(parBeneficiario);
        }
        public static List<cDiagnostico> DiagnosticoTraerTodos()
        {
            return dDiagnostico.TraerTodos();
        }
        public static bool DiagnosticoAgregarDiagnosticoBeneficiario(cBeneficiario parBeneficiario)
        {
            return dDiagnostico.AgregarDiagnosticoBeneficiario(parBeneficiario);
        }
        public static bool DiagnosticoAgregar(cDiagnostico parDiagnostico)
        {
            return dDiagnostico.Agregar(parDiagnostico);
        }
        public static bool DiagnosticoExiste(cDiagnostico parDiagnostico)
        {
            return dDiagnostico.Existe(parDiagnostico);
        }
        public static bool DiagnosticoExisteDiagnosticoBeneficiario(cDiagnostico parDiagnostico)
        {
            return dDiagnostico.ExisteDiagnosticoBeneficiario(parDiagnostico);
        }
        public static bool DiagnosticoEliminar(cDiagnostico parDiagnostico)
        {
            return dDiagnostico.Eliminar(parDiagnostico);
        }
        public static List<string> DiagnosticoTraerTodosAñosQueHayDiagnosticos()
        {
            return dDiagnostico.TraerTodosAñosQueHayDiagnosticos();
        }

        #endregion

        #region ESTADISTICAS
        public static List<List<string>> EstadisticaTraerCantidadSesionPorTipoSesion(string parConsulta)
        {
            return dUsuario.TraerCantidadSesionPorTipoSesion(parConsulta);
        }
        public static List<cBeneficiario> EstadisticaBeneficiarioTraerActivosPorEdad(int parDesde, int parHasta)
        {
            return dBeneficiario.TraerActivosPorEdad(parDesde, parHasta);
        }
        public static Tuple<List<string>, List<int>> EstadisticaTraerCantidadParaCadaAñoPorDiagnostico(cDiagnostico parDiagnostico)
        {
            return dBeneficiario.TraerCantidadParaCadaAñoPorDiagnostico(parDiagnostico);
        }
        public static Tuple<List<cDiagnostico>, List<int>> EstadisticaTraerCantidadParaCadaDiagnosticoPorAño(int parAño)
        {
            return dBeneficiario.TraerCantidadParaCadaDiagnosticoPorAño(parAño);
        }
        #endregion
    }
}
