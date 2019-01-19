﻿using System;
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
                        cmd2.Parameters.Add(new SqlParameter("@PlanId", parItinerario.lstBeneficiarios[i].Plan.Codigo));
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

        public static bool Modificar(cItinerario parItinerario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_EliminarUsuariosYBeneficiarios", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                if (retorno)
                {
                    SqlCommand cmd2 = new SqlCommand("Itinerario_Modificar", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioTipoSesion", parItinerario.TipoSesion));

                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioComentario", parItinerario.Comentario));
                    cmd2.Parameters.Add(new SqlParameter("@ItinerarioCentro", parItinerario.Centro));

                    cmd2.ExecuteNonQuery();

                    foreach (cBeneficiarioItinerario unBenIt in parItinerario.lstBeneficiarios)
                    {
                        SqlCommand cmd3 = new SqlCommand("BeneficiariosItinerarios_Agregar", conn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add(new SqlParameter("@BeneficiarioId", unBenIt.Beneficiario.Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@PlanId", unBenIt.Plan.Codigo));
                        cmd3.ExecuteNonQuery();
                    }

                    foreach(cUsuario unUsu in parItinerario.lstEspecialistas)
                    {
                        SqlCommand cmd4 = new SqlCommand("UsuariosItinerarios_Agregar", conn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add(new SqlParameter("@UsuarioId", unUsu.Codigo));
                        cmd4.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd4.ExecuteNonQuery();
                    }
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
        public static bool Eliminar(cItinerario parItinerario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_Eliminar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

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
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));

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

        public static List<cUsuario> VerificarHorarioUsuarioModificar(cItinerario parItinerario) //Devuelve lista de usuarios que están disponibles
        {
            List<cUsuario> retorno = new List<cUsuario>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosItinerarios_VerificarHorarioModificar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

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
        public static List<cBeneficiario> VerificarHorarioBeneficiariosModificar(cItinerario parItinerario) //Devuelve lista de beneficiarios que están disponibles
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosItinerarios_VerificarHorarioModificar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

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
                        unItinerario.HoraInicio = DateTime.Parse(oReader["ItinerarioHoraInicio"].ToString()).ToShortTimeString();
                        unItinerario.HoraFin = DateTime.Parse(oReader["ItinerarioHoraFin"].ToString()).ToShortTimeString();
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
        public static List<cItinerario> TraerTodosPorEspecialista(cUsuario parUsuario)
        {
            List<cItinerario> retorno = new List<cItinerario>();
            cItinerario unItinerario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerTodosPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUsuario.Codigo));

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
                        unItinerario.HoraInicio = DateTime.Parse(oReader["ItinerarioHoraInicio"].ToString()).ToShortTimeString();
                        unItinerario.HoraFin = DateTime.Parse(oReader["ItinerarioHoraFin"].ToString()).ToShortTimeString();
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
        public static string TraerEncuadrePorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<string> especialidades = new List<string>();
            string retorno = "";
            string s = "";
            int i = 0;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerEncuadrePorBeneficiario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        s = "";
                        i += int.Parse(oReader["cantidad"].ToString());
                        s = oReader["EspecialidadNombre"].ToString();
                        especialidades.Add(s);
                    }
                }
                if (i == 0)
                {
                    retorno = "No tiene abordaje";
                }
                else
                {

                    retorno = "Abordaje ";
                    for (int j = 0; j < especialidades.Count; j++)
                    {
                        if (j == 0)
                        {
                            if (especialidades[j] == "Psicologia")
                                retorno = retorno + "psicológico";
                            if (especialidades[j] == "Pedadogia")
                                retorno = retorno + "pedagógico";
                            if (especialidades[j] == "Fisioterapia")
                                retorno = retorno + "fisioterapéutico";
                            if (especialidades[j] == "Fonoaudiologia")
                                retorno = retorno + "fonoaudiológico";
                            if (especialidades[j] == "Psicomotricidad")
                                retorno = retorno + "psicomotriz";
                        }
                        else if ((especialidades.Count - j) == 1)
                        {
                            if (especialidades[j] == "Psicologia")
                                retorno = retorno + " y psicológico";
                            if (especialidades[j] == "Pedadogia")
                                retorno = retorno + " y pedagógico";
                            if (especialidades[j] == "Fisioterapia")
                                retorno = retorno + " y fisioterapéutico";
                            if (especialidades[j] == "Fonoaudiologia")
                                retorno = retorno + " y fonoaudiológico";
                            if (especialidades[j] == "Psicomotricidad")
                                retorno = retorno + " y psicomotriz";
                        }
                        else
                        {
                            if (especialidades[j] == "Psicologia")
                                retorno = retorno + ", psicológico";
                            if (especialidades[j] == "Pedadogia")
                                retorno = retorno + ", pedagógico";
                            if (especialidades[j] == "Fisioterapia")
                                retorno = retorno + ", fisioterapéutico";
                            if (especialidades[j] == "Fonoaudiologia")
                                retorno = retorno + ", fonoaudiológico";
                            if (especialidades[j] == "Psicomotricidad")
                                retorno = retorno + ", psicomotriz";
                        }
                    }
                    retorno = retorno + " " + i + " veces semanales.";

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

        public static cItinerario TraerEspecifico(cItinerario parItinerario)
        {
            cItinerario retorno = null;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerEspecifico", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cItinerario();

                        retorno.Codigo = int.Parse(oReader["ItinerarioId"].ToString());
                        switch (int.Parse(oReader["ItinerarioTipoSesion"].ToString()))
                        {
                            case 0:
                                retorno.TipoSesion = cUtilidades.TipoSesion.Individual;
                                break;
                            case 1:
                                retorno.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                                break;
                            case 2:
                                retorno.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                                break;
                            case 3:
                                retorno.TipoSesion = cUtilidades.TipoSesion.Taller;
                                break;
                            case 4:
                                retorno.TipoSesion = cUtilidades.TipoSesion.PROES;
                                break;
                        }
                        retorno.Dia = oReader["ItinerarioDia"].ToString();
                        retorno.HoraInicio = DateTime.Parse(oReader["ItinerarioHoraInicio"].ToString()).ToShortTimeString();
                        retorno.HoraFin = DateTime.Parse(oReader["ItinerarioHoraFin"].ToString()).ToShortTimeString();
                        retorno.Comentario = oReader["ItinerarioComentario"].ToString();
                        switch (int.Parse(oReader["ItinerarioCentro"].ToString()))
                        {
                            case 0:
                                retorno.Centro = cUtilidades.Centro.JuanLacaze;
                                break;
                            case 1:
                                retorno.Centro = cUtilidades.Centro.NuevaHelvecia;
                                break;
                        }
                        retorno.Estado = bool.Parse(oReader["ItinerarioEstado"].ToString());

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
    }
}
