using Common.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Clases
{
    public class pSeccion : pPersistencia
    {
        public static List<cSeccion> TraerTodasPorInforme(cInforme parInforme)
        {
            List<cSeccion> lstRetorno = new List<cSeccion>();
            cSeccion unaSeccion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Secciones_TraerTodasPorInforme", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));


                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSeccion = new cSeccion();
                        unaSeccion.Codigo = int.Parse(oReader["SeccionId"].ToString());
                        int i = int.Parse(oReader["SeccionNombre"].ToString());
                        if (i == 0) unaSeccion.Nombre = cUtilidades.NombreSeccion.Título;
                        if (i == 1) unaSeccion.Nombre = cUtilidades.NombreSeccion.Encuadre;
                        if (i == 2) unaSeccion.Nombre = cUtilidades.NombreSeccion.Diagnóstico;
                        if (i == 3) unaSeccion.Nombre = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (i == 4) unaSeccion.Nombre = cUtilidades.NombreSeccion.Desarrollo;
                        if (i == 5) unaSeccion.Nombre = cUtilidades.NombreSeccion.Presentación;
                        if (i == 6) unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (i == 7) unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (i == 8) unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (i == 9) unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (i == 10) unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (i == 11) unaSeccion.Nombre = cUtilidades.NombreSeccion.En_Suma;
                        if (i == 12) unaSeccion.Nombre = cUtilidades.NombreSeccion.Sugerencias;
                        if (i == 13) unaSeccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicopedagógico;
                        unaSeccion.Contenido = oReader["SeccionContenido"].ToString();
                        lstRetorno.Add(unaSeccion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }



    }
}
