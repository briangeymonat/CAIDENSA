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
        public static List<cSesion> TraerPasaronDelDia()
        {
            return pSesion.TraerPasaronDelDia();
        }
        public static List<cSesion> TraerProximasDelDiaPorEspecialista(cUsuario parUsuario)
        {
            return pSesion.TraerProximasDelDiaPorEspecialista(parUsuario);
        }
        public static List<cSesion> TraerPasaronDelDiaPorEspecialista(cUsuario parUsuario)
        {
            return pSesion.TraerPasaronDelDiaPorEspecialista(parUsuario);
        }

        
    }
}