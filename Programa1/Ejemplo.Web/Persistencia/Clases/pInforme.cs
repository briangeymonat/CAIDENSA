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
                        cmd4.Parameters.Add(new SqlParameter("@estado", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Estado));
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
        public static bool Redactar(cInforme parInforme)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                #region Se cambiar el estado al informe a en proceso
                SqlCommand cmd = new SqlCommand("Informes_Redactar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                #endregion
                #region se traen todas las secciones ya agregadas en el informe
                List<cUtilidades.NombreSeccion> nombresSecciones = new List<cUtilidades.NombreSeccion>();
                cUtilidades.NombreSeccion secc;
                SqlCommand cmd1 = new SqlCommand("Secciones_TraerPorInforme", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        secc = new cUtilidades.NombreSeccion();
                        int num = int.Parse(oReader["SeccionNombre"].ToString());
                        if (num == 0) secc = cUtilidades.NombreSeccion.Título;
                        if (num == 1) secc = cUtilidades.NombreSeccion.Encuadre;
                        if (num == 2) secc = cUtilidades.NombreSeccion.Diagnóstico;
                        if (num == 3) secc = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (num == 4) secc = cUtilidades.NombreSeccion.Desarrollo;
                        if (num == 5) secc = cUtilidades.NombreSeccion.Presentación;
                        if (num == 6) secc = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (num == 7) secc = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (num == 8) secc = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (num == 9) secc = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (num == 10) secc = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (num == 11) secc = cUtilidades.NombreSeccion.En_Suma;
                        if (num == 12) secc = cUtilidades.NombreSeccion.Sugerencias;
                        nombresSecciones.Add(secc);
                    }
                }
                #endregion
                #region Se agrega el contenido a las secciones ya agregadas

                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < nombresSecciones.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == nombresSecciones[j])
                        {
                            SqlCommand cmd2 = new SqlCommand("Secciones_AgregarContenido", conn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int rtn1 = cmd2.ExecuteNonQuery();
                            if (rtn1 <= 0)
                            {
                                retorno = false;
                            }
                            for (int g = 0; g < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; g++)
                            {
                                SqlCommand cmd7 = new SqlCommand("UsuariosSecciones_EnProceso", conn);
                                cmd7.CommandType = CommandType.StoredProcedure;
                                cmd7.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                                cmd7.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[g].Usuario.Codigo));
                                int rtn2 = cmd7.ExecuteNonQuery();
                                if (rtn2 <= 0)
                                {
                                    retorno = false;
                                }
                            }

                        }
                    }
                }
                #endregion
                #region Obtengo la lista con las secciones no agregadas
                List<cUtilidades.NombreSeccion> seccionesNoAgregadas = new List<cUtilidades.NombreSeccion>();
                foreach (var item in Enum.GetValues(typeof(cUtilidades.NombreSeccion)))
                {
                    seccionesNoAgregadas.Add((cUtilidades.NombreSeccion)item);
                }

                for (int k = 0; k < seccionesNoAgregadas.Count; k++)
                {
                    for (int l = 0; l < nombresSecciones.Count; l++)
                    {
                        if (seccionesNoAgregadas[k] == nombresSecciones[l])
                        {
                            seccionesNoAgregadas.RemoveAt(k);
                            k--;
                            break;
                        }
                    }
                }
                #endregion
                #region Agrego las secciones nuevas al informe


                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < seccionesNoAgregadas.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == seccionesNoAgregadas[j])
                        {
                            SqlCommand cmd3 = new SqlCommand("Secciones_AgregarNuevas", conn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd3.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                            cmd3.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int rtn2 = cmd3.ExecuteNonQuery();
                            if (rtn2 <= 0)
                            {
                                retorno = false;
                            }

                            int idSeccion = -1;
                            SqlCommand cmd5 = new SqlCommand("Secciones_UltimoIngresado", conn);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader oReader2 = cmd5.ExecuteReader())
                            {
                                while (oReader2.Read())
                                {
                                    idSeccion = new int();
                                    idSeccion = int.Parse(oReader2["ultimo"].ToString());
                                }
                            }

                            for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                            {
                                SqlCommand cmd6 = new SqlCommand("UsuariosSecciones_Agregar", conn);
                                cmd6.CommandType = CommandType.StoredProcedure;
                                cmd6.Parameters.Add(new SqlParameter("@idSeccion", idSeccion));
                                cmd6.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                                cmd6.Parameters.Add(new SqlParameter("@estado", "1"));
                                int rtn3 = cmd6.ExecuteNonQuery();
                                if (rtn3 <= 0)
                                {
                                    retorno = false;
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
            return retorno;
        }
        public static bool Finalizar(cInforme parInforme)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                #region Se cambiar el estado al informe a en proceso
                SqlCommand cmd = new SqlCommand("Informes_Finalizar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                #endregion
                #region se traen todas las secciones ya agregadas en el informe
                List<cUtilidades.NombreSeccion> nombresSecciones = new List<cUtilidades.NombreSeccion>();
                cUtilidades.NombreSeccion secc;
                SqlCommand cmd1 = new SqlCommand("Secciones_TraerPorInforme", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        secc = new cUtilidades.NombreSeccion();
                        int num = int.Parse(oReader["SeccionNombre"].ToString());
                        if (num == 0) secc = cUtilidades.NombreSeccion.Título;
                        if (num == 1) secc = cUtilidades.NombreSeccion.Encuadre;
                        if (num == 2) secc = cUtilidades.NombreSeccion.Diagnóstico;
                        if (num == 3) secc = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (num == 4) secc = cUtilidades.NombreSeccion.Desarrollo;
                        if (num == 5) secc = cUtilidades.NombreSeccion.Presentación;
                        if (num == 6) secc = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (num == 7) secc = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (num == 8) secc = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (num == 9) secc = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (num == 10) secc = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (num == 11) secc = cUtilidades.NombreSeccion.En_Suma;
                        if (num == 12) secc = cUtilidades.NombreSeccion.Sugerencias;
                        nombresSecciones.Add(secc);
                    }
                }
                #endregion
                #region Se agrega el contenido a las secciones ya agregadas

                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < nombresSecciones.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == nombresSecciones[j])
                        {
                            SqlCommand cmd2 = new SqlCommand("Secciones_AgregarContenido", conn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int rtn1 = cmd2.ExecuteNonQuery();
                            if (rtn1 <= 0)
                            {
                                retorno = false;
                            }
                            for (int g = 0; g < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; g++)
                            {
                                SqlCommand cmd7 = new SqlCommand("UsuariosSecciones_Finalizadas", conn);
                                cmd7.CommandType = CommandType.StoredProcedure;
                                cmd7.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                                cmd7.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[g].Usuario.Codigo));
                                int rtn2 = cmd7.ExecuteNonQuery();
                                if (rtn2 <= 0)
                                {
                                    retorno = false;
                                }
                            }

                        }
                    }
                }
                #endregion
                #region Obtengo la lista con las secciones no agregadas
                List<cUtilidades.NombreSeccion> seccionesNoAgregadas = new List<cUtilidades.NombreSeccion>();
                foreach (var item in Enum.GetValues(typeof(cUtilidades.NombreSeccion)))
                {
                    seccionesNoAgregadas.Add((cUtilidades.NombreSeccion)item);
                }

                for (int k = 0; k < seccionesNoAgregadas.Count; k++)
                {
                    for (int l = 0; l < nombresSecciones.Count; l++)
                    {
                        if (seccionesNoAgregadas[k] == nombresSecciones[l])
                        {
                            seccionesNoAgregadas.RemoveAt(k);
                            k--;
                            break;
                        }
                    }
                }
                #endregion
                #region Agrego las secciones nuevas al informe


                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < seccionesNoAgregadas.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == seccionesNoAgregadas[j])
                        {
                            SqlCommand cmd3 = new SqlCommand("Secciones_AgregarNuevas", conn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd3.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                            cmd3.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int rtn2 = cmd3.ExecuteNonQuery();
                            if (rtn2 <= 0)
                            {
                                retorno = false;
                            }

                            int idSeccion = -1;
                            SqlCommand cmd5 = new SqlCommand("Secciones_UltimoIngresado", conn);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader oReader2 = cmd5.ExecuteReader())
                            {
                                while (oReader2.Read())
                                {
                                    idSeccion = new int();
                                    idSeccion = int.Parse(oReader2["ultimo"].ToString());
                                }
                            }

                            for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                            {
                                SqlCommand cmd6 = new SqlCommand("UsuariosSecciones_Agregar", conn);
                                cmd6.CommandType = CommandType.StoredProcedure;
                                cmd6.Parameters.Add(new SqlParameter("@idSeccion", idSeccion));
                                cmd6.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                                cmd6.Parameters.Add(new SqlParameter("@estado", "2"));
                                int rtn3 = cmd6.ExecuteNonQuery();
                                if (rtn3 <= 0)
                                {
                                    retorno = false;
                                }
                            }
                        }
                    }
                }
                #endregion
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
        public static bool FinalizarSecciones(cInforme parInforme, cUsuario parUsuario)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                #region Se cambiar el estado al informe a en proceso
                SqlCommand cmd = new SqlCommand("UsuariosSecciones_FinalizarPorUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", parUsuario.Codigo));
                int rtn = cmd.ExecuteNonQuery();
                if (rtn <= 0)
                {
                    retorno = false;
                }
                #endregion
                #region se traen todas las secciones ya agregadas en el informe
                List<cUtilidades.NombreSeccion> nombresSecciones = new List<cUtilidades.NombreSeccion>();
                cUtilidades.NombreSeccion secc;
                SqlCommand cmd1 = new SqlCommand("Secciones_TraerPorInforme", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                using (SqlDataReader oReader = cmd1.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        secc = new cUtilidades.NombreSeccion();
                        int num = int.Parse(oReader["SeccionNombre"].ToString());
                        if (num == 0) secc = cUtilidades.NombreSeccion.Título;
                        if (num == 1) secc = cUtilidades.NombreSeccion.Encuadre;
                        if (num == 2) secc = cUtilidades.NombreSeccion.Diagnóstico;
                        if (num == 3) secc = cUtilidades.NombreSeccion.Antecedentes_patológicos;
                        if (num == 4) secc = cUtilidades.NombreSeccion.Desarrollo;
                        if (num == 5) secc = cUtilidades.NombreSeccion.Presentación;
                        if (num == 6) secc = cUtilidades.NombreSeccion.Abordaje_Psicomotriz;
                        if (num == 7) secc = cUtilidades.NombreSeccion.Abordaje_Pedagógico;
                        if (num == 8) secc = cUtilidades.NombreSeccion.Abordaje_Psicológico;
                        if (num == 9) secc = cUtilidades.NombreSeccion.Abordaje_Fonoaudiológico;
                        if (num == 10) secc = cUtilidades.NombreSeccion.Abordaje_Fisioterapéutico;
                        if (num == 11) secc = cUtilidades.NombreSeccion.En_Suma;
                        if (num == 12) secc = cUtilidades.NombreSeccion.Sugerencias;
                        nombresSecciones.Add(secc);
                    }
                }
                #endregion
                #region Se agrega el contenido a las secciones ya agregadas

                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < nombresSecciones.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == nombresSecciones[j])
                        {
                            SqlCommand cmd2 = new SqlCommand("Secciones_AgregarContenido", conn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@idSeccion", parInforme.lstSecciones[i].Codigo));
                            cmd2.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int rtn1 = cmd2.ExecuteNonQuery();
                            if (rtn1 <= 0)
                            {
                                retorno = false;
                            }
                        }
                    }
                }
                #endregion
                #region Obtengo la lista con las secciones no agregadas
                List<cUtilidades.NombreSeccion> seccionesNoAgregadas = new List<cUtilidades.NombreSeccion>();
                foreach (var item in Enum.GetValues(typeof(cUtilidades.NombreSeccion)))
                {
                    seccionesNoAgregadas.Add((cUtilidades.NombreSeccion)item);
                }

                for (int k = 0; k < seccionesNoAgregadas.Count; k++)
                {
                    for (int l = 0; l < nombresSecciones.Count; l++)
                    {
                        if (seccionesNoAgregadas[k] == nombresSecciones[l])
                        {
                            seccionesNoAgregadas.RemoveAt(k);
                            k--;
                            break;
                        }
                    }
                }
                #endregion
                #region Agrego las secciones nuevas al informe


                for (int i = 0; i < parInforme.lstSecciones.Count; i++)
                {
                    for (int j = 0; j < seccionesNoAgregadas.Count; j++)
                    {
                        if (parInforme.lstSecciones[i].Nombre == seccionesNoAgregadas[j])
                        {
                            SqlCommand cmd3 = new SqlCommand("Secciones_AgregarNuevas", conn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                            cmd3.Parameters.Add(new SqlParameter("@nombre", parInforme.lstSecciones[i].Nombre));
                            cmd3.Parameters.Add(new SqlParameter("@contenidoSeccion", parInforme.lstSecciones[i].Contenido));
                            int rtn2 = cmd3.ExecuteNonQuery();
                            if (rtn2 <= 0)
                            {
                                retorno = false;
                            }

                            int idSeccion = -1;
                            SqlCommand cmd5 = new SqlCommand("Secciones_UltimoIngresado", conn);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader oReader2 = cmd5.ExecuteReader())
                            {
                                while (oReader2.Read())
                                {
                                    idSeccion = new int();
                                    idSeccion = int.Parse(oReader2["ultimo"].ToString());
                                }
                            }

                            for (int k = 0; k < parInforme.lstSecciones[i].lstUsuariosSeccion.Count; k++)
                            {
                                SqlCommand cmd6 = new SqlCommand("UsuariosSecciones_Agregar", conn);
                                cmd6.CommandType = CommandType.StoredProcedure;
                                cmd6.Parameters.Add(new SqlParameter("@idSeccion", idSeccion));
                                cmd6.Parameters.Add(new SqlParameter("@idUsuario", parInforme.lstSecciones[i].lstUsuariosSeccion[k].Usuario.Codigo));
                                cmd6.Parameters.Add(new SqlParameter("@estado", "2"));
                                int rtn3 = cmd6.ExecuteNonQuery();
                                if (rtn3 <= 0)
                                {
                                    retorno = false;
                                }
                            }
                        }
                    }
                    #endregion
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
        public static List<cInforme> TraerTodosPendientesPorEspecialista(cUsuario parUsuario)
        {
            List<cInforme> retorno = new List<cInforme>();
            cInforme informe;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerPendientesPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        informe = new cInforme();
                        informe.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            informe.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            informe.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            informe.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            informe.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            informe.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            informe.Tipo = cUtilidades.TipoInforme.Otro;

                        int a = int.Parse(oReader["InformeEstado"].ToString());
                        if (a == 0)
                            informe.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (a == 1)
                            informe.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (a == 2)
                            informe.Estado = cUtilidades.EstadoInforme.Terminado;
                        informe.Beneficiario = new cBeneficiario();
                        informe.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Add(informe);
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
        public static List<cInforme> TraerTodosEnProcesoPorEspecialista(cUsuario parUsuario)
        {
            List<cInforme> retorno = new List<cInforme>();
            cInforme informe;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerEnProcesoPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        informe = new cInforme();
                        informe.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            informe.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            informe.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            informe.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            informe.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            informe.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            informe.Tipo = cUtilidades.TipoInforme.Otro;

                        int a = int.Parse(oReader["InformeEstado"].ToString());
                        if (a == 0)
                            informe.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (a == 1)
                            informe.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (a == 2)
                            informe.Estado = cUtilidades.EstadoInforme.Terminado;
                        informe.Beneficiario = new cBeneficiario();
                        informe.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Add(informe);
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
        public static List<cInforme> TraerTodosTerminadosPorEspecialista(cUsuario parUsuario)
        {
            List<cInforme> retorno = new List<cInforme>();
            cInforme informe;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerTerminadosPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        informe = new cInforme();
                        informe.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            informe.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeTipo"].ToString());
                        if (i == 0)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (i == 1)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (i == 2)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (i == 3)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (i == 4)
                            informe.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (i == 5)
                            informe.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (i == 6)
                            informe.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (i == 7)
                            informe.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (i == 8)
                            informe.Tipo = cUtilidades.TipoInforme.Otro;

                        int a = int.Parse(oReader["InformeEstado"].ToString());
                        if (a == 0)
                            informe.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (a == 1)
                            informe.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (a == 2)
                            informe.Estado = cUtilidades.EstadoInforme.Terminado;
                        informe.Beneficiario = new cBeneficiario();
                        informe.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Add(informe);
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
        public static cInforme TraerEspecifico(cInforme parInforme)
        {
            cInforme retorno = new cInforme();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Informes_TraerEspecifico", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            retorno.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeEstado"].ToString());
                        if (i == 0)
                            retorno.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (i == 1)
                            retorno.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (i == 2)
                            retorno.Estado = cUtilidades.EstadoInforme.Terminado;
                        int j = int.Parse(oReader["InformeTipo"].ToString());
                        if (j == 0)
                            retorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (j == 1)
                            retorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (j == 2)
                            retorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (j == 3)
                            retorno.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (j == 4)
                            retorno.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (j == 5)
                            retorno.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (j == 6)
                            retorno.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (j == 7)
                            retorno.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (j == 8)
                            retorno.Tipo = cUtilidades.TipoInforme.Otro;
                        retorno.Beneficiario = new cBeneficiario();
                        retorno.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());

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
        public static int VerificarSeccionesTerminadas(cInforme parInforme, cUsuario parUsuario)
        {
            int retorno = -1;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UsuariosSecciones_VerificarSeccionesTerminadas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idInforme", parInforme.Codigo));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", parUsuario.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new int();
                        retorno = int.Parse(oReader["cantidad"].ToString());
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
        #region
        /*public static List<cInforme> TraerTodos()
        {
            List<cInforme> retorno = new List<cInforme>();
            cInforme informe;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Informes_TraerTodos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        informe = new cInforme();
                        informe.Codigo = int.Parse(oReader["InformeId"].ToString());
                        if (oReader["InformeFecha"] != DBNull.Value)
                            informe.Fecha = DateTime.Parse(oReader["InformeFecha"].ToString()).ToShortDateString();
                        int i = int.Parse(oReader["InformeEstado"].ToString());
                        if (i == 0)
                            informe.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (i == 1)
                            informe.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (i == 2)
                            informe.Estado = cUtilidades.EstadoInforme.Terminado;
                        int j = int.Parse(oReader["InformeTipo"].ToString());
                        if (j == 0)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicomotriz;
                        if (j == 1)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicopedagogica;
                        if (j == 2)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Psicologica;
                        if (j == 3)
                            informe.Tipo = cUtilidades.TipoInforme.Evaluacion_Fonoaudiologa;
                        if (j == 4)
                            informe.Tipo = cUtilidades.TipoInforme.Evolucion;
                        if (j == 5)
                            informe.Tipo = cUtilidades.TipoInforme.Tolerancia_Curricular;
                        if (j == 6)
                            informe.Tipo = cUtilidades.TipoInforme.Juzgado;
                        if (j == 7)
                            informe.Tipo = cUtilidades.TipoInforme.Interdiciplinario;
                        if (j == 8)
                            informe.Tipo = cUtilidades.TipoInforme.Otro;
                        informe.Beneficiario = new cBeneficiario();
                        informe.Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        retorno.Add(informe);
                    }                  
                }
                for(int i=0; i<retorno.Count;i++)
                {
                    SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerEspecifico", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BeneficiarioId", retorno[i].Beneficiario.Codigo));

                    using (SqlDataReader oReader = cmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            retorno[i].Beneficiario = new cBeneficiario();
                            retorno[i].Beneficiario.Codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                            retorno[i].Beneficiario.Nombres = oReader["BeneficiarioNombres"].ToString();
                            retorno[i].Beneficiario.Apellidos = oReader["BeneficiarioApellidos"].ToString();
                            retorno[i].Beneficiario.CI = int.Parse(oReader["BeneficiarioCI"].ToString());
                            retorno[i].Beneficiario.Sexo = oReader["BeneficiarioSexo"].ToString();
                            retorno[i].Beneficiario.Telefono1 = oReader["BeneficiarioTelefono1"].ToString();
                            retorno[i].Beneficiario.Telefono2 = oReader["BeneficiarioTelefono2"].ToString();
                            retorno[i].Beneficiario.Domicilio = oReader["BeneficiarioDomicilio"].ToString();
                            retorno[i].Beneficiario.Email = oReader["BeneficiarioEmail"].ToString();
                            retorno[i].Beneficiario.FechaNacimiento = oReader["BeneficiarioFechaNacimiento"].ToString();
                            string[] ss = retorno[i].Beneficiario.FechaNacimiento.Split(' ');
                            retorno[i].Beneficiario.FechaNacimiento = ss[0];
                            retorno[i].Beneficiario.Atributario = oReader["BeneficiarioAtributario"].ToString();
                            retorno[i].Beneficiario.MotivoConsulta = oReader["BeneficiarioMotivoConsulta"].ToString();
                            retorno[i].Beneficiario.Escolaridad = oReader["BeneficiarioEscolaridad"].ToString();
                            retorno[i].Beneficiario.Derivador = oReader["BeneficiarioDerivador"].ToString();
                            retorno[i].Beneficiario.Estado = bool.Parse(oReader["BeneficiarioEstado"].ToString());
                        }
                    }
                }
                




            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }*/
        #endregion 

    }
}
