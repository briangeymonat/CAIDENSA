using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cUtilidades
    {
        public enum TipoDeUsuario {Administrador, Administrativo, Usuario}
        public enum EstadoInforme { Pendiente, EnProceso, Terminado}
        public enum TipoInforme { EvaluacionPsicomotriz, EvaluacionPsicopedagogica, EvaluacionPsicologica, EvaluacionFonoaudiologa, Evolucion, ToleranciaCurricular, Juzgado, Interdiciplinario, Otro}
        public enum TipoSesion { Individual, Grupo2, Grupo3, Taller, PROES}
        public enum Centro { JuanLacaze, NuevaHelvecia}
        public enum EstadoSesion { Asistio, NoAsistio, Reprogramada, SinEstado}
    }
}
