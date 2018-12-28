using Common.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ejemplo.Web
{
    public class ListarInformes
    {
        public int Codigo { get; set; }
        public string Fecha { get; set; }
        public cUtilidades.TipoInforme Tipo { get; set; }
        public cUtilidades.EstadoInforme Estado { get; set; }
        public int CodigoBeneficiario { get; set; }
        public string NombresBeneficiario { get; set; }
        public string ApellidosBeneficiario { get; set; }

    }
}