using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Clases;

namespace Persistencia.Clases
{
    public class pItinerario : pPersistencia
    {
        public static bool Agregar(cItinerario parItinerario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_Agregar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioTipoSesion", parItinerario.TipoSesion));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioComentario", parItinerario.Comentario));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioCentro", parItinerario.Centro));

                int rtn = cmd.ExecuteNonQuery();

                if (rtn <= 0)
                {
                    retorno = false;
                }

                if (retorno)
                {
                    SqlCommand cmd1 = new SqlCommand("Itinerario_TraerUltimoId", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader oReader = cmd1.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            parItinerario.Codigo = int.Parse(oReader["ItinerarioId"].ToString());
                        }
                    }

                    for (int i = 0; i < parItinerario.lstBeneficiarios.Count; i++)
                    {
                        SqlCommand cmd2 = new SqlCommand("BeneficiariosItinerarios_Agregar", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@BeneficiarioId", parItinerario.lstBeneficiarios[i].Beneficiario.Codigo));
                        cmd2.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                        //ARREGLAR CUANDO SE PONGA EN PRESENTACION PARA ELEGIR EL PLAN
                        cmd2.Parameters.Add(new SqlParameter("@PlanId", parItinerario.lstBeneficiarios[i].Beneficiario.lstPlanes[0].Codigo));
                        cmd2.ExecuteNonQuery();
                    }
                    for (int i = 0; i < parItinerario.lstEspecialistas.Count; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("UsuariosItinerarios_Agregar", conn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add(new SqlParameter("@UsuarioId", parItinerario.lstEspecialistas[i].Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd3.ExecuteNonQuery();
                    }
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
        public static List<cUsuario> VerificarHorarioUsuario(cItinerario parItinerario) //Devuelve lista de usuarios que están disponibles
        {
            List<cUsuario> retorno = new List<cUsuario>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosItinerarios_VerificarHorario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio.ToShortTimeString()));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin.ToShortTimeString()));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int codigo = int.Parse(oReader["UsuarioId"].ToString());
                        for (int i = 0; i < parItinerario.lstEspecialistas.Count; i++)
                        {
                            if (parItinerario.lstEspecialistas[i].Codigo == codigo)
                            {
                                retorno.Add(parItinerario.lstEspecialistas[i]);
                            }
                        }

                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public static List<cBeneficiario> VerificarHorarioBeneficiarios(cItinerario parItinerario) //Devuelve lista de beneficiarios que están disponibles
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosItinerarios_VerificarHorario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        for (int i = 0; i < parItinerario.lstBeneficiarios.Count; i++)
                        {
                            if (parItinerario.lstBeneficiarios[i].Beneficiario.Codigo == codigo)
                            {
                                retorno.Add(parItinerario.lstBeneficiarios[i].Beneficiario);
                            }
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public static List<cItinerario> TraerTodosPorDia(char parDia, int parCentro)
        {
            List<cItinerario> retorno = new List<cItinerario>();
            cItinerario unItinerario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerTodosPorDia", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parDia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioCentro", parCentro));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unItinerario = new cItinerario();

                        unItinerario.Codigo = int.Parse(oReader["ItinerarioId"].ToString());
                        switch (int.Parse(oReader["ItinerarioTipoSesion"].ToString()))
                        {
                            case 0:
                                unItinerario.TipoSesion = cUtilidades.TipoSesion.Individual;
                                break;
                            case 1:
                                unItinerario.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                                break;
                            case 2:
                                unItinerario.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                                break;
                            case 3:
                                unItinerario.TipoSesion = cUtilidades.TipoSesion.Taller;
                                break;
                            case 4:
                                unItinerario.TipoSesion = cUtilidades.TipoSesion.PROES;
                                break;
                        }
                        unItinerario.Dia = oReader["ItinerarioDia"].ToString();
                        unItinerario.HoraInicio = DateTime.Parse(oReader["ItinerarioHoraInicio"].ToString());
                        unItinerario.HoraFin = DateTime.Parse(oReader["ItinerarioHoraFin"].ToString());
                        unItinerario.Comentario = oReader["ItinerarioComentario"].ToString();
                        switch (int.Parse(oReader["ItinerarioCentro"].ToString()))
                        {
                            case 0:
                                unItinerario.Centro = cUtilidades.Centro.JuanLacaze;
                                break;
                            case 1:
                                unItinerario.Centro = cUtilidades.Centro.NuevaHelvecia;
                                break;
                        }
                        unItinerario.Estado = bool.Parse(oReader["ItinerarioEstado"].ToString());

                        retorno.Add(unItinerario);
                    }
                }
                if(conn.State==ConnectionState.Open)
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


        public static bool ModificarEstadoDelDia(char parDia)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerarios_CambiarEstado", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parDia));

                int rtn = cmd.ExecuteNonQuery();
                if(rtn<=0)
                {
                    retorno = false;
                }
                if(conn.State == ConnectionState.Open)
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
