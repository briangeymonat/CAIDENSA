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
            List<cSeccion> retorno = new List<cSeccion>();
            cSeccion seccion;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Secciones_TraerTodasPorInforme", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));


                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        seccion = new cSeccion();
                        seccion.Codigo = int.Parse(oReader["SeccionId"].ToString());
                        int i = int.Parse(oReader["SeccionNombre"].ToString());
                        if (i == 0) seccion.Nombre = cUtilidades.NombreSeccion.Título;
                        if (i == 1) seccion.Nombre = cUtilidades.NombreSeccion.Encuadre;
                        if (i == 2) seccion.Nombre = cUtilidades.NombreSeccion.Diagnóstico;
                        if (i == 3) seccion.Nombre = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (i == 4) seccion.Nombre = cUtilidades.NombreSeccion.Desarrollo;
                        if (i == 5) seccion.Nombre = cUtilidades.NombreSeccion.Presentación;
                        if (i == 6) seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (i == 7) seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (i == 8) seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (i == 9) seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (i == 10) seccion.Nombre = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (i == 11) seccion.Nombre = cUtilidades.NombreSeccion.En_Suma;
                        if (i == 12) seccion.Nombre = cUtilidades.NombreSeccion.Sugerencias;
                        seccion.Contenido = oReader["SeccionContenido"].ToString();
                        retorno.Add(seccion);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }



    }
}
