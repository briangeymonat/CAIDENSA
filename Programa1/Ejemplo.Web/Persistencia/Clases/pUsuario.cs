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
                cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                //DateTime fecha = new DateTime(20101753);
                //string dateInput = "Jan 1, 1753";
                //DateTime parsedDate = DateTime.Parse(dateInput);

                /*if (parUsuario.FechaNacimiento< parsedDate)
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parsedDate)); //    1/1/1753
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                }*/
                /*if (parUsuario.FechaNacimiento != new DateTime())
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                }*/

                cmd.Parameters.Add(new SqlParameter("@telefono", parUsuario.Telefono));
                cmd.Parameters.Add(new SqlParameter("@email", parUsuario.Email));
                cmd.Parameters.Add(new SqlParameter("@tipo", parUsuario.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parUsuario.Especialidad.Codigo));
                cmd.Parameters.Add(new SqlParameter("@tipoContrato", parUsuario.TipoContrato));
                cmd.Parameters.Add(new SqlParameter("@estado", parUsuario.Estado));

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

                /*if (parUsuario.FechaNacimiento < parsedDate)
                {
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parsedDate)); //    1/1/1753
                }*/
                //else
                //{
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", parUsuario.FechaNacimiento));
                //}
                cmd.Parameters.Add(new SqlParameter("@telefono", parUsuario.Telefono));
                cmd.Parameters.Add(new SqlParameter("@email", parUsuario.Email));
                cmd.Parameters.Add(new SqlParameter("@tipo", parUsuario.Tipo));
                cmd.Parameters.Add(new SqlParameter("@idEspecialidad", parUsuario.Especialidad.Codigo));
                cmd.Parameters.Add(new SqlParameter("@tipoContrato", parUsuario.TipoContrato));

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
                        //string[] ss = retorno.FechaNacimiento.Split(' ');
                        //retorno.FechaNacimiento = ss[0];
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = retorno.FechaNacimiento.Split(' ');
                        //retorno.FechaNacimiento = ss[0];
                        /*if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
                        /*if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString());
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
            int retorno = -1;
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            retorno.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                       // string[] ss = retorno.FechaNacimiento.Split(' ');
                       // retorno.FechaNacimiento = ss[0];
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
        public static List<cUsuario> TraerEspecialistasConFiltros(string parConsulta)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario unUsuario;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, conn);

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
                        //string[] ss = unUsuario.FechaNacimiento.Split(' ');
                        //unUsuario.FechaNacimiento = ss[0];
                        unUsuario.Telefono = oReader["UsuarioTelefono"].ToString();
                        unUsuario.Estado = bool.Parse(oReader["UsuarioEstado"].ToString());
                        unUsuario.Email = oReader["UsuarioEmail"].ToString();
                        string a = oReader["UsuarioTipoContrato"].ToString();
                        if (a == "S")
                        {
                            unUsuario.TipoContrato = "Socio";
                        }
                        if (a == "C")
                        {
                            unUsuario.TipoContrato = "Contratado";
                        }
                        if (a == "E")
                        {
                            unUsuario.TipoContrato = "Empleado";
                        }
                        unUsuario.Especialidad = new cEspecialidad();
                        unUsuario.Especialidad.Codigo = int.Parse(oReader["EspecialidadId"].ToString());
                        unUsuario.Especialidad.Nombre = oReader["EspecialidadNombre"].ToString();
                        /*if (unUsuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            unUsuario.FechaNacimiento = new DateTime();
                        }*/
                        retorno.Add(unUsuario);
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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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
                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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
        public static List<cUsuario> TraerTodosPorItinerario(cItinerario parItinerario)
        {
            List<cUsuario> retorno = new List<cUsuario>();
            cUsuario usuario;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Usuario_TraerTodosPorItinerario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ItinerarioId", parItinerario.Codigo));

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
                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)  usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();

                        if (oReader["UsuarioFechaNacimiento"] != DBNull.Value)
                        {
                            usuario.FechaNacimiento = DateTime.Parse(oReader["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                        }
                        //string[] ss = usuario.FechaNacimiento.Split(' ');
                        //usuario.FechaNacimiento = ss[0];
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


                        /*if (usuario.FechaNacimiento.ToShortDateString() == "01/01/1753")
                        {
                            usuario.FechaNacimiento = new DateTime();
                        }*/
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


        #region USUARIOS SECCION
        public static List<cUsuarioSeccion> TraerTodosPorSeccion(cSeccion parSeccion)
        {
            List<cUsuarioSeccion> retorno = new List<cUsuarioSeccion>();
            cUsuarioSeccion us;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSecciones_TraerTodosPorSeccion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idSeccion", parSeccion.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        us = new cUsuarioSeccion();
                        us.Usuario = new cUsuario();
                        us.Usuario.Codigo = int.Parse(oReader["UsuarioId"].ToString());
                        int i = int.Parse(oReader["UsuariosSeccionesEstado"].ToString());
                        if (i == 0) us.Estado = cUtilidades.EstadoInforme.Pendiente;
                        if (i == 1) us.Estado = cUtilidades.EstadoInforme.EnProceso;
                        if (i == 2) us.Estado = cUtilidades.EstadoInforme.Terminado;
                        retorno.Add(us);
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
        #endregion

        #region 

        public static List<List<string>> TraerCantidadSesionPorTipoSesion(string parConsulta)
        {
            List<List<string>> retorno = new List<List<string>>();
            List<string> lst;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, conn);

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        lst = new List<string>();
                        lst.Add(oReader["UsuarioNombres"].ToString());
                        lst.Add(oReader["UsuarioApellidos"].ToString());
                        lst.Add(oReader["Individual"].ToString());
                        lst.Add(oReader["Grupo2"].ToString());
                        lst.Add(oReader["Grupo3"].ToString());
                        lst.Add(oReader["Taller"].ToString());
                        lst.Add(oReader["PROES"].ToString());
                        retorno.Add(lst);
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

        #endregion


    }
}
