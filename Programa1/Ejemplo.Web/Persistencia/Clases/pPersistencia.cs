using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Clases
{
    public class pPersistencia
    {
        protected static string CadenaDeConexion
        {
            get
            {
                //return "Data Source=Brian\\MSSQLSERVER2;Initial Catalog=SACAIDEN_DB;Trusted_Connection=yes";
                return "Data Source=DESKTOP-20O7FSI;Initial Catalog=SACAIDEN_DB;Trusted_Connection=yes";
            }
        }
    }
}
