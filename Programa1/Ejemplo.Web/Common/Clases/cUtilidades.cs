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
        public enum TipoInforme { Evaluacion_Psicomotriz, Evaluacion_Psicopedagogica, Evaluacion_Psicologica, Evaluacion_Fonoaudiologa, Evolucion, Tolerancia_Curricular, Juzgado, Interdiciplinario, Otro}
        public enum TipoSesion { Individual, Grupo2, Grupo3, Taller, PROES}
        public enum Centro { JuanLacaze, NuevaHelvecia}
        public enum EstadoSesion { Asistio, NoAsistio, Reprogramada, SinEstado}
        public enum NombreSeccion {
            Título,
            Encuadre,
            Diagnóstico,
            Antecedentes_patológicos,
            Desarrollo,
            Presentación,
            Abordaje_Psicomotriz,
            Abordaje_Pedagógico,
            Abordaje_Psicológico,
            Abordaje_Fonoaudiológico,
            Abordaje_Fisioterapéutico,
            En_Suma,
            Sugerencias }
    }
}
