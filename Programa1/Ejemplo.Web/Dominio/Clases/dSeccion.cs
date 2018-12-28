using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dSeccion
    {
        public static List<cSeccion> TraerTodasPorInforme(cInforme parInforme)
        {
            return pSeccion.TraerTodasPorInforme(parInforme);
        }
    }
}
