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
    public class pNotificacion:pPersistencia
    {
        public static List<cNotificacion> TraerTodasNuevasAdministrador(cUsuario parUsuario)
        {
            List<cNotificacion> retorno = new List<cNotificacion>();
            cNotificacion unaNotificacion;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Notificaciones_TraerTodasNuevasAdministrador", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        unaNotificacion = new cNotificacion();
                        unaNotificacion.Codigo = int.Parse(oReader["NotificacionId"].ToString());
                        unaNotificacion.Estado = bool.Parse(oReader["NotificacionEstado"].ToString());
                        unaNotificacion.Usuario = new cUsuario();
                        unaNotificacion.Plan = new cPlan();
                        unaNotificacion.Usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unaNotificacion.Plan.Codigo = int.Parse(oReader["PlanId"].ToString());
                        retorno.Add(unaNotificacion);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public static List<cNotificacion> TraerTodasNuevasEspecialista(cUsuario parUsuario)
        {
            List<cNotificacion> retorno = new List<cNotificacion>();
            cNotificacion unaNotificacion;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Notificaciones_TraerTodasNuevasEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaNotificacion = new cNotificacion();
                        unaNotificacion.Codigo = int.Parse(oReader["NotificacionId"].ToString());
                        unaNotificacion.Estado = bool.Parse(oReader["NotificacionEstado"].ToString());
                        unaNotificacion.Informe = new cInforme();
                        unaNotificacion.Usuario = new cUsuario();
                        unaNotificacion.Usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unaNotificacion.Informe.Codigo = int.Parse(oReader["InformeId"].ToString());
                        retorno.Add(unaNotificacion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public static bool AgregarDeEspecialista(cNotificacion parNotificacion)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_AgregarDeEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parNotificacion.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@InformeId", parNotificacion.Informe.Codigo));

                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public static bool AgregarDeAdministrador(cNotificacion parNotificacion)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_AgregarDeAdministrador", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parNotificacion.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@PlanId", parNotificacion.Plan.Codigo));

                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public static int VerificarIngresoParaAdministrador(cNotificacion parNotificacion)
        {
            int retorno = -1;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_VerificarIngresoParaAdministrador", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parNotificacion.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@PlanId", parNotificacion.Plan.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        retorno = new int();
                        retorno = int.Parse(oReader["cantidad"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;

        }
        public static bool CambiarEstadoVista(cNotificacion parNotificacion)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_Vista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idUsuario", parNotificacion.Usuario.Codigo));
                int rtn = cmd.ExecuteNonQuery();
                if(rtn<=0)
                { retorno = false; }
                if(conn.State ==ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
    }
}
