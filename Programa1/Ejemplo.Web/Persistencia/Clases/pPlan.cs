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
        public static bool Agregar(cBeneficiario elBeneficiario)
        {
            bool retorno = true;
            for (int i = 0; i < elBeneficiario.lstPlanes.Count; i++)
            {
                try
                {
                    var conn = new SqlConnection(CadenaDeConexion);
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Planes_Agregar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PlanTipo", elBeneficiario.lstPlanes[i].Tipo));
                    cmd.Parameters.Add(new SqlParameter("@PlanTratamiento", elBeneficiario.lstPlanes[i].Tratamiento));
                    cmd.Parameters.Add(new SqlParameter("@PlanEvaluacion", elBeneficiario.lstPlanes[i].Evaluacion));
                    if(elBeneficiario.lstPlanes[i].FechaInicio != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@PlanFechaInicio", elBeneficiario.lstPlanes[i].FechaInicio));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@PlanFechaInicio", null));
                    }
                    if (elBeneficiario.lstPlanes[i].FechaFin != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@PlanFechaFin", elBeneficiario.lstPlanes[i].FechaFin));
                    }

                    cmd.Parameters.Add(new SqlParameter("@PlanActivo", elBeneficiario.lstPlanes[i].Activo));
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
            }


            return retorno;
        }

        public static bool Eliminar(cPlan elPlan)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Planes_Eliminar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PlanId", elPlan.Codigo));

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

        public static List<cPlan> TraerActivosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            List<cPlan> retorno = new List<cPlan>();
            cPlan unPlan;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Planes_TraerActivosPorBeneficiario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));

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
                        string[] ss = unPlan.FechaInicio.Split(' ');
                        unPlan.FechaInicio = ss[0];
                        if (oReader["PlanFechaFin"] != DBNull.Value)
                        {
                            unPlan.FechaFin = oReader["PlanFechaFin"].ToString();
                            string[] sa = unPlan.FechaFin.Split(' ');
                            unPlan.FechaFin = sa[0];
                        }
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());

                        retorno.Add(unPlan);
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

        public static List<cPlan> TraerInactivosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            List<cPlan> retorno = new List<cPlan>();
            cPlan unPlan;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Planes_TraerInactivosPorBeneficiario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));

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
                        string[] ss = unPlan.FechaInicio.Split(' ');
                        unPlan.FechaInicio = ss[0];
                        if (oReader["PlanFechaFin"] != DBNull.Value)
                        {
                            unPlan.FechaFin =oReader["PlanFechaFin"].ToString();
                            string[] sa = unPlan.FechaFin.Split(' ');
                            unPlan.FechaFin = sa[0];
                        }
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());

                        retorno.Add(unPlan);
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

        public static List<cPlan> TraerTodosPorBeneficiario(cBeneficiario elBeneficiario)
        {
            List<cPlan> retorno = new List<cPlan>();
            cPlan unPlan;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Planes_TraerTodosPorBeneficiario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", elBeneficiario.Codigo));

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
                        string[] ss = unPlan.FechaInicio.Split(' ');
                        unPlan.FechaInicio = ss[0];
                        if (oReader["PlanFechaFin"] != DBNull.Value)
                        {
                            unPlan.FechaFin = oReader["PlanFechaFin"].ToString();
                            string[] sa = unPlan.FechaFin.Split(' ');
                            unPlan.FechaFin = sa[0];
                        }
                        unPlan.Activo = bool.Parse(oReader["PlanActivo"].ToString());

                        retorno.Add(unPlan);
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
    }
}
