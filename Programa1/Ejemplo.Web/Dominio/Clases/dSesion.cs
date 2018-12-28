using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dSesion
    {
        public static bool Agregar(cSesion parSesion)
        {
            return pSesion.Agregar(parSesion);
        }
    }
}