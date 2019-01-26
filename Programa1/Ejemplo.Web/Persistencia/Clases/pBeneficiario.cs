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
    public class pBeneficiario : pPersistencia
    {
        public static bool Agregar(cBeneficiario parBeneficiario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Agregar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioNombres", parBeneficiario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioApellidos", parBeneficiario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", parBeneficiario.CI));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioSexo", parBeneficiario.Sexo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDomicilio", parBeneficiario.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono1", parBeneficiario.Telefono1));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono2", parBeneficiario.Telefono2));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEmail", parBeneficiario.Email));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioFechaNacimiento", parBeneficiario.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioAtributario", parBeneficiario.Atributario));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioMotivoConsulta", parBeneficiario.MotivoConsulta));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEscolaridad", parBeneficiario.Escolaridad));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDerivador", parBeneficiario.Derivador));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEstado", parBeneficiario.Estado));

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

        public static bool Habilitar(cBeneficiario parBeneficiario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Habilitar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));

                int iRtn = cmd.ExecuteNonQuery();

                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }
        public static bool Inhabilitar(cBeneficiario parBeneficiario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Inhabilitar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));

                int iRtn = cmd.ExecuteNonQuery();

                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }

        public static bool Modificar(cBeneficiario parBeneficiario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Modificar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioNombres", parBeneficiario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioApellidos", parBeneficiario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", parBeneficiario.CI));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioSexo", parBeneficiario.Sexo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono1", parBeneficiario.Telefono1));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono2", parBeneficiario.Telefono2));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEmail", parBeneficiario.Email));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDomicilio", parBeneficiario.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioFechaNacimiento", parBeneficiario.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioAtributario", parBeneficiario.Atributario));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioMotivoConsulta", parBeneficiario.MotivoConsulta));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEscolaridad", parBeneficiario.Escolaridad));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDerivador", parBeneficiario.Derivador));

                int iRtn = cmd.ExecuteNonQuery();

                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }

        public static cBeneficiario TraerEspecifico(cBeneficiario parBeneficiario)
        {
            cBeneficiario unRetorno = null;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerEspecifico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cBeneficiario();

                        unRetorno.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unRetorno.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unRetorno.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unRetorno.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unRetorno.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unRetorno.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unRetorno.Email = oReader["BeneficiarioEmail"].ToString();
                        unRetorno.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] aSs = unRetorno.FechaNacimiento.Split(' ');
                        unRetorno.FechaNacimiento = aSs[0];
                        unRetorno.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unRetorno.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unRetorno.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unRetorno.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }

        public static cBeneficiario TraerEspecificoCI(cBeneficiario parBeneficiario)
        {
            cBeneficiario unRetorno = null;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerEspecificoCI", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", parBeneficiario.CI));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cBeneficiario();

                        unRetorno.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unRetorno.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unRetorno.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unRetorno.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unRetorno.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unRetorno.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unRetorno.Email = oReader["BeneficiarioEmail"].ToString();
                        unRetorno.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] aSs = unRetorno.FechaNacimiento.Split(' ');
                        unRetorno.FechaNacimiento = aSs[0];
                        unRetorno.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unRetorno.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unRetorno.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unRetorno.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }
        public static cBeneficiario TraerEspecificoVerificarModificar(cBeneficiario parBeneficiario)
        {
            cBeneficiario unRetorno = null;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_VerificarCIModificar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", parBeneficiario.CI));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cBeneficiario();

                        unRetorno.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unRetorno.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unRetorno.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unRetorno.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unRetorno.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unRetorno.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unRetorno.Email = oReader["BeneficiarioEmail"].ToString();
                        unRetorno.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] aSs = unRetorno.FechaNacimiento.Split(' ');
                        unRetorno.FechaNacimiento = aSs[0];
                        unRetorno.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unRetorno.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unRetorno.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unRetorno.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }

        public static List<cBeneficiario> TraerTodos()
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerTodos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unBeneficiario = new cBeneficiario();

                        unBeneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unBeneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unBeneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unBeneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unBeneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unBeneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unBeneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unBeneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unBeneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        unBeneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        lstRetorno.Add(unBeneficiario);
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

        public static List<cBeneficiario> TraerTodosConFiltros(string parConsulta)
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, vConn);

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unBeneficiario = new cBeneficiario();

                        unBeneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unBeneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unBeneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unBeneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unBeneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unBeneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unBeneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unBeneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unBeneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        unBeneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        lstRetorno.Add(unBeneficiario);
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
        public static List<cBeneficiarioItinerario> TraerTodosPorItinerario (cItinerario parItinerario)
        {
            List<cBeneficiarioItinerario> lstRetorno = new List<cBeneficiarioItinerario>();
            cBeneficiarioItinerario unBeneficiarioYPlan;
            cBeneficiario unBeneficiario;
            cPlan unPlan;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerPorItinerario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using(SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        unBeneficiarioYPlan = new cBeneficiarioItinerario();

                        unBeneficiario = new cBeneficiario();
                        unBeneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unBeneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unBeneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unBeneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unBeneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unBeneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unBeneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unBeneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unBeneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        unBeneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        unPlan = new cPlan();

                        unPlan.Codigo = int.Parse(oReader["PlanId"].ToString());
                        unPlan.Tipo = oReader["PlanTipo"].ToString();
                        unPlan.Tratamiento = bool.Parse(oReader["PlanTratamiento"].ToString());
                        unPlan.Evaluacion = bool.Parse(oReader["PlanEvaluacion"].ToString());
                        unPlan.FechaInicio = DateTime.Parse(oReader["PlanFechaInicio"].ToString()).ToShortDateString();
                        if (oReader["PlanFechaFin"] != DBNull.Value)
                        {
                            unPlan.FechaFin = DateTime.Parse(oReader["PlanFechaFin"].ToString()).ToShortDateString();
                        }
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());

                        unBeneficiarioYPlan.Beneficiario = unBeneficiario;
                        unBeneficiarioYPlan.Plan = unPlan;

                        lstRetorno.Add(unBeneficiarioYPlan);
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
            return lstRetorno;
        }

        public static List<cBeneficiario> TraerTodosPorEspecialista(cUsuario parUsuario)
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerPorEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unBeneficiario = new cBeneficiario();
                        unBeneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unBeneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unBeneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unBeneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unBeneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unBeneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unBeneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unBeneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unBeneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        unBeneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());
                        lstRetorno.Add(unBeneficiario);
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

        public static List<cBeneficiario> TraerActivosPorEdad(int parDesde, int parHasta)
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerActivosPorEdad", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Desde", parDesde));
                cmd.Parameters.Add(new SqlParameter("@Hasta", parHasta));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unBeneficiario = new cBeneficiario();
                        unBeneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unBeneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unBeneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unBeneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unBeneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unBeneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unBeneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unBeneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unBeneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        unBeneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());
                        lstRetorno.Add(unBeneficiario);
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

        public static List<cBeneficiario> TraerTodosPorDiagnostico(cDiagnostico parDiagnostico)
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerTodosPorDiagnostico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idDiagnostico", parDiagnostico.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unBeneficiario = new cBeneficiario();

                        unBeneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        unBeneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        unBeneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        unBeneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        unBeneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        unBeneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        unBeneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        unBeneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        unBeneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        unBeneficiario.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] aSs = unBeneficiario.FechaNacimiento.Split(' ');
                        unBeneficiario.FechaNacimiento = aSs[0];
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        List<cDiagnosticoBeneficiario> lstDB = new List<cDiagnosticoBeneficiario>();
                        cDiagnosticoBeneficiario unDB;

                        SqlCommand cmd1 = new SqlCommand("DiagnosticosBeneficiarios_TraerTodosDiagnosticosPorBeneficiario", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idBeneficiario", unBeneficiario.Codigo));
                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while(oReader1.Read())
                            {
                                unDB = new cDiagnosticoBeneficiario();
                                unDB.Diagnostico = new cDiagnostico();
                                unDB.Diagnostico.Codigo = int.Parse(oReader1["DiagnosticoId"].ToString());
                                unDB.Diagnostico.Tipo = oReader1["DiagnosticoTipo"].ToString();
                                unDB.Fecha = DateTime.Parse(oReader1["DiagnosticosBeneficiariosFecha"].ToString()).ToShortDateString();
                                lstDB.Add(unDB);
                            }
                        }
                        unBeneficiario.lstDiagnosticos = lstDB;
                        lstRetorno.Add(unBeneficiario);
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

        public static Tuple<List<string>, List<int>> TraerCantidadParaCadaAñoPorDiagnostico(cDiagnostico parDiagnostico)
        {
            List<string> lstAños = new List<string>();
            List<int> lstCantidad = new List<int>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("EstadisticasTraerCantidadBeneficiarioParaCadaAñoPorDiagnostico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idDiagnostico", parDiagnostico.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        lstAños.Add(oReader["año"].ToString());
                        lstCantidad.Add(int.Parse(oReader["cantidad"].ToString()));
                    }                    
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Tuple.Create(lstAños, lstCantidad);
        }

        public static Tuple<List<cDiagnostico>, List<int>> TraerCantidadParaCadaDiagnosticoPorAño(int parAño)
        {
            List<cDiagnostico> lstDiagnosticos = new List<cDiagnostico>();
            cDiagnostico unDiagnostico;
            List<int> lstCantidad = new List<int>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("EstadisticasTraerCantidadBeneficiarioParaCadaDiagnosticoPorAño", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fecha", parAño));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unDiagnostico = new cDiagnostico();
                        unDiagnostico.Codigo = int.Parse(oReader["DiagnosticoId"].ToString());
                        unDiagnostico.Tipo = oReader["DiagnosticoTipo"].ToString();
                        lstDiagnosticos.Add(unDiagnostico);
                        lstCantidad.Add(int.Parse(oReader["cantidad"].ToString()));
                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Tuple.Create(lstDiagnosticos, lstCantidad);
        }

        public static string CentroPreferencia(cBeneficiario parBeneficiario)
        {
            string sRetorno = "";
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_CentroPreferencia", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        int i = int.Parse(oReader["Juan Lacaze"].ToString());
                        int iJ = int.Parse(oReader["Nueva Helvecia"].ToString());
                        if (i >= iJ)
                            sRetorno = "Juan Lacaze";
                        else
                            sRetorno = "Nueva Helvecia";
                    }
                }
                vConn.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return sRetorno;
        }


    }


}
