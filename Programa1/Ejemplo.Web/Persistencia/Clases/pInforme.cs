using Common.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Clases
{
    public class pInforme : pPersistencia
    {
        public static bool Agregar(cInforme parInforme)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Informes_Nuevo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@tipoInforme", parInforme.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parInforme.Beneficiario.Codigo));

                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                int idInforme = -1;
                SqlCommand cmd1 = new SqlCommand("Informes_UltimoIngresado", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        idInforme = new int();
                        idInforme = int.Parse(oReader["ultimo"].ToString());
                    }
                }
                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    SqlCommand cmd2 = new SqlCommand("Secciones_Agregar", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                    cmd2.Parameters.Add(new SqlParameter("@idInforme", idInforme));
                    int rtn2 = cmd2.ExecuteNonQuery();
                    if (rtn2 <= 0)
                    {
                        retorno = false;
                    }

                    int idSeccion = -1;
                    SqlCommand cmd3 = new SqlCommand("Secciones_UltimoIngresado", conn);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oReader2 = cmd3.ExecuteReader())
                    {
                        while (oReader2.Read())
                        {
                            idSeccion = new int();
                            idSeccion = int.Parse(oReader2["ultimo"].ToString());
                        }
                    }

                    for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                    {
                        SqlCommand cmd4 = new SqlCommand("UsuariosSecciones_Agregar", conn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add(new SqlParameter("@idSeccion", idSeccion));
                        cmd4.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                        int rtn3 = cmd4.ExecuteNonQuery();
                        if (rtn3 <= 0)
                        {
                            retorno = false;
                        }
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
        public static int UltimoIngresado()
        {
            int retorno = -1;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Informes_UltimoIngresado", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new int();
                        retorno = int.Parse(oReader["ultimo"].ToString());
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
    }
}
