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
        public static bool Agregar(cBeneficiario elBeneficiario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Agregar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioNombres", elBeneficiario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioApellidos", elBeneficiario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", elBeneficiario.CI));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioSexo", elBeneficiario.Sexo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDomicilio", elBeneficiario.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono1", elBeneficiario.Telefono1));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono2", elBeneficiario.Telefono2));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEmail", elBeneficiario.Email));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioFechaNacimiento", elBeneficiario.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioAtributario", elBeneficiario.Atributario));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioMotivoConsulta", elBeneficiario.MotivoConsulta));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEscolaridad", elBeneficiario.Escolaridad));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDerivador", elBeneficiario.Derivador));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEstado", elBeneficiario.Estado));

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

        public static bool Habilitar(cBeneficiario elBeneficiario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Habilitar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));

                int rtn = cmd.ExecuteNonQuery();

                if (rtn <= 0)
                {
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
        public static bool Inhabilitar(cBeneficiario elBeneficiario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Inhabilitar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));

                int rtn = cmd.ExecuteNonQuery();

                if (rtn <= 0)
                {
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public static bool Modificar(cBeneficiario elBeneficiario)
        {
            bool retorno = true;

            try
            {
                SqlConnection conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_Modificar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioNombres", elBeneficiario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioApellidos", elBeneficiario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", elBeneficiario.CI));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioSexo", elBeneficiario.Sexo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono1", elBeneficiario.Telefono1));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioTelefono2", elBeneficiario.Telefono2));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEmail", elBeneficiario.Email));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDomicilio", elBeneficiario.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioFechaNacimiento", elBeneficiario.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioAtributario", elBeneficiario.Atributario));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioMotivoConsulta", elBeneficiario.MotivoConsulta));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioEscolaridad", elBeneficiario.Escolaridad));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioDerivador", elBeneficiario.Derivador));

                int rtn = cmd.ExecuteNonQuery();

                if (rtn <= 0)
                {
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public static cBeneficiario TraerEspecifico(cBeneficiario elBeneficiario)
        {
            cBeneficiario retorno = null;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerEspecifico", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cBeneficiario();

                        retorno.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Nombres = oReader["BeneficiarioNombres"].ToString();
                        retorno.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        retorno.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        retorno.Sexo = oReader["BeneficiarioSexo"].ToString();
                        retorno.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        retorno.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        retorno.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        retorno.Email = oReader["BeneficiarioEmail"].ToString();
                        retorno.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] ss = retorno.FechaNacimiento.Split(' ');
                        retorno.FechaNacimiento = ss[0];
                        retorno.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        retorno.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        retorno.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        retorno.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        retorno.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

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

        public static cBeneficiario TraerEspecificoCI(cBeneficiario elBeneficiario)
        {
            cBeneficiario retorno = null;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerEspecificoCI", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", elBeneficiario.CI));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cBeneficiario();

                        retorno.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Nombres = oReader["BeneficiarioNombres"].ToString();
                        retorno.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        retorno.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        retorno.Sexo = oReader["BeneficiarioSexo"].ToString();
                        retorno.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        retorno.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        retorno.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        retorno.Email = oReader["BeneficiarioEmail"].ToString();
                        retorno.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] ss = retorno.FechaNacimiento.Split(' ');
                        retorno.FechaNacimiento = ss[0];
                        retorno.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        retorno.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        retorno.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        retorno.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        retorno.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

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
        public static cBeneficiario TraerEspecificoVerificarModificar(cBeneficiario elBeneficiario)
        {
            cBeneficiario retorno = null;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_VerificarCIModificar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@BeneficiarioCI", elBeneficiario.CI));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cBeneficiario();

                        retorno.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Nombres = oReader["BeneficiarioNombres"].ToString();
                        retorno.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        retorno.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        retorno.Sexo = oReader["BeneficiarioSexo"].ToString();
                        retorno.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        retorno.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        retorno.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        retorno.Email = oReader["BeneficiarioEmail"].ToString();
                        retorno.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                        string[] ss = retorno.FechaNacimiento.Split(' ');
                        retorno.FechaNacimiento = ss[0];
                        retorno.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        retorno.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        retorno.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        retorno.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        retorno.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

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

        public static List<cBeneficiario> TraerTodos()
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerTodos", conn);
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
                        //string[] ss = unBeneficiario.FechaNacimiento.Split(' ');
                        //unBeneficiario.FechaNacimiento = ss[0];
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        retorno.Add(unBeneficiario);
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

        public static List<cBeneficiario> TraerTodosConFiltros(string parConsulta)
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, conn);

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
                        //string[] ss = unBeneficiario.FechaNacimiento.Split(' ');
                        //unBeneficiario.FechaNacimiento = ss[0];
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        retorno.Add(unBeneficiario);
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
        public static List<cBeneficiarioItinerario> TraerTodosPorItinerario (cItinerario parItinerario)
        {
            List<cBeneficiarioItinerario> retorno = new List<cBeneficiarioItinerario>();
            cBeneficiarioItinerario BeneficiarioYPlan;
            cBeneficiario unBeneficiario;
            cPlan unPlan;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerPorItinerario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using(SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        BeneficiarioYPlan = new cBeneficiarioItinerario();

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

                        BeneficiarioYPlan.Beneficiario = unBeneficiario;
                        BeneficiarioYPlan.Plan = unPlan;

                        retorno.Add(BeneficiarioYPlan);
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


        public static List<cBeneficiario> TraerTodosPorEspecialista(cUsuario parUsuario)
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            cBeneficiario Beneficiario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        Beneficiario = new cBeneficiario();
                        Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        Beneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        Beneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        Beneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        Beneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        Beneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        Beneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        Beneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        Beneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        Beneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        Beneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        Beneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        Beneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        Beneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        Beneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());
                        retorno.Add(Beneficiario);
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

        public static List<cBeneficiario> TraerActivosPorEdad(int parDesde, int parHasta)
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            cBeneficiario Beneficiario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerActivosPorEdad", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Desde", parDesde));
                cmd.Parameters.Add(new SqlParameter("@Hasta", parHasta));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        Beneficiario = new cBeneficiario();
                        Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        Beneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                        Beneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                        Beneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                        Beneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                        Beneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                        Beneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                        Beneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                        Beneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                        Beneficiario.FechaNacimiento = DateTime.Parse(oReader["BeneficiarioFechaNacimiento"].ToString()).ToShortDateString();
                        Beneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        Beneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        Beneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        Beneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        Beneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());
                        retorno.Add(Beneficiario);
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

        public static List<cBeneficiario> TraerTodosPorDiagnostico(cDiagnostico parDiagnostico)
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            cBeneficiario unBeneficiario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_TraerTodosPorDiagnostico", conn);
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
                        string[] ss = unBeneficiario.FechaNacimiento.Split(' ');
                        unBeneficiario.FechaNacimiento = ss[0];
                        unBeneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                        unBeneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                        unBeneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                        unBeneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                        unBeneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());

                        List<cDiagnosticoBeneficiario> lstDB = new List<cDiagnosticoBeneficiario>();
                        cDiagnosticoBeneficiario unDB;

                        SqlCommand cmd1 = new SqlCommand("DiagnosticosBeneficiarios_TraerTodosDiagnosticosPorBeneficiario", conn);
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
                        retorno.Add(unBeneficiario);
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

        public static Tuple<List<string>, List<int>> TraerCantidadParaCadaAñoPorDiagnostico(cDiagnostico parDiagnostico)
        {
            List<string> años = new List<string>();
            List<int> cantidad = new List<int>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("EstadisticasTraerCantidadBeneficiarioParaCadaAñoPorDiagnostico", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idDiagnostico", parDiagnostico.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        años.Add(oReader["año"].ToString());
                        cantidad.Add(int.Parse(oReader["cantidad"].ToString()));
                    }                    
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Tuple.Create(años, cantidad);
        }

        public static Tuple<List<cDiagnostico>, List<int>> TraerCantidadParaCadaDiagnosticoPorAño(int parAño)
        {
            List<cDiagnostico> diagnosticos = new List<cDiagnostico>();
            cDiagnostico diagnostico;
            List<int> cantidad = new List<int>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("EstadisticasTraerCantidadBeneficiarioParaCadaDiagnosticoPorAño", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fecha", parAño));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        diagnostico = new cDiagnostico();
                        diagnostico.Codigo = int.Parse(oReader["DiagnosticoId"].ToString());
                        diagnostico.Tipo = oReader["DiagnosticoTipo"].ToString();
                        diagnosticos.Add(diagnostico);
                        cantidad.Add(int.Parse(oReader["cantidad"].ToString()));
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Tuple.Create(diagnosticos, cantidad);
        }

        public static string CentroPreferencia(cBeneficiario parBeneficiario)
        {
            string retorno = "";
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Beneficiarios_CentroPreferencia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        int i = int.Parse(oReader["Juan Lacaze"].ToString());
                        int j = int.Parse(oReader["Nueva Helvecia"].ToString());
                        if (i >= j)
                            retorno = "Juan Lacaze";
                        else
                            retorno = "Nueva Helvecia";
                    }
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;
        }


    }


}
