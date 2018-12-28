using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Clases;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia.Clases
{
    public class pSesion:pPersistencia
    {
        public static bool Agregar(cSesion parSesion)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_Agregar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SesionTipo", parSesion.TipoSesion));
                cmd.Parameters.Add(new SqlParameter("@SesionFecha", parSesion.Fecha));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraInicio", parSesion.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraFin", parSesion.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@SesionCentro", parSesion.Centro));

                int rtn = cmd.ExecuteNonQuery();
                
                if (rtn <= 0)
                {
                    retorno = false;
                }

                if (retorno)
                {
                    SqlCommand cmd1 = new SqlCommand("Sesiones_TraerUltimoId", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader oReader = cmd1.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            parSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        }
                    }

                    for (int i = 0; i < parSesion.lstBeneficiarios.Count; i++)
                    {
                        SqlCommand cmd2 = new SqlCommand("BeneficiariosSesiones_Agregar", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@BeneficiarioId", parSesion.lstBeneficiarios[i].Beneficiario.Codigo));
                        cmd2.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));

                        //ARREGLAR CUANDO SE PONGA EN PRESENTACION PARA ELEGIR EL PLAN
                        cmd2.Parameters.Add(new SqlParameter("@PlanId", parSesion.lstBeneficiarios[i].Plan.Codigo));
                        cmd2.ExecuteNonQuery();
                    }
                    for (int i = 0; i < parSesion.lstUsuarios.Count; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("UsuariosSesiones_Agregar", conn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add(new SqlParameter("@UsuarioId", parSesion.lstUsuarios[i].Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
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


    }
}
