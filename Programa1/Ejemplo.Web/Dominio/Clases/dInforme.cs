using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dInforme
    {
        public static bool Agregar(cInforme parInforme)
        {
            return pInforme.Agregar(parInforme);
        }
        public static int UltimoIngresado()
        {
            return pInforme.UltimoIngresado();
        }
    }
}
