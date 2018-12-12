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
    public class pUsuario:pPersistencia
    {
        public static bool Agregar(cUsuario parUsuario)
        {
            bool retorno = true;
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
                //DateTime fecha = new DateTime(20101753);
                string dateInput = "Jan 1, 1753";
                DateTime parsedDate = DateTime.Parse(dateInput);

                if (parUsuario.FechaNacimiento< parsedDate)
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parsedDate)); //    1/1/1753
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                }
                cmd.Parameters.Add(new SqlParameter("@telefono", parUsuario.Telefono));
                cmd.Parameters.Add(new SqlParameter("@email", parUsuario.Email));
                cmd.Parameters.Add(new SqlParameter("@tipo", parUsuario.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parUsuario.Especialidad.Codigo));
                cmd.Parameters.Add(new SqlParameter("@tipoContrato", parUsuario.TipoContrato));
                cmd.Parameters.Add(new SqlParameter("@estado", parUsuario.Estado));

                int rtn = cmd.ExecuteNonQuery();
                if(rtn<=0)
                {
                    retorno = false;
                }
                if(conn.State ==ConnectionState.Open)
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
        public static bool Eliminar(cUsuario parUsuario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_Inhabilitar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

                int rtn = cmd.ExecuteNonQuery();

                if(rtn <=0)
                {
                    retorno = false;
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
        public static bool Habilitar(cUsuario parUsuario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_Habilitar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

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
        public static bool Modificar(cUsuario parUsuario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_Modificar", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@nickName", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@nombres", parUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@apellidos", parUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@ci", parUsuario.CI));
                cmd.Parameters.Add(new SqlParameter("@domicilio", parUsuario.Domicilio));
                //DateTime fecha = new DateTime(20101753);
                string dateInput = "Jan 1, 1753";
                DateTime parsedDate = DateTime.Parse(dateInput);

                if (parUsuario.FechaNacimiento < parsedDate)
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parsedDate)); //    1/1/1753
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                }
                cmd.Parameters.Add(new SqlParameter("@telefono", parUsuario.Telefono));
                cmd.Parameters.Add(new SqlParameter("@email", parUsuario.Email));
                cmd.Parameters.Add(new SqlParameter("@tipo", parUsuario.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parUsuario.Especialidad.Codigo));
                cmd.Parameters.Add(new SqlParameter("@tipoContrato", parUsuario.TipoContrato));

                int rtn = cmd.ExecuteNonQuery();
                if(rtn<=0)
                {
                    retorno = false;
                }
                if(conn.State==ConnectionState.Open)
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
        public static bool AgregarContrasena(cUsuario parUsuario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Usuario_AgregarContraseña", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickName", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@contrasena", parUsuario.Contrasena));

                int rtn = cmd.ExecuteNonQuery();
                if(rtn <=0)
                {
                    retorno = false;
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
        public static bool RestablecerContrasena(cUsuario parUsuario)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Usuario_RestablecerContraseña", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

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
        public static cUsuario TraerEspecifico(cUsuario parUsuario)
        {
            cUsuario retorno = null;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerEspecifico", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        retorno = new cUsuario();
                        retorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        retorno.NickName = oReader["UsuarioNickName"].ToString();
                        retorno.Nombres = oReader["UsuarioNombres"].ToString();
                        retorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        retorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if(i==0)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        retorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        retorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        retorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        retorno.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a=="S")
                        {
                            retorno.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            retorno.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            retorno.TipoContrato = "Empleado";
                        }
                        retorno.Especialidad = new cEspecialidad();
                        retorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        retorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();

                        if (retorno.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            retorno.FechaNacimiento = new DateTime();
                        }

                    }
                    conn.Close();
                }


            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public static cUsuario TraerEspecificoXNickName(cUsuario parUsuario)
        {
            cUsuario retorno = null;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerEspecificoXNickName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cUsuario();
                        retorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        retorno.NickName = oReader["UsuarioNickName"].ToString();
                        retorno.Nombres = oReader["UsuarioNombres"].ToString();
                        retorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        retorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        retorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        retorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        retorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        retorno.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            retorno.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            retorno.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            retorno.TipoContrato = "Empleado";
                        }
                        retorno.Especialidad = new cEspecialidad();
                        retorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        retorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();

                        if(retorno.FechaNacimiento.ToShortDateString()=="01/01/1753")
                        {
                            retorno.FechaNacimiento = new DateTime();
                        }

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
        public static List<cUsuario> TraerTodosActivos()
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosActivos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);
                    }
                    conn.Close();

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retorno;

        }
        public static List<cUsuario> TraerTodosInactivos()
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosInactivos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);
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
        public static int VerificarNickNameYCi(cUsuario parUsuario)
        {
            int retorno=-1;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_VerificarNickNameYCi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@codigo", parUsuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@ci", parUsuario.CI));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new int();
                        retorno = int.Parse(oReader["cantidad"].ToString());
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
        public static int ExisteNickNameSinContrasena(cUsuario parUsuario)
        {
            int retorno = -1;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_ExisteNickNameSinContraseña", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new int();
                        retorno = int.Parse(oReader["cantidad"].ToString());
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
        public static cUsuario VerificarInicioSesion(cUsuario parUsuario)
        {
            cUsuario retorno = null;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_VerificarInicioSesion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nickname", parUsuario.NickName));
                cmd.Parameters.Add(new SqlParameter("@contrasena", parUsuario.Contrasena));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cUsuario();
                        retorno.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        retorno.NickName = oReader["UsuarioNickName"].ToString();
                        retorno.Nombres = oReader["UsuarioNombres"].ToString();
                        retorno.Apellidos = oReader["UsuarioApellidos"].ToString();
                        retorno.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            retorno.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        retorno.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        retorno.Telefono = oReader["UsuarioTelefono"].ToString();
                        retorno.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        retorno.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            retorno.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            retorno.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            retorno.TipoContrato = "Empleado";
                        }
                        retorno.Especialidad = new cEspecialidad();
                        retorno.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        retorno.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
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
        public static List<cUsuario> TraerTodosActivosPorNombreApellido(string texto)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosActivosXNombreApellido", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", texto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";

                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);

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
        public static List<cUsuario> TraerTodosInactivosPorNombreApellido(string texto)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosInactivosXNombreApellido", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", texto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);

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
        public static List<cUsuario> TraerTodosActivosPorCI(string texto)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosActivosXCI", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", texto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);

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
        public static List<cUsuario> TraerTodosInactivosPorCI(string texto)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosInactivosXci", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@texto", texto));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);

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
        public static int CantidadAdministradoresActivos()
        {
            int retorno = -1;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_CantidadAdministradoresActivos", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new int();
                        retorno = int.Parse(oReader["cantidad"].ToString());
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
        public static List<cUsuario> TraerTodosEspecialistasActivos()
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosEspecialistasActivos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);
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
        public static List<cUsuario> TraerTodosEspecialistasActivosPorEspecialidad(cEspecialidad parEspecialidad)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosEspecialistasActivosXEspecialidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parEspecialidad.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);
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
        public static List<cUsuario> TraerTodosEspecialistasConInformesPendientes()
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosEspecialistasConInformesPendientes", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        usuario = new cUsuario();
                        usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        usuario.NickName = oReader["UsuarioNickName"].ToString();
                        usuario.Nombres = oReader["UsuarioNombres"].ToString();
                        usuario.Apellidos = oReader["UsuarioApellidos"].ToString();
                        usuario.CI = int.Parse(oReader["UsuarioCI"].ToString());
                        int i = int.Parse(oReader["UsuarioTipo"].ToString());
                        if (i == 0)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                        }
                        if (i == 1)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                        }
                        if (i == 2)
                        {
                            usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                        }
                        usuario.Domicilio = oReader["UsuarioDomicilio"].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        usuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        usuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        usuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            usuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            usuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            usuario.TipoContrato = "Empleado";
                        }
                        usuario.Especialidad = new cEspecialidad();
                        usuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        usuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }
                        retorno.Add(usuario);
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
