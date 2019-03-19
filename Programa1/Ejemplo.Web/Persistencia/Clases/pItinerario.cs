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
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_Agregar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioTipoSesion", parItinerario.TipoSesion));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioComentario", parItinerario.Comentario));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioCentro", parItinerario.Centro));

                int iRtn = cmd.ExecuteNonQuery();

                if (iRtn <= 0)
                {
                    bRetorno = false;
                }

                if (bRetorno)
                {
                    SqlCommand cmd1 = new SqlCommand("Itinerario_TraerUltimoId", vConn);
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
                        SqlCommand cmd2 = new SqlCommand("BeneficiariosItinerarios_Agregar", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@BeneficiarioId", parItinerario.lstBeneficiarios[i].Beneficiario.Codigo));
                        cmd2.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd2.Parameters.Add(new SqlParameter("@PlanId", parItinerario.lstBeneficiarios[i].Plan.Codigo));
                        cmd2.ExecuteNonQuery();
                    }
                    for (int i = 0; i < parItinerario.lstEspecialistas.Count; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("UsuariosItinerarios_Agregar", vConn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add(new SqlParameter("@UsuarioId", parItinerario.lstEspecialistas[i].Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd3.ExecuteNonQuery();
                    }
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

        public static bool Modificar(cItinerario parItinerario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_EliminarUsuariosYBeneficiarios", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                if (bRetorno)
                {
                    SqlCommand cmd2 = new SqlCommand("Itinerario_Modificar", vConn);
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
                        SqlCommand cmd3 = new SqlCommand("BeneficiariosItinerarios_Agregar", vConn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add(new SqlParameter("@BeneficiarioId", unBenIt.Beneficiario.Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@PlanId", unBenIt.Plan.Codigo));
                        cmd3.ExecuteNonQuery();
                    }

                    foreach(cUsuario unUsu in parItinerario.lstEspecialistas)
                    {
                        SqlCommand cmd4 = new SqlCommand("UsuariosItinerarios_Agregar", vConn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add(new SqlParameter("@UsuarioId", unUsu.Codigo));
                        cmd4.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));
                        cmd4.ExecuteNonQuery();
                    }
                }
                if(vConn.State == ConnectionState.Open)
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
        public static bool Eliminar(cItinerario parItinerario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_Eliminar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                int iRtn = cmd.ExecuteNonQuery();
                if(iRtn<=0)
                {
                    bRetorno = false;
                }
                if(vConn.State == ConnectionState.Open)
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

        public static List<cUsuario> VerificarHorarioUsuario(cItinerario parItinerario) //Devuelve lista de usuarios que están disponibles
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosItinerarios_VerificarHorario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int iCodigo = int.Parse(oReader["UsuarioId"].ToString());
                        for (int i = 0; i < parItinerario.lstEspecialistas.Count; i++)
                        {
                            if (parItinerario.lstEspecialistas[i].Codigo == iCodigo)
                            {
                                lstRetorno.Add(parItinerario.lstEspecialistas[i]);
                            }
                        }

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static List<cBeneficiario> VerificarHorarioBeneficiarios(cItinerario parItinerario) //Devuelve lista de beneficiarios que están disponibles
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosItinerarios_VerificarHorario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int iCodigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        for (int i = 0; i < parItinerario.lstBeneficiarios.Count; i++)
                        {
                            if (parItinerario.lstBeneficiarios[i].Beneficiario.Codigo == iCodigo)
                            {
                                lstRetorno.Add(parItinerario.lstBeneficiarios[i].Beneficiario);
                            }
                        }
                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cUsuario> VerificarHorarioUsuarioModificar(cItinerario parItinerario) //Devuelve lista de usuarios que están disponibles
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosItinerarios_VerificarHorarioModificar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int iCodigo = int.Parse(oReader["UsuarioId"].ToString());
                        for (int i = 0; i < parItinerario.lstEspecialistas.Count; i++)
                        {
                            if (parItinerario.lstEspecialistas[i].Codigo == iCodigo)
                            {
                                lstRetorno.Add(parItinerario.lstEspecialistas[i]);
                            }
                        }

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static List<cBeneficiario> VerificarHorarioBeneficiariosModificar(cItinerario parItinerario) //Devuelve lista de beneficiarios que están disponibles
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosItinerarios_VerificarHorarioModificar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parItinerario.Dia));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraInicio", parItinerario.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioHoraFin", parItinerario.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int iCodigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        for (int i = 0; i < parItinerario.lstBeneficiarios.Count; i++)
                        {
                            if (parItinerario.lstBeneficiarios[i].Beneficiario.Codigo == iCodigo)
                            {
                                lstRetorno.Add(parItinerario.lstBeneficiarios[i].Beneficiario);
                            }
                        }
                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cItinerario> TraerTodosPorDia(char parDia, int parCentro)
        {
            List<cItinerario> lstRetorno = new List<cItinerario>();
            cItinerario unItinerario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerTodosPorDia", vConn);
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

                        lstRetorno.Add(unItinerario);
                    }
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
            return lstRetorno;
        }
        public static List<cItinerario> TraerTodosPorEspecialista(cUsuario parUsuario)
        {
            List<cItinerario> lstRetorno = new List<cItinerario>();
            cItinerario unItinerario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerTodosPorEspecialista", vConn);
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

                        lstRetorno.Add(unItinerario);
                    }
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
            return lstRetorno;
        }
        public static List<cItinerario> TraerTodosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cItinerario> lstRetorno = new List<cItinerario>();
            cItinerario unItinerario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerTodosPorBeneficiario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));

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

                        lstRetorno.Add(unItinerario);
                    }
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
            return lstRetorno;
        }
        public static bool ModificarEstadoDelDia(char parDia)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerarios_CambiarEstado", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioDia", parDia));

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
        public static string TraerEncuadrePorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<string> lstEspecialidades = new List<string>();
            string sRetorno = "";
            string s = "";
            int i = 0;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerEncuadrePorBeneficiario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        s = "";
                        i += int.Parse(oReader["cantidad"].ToString());
                        s = oReader["EspecialidadNombre"].ToString();
                        lstEspecialidades.Add(s);
                    }
                }
                if (i == 0)
                {
                    sRetorno = "No tiene abordaje";
                }
                else
                {

                    sRetorno = "Abordaje ";
                    for (int j = 0; j < lstEspecialidades.Count; j++)
                    {
                        if (j == 0)
                        {
                            if (lstEspecialidades[j] == "Psicologia")
                                sRetorno = sRetorno + "psicológico";
                            if (lstEspecialidades[j] == "Pedadogia")
                                sRetorno = sRetorno + "pedagógico";
                            if (lstEspecialidades[j] == "Fisioterapia")
                                sRetorno = sRetorno + "fisioterapéutico";
                            if (lstEspecialidades[j] == "Fonoaudiologia")
                                sRetorno = sRetorno + "fonoaudiológico";
                            if (lstEspecialidades[j] == "Psicomotricidad")
                                sRetorno = sRetorno + "psicomotriz";
                            if (lstEspecialidades[j] == "Psicopedagogo")
                                sRetorno = sRetorno + "psicopedagógico";
                        }
                        else if ((lstEspecialidades.Count - j) == 1)
                        {
                            if (lstEspecialidades[j] == "Psicologia")
                                sRetorno = sRetorno + " y psicológico";
                            if (lstEspecialidades[j] == "Pedadogia")
                                sRetorno = sRetorno + " y pedagógico";
                            if (lstEspecialidades[j] == "Fisioterapia")
                                sRetorno = sRetorno + " y fisioterapéutico";
                            if (lstEspecialidades[j] == "Fonoaudiologia")
                                sRetorno = sRetorno + " y fonoaudiológico";
                            if (lstEspecialidades[j] == "Psicomotricidad")
                                sRetorno = sRetorno + " y psicomotriz";
                            if (lstEspecialidades[j] == "Psicopedagogo")
                                sRetorno = sRetorno + " y psicopedagógico";
                        }
                        else
                        {
                            if (lstEspecialidades[j] == "Psicologia")
                                sRetorno = sRetorno + ", psicológico";
                            if (lstEspecialidades[j] == "Pedadogia")
                                sRetorno = sRetorno + ", pedagógico";
                            if (lstEspecialidades[j] == "Fisioterapia")
                                sRetorno = sRetorno + ", fisioterapéutico";
                            if (lstEspecialidades[j] == "Fonoaudiologia")
                                sRetorno = sRetorno + ", fonoaudiológico";
                            if (lstEspecialidades[j] == "Psicomotricidad")
                                sRetorno = sRetorno + ", psicomotriz";
                            if (lstEspecialidades[j] == "Psicopedagogo")
                                sRetorno = sRetorno + ", psicopedagógico";
                        }
                    }
                    sRetorno = sRetorno + " " + i + " veces semanales.";

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
            return sRetorno;
        }

        public static cItinerario TraerEspecifico(cItinerario parItinerario)
        {
            cItinerario unRetorno = null;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerEspecifico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cItinerario();

                        unRetorno.Codigo = int.Parse(oReader["ItinerarioId"].ToString());
                        switch (int.Parse(oReader["ItinerarioTipoSesion"].ToString()))
                        {
                            case 0:
                                unRetorno.TipoSesion = cUtilidades.TipoSesion.Individual;
                                break;
                            case 1:
                                unRetorno.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                                break;
                            case 2:
                                unRetorno.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                                break;
                            case 3:
                                unRetorno.TipoSesion = cUtilidades.TipoSesion.Taller;
                                break;
                            case 4:
                                unRetorno.TipoSesion = cUtilidades.TipoSesion.PROES;
                                break;
                        }
                        unRetorno.Dia = oReader["ItinerarioDia"].ToString();
                        unRetorno.HoraInicio = DateTime.Parse(oReader["ItinerarioHoraInicio"].ToString()).ToShortTimeString();
                        unRetorno.HoraFin = DateTime.Parse(oReader["ItinerarioHoraFin"].ToString()).ToShortTimeString();
                        unRetorno.Comentario = oReader["ItinerarioComentario"].ToString();
                        switch (int.Parse(oReader["ItinerarioCentro"].ToString()))
                        {
                            case 0:
                                unRetorno.Centro = cUtilidades.Centro.JuanLacaze;
                                break;
                            case 1:
                                unRetorno.Centro = cUtilidades.Centro.NuevaHelvecia;
                                break;
                        }
                        unRetorno.Estado = bool.Parse(oReader["ItinerarioEstado"].ToString());

                    }
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
            return unRetorno;
        }


        public static bool Restablecer()
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_Restablecer", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
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


        public static List<cItinerario> TraerTodos()
        {
            List<cItinerario> lstRetorno = new List<cItinerario>();
            cItinerario unItinerario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Itinerario_TraerTodos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;            
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

                        lstRetorno.Add(unItinerario);
                    }
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
            return lstRetorno;
        }



    }
}
