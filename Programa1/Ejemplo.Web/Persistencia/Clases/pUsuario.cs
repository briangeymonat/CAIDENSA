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
    public class pUsuario : pPersistencia
    {
        public static bool Agregar(cUsuario parUsuario)
        {
            bool bRetorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Usuario_Agregar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickName", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@nombres", parUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@apellidos", parUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@ci", parUsuario.CI));
                cmd.Parameters.Add(new SqlParameter("@domicilio", parUsuario.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@telefono", parUsuario.Telefono));
                cmd.Parameters.Add(new SqlParameter("@email", parUsuario.Email));
                cmd.Parameters.Add(new SqlParameter("@tipo", parUsuario.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parUsuario.Especialidad.Codigo));
                cmd.Parameters.Add(new SqlParameter("@tipoContrato", parUsuario.TipoContrato));
                cmd.Parameters.Add(new SqlParameter("@estado", parUsuario.Estado));

                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
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
            return bRetorno;
        }
        public static bool Eliminar(cUsuario parUsuario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_Inhabilitar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

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
        public static bool Habilitar(cUsuario parUsuario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_Habilitar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

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
        public static bool Modificar(cUsuario parUsuario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_Modificar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@nickName", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@nombres", parUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@apellidos", parUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@ci", parUsuario.CI));
                cmd.Parameters.Add(new SqlParameter("@domicilio", parUsuario.Domicilio));
                string sDateInput = "Jan 1, 1753";
                DateTime dParsedDate = DateTime.Parse(sDateInput);
                cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@telefono", parUsuario.Telefono));
                cmd.Parameters.Add(new SqlParameter("@email", parUsuario.Email));
                cmd.Parameters.Add(new SqlParameter("@tipo", parUsuario.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parUsuario.Especialidad.Codigo));
                cmd.Parameters.Add(new SqlParameter("@tipoContrato", parUsuario.TipoContrato));

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
        public static bool AgregarContrasena(cUsuario parUsuario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Usuario_AgregarContraseña", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickName", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@contrasena", parUsuario.Contrasena));

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
        public static bool RestablecerContrasena(cUsuario parUsuario)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();
                SqlCommand cmd = new SqlCommand("Usuario_RestablecerContraseña", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

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
        public static cUsuario TraerEspecifico(cUsuario parUsuario)
        {
            cUsuario unRetorno = null;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerEspecifico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cUsuario();
                        unRetorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unRetorno.NickName = oReader["UsuarioNickName"].ToString();
                        unRetorno.Nombres = oReader["UsuarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unRetorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unRetorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unRetorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unRetorno.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unRetorno.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unRetorno.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unRetorno.TipoContrato = "Empleado";
                        }
                        unRetorno.Especialidad = new cEspecialidad();
                        unRetorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unRetorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();



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
        public static cUsuario TraerEspecificoXNickName(cUsuario parUsuario)
        {
            cUsuario unRetorno = null;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerEspecificoXNickName", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cUsuario();
                        unRetorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unRetorno.NickName = oReader["UsuarioNickName"].ToString();
                        unRetorno.Nombres = oReader["UsuarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unRetorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unRetorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        
                        unRetorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unRetorno.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unRetorno.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unRetorno.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unRetorno.TipoContrato = "Empleado";
                        }
                        unRetorno.Especialidad = new cEspecialidad();
                        unRetorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unRetorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();


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
        public static List<cUsuario> TraerTodosActivos()
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosActivos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();

                        lstRetorno.Add(unUsuario);
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
        public static List<cUsuario> TraerTodosInactivos()
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosInactivos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();

                        lstRetorno.Add(unUsuario);
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
        public static int VerificarNickNameYCi(cUsuario parUsuario)
        {
            int iRetorno = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_VerificarNickNameYCi", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@ci", parUsuario.CI));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iRetorno = new int();
                        iRetorno = int.Parse(oReader["cantidad"].ToString());
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
        public static int ExisteNickNameSinContrasena(cUsuario parUsuario)
        {
            int iRetorno = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_ExisteNickNameSinContraseña", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iRetorno = new int();
                        iRetorno = int.Parse(oReader["cantidad"].ToString());
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
        public static cUsuario VerificarInicioSesion(cUsuario parUsuario)
        {
            cUsuario unRetorno = null;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_VerificarInicioSesion", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@contrasena", parUsuario.Contrasena));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cUsuario();
                        unRetorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unRetorno.NickName = oReader["UsuarioNickName"].ToString();
                        unRetorno.Nombres = oReader["UsuarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unRetorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unRetorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unRetorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unRetorno.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unRetorno.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unRetorno.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unRetorno.TipoContrato = "Empleado";
                        }
                        unRetorno.Especialidad = new cEspecialidad();
                        unRetorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unRetorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
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
        public static List<cUsuario> TraerTodosActivosPorNombreApellido(string parTexto)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosActivosXNombreApellido", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", parTexto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";

                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        
                        lstRetorno.Add(unUsuario);

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
        public static List<cUsuario> TraerTodosInactivosPorNombreApellido(string parTexto)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosInactivosXNombreApellido", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", parTexto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);

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
        public static List<cUsuario> TraerTodosActivosPorCI(string parTexto)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosActivosXCI", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", parTexto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);

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
        public static List<cUsuario> TraerTodosInactivosPorCI(string parTexto)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosInactivosXci", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", parTexto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);

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
        public static int CantidadAdministradoresActivos()
        {
            int iRetorno = -1;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_CantidadAdministradoresActivos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iRetorno = new int();
                        iRetorno = int.Parse(oReader["cantidad"].ToString());
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
        public static List<cUsuario> TraerTodosEspecialistasActivos()
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosEspecialistasActivos", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);
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
        public static List<cUsuario> TraerTodosEspecialistasActivosPorEspecialidad(cEspecialidad parEspecialidad)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosEspecialistasActivosXEspecialidad", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parEspecialidad.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);
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
        public static List<cUsuario> TraerEspecialistasConFiltros(string parConsulta)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, vConn);

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);
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
        public static List<cUsuario> TraerTodosEspecialistasConInformesPendientes()
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosEspecialistasConInformesPendientes", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);
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
        public static List<cUsuario> TraerTodosPorItinerario(cItinerario parItinerario)
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            cUsuario unUsuario;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosPorItinerario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUsuario = new cUsuario();
                        unUsuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unUsuario.NickName = oReader["UsuarioNickName"].ToString();
                        unUsuario.Nombres = oReader["UsuarioNombres"].ToString();
                        unUsuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unUsuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unUsuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value) unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();

                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unUsuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        lstRetorno.Add(unUsuario);
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
        public static cUsuario TraerPrimeroPorEspecialidad(cEspecialidad parEspecialidad)
        {
            cUsuario unRetorno = null;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerPrimeroPorEspecialidad", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EspecialidadNombre", parEspecialidad.Nombre));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cUsuario();
                        unRetorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        unRetorno.NickName = oReader["UsuarioNickName"].ToString();
                        unRetorno.Nombres = oReader["UsuarioNombres"].ToString();
                        unRetorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        unRetorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            unRetorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        unRetorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            unRetorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }

                        unRetorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        unRetorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unRetorno.Email = oReader["UsuarioEmail"].ToString();
                        string sA = oReader["UsuarioTipoContrato"].ToString();
                        if (sA == "S")
                        {
                            unRetorno.TipoContrato = "Socio";
                        }
                        if (sA == "C")
                        {
                            unRetorno.TipoContrato = "Contratado";
                        }
                        if (sA == "E")
                        {
                            unRetorno.TipoContrato = "Empleado";
                        }
                        unRetorno.Especialidad = new cEspecialidad();
                        unRetorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unRetorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();


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

        #region USUARIOS SECCION
        public static List<cUsuarioSeccion> TraerTodosPorSeccion(cSeccion parSeccion)
        {
            List<cUsuarioSeccion> lstRetorno = new List<cUsuarioSeccion>();
            cUsuarioSeccion unUS;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSecciones_TraerTodosPorSeccion", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idSeccion", parSeccion.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unUS = new cUsuarioSeccion();
                        unUS.Usuario = new cUsuario();
                        unUS.Usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        int i = int.Parse(oReader["UsuariosSeccionesEstado"].ToString());
                        if (i == 0) unUS.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (i == 1) unUS.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (i == 2) unUS.Estado = cUtilidades.EstadoInforme.Terminado;
                        lstRetorno.Add(unUS);
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
        #endregion

        #region 

        public static List<List<string>> TraerCantidadSesionPorTipoSesion(string parConsulta)
        {
            List<List<string>> lstRetorno = new List<List<string>>();
            List<string> lst;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, vConn);

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        lst = new List<string>();
                        lst.Add(oReader["UsuarioNombres"].ToString());
                        lst.Add(oReader["UsuarioApellidos"].ToString());
                        lst.Add(oReader["Individual"].ToString());
                        lst.Add(oReader["Grupo2"].ToString());
                        lst.Add(oReader["Grupo3"].ToString());
                        lst.Add(oReader["Taller"].ToString());
                        lst.Add(oReader["PROES"].ToString());
                        lstRetorno.Add(lst);
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

        #endregion


    }
}
