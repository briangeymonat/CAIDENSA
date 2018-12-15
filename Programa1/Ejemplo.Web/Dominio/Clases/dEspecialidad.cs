using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dEspecialidad
    {
        public static List<cEspecialidad> TraerTodas()
        {
            return pEspecialidad.TraerTodas();
        }
        public static cEspecialidad TraerEspecifica(cEspecialidad parEspecialidad)
        {
            return pEspecialidad.TraerEspecifica(parEspecialidad);
        }
        public static cEspecialidad TraerEspecificaPorNombre(cEspecialidad parEspecialidad)
        {
            return pEspecialidad.TraerEspecificaPorNombre(parEspecialidad);
        }
    }
}
