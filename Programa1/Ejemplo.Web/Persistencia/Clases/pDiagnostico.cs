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
            List<string> retorno = new List<string>();
            string diagnostico;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DiagnosticosBeneficiarios_TraerDiagnosticosPorBeneficiarios", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        diagnostico = "";
                        diagnostico = oReader["DiagnosticoTipo"].ToString();
                        retorno.Add(diagnostico);
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


        public static List<cDiagnostico> TraerTodos()
        {
            List<cDiagnostico> retorno = new List<cDiagnostico>();
            cDiagnostico diagnostico;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Diagnostico_TraerTodos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        diagnostico = new cDiagnostico();
                        diagnostico.Codigo = int.Parse(oReader["DiagnosticoId"].ToString());
                        diagnostico.Tipo = oReader["DiagnosticoTipo"].ToString();
                        retorno.Add(diagnostico);
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

        public static bool AgregarDiagnosticoBeneficiario(cBeneficiario parBeneficiario, List<cDiagnostico> parLstDiagnosticos)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                for(int i=0; i<parLstDiagnosticos.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("DiagnosticosBeneficiarios_Agregar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));
                    cmd.Parameters.Add(new SqlParameter("@idDiagnostico", parLstDiagnosticos[i].Codigo));
                    int rtn = cmd.ExecuteNonQuery();
                    if(rtn<=0)
                    {
                        retorno = false;
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
