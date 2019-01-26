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
    public class pDiagnostico:pPersistencia
    {
        public static List<string> TraerUltimosDiagnosticosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<string> lstRetorno = new List<string>();
            string sDiagnostico;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("DiagnosticosBeneficiarios_TraerDiagnosticosPorBeneficiarios", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        sDiagnostico = "";
                        sDiagnostico = oReader["DiagnosticoTipo"].ToString();
                        lstRetorno.Add(sDiagnostico);
                    }
                    vConn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static List<cDiagnosticoBeneficiario> TraerTodosDiagnosticosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cDiagnosticoBeneficiario> lstRetorno = new List<cDiagnosticoBeneficiario>();
            cDiagnosticoBeneficiario unDiagnosticoBeneficiario;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("DiagnosticosBeneficiarios_TraerTodosDiagnosticosPorBeneficiarioFechaDesc", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unDiagnosticoBeneficiario = new cDiagnosticoBeneficiario();
                        unDiagnosticoBeneficiario.Diagnostico = new cDiagnostico();
                        unDiagnosticoBeneficiario.Diagnostico.Codigo = int.Parse(oReader["DiagnosticoId"].ToString());
                        unDiagnosticoBeneficiario.Diagnostico.Tipo = oReader["DiagnosticoTipo"].ToString();
                        unDiagnosticoBeneficiario.Fecha = DateTime.Parse(oReader["DiagnosticosBeneficiariosFecha"].ToString()).ToShortDateString();
                        lstRetorno.Add(unDiagnosticoBeneficiario);
                    }
                    vConn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cDiagnostico> TraerTodos()
        {
            List<cDiagnostico> lstRetorno = new List<cDiagnostico>();
            cDiagnostico unDiagnostico;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Diagnostico_TraerTodos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unDiagnostico = new cDiagnostico();
                        unDiagnostico.Codigo = int.Parse(oReader["DiagnosticoId"].ToString());
                        unDiagnostico.Tipo = oReader["DiagnosticoTipo"].ToString();
                        lstRetorno.Add(unDiagnostico);
                    }
                    vConn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static bool AgregarDiagnosticoBeneficiario(cBeneficiario parBeneficiario)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                for(int i=0; i<parBeneficiario.lstDiagnosticos.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("DiagnosticosBeneficiarios_Agregar", vConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));
                    cmd.Parameters.Add(new SqlParameter("@idDiagnostico", parBeneficiario.lstDiagnosticos[i].Diagnostico.Codigo));
                    cmd.Parameters.Add(new SqlParameter("@fecha", parBeneficiario.lstDiagnosticos[i].Fecha));
                    int iRtn = cmd.ExecuteNonQuery();
                    if(iRtn<=0)
                    {
                        bRetorno = false;
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

        public static bool Agregar(cDiagnostico parDiagnostico)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Diagnostico_Agregar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@tipo", parDiagnostico.Tipo));

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
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }

        public static bool Existe(cDiagnostico parDiagnostico)
        {
            bool bRetorno = true;
            int i = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Diagnostico_VerificarSiExiste", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@tipo", parDiagnostico.Tipo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        i = new int();
                        i = int.Parse(oReader["existe"].ToString());
                    }
                }
                if(i==0)
                {
                    bRetorno = false;
                }
                if(vConn.State == ConnectionState.Open)
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

        public static bool ExisteDiagnosticoBeneficiario(cDiagnostico parDiagnostico)
        {
            bool bRetorno = true;
            int i = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("DiagnosticosBeneficiarios_Existe", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", parDiagnostico.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        i = new int();
                        i = int.Parse(oReader["existe"].ToString());
                    }
                }
                if(i==0)
                {
                    bRetorno = false;
                }
                if(vConn.State == ConnectionState.Open)
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
        public static bool Eliminar(cDiagnostico parDiagnostico)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Diagnostico_Eliminar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", parDiagnostico.Codigo));
                int iRtn = cmd.ExecuteNonQuery();
                if(iRtn<=0)
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

        public static List<string> TraerTodosAñosQueHayDiagnosticos()
        {
            List<string> lstAños = new List<string>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("DignosticosBeneficiarios_TraerTodosLosAños", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        lstAños.Add(oReader["año"].ToString());
                    }
                }
                vConn.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return lstAños;
        }
    }
}
