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
            List<cEspecialidad> lstRetorno = new List<cEspecialidad>();
            cEspecialidad unaEspecialidad = null;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Especialidades_TraerTodas", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        unaEspecialidad = new cEspecialidad();
                        unaEspecialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unaEspecialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unaEspecialidad);
                    }
                    vConn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static cEspecialidad TraerEspecifica(cEspecialidad parEspecialidad)
        {
            cEspecialidad unRetorno = null;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Especialidad_TraerEspecifica", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parEspecialidad.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cEspecialidad();
                        unRetorno.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unRetorno.Nombre = oReader["EspecialidadNombre"].ToString();
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }
        public static cEspecialidad TraerEspecificaPorNombre(cEspecialidad parEspecialidad)
        {
            cEspecialidad unRetorno = new cEspecialidad();

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Especialidad_TraerEspecificaPorNombre", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EspecialidadNombre", parEspecialidad.Nombre));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unRetorno.Nombre = oReader["EspecialidadNombre"].ToString();
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }
    }
}
