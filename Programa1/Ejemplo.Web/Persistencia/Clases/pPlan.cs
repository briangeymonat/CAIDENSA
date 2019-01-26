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
    public class pPlan : pPersistencia
    {
        public static bool Agregar(cBeneficiario parBeneficiario)
        {
            bool bRetorno = true;
            for (int i = 0; i < parBeneficiario.lstPlanes.Count; i++)
            {
                try
                {
                    var vConn = new SqlConnection(CadenaDeConexion);
                    vConn.Open();

                    SqlCommand cmd = new SqlCommand("Planes_Agregar", vConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PlanTipo", parBeneficiario.lstPlanes[i].Tipo));
                    cmd.Parameters.Add(new SqlParameter("@PlanTratamiento", parBeneficiario.lstPlanes[i].Tratamiento));
                    cmd.Parameters.Add(new SqlParameter("@PlanEvaluacion", parBeneficiario.lstPlanes[i].Evaluacion));
                    if(parBeneficiario.lstPlanes[i].FechaInicio != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@PlanFechaInicio", parBeneficiario.lstPlanes[i].FechaInicio));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@PlanFechaInicio", null));
                    }
                    if (parBeneficiario.lstPlanes[i].FechaFin != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@PlanFechaFin", parBeneficiario.lstPlanes[i].FechaFin));
                    }

                    cmd.Parameters.Add(new SqlParameter("@PlanActivo", parBeneficiario.lstPlanes[i].Activo));
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
            }


            return bRetorno;
        }

        public static bool Eliminar(cPlan parPlan)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Planes_Eliminar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PlanId", parPlan.Codigo));

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

        public static List<cPlan> TraerActivosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cPlan> lstRetorno = new List<cPlan>();
            cPlan unPlan;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Planes_TraerActivosPorBeneficiario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unPlan = new cPlan();

                        unPlan.Codigo = int.Parse(oReader["PlanId"].ToString());
                        unPlan.Tipo = oReader["PlanTipo"].ToString();
                        unPlan.Tratamiento = bool.Parse(oReader["PlanTratamiento"].ToString());
                        unPlan.Evaluacion = bool.Parse(oReader["PlanEvaluacion"].ToString());
                        unPlan.FechaInicio = oReader["PlanFechaInicio"].ToString();
                        string[] aSs = unPlan.FechaInicio.Split(' ');
                        unPlan.FechaInicio = aSs[0];
                        if (oReader["PlanFechaFin"] != DBNull.Value)
                        {
                            unPlan.FechaFin = oReader["PlanFechaFin"].ToString();
                            string[] aSa = unPlan.FechaFin.Split(' ');
                            unPlan.FechaFin = aSa[0];
                        }
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());

                        lstRetorno.Add(unPlan);
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

        public static List<cPlan> TraerInactivosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cPlan> lstRetorno = new List<cPlan>();
            cPlan unPlan;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Planes_TraerInactivosPorBeneficiario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unPlan = new cPlan();

                        unPlan.Codigo = int.Parse(oReader["PlanId"].ToString());
                        unPlan.Tipo = oReader["PlanTipo"].ToString();
                        unPlan.Tratamiento = bool.Parse(oReader["PlanTratamiento"].ToString());
                        unPlan.Evaluacion = bool.Parse(oReader["PlanEvaluacion"].ToString());
                        unPlan.FechaInicio = oReader["PlanFechaInicio"].ToString();
                        string[] aSs = unPlan.FechaInicio.Split(' ');
                        unPlan.FechaInicio = aSs[0];
                        if (oReader["PlanFechaFin"] != DBNull.Value)
                        {
                            unPlan.FechaFin =oReader["PlanFechaFin"].ToString();
                            string[] aSa = unPlan.FechaFin.Split(' ');
                            unPlan.FechaFin = aSa[0];
                        }
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());

                        lstRetorno.Add(unPlan);
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

        public static List<cPlan> TraerTodosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cPlan> lstRetorno = new List<cPlan>();
            cPlan unPlan;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Planes_TraerTodosPorBeneficiario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
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

                        lstRetorno.Add(unPlan);
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

        public static bool ModificarFechaVencimiento(cPlan parPlan)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Planes_ModificarFechaVencimiento", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PlanId", parPlan.Codigo));
                cmd.Parameters.Add(new SqlParameter("@PlanFechaFin", parPlan.FechaFin));

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
    }
}
