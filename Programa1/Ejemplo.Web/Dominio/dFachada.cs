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
        public static List<cUsuario> UsuarioTraerTodosActivosPorNombreApellido(string texto)
        {
            return dUsuario.TraerTodosActivosPorNombreApellido(texto);
        }
        public static List<cUsuario> UsuarioTraerTodosInactivosPorNombreApellido(string texto)
        {
            return dUsuario.TraerTodosInactivosPorNombreApellido(texto);
        }
        public static List<cUsuario> UsuarioTraerTodosActivosPorCI(string texto)
        {
            return dUsuario.TraerTodosActivosPorCI(texto);
        }
        public static List<cUsuario> UsuarioTraerTodosInactivosPorCI(string texto)
        {
            return dUsuario.TraerTodosInactivosPorCI(texto);
        }
        public static int UsuarioCantidadAdministradoresActivos()
        {
            return dUsuario.CantidadAdministradoresActivos();
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
        public static List<cBeneficiario> BeneficiarioTraerTodos()
        {
            return dBeneficiario.TraerTodos();
        }
        public static List<cBeneficiario> BeneficiarioTraerTodosConFiltros(string parConsulta)
        {
            return dBeneficiario.TraerTodosConFiltros(parConsulta);
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

        #endregion

    }
}
