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
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Informes_Nuevo", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@tipoInforme", parInforme.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parInforme.Beneficiario.Codigo));

                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                int iIdInforme = -1;
                SqlCommand cmd1 = new SqlCommand("Informes_UltimoIngresado", vConn);
                cmd1.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iIdInforme = new int();
                        iIdInforme = int.Parse(oReader["ultimo"].ToString());
                    }
                }
                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    SqlCommand cmd2 = new SqlCommand("Secciones_Agregar", vConn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                    cmd2.Parameters.Add(new SqlParameter("@idInforme", iIdInforme));
                    int iRtn2 = cmd2.ExecuteNonQuery();
                    if (iRtn2 <= 0)
                    {
                        bRetorno = false;
                    }

                    int iIdSeccion = -1;
                    SqlCommand cmd3 = new SqlCommand("Secciones_UltimoIngresado", vConn);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oReader2 = cmd3.ExecuteReader())
                    {
                        while (oReader2.Read())
                        {
                            iIdSeccion = new int();
                            iIdSeccion = int.Parse(oReader2["ultimo"].ToString());
                        }
                    }

                    for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                    {
                        SqlCommand cmd4 = new SqlCommand("UsuariosSecciones_Agregar", vConn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add(new SqlParameter("@idSeccion", iIdSeccion));
                        cmd4.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                        cmd4.Parameters.Add(new SqlParameter("@estado", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Estado));
                        int iRtn3 = cmd4.ExecuteNonQuery();
                        if (iRtn3 <= 0)
                        {
                            bRetorno = false;
                        }
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
        public static bool Redactar(cInforme parInforme)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                #region Se cambiar el estado al informe a en proceso
                SqlCommand cmd = new SqlCommand("Informes_Redactar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                #endregion
                #region se traen todas las secciones ya agregadas en el informe
                List<cUtilidades.NombreSeccion> lstNombresSecciones = new List<cUtilidades.NombreSeccion>();
                cUtilidades.NombreSeccion unSecc;
                SqlCommand cmd1 = new SqlCommand("Secciones_TraerPorInforme", vConn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unSecc = new cUtilidades.NombreSeccion();
                        int iNum = int.Parse(oReader["SeccionNombre"].ToString());
                        if (iNum == 0) unSecc = cUtilidades.NombreSeccion.Título;
                        if (iNum == 1) unSecc = cUtilidades.NombreSeccion.Encuadre;
                        if (iNum == 2) unSecc = cUtilidades.NombreSeccion.Diagnóstico;
                        if (iNum == 3) unSecc = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (iNum == 4) unSecc = cUtilidades.NombreSeccion.Desarrollo;
                        if (iNum == 5) unSecc = cUtilidades.NombreSeccion.Presentación;
                        if (iNum == 6) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (iNum == 7) unSecc = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (iNum == 8) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (iNum == 9) unSecc = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (iNum == 10) unSecc = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (iNum == 11) unSecc = cUtilidades.NombreSeccion.En_Suma;
                        if (iNum == 12) unSecc = cUtilidades.NombreSeccion.Sugerencias;
                        if (iNum == 13) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicopedagógico;
                        lstNombresSecciones.Add(unSecc);
                    }
                }
                #endregion
                #region Se agrega el contenido a las secciones ya agregadas

                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < lstNombresSecciones.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == lstNombresSecciones[j])
                        {
                            SqlCommand cmd2 = new SqlCommand("Secciones_AgregarContenido", vConn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int iRtn1 = cmd2.ExecuteNonQuery();
                            if (iRtn1 <= 0)
                            {
                                bRetorno = false;
                            }
                            for (int g = 0; g < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; g++)
                            {
                                SqlCommand cmd7 = new SqlCommand("UsuariosSecciones_EnProceso", vConn);
                                cmd7.CommandType = CommandType.StoredProcedure;
                                cmd7.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                                cmd7.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[g].Usuario.Codigo));
                                int iRtn2 = cmd7.ExecuteNonQuery();
                                if (iRtn2 <= 0)
                                {
                                    bRetorno = false;
                                }
                            }

                        }
                    }
                }
                #endregion
                #region Obtengo la lista con las secciones no agregadas
                List<cUtilidades.NombreSeccion> lstSeccionesNoAgregadas = new List<cUtilidades.NombreSeccion>();
                foreach (var item in Enum.GetValues(typeof(cUtilidades.NombreSeccion)))
                {
                    lstSeccionesNoAgregadas.Add((cUtilidades.NombreSeccion)item);
                }

                for (int k = 0; k < lstSeccionesNoAgregadas.Count; k++)
                {
                    for (int l = 0; l < lstNombresSecciones.Count; l++)
                    {
                        if (lstSeccionesNoAgregadas[k] == lstNombresSecciones[l])
                        {
                            lstSeccionesNoAgregadas.RemoveAt(k);
                            k--;
                            break;
                        }
                    }
                }
                #endregion
                #region Agrego las secciones nuevas al informe


                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < lstSeccionesNoAgregadas.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == lstSeccionesNoAgregadas[j])
                        {
                            SqlCommand cmd3 = new SqlCommand("Secciones_AgregarNuevas", vConn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd3.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                            cmd3.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int iRtn2 = cmd3.ExecuteNonQuery();
                            if (iRtn2 <= 0)
                            {
                                bRetorno = false;
                            }

                            int iIdSeccion = -1;
                            SqlCommand cmd5 = new SqlCommand("Secciones_UltimoIngresado", vConn);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader oReader2 = cmd5.ExecuteReader())
                            {
                                while (oReader2.Read())
                                {
                                    iIdSeccion = new int();
                                    iIdSeccion = int.Parse(oReader2["ultimo"].ToString());
                                }
                            }

                            for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                            {
                                SqlCommand cmd6 = new SqlCommand("UsuariosSecciones_Agregar", vConn);
                                cmd6.CommandType = CommandType.StoredProcedure;
                                cmd6.Parameters.Add(new SqlParameter("@idSeccion", iIdSeccion));
                                cmd6.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                                cmd6.Parameters.Add(new SqlParameter("@estado", "1"));
                                int iRtn3 = cmd6.ExecuteNonQuery();
                                if (iRtn3 <= 0)
                                {
                                    bRetorno = false;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRetorno;
        }
        public static bool Finalizar(cInforme parInforme)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                #region Se cambiar el estado al informe a en proceso
                SqlCommand cmd = new SqlCommand("Informes_Finalizar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                #endregion
                #region se traen todas las secciones ya agregadas en el informe
                List<cUtilidades.NombreSeccion> lstNombresSecciones = new List<cUtilidades.NombreSeccion>();
                cUtilidades.NombreSeccion unSecc;
                SqlCommand cmd1 = new SqlCommand("Secciones_TraerPorInforme", vConn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unSecc = new cUtilidades.NombreSeccion();
                        int iNum = int.Parse(oReader["SeccionNombre"].ToString());
                        if (iNum == 0) unSecc = cUtilidades.NombreSeccion.Título;
                        if (iNum == 1) unSecc = cUtilidades.NombreSeccion.Encuadre;
                        if (iNum == 2) unSecc = cUtilidades.NombreSeccion.Diagnóstico;
                        if (iNum == 3) unSecc = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (iNum == 4) unSecc = cUtilidades.NombreSeccion.Desarrollo;
                        if (iNum == 5) unSecc = cUtilidades.NombreSeccion.Presentación;
                        if (iNum == 6) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (iNum == 7) unSecc = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (iNum == 8) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (iNum == 9) unSecc = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (iNum == 10) unSecc = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (iNum == 11) unSecc = cUtilidades.NombreSeccion.En_Suma;
                        if (iNum == 12) unSecc = cUtilidades.NombreSeccion.Sugerencias;
                        if (iNum == 13) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicopedagógico;
                        lstNombresSecciones.Add(unSecc);
                    }
                }
                #endregion
                #region Se agrega el contenido a las secciones ya agregadas

                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < lstNombresSecciones.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == lstNombresSecciones[j])
                        {
                            SqlCommand cmd2 = new SqlCommand("Secciones_AgregarContenido", vConn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int iRtn1 = cmd2.ExecuteNonQuery();
                            if (iRtn1 <= 0)
                            {
                                bRetorno = false;
                            }
                            for (int g = 0; g < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; g++)
                            {
                                SqlCommand cmd7 = new SqlCommand("UsuariosSecciones_Finalizadas", vConn);
                                cmd7.CommandType = CommandType.StoredProcedure;
                                cmd7.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                                cmd7.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[g].Usuario.Codigo));
                                int iRtn2 = cmd7.ExecuteNonQuery();
                                if (iRtn2 <= 0)
                                {
                                    bRetorno = false;
                                }
                            }

                        }
                    }
                }
                #endregion
                #region Obtengo la lista con las secciones no agregadas
                List<cUtilidades.NombreSeccion> lstSeccionesNoAgregadas = new List<cUtilidades.NombreSeccion>();
                foreach (var item in Enum.GetValues(typeof(cUtilidades.NombreSeccion)))
                {
                    lstSeccionesNoAgregadas.Add((cUtilidades.NombreSeccion)item);
                }

                for (int k = 0; k < lstSeccionesNoAgregadas.Count; k++)
                {
                    for (int l = 0; l < lstNombresSecciones.Count; l++)
                    {
                        if (lstSeccionesNoAgregadas[k] == lstNombresSecciones[l])
                        {
                            lstSeccionesNoAgregadas.RemoveAt(k);
                            k--;
                            break;
                        }
                    }
                }
                #endregion
                #region Agrego las secciones nuevas al informe


                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < lstSeccionesNoAgregadas.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == lstSeccionesNoAgregadas[j])
                        {
                            SqlCommand cmd3 = new SqlCommand("Secciones_AgregarNuevas", vConn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd3.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                            cmd3.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int iRtn2 = cmd3.ExecuteNonQuery();
                            if (iRtn2 <= 0)
                            {
                                bRetorno = false;
                            }

                            int iIdSeccion = -1;
                            SqlCommand cmd5 = new SqlCommand("Secciones_UltimoIngresado", vConn);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader oReader2 = cmd5.ExecuteReader())
                            {
                                while (oReader2.Read())
                                {
                                    iIdSeccion = new int();
                                    iIdSeccion = int.Parse(oReader2["ultimo"].ToString());
                                }
                            }

                            for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                            {
                                SqlCommand cmd6 = new SqlCommand("UsuariosSecciones_Agregar", vConn);
                                cmd6.CommandType = CommandType.StoredProcedure;
                                cmd6.Parameters.Add(new SqlParameter("@idSeccion", iIdSeccion));
                                cmd6.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                                cmd6.Parameters.Add(new SqlParameter("@estado", "2"));
                                int iRtn3 = cmd6.ExecuteNonQuery();
                                if (iRtn3 <= 0)
                                {
                                    bRetorno = false;
                                }
                            }
                        }
                    }
                }
                #endregion
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
        public static bool FinalizarSecciones(cInforme parInforme, cUsuario parUsuario)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                #region Se cambiar el estado al informe a en proceso
                SqlCommand cmd = new SqlCommand("UsuariosSecciones_FinalizarPorUsuario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", parUsuario.Codigo));
                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                #endregion
                #region se traen todas las secciones ya agregadas en el informe
                List<cUtilidades.NombreSeccion> lstNombresSecciones = new List<cUtilidades.NombreSeccion>();
                cUtilidades.NombreSeccion unSecc;
                SqlCommand cmd1 = new SqlCommand("Secciones_TraerPorInforme", vConn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unSecc = new cUtilidades.NombreSeccion();
                        int iNum = int.Parse(oReader["SeccionNombre"].ToString());
                        if (iNum == 0) unSecc = cUtilidades.NombreSeccion.Título;
                        if (iNum == 1) unSecc = cUtilidades.NombreSeccion.Encuadre;
                        if (iNum == 2) unSecc = cUtilidades.NombreSeccion.Diagnóstico;
                        if (iNum == 3) unSecc = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (iNum == 4) unSecc = cUtilidades.NombreSeccion.Desarrollo;
                        if (iNum == 5) unSecc = cUtilidades.NombreSeccion.Presentación;
                        if (iNum == 6) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (iNum == 7) unSecc = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (iNum == 8) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (iNum == 9) unSecc = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (iNum == 10) unSecc = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (iNum == 11) unSecc = cUtilidades.NombreSeccion.En_Suma;
                        if (iNum == 12) unSecc = cUtilidades.NombreSeccion.Sugerencias;
                        if (iNum == 13) unSecc = cUtilidades.NombreSeccion.Abordaje_Psicopedagógico;
                        lstNombresSecciones.Add(unSecc);
                    }
                }
                #endregion
                #region Se agrega el contenido a las secciones ya agregadas

                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < lstNombresSecciones.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == lstNombresSecciones[j])
                        {
                            SqlCommand cmd2 = new SqlCommand("Secciones_AgregarContenido", vConn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int iRtn1 = cmd2.ExecuteNonQuery();
                            if (iRtn1 <= 0)
                            {
                                bRetorno = false;
                            }
                        }
                    }
                }
                #endregion
                #region Obtengo la lista con las secciones no agregadas
                List<cUtilidades.NombreSeccion> lstSeccionesNoAgregadas = new List<cUtilidades.NombreSeccion>();
                foreach (var item in Enum.GetValues(typeof(cUtilidades.NombreSeccion)))
                {
                    lstSeccionesNoAgregadas.Add((cUtilidades.NombreSeccion)item);
                }

                for (int k = 0; k < lstSeccionesNoAgregadas.Count; k++)
                {
                    for (int l = 0; l < lstNombresSecciones.Count; l++)
                    {
                        if (lstSeccionesNoAgregadas[k] == lstNombresSecciones[l])
                        {
                            lstSeccionesNoAgregadas.RemoveAt(k);
                            k--;
                            break;
                        }
                    }
                }
                #endregion
                #region Agrego las secciones nuevas al informe


                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < lstSeccionesNoAgregadas.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == lstSeccionesNoAgregadas[j])
                        {
                            SqlCommand cmd3 = new SqlCommand("Secciones_AgregarNuevas", vConn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd3.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                            cmd3.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int iRtn2 = cmd3.ExecuteNonQuery();
                            if (iRtn2 <= 0)
                            {
                                bRetorno = false;
                            }

                            int iIdSeccion = -1;
                            SqlCommand cmd5 = new SqlCommand("Secciones_UltimoIngresado", vConn);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader oReader2 = cmd5.ExecuteReader())
                            {
                                while (oReader2.Read())
                                {
                                    iIdSeccion = new int();
                                    iIdSeccion = int.Parse(oReader2["ultimo"].ToString());
                                }
                            }

                            for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                            {
                                SqlCommand cmd6 = new SqlCommand("UsuariosSecciones_Agregar", vConn);
                                cmd6.CommandType = CommandType.StoredProcedure;
                                cmd6.Parameters.Add(new SqlParameter("@idSeccion", iIdSeccion));
                                cmd6.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                                cmd6.Parameters.Add(new SqlParameter("@estado", "2"));
                                int iRtn3 = cmd6.ExecuteNonQuery();
                                if (iRtn3 <= 0)
                                {
                                    bRetorno = false;
                                }
                            }
                        }
                    }
                    #endregion
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


        public static int UltimoIngresado()
        {
            int iRetorno = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Informes_UltimoIngresado", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iRetorno = new int();
                        iRetorno = int.Parse(oReader["ultimo"].ToString());
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRetorno;
        }
        public static List<cInforme> TraerTodosPendientesPorEspecialista(cUsuario parUsuario)
        {
            List<cInforme> lstRetorno = new List<cInforme>();
            cInforme unInforme;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerPendientesPorEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unInforme = new cInforme();
                        unInforme.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            unInforme.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            unInforme.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            unInforme.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            unInforme.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            unInforme.Tipo = cUtilidades.TipoInforme.Otro;

                        int iA = int.Parse(oReader["InformeEstado"].ToString());
                        if (iA == 0)
                            unInforme.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (iA == 1)
                            unInforme.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (iA == 2)
                            unInforme.Estado = cUtilidades.EstadoInforme.Terminado;
                        unInforme.Beneficiario = new cBeneficiario();
                        unInforme.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        lstRetorno.Add(unInforme);
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
        public static List<cInforme> TraerTodosEnProcesoPorEspecialista(cUsuario parUsuario)
        {
            List<cInforme> lstRetorno = new List<cInforme>();
            cInforme unInforme;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerEnProcesoPorEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unInforme = new cInforme();
                        unInforme.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            unInforme.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            unInforme.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            unInforme.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            unInforme.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            unInforme.Tipo = cUtilidades.TipoInforme.Otro;

                        int iA = int.Parse(oReader["InformeEstado"].ToString());
                        if (iA == 0)
                            unInforme.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (iA == 1)
                            unInforme.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (iA == 2)
                            unInforme.Estado = cUtilidades.EstadoInforme.Terminado;
                        unInforme.Beneficiario = new cBeneficiario();
                        unInforme.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        lstRetorno.Add(unInforme);
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
        public static List<cInforme> TraerTodosTerminadosPorEspecialista(cUsuario parUsuario)
        {
            List<cInforme> lstRetorno = new List<cInforme>();
            cInforme unInforme;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerTerminadosPorEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unInforme = new cInforme();
                        unInforme.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            unInforme.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            unInforme.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            unInforme.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            unInforme.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            unInforme.Tipo = cUtilidades.TipoInforme.Otro;

                        int iA = int.Parse(oReader["InformeEstado"].ToString());
                        if (iA == 0)
                            unInforme.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (iA == 1)
                            unInforme.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (iA == 2)
                            unInforme.Estado = cUtilidades.EstadoInforme.Terminado;
                        unInforme.Beneficiario = new cBeneficiario();
                        unInforme.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        lstRetorno.Add(unInforme);
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
        public static cInforme TraerEspecifico(cInforme parInforme)
        {
            cInforme unRetorno = new cInforme();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerEspecifico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            unRetorno.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeEstado"].ToString());
                        if (i == 0)
                            unRetorno.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (i == 1)
                            unRetorno.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (i == 2)
                            unRetorno.Estado = cUtilidades.EstadoInforme.Terminado;
                        int iJ = int.Parse(oReader["InformeTipo"].ToString());
                        if (iJ == 0)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (iJ == 1)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (iJ == 2)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (iJ == 3)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (iJ == 4)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (iJ == 5)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (iJ == 6)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (iJ == 7)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (iJ == 8)
                            unRetorno.Tipo = cUtilidades.TipoInforme.Otro;
                        unRetorno.Beneficiario = new cBeneficiario();
                        unRetorno.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());

                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }
        public static int VerificarSeccionesTerminadas(cInforme parInforme, cUsuario parUsuario)
        {
            int iRetorno = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("UsuariosSecciones_VerificarSeccionesTerminadas", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", parUsuario.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iRetorno = new int();
                        iRetorno = int.Parse(oReader["cantidad"].ToString());
                    }

                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRetorno;
        }
        public static List<cInforme> TraerTodosConFiltros (string parConsulta)
        {
            List<cInforme> lstRetorno = new List<cInforme>();
            cInforme unInforme;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, vConn);
                using(SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        unInforme = new cInforme();
                        unInforme.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            unInforme.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            unInforme.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            unInforme.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            unInforme.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            unInforme.Tipo = cUtilidades.TipoInforme.Otro;

                        int iA = int.Parse(oReader["InformeEstado"].ToString());
                        if (iA == 0)
                            unInforme.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (iA == 1)
                            unInforme.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (iA == 2)
                            unInforme.Estado = cUtilidades.EstadoInforme.Terminado;
                        unInforme.Beneficiario = new cBeneficiario();
                        unInforme.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        lstRetorno.Add(unInforme);
                    }
                    vConn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cInforme> TraerTodosTerminadosPorBeneficiario(cBeneficiario parBeneficiario)
        {
            List<cInforme> lstRetorno = new List<cInforme>();
            cInforme unInforme;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerTerminadosPorBeneficiario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idBeneficiario", parBeneficiario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unInforme = new cInforme();
                        unInforme.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            unInforme.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            unInforme.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            unInforme.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            unInforme.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            unInforme.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            unInforme.Tipo = cUtilidades.TipoInforme.Otro;

                        int iA = int.Parse(oReader["InformeEstado"].ToString());
                        if (iA == 0)
                            unInforme.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (iA == 1)
                            unInforme.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (iA == 2)
                            unInforme.Estado = cUtilidades.EstadoInforme.Terminado;
                        unInforme.Beneficiario = new cBeneficiario();
                        unInforme.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        lstRetorno.Add(unInforme);
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
    }
}
