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
    public class pEspecialidad:pPersistencia
    {
        public static List<cEspecialidad> TraerTodas()
        {
            List<cEspecialidad> retorno = new List<cEspecialidad>();
            cEspecialidad especialidad = null;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Especialidades_TraerTodas", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        especialidad = new cEspecialidad();
                        especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        retorno.Add(especialidad);
                    }
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public static cEspecialidad TraerEspecifica(cEspecialidad parEspecialidad)
        {
            cEspecialidad retorno = null;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Especialidad_TraerEspecifica", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parEspecialidad.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cEspecialidad();
                        retorno.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        retorno.Nombre = oReader["EspecialidadNombre"].ToString();
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
