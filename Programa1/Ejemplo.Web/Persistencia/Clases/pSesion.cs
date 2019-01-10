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
                cmd.Parameters.Add(new SqlParameter("@SesionFecha", DateTime.Parse(parSesion.Fecha)));
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
                        cmd2.Parameters.Add(new SqlParameter("@Estado", parSesion.lstBeneficiarios[i].Estado));

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
        public static List<cSesion> TraerPasaronDelDia()
        {
            List<cSesion> retorno = new List<cSesion>();
            cSesion sesion;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerPasaronDelDia", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        sesion = new cSesion();
                        sesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        sesion.Comentario = oReader["SesionComentario"].ToString();
                        sesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        sesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        sesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) sesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) sesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) sesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) sesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) sesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int a = int.Parse(oReader["SesionCentro"].ToString());
                        if (a == 0) sesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (a == 1) sesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion beneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", sesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while(oReader1.Read())
                            {
                                beneficiarioSesion = new cBeneficiarioSesion();
                                beneficiarioSesion.Beneficiario = new cBeneficiario();
                                beneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                beneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                beneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                beneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                beneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                beneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                beneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                beneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] ss = beneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                beneficiarioSesion.Beneficiario.FechaNacimiento = ss[0];
                                beneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                beneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                beneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                beneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                beneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                beneficiarioSesion.Plan = new cPlan();
                                beneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int b = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (b == 0) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (b == 1) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (b == 2) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (b == 3) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(beneficiarioSesion);
                            }
                        }
                        sesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        sesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario usuario;
                        SqlCommand cmd2  = new SqlCommand("Usuario_TraerTodosPorSesion", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", sesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                usuario = new cUsuario();
                                usuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                usuario.NickName = oReader2["UsuarioNickName"].ToString();
                                usuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                usuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                usuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int c = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (c == 0)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (c == 1)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (c == 2)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                usuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    usuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                usuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                usuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                usuario.Email = oReader2["UsuarioEmail"].ToString();
                                string d = oReader2["UsuarioTipoContrato"].ToString();
                                if (d == "S")
                                {
                                    usuario.TipoContrato = "Socio";
                                }
                                if (d == "C")
                                {
                                    usuario.TipoContrato = "Contratado";
                                }
                                if (d == "E")
                                {
                                    usuario.TipoContrato = "Empleado";
                                }
                                usuario.Especialidad = new cEspecialidad();
                                usuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                usuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(usuario);
                            }
                        }
                        sesion.lstUsuarios = new List<cUsuario>();
                        sesion.lstUsuarios = lstUsuarios;

                        retorno.Add(sesion);
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
        public static List<cSesion> TraerProximasDelDiaPorEspecialista(cUsuario parUsuario)
        {
            List<cSesion> retorno = new List<cSesion>();
            cSesion sesion;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerProximasDelDiaPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while(oReader.Read())
                    {
                        sesion = new cSesion();
                        sesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        sesion.Comentario = oReader["SesionComentario"].ToString();
                        sesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        sesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        sesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) sesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) sesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) sesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) sesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) sesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int a = int.Parse(oReader["SesionCentro"].ToString());
                        if (a == 0) sesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (a == 1) sesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion beneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", sesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                beneficiarioSesion = new cBeneficiarioSesion();
                                beneficiarioSesion.Beneficiario = new cBeneficiario();
                                beneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                beneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                beneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                beneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                beneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                beneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                beneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                beneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] ss = beneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                beneficiarioSesion.Beneficiario.FechaNacimiento = ss[0];
                                beneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                beneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                beneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                beneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                beneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                beneficiarioSesion.Plan = new cPlan();
                                beneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int b = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (b == 0) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (b == 1) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (b == 2) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (b == 3) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(beneficiarioSesion);
                            }
                        }
                        sesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        sesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario usuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", sesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                usuario = new cUsuario();
                                usuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                usuario.NickName = oReader2["UsuarioNickName"].ToString();
                                usuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                usuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                usuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int c = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (c == 0)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (c == 1)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (c == 2)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                usuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    usuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                usuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                usuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                usuario.Email = oReader2["UsuarioEmail"].ToString();
                                string d = oReader2["UsuarioTipoContrato"].ToString();
                                if (d == "S")
                                {
                                    usuario.TipoContrato = "Socio";
                                }
                                if (d == "C")
                                {
                                    usuario.TipoContrato = "Contratado";
                                }
                                if (d == "E")
                                {
                                    usuario.TipoContrato = "Empleado";
                                }
                                usuario.Especialidad = new cEspecialidad();
                                usuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                usuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(usuario);
                            }
                        }
                        sesion.lstUsuarios = new List<cUsuario>();
                        sesion.lstUsuarios = lstUsuarios;

                        retorno.Add(sesion);
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

        public static List<cSesion> TraerPasaronDelDiaPorEspecialista(cUsuario parUsuario)
        {
            List<cSesion> retorno = new List<cSesion>();
            cSesion sesion;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerPasaronDelDiaPorEspecialista", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        sesion = new cSesion();
                        sesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        sesion.Comentario = oReader["SesionComentario"].ToString();
                        sesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        sesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        sesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) sesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) sesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) sesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) sesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) sesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int a = int.Parse(oReader["SesionCentro"].ToString());
                        if (a == 0) sesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (a == 1) sesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion beneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", sesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                beneficiarioSesion = new cBeneficiarioSesion();
                                beneficiarioSesion.Beneficiario = new cBeneficiario();
                                beneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                beneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                beneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                beneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                beneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                beneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                beneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                beneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] ss = beneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                beneficiarioSesion.Beneficiario.FechaNacimiento = ss[0];
                                beneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                beneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                beneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                beneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                beneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                beneficiarioSesion.Plan = new cPlan();
                                beneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int b = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (b == 0) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (b == 1) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (b == 2) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (b == 3) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(beneficiarioSesion);
                            }
                        }
                        sesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        sesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario usuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", sesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                usuario = new cUsuario();
                                usuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                usuario.NickName = oReader2["UsuarioNickName"].ToString();
                                usuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                usuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                usuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int c = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (c == 0)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (c == 1)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (c == 2)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                usuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    usuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                usuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                usuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                usuario.Email = oReader2["UsuarioEmail"].ToString();
                                string d = oReader2["UsuarioTipoContrato"].ToString();
                                if (d == "S")
                                {
                                    usuario.TipoContrato = "Socio";
                                }
                                if (d == "C")
                                {
                                    usuario.TipoContrato = "Contratado";
                                }
                                if (d == "E")
                                {
                                    usuario.TipoContrato = "Empleado";
                                }
                                usuario.Especialidad = new cEspecialidad();
                                usuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                usuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(usuario);
                            }
                        }
                        sesion.lstUsuarios = new List<cUsuario>();
                        sesion.lstUsuarios = lstUsuarios;

                        retorno.Add(sesion);
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
        public static cSesion TraerEspecifico(cSesion parSesion)
        {
            cSesion retorno = null;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerEspecifico", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        retorno = new cSesion();
                        retorno.Codigo = int.Parse(oReader["SesionId"].ToString());
                        retorno.Comentario = oReader["SesionComentario"].ToString();
                        retorno.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        retorno.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        retorno.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) retorno.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) retorno.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) retorno.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) retorno.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) retorno.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int a = int.Parse(oReader["SesionCentro"].ToString());
                        if (a == 0) retorno.Centro = cUtilidades.Centro.JuanLacaze;
                        if (a == 1) retorno.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion beneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", retorno.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                beneficiarioSesion = new cBeneficiarioSesion();
                                beneficiarioSesion.Beneficiario = new cBeneficiario();
                                beneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                beneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                beneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                beneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                beneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                beneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                beneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                beneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                beneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] ss = beneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                beneficiarioSesion.Beneficiario.FechaNacimiento = ss[0];
                                beneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                beneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                beneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                beneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                beneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                beneficiarioSesion.Plan = new cPlan();
                                beneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int b = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (b == 0) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (b == 1) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (b == 2) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (b == 3) beneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(beneficiarioSesion);
                            }
                        }
                        retorno.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        retorno.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario usuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", retorno.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                usuario = new cUsuario();
                                usuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                usuario.NickName = oReader2["UsuarioNickName"].ToString();
                                usuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                usuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                usuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int c = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (c == 0)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (c == 1)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (c == 2)
                                {
                                    usuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                usuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    usuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                usuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                usuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                usuario.Email = oReader2["UsuarioEmail"].ToString();
                                string d = oReader2["UsuarioTipoContrato"].ToString();
                                if (d == "S")
                                {
                                    usuario.TipoContrato = "Socio";
                                }
                                if (d == "C")
                                {
                                    usuario.TipoContrato = "Contratado";
                                }
                                if (d == "E")
                                {
                                    usuario.TipoContrato = "Empleado";
                                }
                                usuario.Especialidad = new cEspecialidad();
                                usuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                usuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(usuario);
                            }
                        }
                        retorno.lstUsuarios = new List<cUsuario>();
                        retorno.lstUsuarios = lstUsuarios;
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

        public static bool MarcarAsitencias(cSesion parSesion)
        {
            bool retorno = true;

            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                foreach (cBeneficiarioSesion unBen in parSesion.lstBeneficiarios)
                {
                    SqlCommand cmd = new SqlCommand("BeneficiariosSesiones_MarcarAsistencia", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                    cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", unBen.Beneficiario.Codigo));
                    int estado;
                    switch (unBen.Estado)
                    {
                        case cUtilidades.EstadoSesion.Asistio:
                            estado = 0;
                            break;
                        case cUtilidades.EstadoSesion.NoAsistio:
                            estado = 1;
                            break;
                        case cUtilidades.EstadoSesion.Reprogramada:
                            estado = 2;
                            break;
                        default:
                            estado = 3;
                            break;

                    }
                    cmd.Parameters.Add(new SqlParameter("@BeneficiarioSesionesEstado", estado));

                    int rtn = cmd.ExecuteNonQuery();
                }
                SqlCommand cmd1 = new SqlCommand("Sesiones_AgregarComentario", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                cmd1.Parameters.Add(new SqlParameter("@SesionComentario", parSesion.Comentario));

                int rtn1 = cmd1.ExecuteNonQuery();
                if(rtn1<=0)
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
        public static List<cUsuario> VerificarFechaYHorarioUsuario(cSesion parSesion) //Devuelve lista de usuarios que no están disponibles
        {
            List<cUsuario> retorno = new List<cUsuario>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSesiones_VerificarFechaYHorario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SesionFecha", parSesion.Fecha));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraInicio", parSesion.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraFin", parSesion.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int codigo = int.Parse(oReader["UsuarioId"].ToString());
                        for (int i = 0; i < parSesion.lstUsuarios.Count; i++)
                        {
                            if (parSesion.lstUsuarios[i].Codigo == codigo)
                            {
                                retorno.Add(parSesion.lstUsuarios[i]);
                            }
                        }

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
        public static List<cBeneficiario> VerificarFechaYHorarioBeneficiario(cSesion parSesion) //Devuelve lista de beneficiarios que no están disponibles
        {
            List<cBeneficiario> retorno = new List<cBeneficiario>();
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosSesiones_VerificarFechaYHorario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SesionFecha", parSesion.Fecha));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraInicio", parSesion.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraFin", parSesion.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int codigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        for (int i = 0; i < parSesion.lstBeneficiarios.Count; i++)
                        {
                            if (parSesion.lstBeneficiarios[i].Beneficiario.Codigo == codigo)
                            {
                                retorno.Add(parSesion.lstBeneficiarios[i].Beneficiario);
                            }
                        }

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

        public static bool AgregarObservacion(cUsuario parUsuario, cSesion parSesion, string parObservacion)
        {
            bool retorno = true;
            try
            {
                var conn = new SqlConnection(CadenaDeConexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSesiones_AgregarObservacion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUsuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                cmd.Parameters.Add(new SqlParameter("@observacion", parObservacion));
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
    }
}
