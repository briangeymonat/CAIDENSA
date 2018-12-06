using Common.Clases;
using Persistencia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class dBeneficiario
    {
        public static bool Agregar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Agregar(elBeneficiario);
        }
        public static bool Habilitar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Habilitar(elBeneficiario);
        }
        public static bool Inhabilitar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Inhabilitar(elBeneficiario);
        }
        public static bool Modificar(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.Modificar(elBeneficiario);
        }
        public static cBeneficiario TraerEspecifico(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.TraerEspecifico(elBeneficiario);
        }
        public static cBeneficiario TraerEspecificoCI(cBeneficiario elBeneficiario)
        {
            return pBeneficiario.TraerEspecificoCI(elBeneficiario);
        }
        public static List<cBeneficiario> TraerTodos()
        {
            return pBeneficiario.TraerTodos();
        }
        public static List<cBeneficiario> TraerTodosConFiltros(string parConsulta)
        {
            return pBeneficiario.TraerTodosConFiltros(parConsulta);
        }
    }
}
