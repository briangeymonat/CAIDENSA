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
            List<cNotificacion> lstRetorno = new List<cNotificacion>();
            cNotificacion unaNotificacion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Notificaciones_TraerTodasNuevasAdministrador", vConn);
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
                        lstRetorno.Add(unaNotificacion);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cNotificacion> TraerTodasNuevasEspecialista(cUsuario parUsuario)
        {
            List<cNotificacion> lstRetorno = new List<cNotificacion>();
            cNotificacion unaNotificacion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Notificaciones_TraerTodasNuevasEspecialista", vConn);
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
                        lstRetorno.Add(unaNotificacion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static bool AgregarDeEspecialista(cNotificacion parNotificacion)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_AgregarDeEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parNotificacion.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@InformeId", parNotificacion.Informe.Codigo));

                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                if (vConn.State == ConnectionState.Open)
                {
                    vConn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bRetorno;
        }

        public static bool AgregarDeAdministrador(cNotificacion parNotificacion)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_AgregarDeAdministrador", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parNotificacion.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@PlanId", parNotificacion.Plan.Codigo));

                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                if (vConn.State == ConnectionState.Open)
                {
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRetorno;
        }
        public static int VerificarIngresoParaAdministrador(cNotificacion parNotificacion)
        {
            int iRetorno = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_VerificarIngresoParaAdministrador", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parNotificacion.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@PlanId", parNotificacion.Plan.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        iRetorno = new int();
                        iRetorno = int.Parse(oReader["cantidad"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRetorno;

        }
        public static bool CambiarEstadoVista(cNotificacion parNotificacion)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Notificacion_Vista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idUsuario", parNotificacion.Usuario.Codigo));
                int iRtn = cmd.ExecuteNonQuery();
                if(iRtn<=0)
                { bRetorno = false; }
                if(vConn.State ==ConnectionState.Open)
                {
                    vConn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bRetorno;
        }
    }
}
