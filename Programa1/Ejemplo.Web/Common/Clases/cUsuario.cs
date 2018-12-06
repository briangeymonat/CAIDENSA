using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clases
{
    public class cUsuario
    {
        public int Codigo { get; set; }
        public string NickName { get; set; }
        public string Constrasena { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int CI { get; set; }
        public cUtilidades.TipoDeUsuario Tipo { get; set; }
        public string Domicilio { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public Boolean Estado { get; set; }
        public cEspecialidad Especialidad { get; set; }

    }
}
