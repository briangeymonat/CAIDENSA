﻿using System;
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
                //return "Data Source=SQL7002.site4now.net;Initial Catalog=DB_A4558A_sacaiden;User Id=DB_A4558A_sacaiden_admin;Password=caiden2019;";
                //return "Data Source=Brian\\MSSQLSERVER2;Initial Catalog=SACAIDEN_DB_Prueba;Trusted_Connection=yes;MultipleActiveResultSets=true;";
                return "Data Source=DESKTOP-20O7FSI;Initial Catalog=SACAIDEN_DB;Trusted_Connection=yes;MultipleActiveResultSets=true;";
                //return "Data Source=Brian\\MSSQLSERVER2;Initial Catalog=SACAIDEN_DB;Trusted_Connection=yes;MultipleActiveResultSets=true;";
                //return "Data Source=SEBASTIÁN\\SQLEXPRESS;Initial Catalog=SACAIDEN_DB;Trusted_Connection=yes;MultipleActiveResultSets=true;";
            }
        }
    }
}
